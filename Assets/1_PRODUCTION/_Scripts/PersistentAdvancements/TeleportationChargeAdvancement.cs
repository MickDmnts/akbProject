using System.Collections.Generic;
using UnityEngine.UI;

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

    public sealed class TeleportationChargeAdvancement : Advancement
    {
        Dictionary<int, int> _teleportTierUpdatePairs;

        private TeleportationChargeAdvancement()
        {
            _teleportTierUpdatePairs = new Dictionary<int, int>
            {
                { -1, 1}, // tier 1
                {0, 2 }, // tier 2
            };
        }

        public TeleportationChargeAdvancement(int zeroBasedMaxTier) : this()
        {
            this.advancementMaxTier = zeroBasedMaxTier;

            advancementType = AdvancementType.TeleportationCharge;
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
            ManagerHUB.GetManager.PlayerEntity.PlayerSpearTeleporting.IncreaseTeleportChargesBy(_teleportTierUpdatePairs[currentTier]);
        }
    }
}