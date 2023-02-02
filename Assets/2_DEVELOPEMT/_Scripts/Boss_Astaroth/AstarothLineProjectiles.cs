using System.Collections;
using System.Collections.Generic;
using akb.Core.Managing;
using UnityEngine;

public class AstarothLineProjectiles : MonoBehaviour
{
    [Header("Set in inspector")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] Transform spawnPoint;
    [SerializeField] int projectilesPerThrow;
    [SerializeField] float spawnInterval = 5f;

    bool canSpawn = false;
    float currentInterval;

    private void Start()
    {
        currentInterval = spawnInterval;
        EnableSpawning();

        ManagerHUB.GetManager.GameEventsHandler.OnAstarothFirstPhase();
        ManagerHUB.GetManager.GameEventsHandler.onAstarothSecondPhase += DisableSpawning;
    }

    void EnableSpawning()
    {
        canSpawn = true;
    }

    void DisableSpawning()
    {
        canSpawn = false;
    }

    private void Update()
    {
        if (!canSpawn) { return; }

        currentInterval += Time.deltaTime;
        if (currentInterval >= spawnInterval)
        {
            StartCoroutine(SpawnProjectiles());
            currentInterval = 0f;
        }
    }

    IEnumerator SpawnProjectiles()
    {
        for (int i = 0; i != projectilesPerThrow; i++)
        {
            Instantiate(projectilePrefab, spawnPoint.position, spawnPoint.rotation);
            yield return new WaitForSeconds(0.1f);
        }

        yield return null;
    }

    private void OnDestroy()
    {
        ManagerHUB.GetManager.GameEventsHandler.onAstarothSecondPhase -= DisableSpawning;
    }
}
