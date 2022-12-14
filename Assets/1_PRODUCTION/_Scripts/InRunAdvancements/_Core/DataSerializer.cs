using System;
using System.Collections.Generic;
using UnityEngine;

using AKB.Core.Managing.InRunUpdates;
using AKB.Core.Managing.PCG;

namespace AKB.Core.Serialization
{
    public static class DataSerializer
    {
        #region InRunAdvancements
        /// <summary>
        /// Creates a JSON representation of the passed array of strings.
        /// </summary>
        /// <param name="arrayOfOrderedAdvancements">The unused advancements from the copy.</param>
        /// <returns>The JSON string.</returns>
        public static string SerializeInRunAdvancements(string[] arrayOfOrderedAdvancements, string[] arrayOfSlottedOrderedAdvs)
        {
            AdvancementData data = new AdvancementData();
            data.unusedTypes = arrayOfOrderedAdvancements;
            data.slottedTypes = arrayOfSlottedOrderedAdvs;

            return JsonUtility.ToJson(data);
        }

        /// <summary>
        /// Creates an array of AdvancementTypes based on the JSON representation passed in.
        /// </summary>
        /// <param name="jsonStrFromDB">The JSON string loaded from the database.</param>
        /// <returns>An array containg all the AdvancementTypes loaded from the passed JSON.</returns>
        public static AdvancementData DeserializeInRunAdvancements(string jsonStrFromDB)
        {
            AdvancementData data = new AdvancementData();
            JsonUtility.FromJsonOverwrite(jsonStrFromDB, data);

            return data;
        }
        #endregion

        #region PCG_Rooms
        public static string SerializeUnusedRooms(int[] roomIDs, RoomWorld roomWorld)
        {
            PCGData data = new PCGData();
            data.unusedRooms = roomIDs;
            data.correspondingWorld = (int)roomWorld;

            return JsonUtility.ToJson(data);
        }

        public static PCGData DeserializePCGData(string jsonStrFromDB)
        {
            PCGData data = new PCGData();
            JsonUtility.FromJsonOverwrite(jsonStrFromDB, data);

            return data;
        }
        #endregion
    }
}