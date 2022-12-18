using System.Collections.Generic;

namespace akb.Core.Managing.InRunUpdates
{
    [System.Serializable]
    public class AttackRunAdvancements : IAdvanceable
    {
        AdvancementTypes activeAdvancement = AdvancementTypes.None;

        Dictionary<AdvancementTypes, EffectType> attackEffectPairs = new Dictionary<AdvancementTypes, EffectType>()
        {
            {AdvancementTypes.None, EffectType.None},
            {AdvancementTypes.ThirdEnflamed, EffectType.Enflamed },
            {AdvancementTypes.Lighting, EffectType.Shocked },
            {AdvancementTypes.ThirdStun, EffectType.Stunned },
        };

        public AttackRunAdvancements() { }

        public EffectType GetCurrentAdvancementEffect()
        {
            return attackEffectPairs[activeAdvancement];
        }

        public void SetActiveAdvancement(AdvancementTypes advancement)
        {
            activeAdvancement = advancement;
        }

        public string GetActiveName()
        {
            return System.Enum.GetName(typeof(AdvancementTypes), activeAdvancement);
        }
    }
}