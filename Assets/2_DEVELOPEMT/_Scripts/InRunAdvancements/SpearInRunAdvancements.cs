using System;
using System.Collections.Generic;
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

    public enum SpearRunAdvancements
    {
        None = 0,

        SpearPierce = 1,
        DamageAtTeleportPoint = 2,
        PullEnemyOnSpearRecall = 3,
    }

    public class SpearInRunAdvancements : MonoBehaviour,
        IAdvanceable
    {
        SpearRunAdvancements activeAdvancement = SpearRunAdvancements.None;

        /*Dictionary<SpearRunAdvancements, bool> spearBoolPairs = new Dictionary<SpearRunAdvancements, bool>
        {
            {SpearRunAdvancements.None, false},
            {SpearRunAdvancements.SpearPierce, true},
            {SpearRunAdvancements.DamageAtTeleportPoint, true},
            {SpearRunAdvancements.PullEnemyOnSpearRecall, true},
        };
*/
        public void SetActiveAdvancement(SpearRunAdvancements advancement) => activeAdvancement = advancement;

        public bool GetIsAdvancementActive(SpearRunAdvancements advancement)
        {
            return advancement == activeAdvancement;
        }
    }
}