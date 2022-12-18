using UnityEngine;

namespace akb.Entities.Interactions
{
    public interface IInteractable
    {
        void AttackInteraction(float damageValue);
        void ApplyStatusEffect(GameObject effect);
    }
}