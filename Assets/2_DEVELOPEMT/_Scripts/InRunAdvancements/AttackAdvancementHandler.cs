using System.Collections.Generic;
using UnityEngine;

namespace AKB.Core.Managing.InRunUpdates
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

    public enum AttackInRunAdvancements
    {
        None,

        ThirdEnflamed,
        Lighting,
        ThirdStun,
    }

    public class AttackAdvancementHandler : MonoBehaviour,
        IAdvanceable
    {
        AttackInRunAdvancements activeAdvancement = AttackInRunAdvancements.None;

        Dictionary<AttackInRunAdvancements, EffectType> attackEffectPairs = new Dictionary<AttackInRunAdvancements, EffectType>()
        {
            {AttackInRunAdvancements.None, EffectType.None},
            {AttackInRunAdvancements.ThirdEnflamed, EffectType.Enflamed },
            {AttackInRunAdvancements.Lighting, EffectType.Shocked },
            {AttackInRunAdvancements.ThirdStun, EffectType.Stunned },
        };

        private void Start()
        {
            GameManager.S.SlotsHandler.SetAdvanceableAtSlot(SlotType.Attack, this);
        }

        public EffectType GetCurrentAdvancementEffect()
        {
            return attackEffectPairs[activeAdvancement];
        }

        public void SetActiveAdvancement(AttackInRunAdvancements advancement)
        {
            activeAdvancement = advancement;
        }
    }
}