using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

namespace akb.Core.Managing.PCG
{
    [System.Serializable]
    public struct EnemySpawnInfo
    {
        public int PairID;
        public GameObject Enemy;
        public Transform EnemySpawn;
    }

    /// <summary>
    /// This class is responsible for holding all the information a room will need to 
    /// interact with the PCG system.
    /// </summary>
    public class RoomData : MonoBehaviour
    {
        /// <summary>
        /// The room player spawn point.
        /// </summary>
        [Header("Set in inspector")]
        [SerializeField, Tooltip("The room player spawn point.")] Transform roomEntryPoint;
        /// <summary>
        /// The room enemy spawn points.
        /// </summary>
        [SerializeField, Tooltip("The room enemy spawn points.")] List<EnemySpawnInfo> enemySpawnPairs;

        /// <summary>
        /// The world the room belongs to.
        /// </summary>
        [SerializeField, Tooltip("The world the room belongs to.")] RoomWorld correspondingWorld;
        /// <summary>
        /// The type of this room.
        /// </summary>
        [SerializeField, Tooltip("The type of this room.")] RoomType roomType;

        #region ROOM_INIT
        /// <summary>
        /// THIS rooms' ID set from the RoomDataContainer initializer.
        /// </summary>
        int roomID;

        /// <summary>
        /// Get the rooms' ID.
        /// </summary>
        public int GetRoomID() => roomID;
        /// <summary>
        /// Set THIS rooms' ID.
        /// </summary>
        public void SetRoomID(int value) { roomID = value; }
        #endregion

        #region EXTERNAL_USE
        /// <summary>
        /// Get the rooms' player spawn point.
        /// </summary>
        public Transform GetRoomEntryPoint() => roomEntryPoint;

        /// <summary>
        /// Get this rooms' corresponding world type.
        /// </summary>
        public RoomWorld GetRoomWorld() => correspondingWorld;

        /// <summary>
        /// Get this rooms' type.
        /// </summary>
        public RoomType GetRoomType() => roomType;

        /// <summary>
        /// Get this rooms game object.
        /// </summary>
        public GameObject GetRoomPrefab() => transform.gameObject;

        /// <summary>
        /// Get this rooms list of spawn points.
        /// </summary>
        public List<EnemySpawnInfo> GetRoomSpawnPairs() => enemySpawnPairs;
        #endregion
    }
}