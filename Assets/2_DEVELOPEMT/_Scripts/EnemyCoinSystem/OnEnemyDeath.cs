using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AKB.Entities.Player;
using AKB.Entities.Player.Interactions;

namespace AKB.Core.Managing
{
    public class OnEnemyDeath : MonoBehaviour
    {
        int deadEnemies;
        bool comboStart;
        PlayerInteractable playerInteractables;
        PlayerEntity playerEntity;
        private void Start()
        {
            ManagerHUB.GetManager.SetOnEnemyDeath(this);
        }
        public bool StartComboCounter()
        {
            if (playerEntity.IsDead) 
            {
                comboStart = false;
                deadEnemies = 0;
            }
            if (playerInteractables.TookDamage())
            {
                comboStart = false;
                deadEnemies = 0;
            }
            ComboCounter();
            return comboStart;
        }
        public int ComboCounter()
        {
            deadEnemies++;
            return deadEnemies;
        }
    }
}

