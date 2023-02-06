using System.Collections.Generic;

namespace AKB.Core.Managing.UpdateSystem
{
    using AKB.Core.Managing.UpdateSystem.Implementations;
    /* CLASS DOCUMENTATION *\
     * 
     * [Variable Specifics]
     * Inspector values: ___ MUST be set from the inspector
     * Dynamically changed: These variables dynamically change throughout the game
     * 
     * [Class Flow]
     * 1. ....
     * 2. ....
     * 
     * [Must Know]
     * 1. ...
     */

    public enum AdvancementType
    {
        Health,
        MeleeAttacks,
        SpearThrow,
        Dodge,
        TeleportationCharge,
        DevilRage,
    }

    public class AdvancementHandler
    {
        //Sinner souls should be loaded from the save file later
        public int SinnerSouls { get; private set; }

        Dictionary<AdvancementType, Advancement> advancements = new Dictionary<AdvancementType, Advancement>()
        {
            {AdvancementType.Health, new HealthAdvancement(2) },
            {AdvancementType.MeleeAttacks, new MeleeAdvancement(1) },
            {AdvancementType.SpearThrow, new SpearThrowAdvancement(1) },
            {AdvancementType.Dodge, new DodgeAdvancement(1) },
            {AdvancementType.TeleportationCharge, new TeleportationChargeAdvancement(1) },
            {AdvancementType.DevilRage, new DevilRageAdvancement(2) },
        };

        public AdvancementHandler()
        {
            //LoadSinnerSoulsValue();
        }

        /*void LoadSinnerSoulsValue()
        {
            SinnerSouls = SaveDataHandler.ReadSinnerSoulsValue();
        }*/

        public bool AdvanceTierOf(AdvancementType typeOfAdvancement)
        {
            return advancements[typeOfAdvancement].AdvanceSkill();
        }

        public bool IsAdvancementUnlocked(AdvancementType advancementType, int desiredTier)
        {
            return advancements[advancementType].GetCurrentTier() == desiredTier;
        }
    }
}