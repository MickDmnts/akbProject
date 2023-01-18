using UnityEngine;

using System.Collections.Generic;
using System.Collections;

using akb.Core.Managing.PCG;

namespace akb.Core.Managing
{
    public class EnemyHandler : MonoBehaviour
    {
        [Header("Set enemy spawning GFX")]
        [SerializeField] GameObject enemySpawnEffect;

        IEnumerator activeBehaviour;
        int roomEnemyCounter = -1;

        private void Start()
        {
            ManagerHUB.GetManager.GameEventsHandler.onNextRoomEntry += SpawnRoomEnemies;
            ManagerHUB.GetManager.GameEventsHandler.onEnemyEntryUpdate += UpdateEnemyEntry;
            ManagerHUB.GetManager.GameEventsHandler.onEnemyDeath += SubtractActiveEnemy;
        }

        void SpawnRoomEnemies()
        {
            activeBehaviour = SequencialySpawnEnemies();
            StartCoroutine(activeBehaviour);
        }

        IEnumerator SequencialySpawnEnemies()
        {
            List<EnemySpawnInfo> enemyPairs = ManagerHUB.GetManager.RoomSelector.GetCurrentRoomEnemies();

            Transform anchor = ManagerHUB.GetManager.RoomSelector.CurrentRoomGO.transform;

            roomEnemyCounter = enemyPairs.Count;

            foreach (EnemySpawnInfo enemyPosPair in enemyPairs)
            {
                if (enemyPosPair.Enemy == null || enemyPosPair.EnemySpawn == null)

                    break;

                Vector3 roomPos = enemyPosPair.EnemySpawn.position + anchor.position;

                Instantiate(enemyPosPair.Enemy, roomPos, enemyPosPair.EnemySpawn.rotation);

                yield return new WaitForSeconds(0.8f);
            }
        }

        ///<summary>Updates the database entry of the passed enemy ID to "found".
        /// <para>Raised through onEnemyEntryUpdate event.</para>
        /// </summary>
        void UpdateEnemyEntry(int enemyID)
        {
            if (GameManager.GetManager.Database.GetIsMonsterFoundValue(GameManager.GetManager.ActiveFileID, enemyID) == 0)
            {
                GameManager.GetManager.Database.UpdateIsMonsterFound(GameManager.GetManager.ActiveFileID, enemyID, true);
                Debug.Log($"Updated entry for monster id {enemyID} to is found");
            }
            else
            {
                Debug.Log($"Monster with ID {enemyID} is already found");
            }
        }

        void SubtractActiveEnemy()
        {
            roomEnemyCounter--;

            if (roomEnemyCounter <= 0)
            {
                ManagerHUB.GetManager.GameEventsHandler.OnRoomClear();
                Debug.Log("Room cleared");
            }
        }

        private void OnDestroy()
        {
            ManagerHUB.GetManager.GameEventsHandler.onNextRoomEntry -= SpawnRoomEnemies;
            ManagerHUB.GetManager.GameEventsHandler.onEnemyEntryUpdate -= UpdateEnemyEntry;
            ManagerHUB.GetManager.GameEventsHandler.onEnemyDeath -= SubtractActiveEnemy;

            StopAllCoroutines();
        }
    }
}