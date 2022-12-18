using UnityEngine;

namespace akb.Entities.Interactions
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
