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
                    GameManager.S.PlayerEntity.DevilRage.SetIsUnlockedState(true);
                }
                break;

                case 0:
                {
                    float fill = GameManager.S.PlayerEntity.DevilRage.GetRageFillRate();

                    fill *= 2;

                    GameManager.S.PlayerEntity.DevilRage.SetRageFillValue(fill);
                }
                break;

                case 1:
                {
                    float duration = GameManager.S.PlayerEntity.DevilRage.GetRageDuration();

                    duration *= 2;

                    GameManager.S.PlayerEntity.DevilRage.SetRageDuration(duration);
                }
                break;
            }
        }
    }
}