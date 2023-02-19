using System.Collections;
using System.Collections.Generic;
using akb.Core.Managing;
using akb.Entities.Player;
using UnityEngine;

public class VoidChecker : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerEntity>())
        {
            ManagerHUB.GetManager.LevelManager.TransitToHub();
        }
    }
}
