using System.Collections.Generic;

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

    public class DodgeAdvancement : Advancement
    {
        Dictionary<int, int> _dodgeTierUpdatePairs;

        private DodgeAdvancement()
        {
            _dodgeTierUpdatePairs = new Dictionary<int, int>
            {
                { -1, 2}, // tier 1
                {0, 3 }, // tier 2
            };
        }

        public DodgeAdvancement(int zeroBasedMaxTier) : this()
        {
            this.advancementMaxTier = zeroBasedMaxTier;

            advancementType = AdvancementType.Dodge;
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
            ManagerHUB.GetManager.PlayerEntity.PlayerDodgeRoll.SetMaxDodges(_dodgeTierUpdatePairs[currentTier]);
        }
    }
}