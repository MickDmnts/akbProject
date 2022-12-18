using System.Collections.Generic;

namespace akb.Core.Managing.UpdateSystem
{
    using System.Text;
    using akb.Core.Managing.UpdateSystem.Implementations;
    using UnityEngine;

    public enum AdvancementType
    {
        Health,
        MeleeAttacks,
        SpearThrow,
        Dodge,
        TeleportationCharge,
        DevilRage,
    }

    [System.Serializable]
    public class AdvancementHandler
    {
        Dictionary<AdvancementType, Advancement> advancements = new Dictionary<AdvancementType, Advancement>()
        {
            {AdvancementType.Health, new HealthAdvancement(2) },
            {AdvancementType.MeleeAttacks, new MeleeAdvancement(1) },
            {AdvancementType.SpearThrow, new SpearThrowAdvancement(1) },
            {AdvancementType.Dodge, new DodgeAdvancement(1) },
            {AdvancementType.TeleportationCharge, new TeleportationChargeAdvancement(1) },
            {AdvancementType.DevilRage, new DevilRageAdvancement(2) },
        };

        public AdvancementHandler(ManagerHUB hub) { }

        public bool AdvanceTierOf(AdvancementType typeOfAdvancement)
        {
            Debug.Log("Updated " + typeOfAdvancement);
            return advancements[typeOfAdvancement].AdvanceSkill();
        }

        public bool IsAdvancementUnlocked(AdvancementType advancementType, int desiredTier)
        {
            return advancements[advancementType].GetCurrentTier() == desiredTier;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            foreach (KeyValuePair<AdvancementType, Advancement> pair in advancements)
            {
                sb.Append(pair.Key.ToString());
                sb.Append(" : ");
                sb.Append(pair.Value.GetCurrentTier());
                sb.Append("\n");
            }

            return sb.ToString();
        }
    }
}