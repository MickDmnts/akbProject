using UnityEngine;
using System.Collections.Generic;
using System;

using AKB.Core.Serialization;

namespace AKB.Core.Managing.InRunUpdates
{
    public class InRunAdvancementHandler : MonoBehaviour
    {
        /// <summary>
        /// All the runtime created gameObjects created based on the AdvancementTypes enum names.
        /// *Remark: None values gets ommited*
        /// </summary>
        Dictionary<AdvancementTypes, GameObject> inRunAdvancementPairs = new Dictionary<AdvancementTypes, GameObject>();

        private void Start()
        {
            SaveDependentBehaviour();
        }

        /// <summary>
        /// Creates a new copy or loads the previous unused advancements from the database based on
        /// If the save file was saved during run.
        /// </summary>
        void SaveDependentBehaviour()
        {
            if (GameManager.GetManager.Database.GetHasActiveRun(0))
            {
                Debug.Log("Had save");
                LoadUnusedAdvancements();
            }
            else
            {
                Debug.Log("Had NO save");
                InitializeAdvancements();

                //FOR DEBUGGING
                SaveMidRun();
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
            GameManager.GetManager.Database.UpdateUnusedAdvancementsCell(jsonStr, 0); //zero gets replaced from the active save file.
        }

        void LoadUnusedAdvancements()
        {
            //Read the json string from the db
            string jsonStr = GameManager.GetManager.Database.GetUnusedAdvancements(0);

            //Get the deserialized advancement data from the JSON deserializer
            AdvancementData deserializedData = DataSerializer.DeserializeInRunAdvancements(jsonStr);

            //Parse every string type to its corresponding enum type
            List<AdvancementTypes> types = new List<AdvancementTypes>();
            foreach (string str in deserializedData.unusedTypes)
            {
                AdvancementTypes parsedType = Enum.Parse<AdvancementTypes>(str);
                types.Add(parsedType);
            }

            //Finaly write the loaded enum types to the inRunAdvancements list.
            foreach (AdvancementTypes type in types)
            {
                inRunAdvancementPairs.Add(type, CreateAdvancementGameobject(type));
            }
        }

        /// <summary>
        /// Creates a gameobject as advancement and assigns it the passed type.
        /// </summary>
        /// <param name="type">The type you want the advancement to be.</param>
        /// <returns>The created GameObject</returns>
        GameObject CreateAdvancementGameobject(AdvancementTypes type)
        {
            //Create a new gameObject with a pick up data
            GameObject newAdv = new GameObject($"{type}");
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
    }
}