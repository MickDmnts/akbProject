﻿namespace AKB.Core.Managing.UpdateSystem.Implementations
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

    public sealed class SpearThrowAdvancement : Advancement
    {
        int spearDamageMultiplier = 2;
        float spearRangeMultiplier = 0.3f;

        private SpearThrowAdvancement() { }

        public SpearThrowAdvancement(int zeroBasedMaxTier) : this()
        {
            advancementType = AdvancementType.SpearThrow;
            advancementMaxTier = zeroBasedMaxTier;
        }

        public override bool AdvanceSkill()
        {
            if (CanAdvance())
            {
                UpdatePerTier(advancementCurrentTier);
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
                    IncreaseSpearDamage();
                }
                break;

                case 0:
                {
                    IncreaseSpearRange();
                }
                break;
            }

            advancementCurrentTier++;
        }

        void IncreaseSpearDamage()
        {
            int currentSpearDamage = GameManager.S.PlayerEntity.PlayerSpearThrow.GetSpearRecallDamage();

            currentSpearDamage *= spearDamageMultiplier;

            GameManager.S.PlayerEntity.PlayerSpearThrow.SetSpearRecallDamage(currentSpearDamage);
        }

        void IncreaseSpearRange()
        {
            float currentSpearRange = GameManager.S.PlayerEntity.PlayerSpearThrow.GetHoldCounterMultiplier();

            currentSpearRange += currentSpearRange * spearRangeMultiplier;

            GameManager.S.PlayerEntity.PlayerSpearThrow.SetHoldCounterMultiplier(currentSpearRange);
        }
    }
}