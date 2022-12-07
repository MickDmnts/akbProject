using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinDrop : MonoBehaviour
{
    public GameObject Coin;
    public float health = 100f;
    public Transform coinDropPos;

    //For ASSIGNES in this section
    // Coin dropage should not be handled here, instead just generate a multiplyer number that multiplies the 
    // initial coin drop value of an enemy. Hence the structure should be:
    //Player attacks -> multiplyer increases by a small amount (inspector mutated)
    //Player gets hit OR a timer (inspector mutated) -> multiplyer back to 1f;
    //REPEAT 
    /* void DropCoin()
    {
        Vector3 position = coinDropPos.position;
        GameObject coin = Instantiate(Coin, position, Quaternion.identity);
    } */
}