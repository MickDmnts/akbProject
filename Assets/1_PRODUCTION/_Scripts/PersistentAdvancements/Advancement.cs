namespace AKB.Core.Managing.UpdateSystem
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

    public abstract class Advancement
    {
        /* Tier Specifics
         * --------------
         * -1 : Not unclocked
         * 0 : First tier
         * 1 : etc...
         */
        protected int advancementCurrentTier = -1;
        protected int advancementMaxTier = -1;

        protected AdvancementType advancementType;

        public abstract bool AdvanceSkill();

        protected abstract void UpdatePerTier(int currentTier);

        protected virtual bool CanAdvance()
        {
            return advancementCurrentTier < advancementMaxTier;
        }

        public virtual int GetCurrentTier()
        {
            return advancementCurrentTier;
        }
    }
}