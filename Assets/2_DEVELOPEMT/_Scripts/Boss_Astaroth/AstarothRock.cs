using UnityEngine;

using akb.Entities.Interactions;
using akb.Core.Managing;

namespace akb.Entities.AI.Implementations.Astaroth
{
    public class AstarothRock : MonoBehaviour, IInteractable
    {
        [Header("Set in inspector")]
        [SerializeField] int rockID = 0;
        [SerializeField] int rockHits = 999999999;

        bool isBreakable = false;

        private void Start()
        {
            isBreakable = false;

            ManagerHUB.GetManager.GameEventsHandler.onAstarothSecondPhase += MakeBreakable;
        }

        void MakeBreakable()
        {
            isBreakable = true;
            rockHits = 6;
        }

        public void AttackInteraction(float damageValue)
        {
            _ = damageValue;

            if (!isBreakable) { return; }

            SubtractRockHits(1);

            if (CheckIfBroken())
            {
                //Play broke GFX
                ManagerHUB.GetManager.GameEventsHandler.OnRockBroken(rockID);
            }
            else
            {
                //Play broken rock particles GFX.
            }
        }

        void SubtractRockHits(int dmg)
        {
            rockHits -= dmg;
        }

        bool CheckIfBroken()
        {
            if (rockHits <= 0)
                return true;

            return false;
        }

        public void MarkBroken() => isBreakable = false;

        public void ApplyStatusEffect(GameObject effect)
        {
            //nop...
        }

        private void OnDestroy()
        {
            ManagerHUB.GetManager.GameEventsHandler.onAstarothSecondPhase -= MakeBreakable;
        }
    }
}