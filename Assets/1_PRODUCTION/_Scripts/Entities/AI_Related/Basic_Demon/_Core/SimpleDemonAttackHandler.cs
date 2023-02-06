using System.Collections.Generic;
using UnityEngine;

using AKB.Entities.Interactions;

namespace AKB.Entities.AI.Implementations.Simple_Demon
{
    public class SimpleDemonAttackHandler : MonoBehaviour
    {
        [SerializeField] int attackDamage;

        SimpleDemon simpleDemonEntity;
        EntityAttackFOV attackFOV;

        private void Start()
        {
            simpleDemonEntity = transform.root.GetComponent<SimpleDemon>();
            attackFOV = GetComponent<EntityAttackFOV>();
        }

        public void InitiateAttack()
        {
            List<Transform> hits = attackFOV.GetHitsInsideFrustrum(0);

            foreach (Transform hit in hits)
            {
                IInteractable interactable = hit.GetComponent<IInteractable>();
                if (interactable != null)
                {
                    interactable.AttackInteraction(attackDamage);
                }
            }
        }
    }
}