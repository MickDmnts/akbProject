using System;
using AKB.Entities.Player;
using UnityEngine;

namespace AKB.Core.Managing.InRunUpdates
{
    public class AdvancementPickUp : MonoBehaviour
    {
        /// <summary>
        /// The advancement type THIS gameObject represents in the world.
        /// </summary>
        AdvancementTypes advType;

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<PlayerEntity>())
            {
                Debug.Log("Is player");
                ManagerHUB.GetManager.SlotsHandler.PassiveRunAdvancements.SetActiveAdvancement(advType);
                Destroy(transform.root.gameObject);
            }
            else
            {
                Debug.Log("Not player");
            }
        }

        public void SetAdvancementType(AdvancementTypes passedType)
        {
            advType = passedType;
        }

        public AdvancementTypes GetAdvancementType() => advType;
    }
}