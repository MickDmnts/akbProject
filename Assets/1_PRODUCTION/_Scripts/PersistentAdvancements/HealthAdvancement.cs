using System.Collections.Generic;

namespace akb.Core.Managing.UpdateSystem.Implementations
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

    public class HealthAdvancement : Advancement
    {
        Dictionary<int, int> _healthTierUpdatesPairs;

        private HealthAdvancement()
        {
            _healthTierUpdatesPairs = new Dictionary<int, int>
            {
                { -1, 50}, // tier 1
                {0, 150 }, // tier 2
                {1, 300 }, // tier 3
            };
        }

        public HealthAdvancement(int zeroBasedMaxTier) : this()
        {
            this.advancementMaxTier = zeroBasedMaxTier;

            advancementType = AdvancementType.Health;
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
            ManagerHUB.GetManager.PlayerEntity.IncrementPlayerMaxHealthBy(_healthTierUpdatesPairs[currentTier]);
        }
    }
}