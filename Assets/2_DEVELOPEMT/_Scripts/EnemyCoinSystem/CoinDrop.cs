using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinDrop : MonoBehaviour
{

    public GameObject Coin;
    public float health = 100f;
    public Transform transform;
    void DropCoin()
    {
        Vector3 position = transform.position;
        GameObject coin = Instantiate(Coin, position, Quaternion.identity);

    }
}


