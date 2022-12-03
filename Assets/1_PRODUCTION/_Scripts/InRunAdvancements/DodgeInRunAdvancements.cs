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

    public enum DodgeRunAdvancements
    {
        None = 0,

        MovementSpeed = 1,
        PushAway = 2,
        ShockOnTouch = 3,
    }

    public class DodgeInRunAdvancements : MonoBehaviour,
        IAdvanceable
    {
        DodgeRunAdvancements activeAdvancement = DodgeRunAdvancements.None;

        public void SetActiveAdvancement(DodgeRunAdvancements advancement) => activeAdvancement = advancement;

        public bool GetIsAdvancementActive(DodgeRunAdvancements advancement)
        {
            return advancement == activeAdvancement;
        }
    }
}