using UnityEngine;
using System.Collections.Generic;
using System;

using akb.Core.Serialization;

namespace akb.Core.Managing.InRunUpdates
{
    public class InRunAdvancementHandler : MonoBehaviour
    {
        [Header("Set in inspector")]
        [SerializeField] Sprite[] inRunAdvancementsSprites;
        [SerializeField] GameObject prefabCase;

        /// <summary>
        /// All the runtime created gameObjects based on the AdvancementTypes enum names.
        /// *Remark: None values gets ommited*
        /// </summary>
        Dictionary<AdvancementTypes, GameObject> inRunAdvancementPairs = new Dictionary<AdvancementTypes, GameObject>();

        Dictionary<AdvancementTypes, Sprite> inRunAdvancementsSpritesPairs = new Dictionary<AdvancementTypes, Sprite>();

        public int InRunAdvancementPairsCount => inRunAdvancementPairs.Count;

        private void Awake()
        {
            ManagerHUB.GetManager.InRunAdvancementHandler = this;
        }

        private void Start()
        {
            SaveDependentBehaviour(GameManager.GetManager.ActiveFileID);
            SetInRunAdvancementsSprites();

            ManagerHUB.GetManager.GameEventsHandler.onNewGame += SaveDependentBehaviour;
            ManagerHUB.GetManager.GameEventsHandler.onLoadGame += SaveDependentBehaviour;

            ManagerHUB.GetManager.GameEventsHandler.onSaveInitialized += SaveMidRun;
            ManagerHUB.GetManager.GameEventsHandler.onRoomClear += DropReward;
        }

        void DropReward()
        {
            int rng = UnityEngine.Random.Range(0, inRunAdvancementPairs.Count);

            AdvancementTypes type = (AdvancementTypes)rng;

            GameObject advancement = GetAdvancementGameObject(type);

            advancement.GetComponent<AdvancementPickUp>().SetPickupType(AdvancementPickUp.PickType.AutoPickup, 0);

            advancement.transform.position = ManagerHUB.GetManager.PlayerEntity.transform.position;
        }

        /// <summary>
        /// Creates a new copy or loads the previous unused advancements from the database based on
        /// If the save file was saved during run.
        /// </summary>
        void SaveDependentBehaviour(int saveFileID)
        {
            if (GameManager.GetManager.Database.GetHasActiveRun(saveFileID))
            {
                Debug.Log("Had save");
                LoadUnusedAdvancements(saveFileID);
            }
            else
            {
                Debug.Log("Had NO save");
                InitializeAdvancements();
            }
        }

        void SetInRunAdvancementsSprites()
        {
            string[] enumTypes = Enum.GetNames(typeof(AdvancementTypes));

            for (int i = 0; i < enumTypes.Length; i++)
            {
                AdvancementTypes advType = Enum.Parse<AdvancementTypes>(enumTypes[i]);

                //Bypass the none enum type.
                if (Enum.Parse<AdvancementTypes>(enumTypes[i]).Equals(AdvancementTypes.None)) continue;

                inRunAdvancementsSpritesPairs.Add(advType, inRunAdvancementsSprites[i]);
            }
        }

        /// <summary>
        /// Creates and populates the inRunAdvancements Dictionary with every AdvancementTypes enum name except NONE.
        /// </summary>
        void InitializeAdvancements()
        {
            string[] enumTypes = Enum.GetNames(typeof(AdvancementTypes));

            foreach (string type in enumTypes)
            {
                AdvancementTypes advType = Enum.Parse<AdvancementTypes>(type);

                //Bypass the none enum type.
                if (Enum.Parse<AdvancementTypes>(type).Equals(AdvancementTypes.None)) continue;

                inRunAdvancementPairs.Add(advType, CreateAdvancementGameobject(advType));
            }
        }

