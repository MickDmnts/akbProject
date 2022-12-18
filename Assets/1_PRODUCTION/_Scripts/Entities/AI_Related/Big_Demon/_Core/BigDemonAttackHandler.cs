using System.Collections.Generic;
using UnityEngine;

using akb.Entities.Interactions;

namespace akb.Entities.AI.Implementations.Big_Demon
{
    public class BigDemonAttackHandler : MonoBehaviour
    {
        [SerializeField] int attackDamage;

        BigDemon parentEntity;
        EntityAttackFOV attackFOV;

        private void Start()
        {
            parentEntity = transform.root.GetComponent<BigDemon>();
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