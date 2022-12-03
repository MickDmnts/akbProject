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

    public enum SpearTypeAdvancements
    {
        None = 0,

        SpearPierce = 1,
        DamageAtTeleportPoint = 2,
        //PullEnemyOnSpearRecall = 3,
    }

    public class SpearRunAdvancements : MonoBehaviour,
        IAdvanceable
    {
        SpearTypeAdvancements activeAdvancement = SpearTypeAdvancements.None;

        public void SetActiveAdvancement(SpearTypeAdvancements advancement) => activeAdvancement = advancement;

        public bool GetIsAdvancementActive(SpearTypeAdvancements advancement)
        {
            return advancement == activeAdvancement;
        }
    }
}