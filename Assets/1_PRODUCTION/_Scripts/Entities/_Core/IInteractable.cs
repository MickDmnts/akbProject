using UnityEngine;

namespace AKB.Entities.Interactions
{
    public interface IInteractable
    {
        void AttackInteraction(float damageValue);
        void ApplyStatusEffect(GameObject effect);
    }
}