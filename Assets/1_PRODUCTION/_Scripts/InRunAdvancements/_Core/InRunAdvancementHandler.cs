using UnityEngine;
using System.Collections.Generic;
using System;
using AKB.Core.Database;
using AKB.Core.Serialization;

namespace AKB.Core.Managing.InRunUpdates
{
    [DefaultExecutionOrder(100)]
    public class InRunAdvancementHandler : MonoBehaviour
    {
        /// <summary>
        /// All the runtime created gameObjects created based on the AdvancementTypes enum names.
        /// *Remark: None values gets ommited*
        /// </summary>
        Dictionary<AdvancementTypes, GameObject> inRunAdvancements = new Dictionary<AdvancementTypes, GameObject>();

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
            if (SQLiteHandler.GetHasActiveRun(0))
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

                inRunAdvancements.Add(advType, CreateAdvancementGameobject(advType));
            }
        }

        /// <summary>
        /// Writes the serialized JSON string generated from the remaining items inside the inRunAdvancementsCopy 
        /// to the SQLite database appropriate field.
        /// </summary>
        void SaveMidRun()
        {
            List<string> typeStrings = new List<string>();

            foreach (KeyValuePair<AdvancementTypes, GameObject> pair in inRunAdvancements)
            {
                AdvancementTypes type = inRunAdvancements[pair.Key].GetComponent<AdvancementPickUp>().GetAdvancementType();
                typeStrings.Add(type.ToString());
            }

            string jsonStr = DataSerializer.SerializeInRunAdvancements(typeStrings.ToArray());
            SQLiteHandler.UpdateUnusedAdvancementsCell(jsonStr, 0);
        }

        void LoadUnusedAdvancements()
        {
            string jsonStr = SQLiteHandler.GetUnusedAdvancements(0);

            AdvancementTypes[] deserializedJson = DataSerializer.DeserializeInRunAdvancements(jsonStr);

            foreach (AdvancementTypes type in deserializedJson)
            {
                inRunAdvancements.Add(type, CreateAdvancementGameobject(type));
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
            newAdv.AddComponent<AdvancementPickUp>().SetAdvancementType(type);
            newAdv.transform.SetParent(transform);
            newAdv.transform.position = transform.root.position;

            return newAdv;
        }
    }
}