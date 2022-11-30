using UnityEngine;

using AKB.Core.Managing;
using AKB.Entities.Interactions;
using AKB.Entities.Player.Interactions;

namespace AKB.Entities.AI.Implementations.Status_Demon
{
    public class StatusDemonAOEHandler : MonoBehaviour
    {
        [Header("Set in inspector")]
        [SerializeField] float aoeRadius;
        [SerializeField] LayerMask playerLayer;

        StatusDemon parentEntity;
        EffectType effectType;

        private void Start()
        {
            parentEntity = transform.root.GetComponent<StatusDemon>();
            effectType = parentEntity.GetEffectType();
        }

        public bool StartAttack()
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, 3, playerLayer.value);

            PlayerInteractable interactable;

            foreach (Collider hit in hits)
            {
                interactable = hit.GetComponent<PlayerInteractable>();

                if (interactable != null)
                {
                    switch (effectType)
                    {
                        case EffectType.Confused:
                            //nop...
                            break;

                        case EffectType.Charmed:
                            //Prep the entity
                            ICharmable charmable = hit.GetComponent<ICharmable>();
                            charmable.SetInflictedFromDirection(transform.position);
                            break;

                            //open for extension
                    }

                    interactable.ApplyStatusEffect(GameManager.S.StatusEffectManager.GetNeededEffect(effectType));

                    return true;
                }
            }

            return false;
        }
    }
}