using System.Collections.Generic;
using UnityEngine;

namespace AKB.Core.Managing.PCG
{
    public class RoomData : MonoBehaviour
    {
        [Header("Set in inspector")]
        [SerializeField] Transform roomEntryPoint;
        [SerializeField] List<Transform> roomSpawnPoints;
        [SerializeField] RoomWorld correspondingWorld;
        [SerializeField] RoomType roomType;

        int roomID;

        public Transform GetRoomEntryPoint() => roomEntryPoint;

        public int GetRoomID() => roomID;
        public void SetRoomID(int value) { roomID = value; }

        public RoomWorld GetRoomWorld() => correspondingWorld;
        public RoomType GetRoomType() => roomType;
        public GameObject GetRoomPrefab() => gameObject;
        public List<Transform> GetRoomSpawnPoints() => roomSpawnPoints;
    }
}