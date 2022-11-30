using UnityEngine;

namespace AKB.Entities.Interactions
{
    public interface ICharmable
    {
        bool IsAlreadyCharmed();

        void SetInflictedFromDirection(Vector3 transform);
        Vector3 GetInflictedFromDirection();
        void DeactivateEntityControls();
        void ActivateEntityControls();
    }
}
