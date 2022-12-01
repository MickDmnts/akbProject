using UnityEngine;

namespace AKB.Core.Managing.InRunUpdates
{

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

    public enum PassiveAdvancements
    {
        None = 0,

        IgnoreFirstHit = 1,
        MultiplyCoins = 2,
        RegenHealthOnRoomEntry = 3,
    }

    public class PassiveRunAdvancements : MonoBehaviour,
        IAdvanceable
    {
        PassiveAdvancements activeAdvancement = PassiveAdvancements.None;

        public void SetActiveAdvancement(PassiveAdvancements advancement) => activeAdvancement = advancement;

        public bool GetIsAdvancementActive(PassiveAdvancements advancement)
        {
            return advancement == activeAdvancement;
        }
    }
}