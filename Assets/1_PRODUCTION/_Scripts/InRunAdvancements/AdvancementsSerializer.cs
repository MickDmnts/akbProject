using System;
using System.Collections.Generic;
using UnityEngine;

namespace AKB.Core.Managing.InRunUpdates
{
    public static class AdvancementsSerializer
    {
        /// <summary>
        /// Creates a JSON representation of the passed array of strings.
        /// </summary>
        /// <param name="arrayOfOrderedAdvancements">The unused advancements from the copy.</param>
        /// <returns>The JSON string.</returns>
        public static string SerializeInRunAdvancements(string[] arrayOfOrderedAdvancements)
        {
            AdvancementData data = new AdvancementData();
            data.unusedTypes = arrayOfOrderedAdvancements;

            return JsonUtility.ToJson(data);
        }

        /// <summary>
        /// Creates an array of AdvancementTypes based on the JSON representation passed in.
        /// </summary>
        /// <param name="jsonStrFromDB">The JSON string loaded from the database.</param>
        /// <returns>An array containg all the AdvancementTypes loaded from the passed JSON.</returns>
        public static AdvancementTypes[] DeserializeInRunAdvancements(string jsonStrFromDB)
        {
            AdvancementData data = new AdvancementData();
            JsonUtility.FromJsonOverwrite(jsonStrFromDB, data);

            List<AdvancementTypes> types = new List<AdvancementTypes>();

            foreach (string str in data.unusedTypes)
            {
                AdvancementTypes parsedType = Enum.Parse<AdvancementTypes>(str);
                types.Add(parsedType);
            }

            return types.ToArray();
        }
    }
}