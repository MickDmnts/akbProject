using System.Collections.Generic;
using UnityEngine;

using akb.Entities.Interactions;

namespace akb.Entities.AI.Implementations.Astaroth
{
    public class AstarothAttackHandler : MonoBehaviour
    {
        [SerializeField] int attackDamage;

        BossAstaroth astarothEntity;
        EntityAttackFOV attackFOV;

        private void Start()
        {
            astarothEntity = transform.root.GetComponent<BossAstaroth>();
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
