using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePadScript : MonoBehaviour
{
    [Header("Set in inspector")]
    [SerializeField] GameObject projectile;
    [SerializeField] Transform spawn;
    [SerializeField] float spawnTime = 0.5f;
    [SerializeField] float timeTillSpawn;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Spawn();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Spawn();
        }
    }

    public void Spawn()
    {
        timeTillSpawn += Time.deltaTime;
        if (timeTillSpawn >= spawnTime)
        {
            Instantiate(projectile, spawn.position, spawn.rotation);
            timeTillSpawn = 0;
        }
    }
}
