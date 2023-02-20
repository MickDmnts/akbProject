using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine;
using akb.Core.Managing;


public class Spawner : MonoBehaviour
{
    public enum SpawnState
    {
        SPAWNING,
        WAITING,
        COUNTING
    }
    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform Enemy;
        public int count;
        public float rate;
    }
    public Wave[] waves;
    [SerializeField] Transform[] spawnPoints;
    private int nextWave = 0;
    public SpawnState state = SpawnState.COUNTING;
    public float timeBetweenWaves = 5f;
    public float waveCountdown;
    float searchCountdown = 1f;
    private void Start()
    {
        waveCountdown = timeBetweenWaves;
    }

    private void Update()
    {
        if(state == SpawnState.WAITING)
        {
            if (!EnemyIsAlive())
            {
                WaveCompleted();
            }
            else
            {
                return;
            }
        }

        if(waveCountdown <=0)
        {
            if(state != SpawnState.SPAWNING)
            {
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }
    //MIXAHL CHECK HERE PLEASE
    bool EnemyIsAlive()
    {
        bool enemyIsAlive = false;
        searchCountdown -= Time.deltaTime;
        if(searchCountdown == 0)
        {
            searchCountdown = 1f;
            if (GameObject.FindGameObjectsWithTag("Demon") == null)
            {
                enemyIsAlive = false;
            }
            else
            {
                enemyIsAlive = true;
            }
        }
        return enemyIsAlive;
    }
    void WaveCompleted()
    {
        Debug.Log("Starting new round");

        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;

        if(nextWave + 1 > waves.Length -1)
        {
            //teleport to hub
            nextWave = -1;
            SceneManager.LoadScene("PlayerHUB");
        }
        else
        {
            return;
        }
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        state = SpawnState.SPAWNING;

        for(int i =0; i< _wave.count; i++)
        {
            foreach (Transform spawnPoint in spawnPoints)
            {
                SpawnEnemy(_wave.Enemy,spawnPoint);
            }
            yield return new WaitForSeconds(1f / _wave.rate);
        }

        state = SpawnState.WAITING;

        yield break;
    }

    void SpawnEnemy(Transform _enemy,Transform spawnPoint)
    {
            Instantiate(_enemy,spawnPoint);
            Debug.Log("Spawning Enemy:" + _enemy);
    }
}
