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

    public class MeleeAdvancement : Advancement
    {
        private MeleeAdvancement() { }

        public MeleeAdvancement(int zeroBasedMaxTier) : this()
        {
            advancementType = AdvancementType.MeleeAttacks;
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
                    IncreasePlayerDamage();
                }
                break;

                case 0:
                {
                    EnableHealthRegen();
                }
                break;
            }

            advancementCurrentTier++;
        }

        void IncreasePlayerDamage()
        {
            int currentDamage = ManagerHUB.GetManager.PlayerEntity.PlayerAttack.GetAttackDamage();

            currentDamage = (currentDamage / 2) + currentDamage;

            ManagerHUB.GetManager.PlayerEntity.PlayerAttack.SetAttackDamage(currentDamage);
        }

        void EnableHealthRegen()
        {
            ManagerHUB.GetManager.PlayerEntity.PlayerAttack.SetHealthRegenState(true);
        }
    }
}