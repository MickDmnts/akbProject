using System;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;

namespace AKB.Core.Managing.PCG
{
    /// <summary>
    /// The available game worlds.
    /// </summary>
    public enum RoomWorld
    {
        World1 = 0,
        World2 = 1,
    }

    /// <summary>
    /// The available game room types.
    /// </summary>
    public enum RoomType
    {
        Entry = 0,
        Heal = 1,
        Store = 2,
        Battle = 3,
        Boss = 4,
    }

    /// <summary>
    /// This struct represents a whole world and is used for ease of world
    /// creation through the editor.
    /// </summary>
    [System.Serializable]
    struct WorldContainer
    {
        /// <summary>
        /// The world name.
        /// </summary>
        [Header("Set in inspector")]
        [Tooltip("The world name")] public string WorldName;
        /// <summary>
        /// The worlds entry room.
        /// </summary>
        [Tooltip("The worlds entry room.")] public RoomData entry;
        /// <summary>
        /// The worlds battle rooms.
        /// </summary>
        [Tooltip("The worlds' battle rooms.")] public List<RoomData> battleRooms;

        /// <summary>
        /// A copy of the worlds battle rooms used from as a cache so the original list stays unchanged.
        /// </summary>
        [Header("Set dynamically")]
        [Tooltip("A copy of the worlds battle rooms used from as a cache"
        + " so the original list stays unchanged.")]
        public List<RoomData> battleRoomsCopy;
        /// <summary>
        /// The battle rooms the player already passed.
        /// </summary>
        [Tooltip("The battle rooms the player already passed.")] public List<RoomData> usedBattleRooms;

        /// <summary>
        /// The heal room of the world
        /// </summary>
        [Header("Set in inspector")]
        [Tooltip("The heal room of the world.")] public RoomData healRoom;
        /// <summary>
        /// The store room of the world.
        /// </summary>
        [Tooltip("The store room of the world.")] public RoomData storeRoom;
        /// <summary>
        /// The boss room of the world.
        /// </summary>
        [Tooltip("The boss room of the world.")] public RoomData bossRoom;
    }

    /// <summary>
    /// This class is responsible for initializing the PCG mechaninsms of the game.
    /// </summary>
    [DefaultExecutionOrder(-396)]
    public class RoomDataContainer : MonoBehaviour
    {
        /// <summary>
        /// The custom worlds of the game.
        /// </summary>
        [Header("Set in inspector")]
        [SerializeField, Tooltip("The custom worlds of the game.")] List<WorldContainer> worldData;

        private void Start()
        {
            SetupRoomsForUse();
        }

        /// <summary>
        /// Initializes the the world data passed from the inspector, sets the room IDs and caches the rooms
        /// so they can be used from the PCG system.
        /// </summary>
        void SetupRoomsForUse()
        {
            for (int i = 0; i < worldData.Count; i++)
            {
                worldData[i].entry.SetRoomID(i);
                worldData[i].entry.GetRoomPrefab().SetActive(false);

                worldData[i].battleRoomsCopy.Clear();
                worldData[i].usedBattleRooms.Clear();

                for (int j = 0; j < worldData[i].battleRooms.Count; j++)
                {
                    worldData[i].battleRooms[j].SetRoomID(j);
                    worldData[i].battleRooms[j].GetRoomPrefab().SetActive(false);

                    worldData[i].battleRoomsCopy.Add(worldData[i].battleRooms[j]);
                    worldData[i].battleRoomsCopy[j].GetRoomPrefab().SetActive(false);
                }

                worldData[i].healRoom.SetRoomID(i);
                worldData[i].healRoom.GetRoomPrefab().SetActive(false);

                worldData[i].storeRoom.SetRoomID(i);
                worldData[i].storeRoom.GetRoomPrefab().SetActive(false);

                worldData[i].bossRoom.SetRoomID(i);
                worldData[i].bossRoom.GetRoomPrefab().SetActive(false);
            }
        }

        /// <summary>
        /// Returns a room based on the passed room type and world.
        /// <para>Remark: The battle room section returns a random room based on battleRooms Length.</para>
        /// </summary>
        public RoomData GetRoomData(RoomWorld roomWorld, RoomType roomType)
        {
            int worldToInt = (int)roomWorld;
            RoomData data;

            switch (roomType)
            {
                //Return entry room of the world.
                case RoomType.Entry:
                    {
                        data = worldData[worldToInt].entry;
                    }
                    break;

                //Return heal room of the world.
                case RoomType.Heal:
                    {
                        data = worldData[worldToInt].healRoom;
                    }
                    break;

                //Return store room of the world.
                case RoomType.Store:
                    {
                        data = worldData[worldToInt].storeRoom;
                    }
                    break;

                //Return a random battle room
                case RoomType.Battle:
                    {
                        int randomRoomOrder = Random.Range(0, worldData[worldToInt].battleRoomsCopy.Count);

                        worldData[worldToInt].usedBattleRooms.Add(worldData[worldToInt].battleRooms[randomRoomOrder]);

                        //Get and delete room from the copy list
                        data = worldData[worldToInt].battleRoomsCopy[randomRoomOrder];
                        worldData[worldToInt].battleRoomsCopy.RemoveAt(randomRoomOrder);
                    }
                    break;

                //Return the boss room of the world.
                case RoomType.Boss:
                    {
                        data = worldData[worldToInt].bossRoom;
                    }
                    break;

                //Throw error if the room type does not exist.
                default:
                    {
                        throw new ArgumentException();
                    }
            }

            //return the room information script.
            return data;
        }

        /// <summary>
        /// Re-initializes the Room container data with inspector given data.
        /// </summary>
        public void ResetWorlds()
        {
            SetupRoomsForUse();
        }
    }
}