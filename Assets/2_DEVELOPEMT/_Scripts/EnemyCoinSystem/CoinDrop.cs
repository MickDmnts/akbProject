using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinDrop : MonoBehaviour
{
    [SerializedFieled] public GameObject Coin;
    [SerializedFieled] public float health = 100f;
    [SerializedFieled] public Transform coinDropPos;
    [SerializedFieled] public float coinNumber;

    public void EnemyDies()
    {
        if(health <=0f)
        {
            public void Die()
            {
                Destroy(gameObject);
                DropCoin();
            }
        }
    }
    //(TO DO: In a new script create a state that checks if a previous enemy is dead. If the state is true then coinNumber++.)
    //(+ a timer for it.)
    //public void CoinCount()  
    //{
    //    if()
    //    {

    //    }
    //    else
    //    {
    //        coinNumber = 25f;
    //    }
    //}

    void DropCoin()
    {
        CoinCount();
        Vector3 position = transform.position;
        GameObject coin = Instantiate(Coin, position + new Vector3(0.0f, 1.0f, 0.0f), Quaternion.identity);
        coin.SetActive(true);
    }


}