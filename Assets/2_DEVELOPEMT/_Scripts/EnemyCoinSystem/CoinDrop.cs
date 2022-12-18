using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AKB.Core.Managing;
public class CoinDrop : MonoBehaviour
{
    [SerializeField] public GameObject Coin;
    [SerializeField] public float health = 100f;
    [SerializeField] public Transform coinDropPos;
    [SerializeField] public float coinNumber = 25f;
    OnEnemyDeath onEnemyDeath;
    public void EnemyDies()
    {
        if (health <= 0f)
        {
            onEnemyDeath.StartComboCounter();
        }
    }

    //(TO DO: In a new script create a state that checks if a previous enemy is dead. If the state is true then coinNumber++.)
    //(+ a timer for it.)
    public void CoinCount()
    {
        if (onEnemyDeath.StartComboCounter() == true)
        {
            coinNumber *= 0.2f;
        }
        else
        {
            coinNumber = 25f;
        }
    }

    public void Die()
    {
        Destroy(gameObject);
        DropCoin();
    }

    void DropCoin()
    {
        //CoinCount();
        Vector3 position = transform.position;
        //GameObject coin = Instantiate(Coin, position + new Vector3(0.0f, 1.0f, 0.0f), Quaternion.identity);
        //coin.SetActive(true);
    }
}