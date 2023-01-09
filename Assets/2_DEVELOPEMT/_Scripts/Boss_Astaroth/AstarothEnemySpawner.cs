using UnityEngine;
using System.Collections;

using akb.Core.Managing;

namespace akb.Entities.AI.Implementations.Astaroth
{
    public class AstarothEnemySpawner : MonoBehaviour
    {
        [Header("Set in inspector")]
        [SerializeField] Transform[] spawnPositions;
        [SerializeField] GameObject enemyToSpawn;
        [SerializeField] int maxSpawnedEnemiesCap;

        IEnumerator activeBehaviour;
        bool canSpawn = false;

        float spawnInterval = 5f;
        float currentInterval = 0;

        int activeEnemies = 0;

        private void Start()
        {
            ManagerHUB.GetManager.GameEventsHandler.onAstarothSecondPhase += EnableSpawning;
            ManagerHUB.GetManager.GameEventsHandler.onAllRocksBroken += StopSpawning;
            ManagerHUB.GetManager.GameEventsHandler.onEnemyDeath += SubtractActiveEnemy;

            canSpawn = false;
        }

        void EnableSpawning()
        {
            canSpawn = true;
        }

        void StopSpawning()
        {
            canSpawn = false;
        }

        void SubtractActiveEnemy()
        {
            activeEnemies--;

            if (activeEnemies < 0) activeEnemies = 0;
        }

        private void Update()
        {
            if (!canSpawn) { return; }

            currentInterval += Time.deltaTime;
            if (currentInterval >= spawnInterval)
            {
                if (activeEnemies >= maxSpawnedEnemiesCap) { return; }

                activeBehaviour = SpawnEnemies();
                StartCoroutine(activeBehaviour);
                currentInterval = 0f;
            }
        }

        IEnumerator SpawnEnemies()
        {
            for (int i = 0; i < spawnPositions.Length; i++)
            {
                Instantiate(enemyToSpawn, spawnPositions[i].position, enemyToSpawn.transform.rotation);

                activeEnemies++;

                if (activeEnemies >= maxSpawnedEnemiesCap) { break; }

                yield return null;
            }

            yield return null;
        }

        private void OnDestroy()
        {
            ManagerHUB.GetManager.GameEventsHandler.onAstarothSecondPhase -= EnableSpawning;
            ManagerHUB.GetManager.GameEventsHandler.onAllRocksBroken -= StopSpawning;
            ManagerHUB.GetManager.GameEventsHandler.onEnemyDeath -= SubtractActiveEnemy;

            StopAllCoroutines();
        }
    }
}