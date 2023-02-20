using System.Collections;
using System;
using UnityEngine;
using akb.Core.Managing;

public class Spawner : MonoBehaviour
{
    [Header("Set in inspector")]
    [SerializeField] Transform[] spawnPositions;
    [SerializeField] GameObject[] tutorialPanels;
    [SerializeField] GameObject enemyToSpawn;

    IEnumerator activeBehaviour;

    int activeEnemies = 0;
    int currentRound = 0;

    private void Start()
    {
        ManagerHUB.GetManager.GameEventsHandler.onEnemyDeath += SubtractActiveEnemy;
        currentRound++;
        SpawnWave();
    }

    void SpawnWave()
    {
        activeBehaviour = SpawnEnemies();
        StartCoroutine(activeBehaviour);
    }

    void SubtractActiveEnemy()
    {
        activeEnemies--;

        if (activeEnemies <= 0)
        {
            activeEnemies = 0;
            SpawnWave();

            for (int i = 0; i < tutorialPanels.Length; i++)
            {
                if (i == currentRound)
                {
                    tutorialPanels[i].SetActive(true);
                }
                else { tutorialPanels[i].SetActive(false); }
            }

            currentRound++;
        }
    }

    IEnumerator SpawnEnemies()
    {
        activeEnemies = 6;

        for (int i = 0; i < spawnPositions.Length; i++)
        {
            GameObject temp = Instantiate(enemyToSpawn, spawnPositions[i].position, enemyToSpawn.transform.rotation);

            yield return null;
        }

        yield return null;
    }

    private void OnDestroy()
    {
        ManagerHUB.GetManager.GameEventsHandler.onEnemyDeath -= SubtractActiveEnemy;
        StopAllCoroutines();
    }
}