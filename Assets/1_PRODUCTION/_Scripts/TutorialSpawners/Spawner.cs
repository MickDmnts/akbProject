using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] List<int> waves;
    [SerializeField] List<Transform> spawnPoints;
    [SerializeField] GameObject simpleDemon;

    private void Start()
    {
        WaveManager();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void WaveManager()
    {

        foreach (Transform spawnPoint in spawnPoints)
        {
            Instantiate(simpleDemon,spawnPoint);
        }
    }
}