        /// <summary>
        /// Writes the serialized JSON string generated from the remaining items inside the inRunAdvancementsCopy 
        /// to the SQLite database appropriate field.
        /// </summary>
        void SaveMidRun()
        {
            //Parse the unused advancements enum types to strings and cache them in the list of strings.
            List<string> typeStrings = new List<string>();
            foreach (KeyValuePair<AdvancementTypes, GameObject> pair in inRunAdvancementPairs)
            {
                AdvancementTypes type = inRunAdvancementPairs[pair.Key].GetComponent<AdvancementPickUp>().GetAdvancementType();
                typeStrings.Add(type.ToString());
            }

            //Convert the slotted advancements and the unused advancements to arrays for serialization
            string[] unusedAdvs = typeStrings.ToArray();
            string[] activeAdvs = ManagerHUB.GetManager.SlotsHandler.GetSlottedAdvancementTypes();

            //Write the serialized json string to the corresponding save file in the DB.
            string jsonStr = DataSerializer.SerializeInRunAdvancements(unusedAdvs, activeAdvs);
            GameManager.GetManager.Database.UpdateInRunAdvancementDataCell(jsonStr, GameManager.GetManager.ActiveFileID); //zero gets replaced from the active save file.
        }

        void LoadUnusedAdvancements(int saveFileID)
        {
            //Read the json string from the db
            string jsonStr = GameManager.GetManager.Database.GetInRunAdvancementData(saveFileID);

            //Get the deserialized advancement data from the JSON deserializer
            AdvancementData deserializedData = DataSerializer.DeserializeInRunAdvancements(jsonStr);

            //Parse the slotted items back to enums
            List<AdvancementTypes> slottedTypes = ParseToEnumTypes(deserializedData.slottedTypes);
            //Parse the unused types back to enums
            List<AdvancementTypes> unusedTypes = ParseToEnumTypes(deserializedData.unusedTypes);

            //Write the slotted types to the inRunAdvancements list.
            string[] slotTypes = Enum.GetNames(typeof(SlotType));
            for (int i = 0; i < slotTypes.Length; i++)
            {
                ManagerHUB.GetManager.SlotsHandler.SetAdvancement(Enum.Parse<SlotType>(slotTypes[i]), slottedTypes[i]);
            }

            //Write the unused types to the inRunAdvancements list.
            foreach (AdvancementTypes unusedType in unusedTypes)
            {
                inRunAdvancementPairs.Add(unusedType, CreateAdvancementGameobject(unusedType));
            }
        }

        ///<summary>Parses the passed array to AdvancementTypes object type.</summary>
        List<AdvancementTypes> ParseToEnumTypes(string[] toBeParsed)
        {
            //Parse the slotted items back to enums
            List<AdvancementTypes> enumTypes = new List<AdvancementTypes>();
            foreach (string str in toBeParsed)
            {
                AdvancementTypes parsedType = Enum.Parse<AdvancementTypes>(str);
                enumTypes.Add(parsedType);
            }

            return enumTypes;
        }

        /// <summary>
        /// Creates a gameobject as advancement and assigns it the passed type.
        /// </summary>
        /// <param name="type">The type you want the advancement to be.</param>
        /// <returns>The created GameObject</returns>
        GameObject CreateAdvancementGameobject(AdvancementTypes type)
        {
            //Create a new gameObject with a pick up data
            GameObject newAdv = Instantiate(prefabCase);
            newAdv.name = $"{type}";
            AdvancementPickUp advPickUp = newAdv.AddComponent<AdvancementPickUp>();

            advPickUp.SetAdvancementType(type);

            newAdv.transform.SetParent(transform);
            newAdv.transform.position = transform.root.position;

            //make a switch here

            return newAdv;
        }

        /// <summary>
        /// Returns a copy of the GameObject of the passed type.
        /// </summary>
        public GameObject GetAdvancementGameObject(AdvancementTypes type)
        {
            GameObject copy = inRunAdvancementPairs[type];
            return copy;
        }

        public Sprite GetAdvacementSprite(AdvancementTypes type)
        {
            Sprite sprite = inRunAdvancementsSpritesPairs[type];
            return sprite;
        }

        private void OnDestroy()
        {
            ManagerHUB.GetManager.GameEventsHandler.onSaveInitialized -= SaveMidRun;
            ManagerHUB.GetManager.GameEventsHandler.onRoomClear -= DropReward;
        }
    }
}