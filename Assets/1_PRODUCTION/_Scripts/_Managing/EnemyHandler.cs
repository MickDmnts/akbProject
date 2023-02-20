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

        List<GameObject> spawnedEnemies;

        private void Start()
        {
            spawnedEnemies = new List<GameObject>();

            ManagerHUB.GetManager.GameEventsHandler.onNextRoomEntry += SpawnRoomEnemies;
            ManagerHUB.GetManager.GameEventsHandler.onNextRoomEntry += KillCachedEnemies;
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
            yield return new WaitForSeconds(2f);

            List<EnemySpawnInfo> enemyPairs = ManagerHUB.GetManager.RoomSelector.GetCurrentRoomEnemies();

            Transform anchor;
            if (ManagerHUB.GetManager.RoomSelector.CurrentRoomGO != null)
            {
                anchor = ManagerHUB.GetManager.RoomSelector.CurrentRoomGO.transform;
            }
            else { yield break; }

            roomEnemyCounter = enemyPairs.Count;

            foreach (EnemySpawnInfo enemyPosPair in enemyPairs)
            {
                if (enemyPosPair.Enemy != null
                    & enemyPosPair.EnemySpawn != null)
                {
                    Vector3 roomPos = enemyPosPair.EnemySpawn.position + anchor.position;

                    GameObject spawnedEnemy = Instantiate(enemyPosPair.Enemy, roomPos, enemyPosPair.EnemySpawn.rotation);
                    GameObject spawnVfx = Instantiate(enemySpawnEffect);
                    spawnVfx.transform.position = spawnedEnemy.transform.position + (Vector3.up / 2);
                    spawnVfx.transform.SetParent(ManagerHUB.GetManager.RoomSelector.CurrentRoomGO.transform);

                    spawnedEnemies.Add(spawnedEnemy);

                    spawnedEnemy.transform.SetParent(ManagerHUB.GetManager.RoomSelector.CurrentRoomGO.transform);

                    yield return new WaitForSeconds(0.8f);
                }
            }
        }

        void KillCachedEnemies()
        {
            foreach (GameObject enemy in spawnedEnemies)
            {
                Destroy(enemy);
            }

            spawnedEnemies = new List<GameObject>();
        }

        ///<summary>Updates the database entry of the passed enemy ID to "found".
        /// <para>Raised through onEnemyEntryUpdate event.</para>
        /// </summary>
        void UpdateEnemyEntry(int enemyID)
        {
            if (GameManager.GetManager.Database.GetIsMonsterFoundValue(GameManager.GetManager.ActiveFileID, enemyID) == 0)
            {
                GameManager.GetManager.Database.UpdateIsMonsterFound(GameManager.GetManager.ActiveFileID, enemyID, true);
            }
        }

        void SubtractActiveEnemy()
        {
            roomEnemyCounter--;

            if (roomEnemyCounter <= 0)
            {
                ManagerHUB.GetManager.GameEventsHandler.OnRoomClear();
            }
        }

        private void OnDestroy()
        {
            ManagerHUB.GetManager.GameEventsHandler.onNextRoomEntry -= SpawnRoomEnemies;
            ManagerHUB.GetManager.GameEventsHandler.onNextRoomEntry -= KillCachedEnemies;
            ManagerHUB.GetManager.GameEventsHandler.onEnemyEntryUpdate -= UpdateEnemyEntry;
            ManagerHUB.GetManager.GameEventsHandler.onEnemyDeath -= SubtractActiveEnemy;

            StopAllCoroutines();
        }
    }
}