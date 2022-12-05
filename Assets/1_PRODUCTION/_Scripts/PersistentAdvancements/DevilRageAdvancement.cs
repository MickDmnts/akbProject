using System;

namespace AKB.Core.Managing.UpdateSystem.Implementations
{
    /* CLASS DOCUMENTATION *\
     * 
     * [Variable Specifics]
     * 
     * [Class Flow]
     * 1. ....
     * 2. ....
     * 
     * [Must Know]
     * 1. ...
     */

    public class DevilRageAdvancement : Advancement
    {
        private DevilRageAdvancement()
        {
            advancementType = AdvancementType.DevilRage;
        }

        public DevilRageAdvancement(int zeroBasedMaxTier) : this()
        {
            this.advancementMaxTier = zeroBasedMaxTier;
        }

        public override bool AdvanceSkill()
        {
            if (CanAdvance())
            {
                UpdatePerTier(advancementCurrentTier);

                advancementCurrentTier++;
                return true;
            }

            return false;
        }

        protected override void UpdatePerTier(int currentTier)
        {
            switch (currentTier)
            {
                case -1:
                {
                    ManagerHUB.GetManager.PlayerEntity.DevilRage.SetIsUnlockedState(true);
                }
                break;

                case 0:
                {
                    float fill = ManagerHUB.GetManager.PlayerEntity.DevilRage.GetRageFillRate();

                    fill *= 2;

                    ManagerHUB.GetManager.PlayerEntity.DevilRage.SetRageFillValue(fill);
                }
                break;

                case 1:
                {
                    float duration = ManagerHUB.GetManager.PlayerEntity.DevilRage.GetRageDuration();

                    duration *= 2;

                    ManagerHUB.GetManager.PlayerEntity.DevilRage.SetRageDuration(duration);
                }
                break;
            }
        }
    }
}