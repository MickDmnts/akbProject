using System;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;

namespace AKB.Core.Managing.PCG
{
    public enum RoomWorld
    {
        World1 = 0,
        World2 = 1,
    }

    public enum RoomType
    {
        Entry = 0,
        Heal = 1,
        Store = 2,
        Battle = 3,
        Boss = 4,
    }

    [System.Serializable]
    struct WorldContainer
    {
        [Header("Set in inspector")]
        public string WorldName;
        public RoomData entry;
        public List<RoomData> battleRooms;
        [Header("Set dynamically")]
        public List<RoomData> battleRoomsCopy;
        public List<RoomData> usedBattleRooms;
        [Header("Set in inspector")]
        public RoomData healRoom;
        public RoomData storeRoom;
        public RoomData bossRoom;
    }

    public class RoomDataContainer : MonoBehaviour
    {
        [Header("Set in inspector")]
        [SerializeField] List<WorldContainer> worldData;

        private void Start()
        {
            SetupRoomsForUse();
        }

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

        public RoomData GetRoomData(RoomWorld roomWorld, RoomType roomType)
        {
            int worldToInt = (int)roomWorld;
            RoomData data;

            switch (roomType)
            {
                case RoomType.Entry:
                    {
                        data = worldData[worldToInt].entry;
                    }
                    break;

                case RoomType.Heal:
                    {
                        data = worldData[worldToInt].healRoom;
                    }
                    break;

                case RoomType.Store:
                    {
                        data = worldData[worldToInt].storeRoom;
                    }
                    break;

                case RoomType.Battle:
                    {
                        int randomRoomOrder = Random.Range(0, worldData[worldToInt].battleRoomsCopy.Count);

                        worldData[worldToInt].usedBattleRooms.Add(worldData[worldToInt].battleRooms[randomRoomOrder]);

                        //Get and delete room from the copy list
                        data = worldData[worldToInt].battleRoomsCopy[randomRoomOrder];
                        worldData[worldToInt].battleRoomsCopy.RemoveAt(randomRoomOrder);
                    }
                    break;

                case RoomType.Boss:
                    {
                        data = worldData[worldToInt].bossRoom;
                    }
                    break;

                default:
                    {
                        throw new ArgumentException();
                    }
            }

            //return the room information script.
            return data;
        }

        public void ResetWorlds()
        {
            SetupRoomsForUse();
        }
    }
}