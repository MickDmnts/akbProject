using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace akb.Core.Managing.InRunUpdates
{
    /// <summary>
    /// The available player slots.
    /// Each slot type represents an array index.
    /// </summary>
    public enum SlotType
    {
        None = -1,

        Attack = 0,
        Throw = 1,
        DodgeRoll = 2,
        Passive = 3,
        DevilRage = 4,
    }

    /// <summary>
    /// The available advancements of the game.
    /// </summary>
    public enum AdvancementTypes
    {
        None = -1,

        //Attack specific
        ThirdEnflamed = 0,
        Lighting = 1,
        ThirdStun = 2,

        //Spear Throw specific
        SpearPierce = 3,
        DamageAtTeleportPoint = 4,

        //Dodge Roll Specific
        MovementSpeed = 5,
        PushAway = 6,
        ShockOnTouch = 7,

        //Passives
        IgnoreFirstHit = 8,
        MultiplyCoins = 9,
        RegenHealthOnRoomEntry = 10,

        //Devil Rage Specific
        EnflameSuroundings = 11,
        DoubleSpeed = 12,
        TeleportOnDodge = 13,
    }

    public class SlotsHandler : MonoBehaviour
    {
        /// <summary>
        /// The images holding the slots in the user HUD.
        /// </summary>
        /// <returns></returns>
        [SerializeField, Tooltip("The images holding the slots in the user HUD.")] List<Image> slotImages;

        /// <summary>
        /// The player slots containg the advancement handlers.
        /// </summary>
        Dictionary<SlotType, IAdvanceable> slotHandlerPairs;

        #region IN_RUN_ADVANCEMENT_HANDLERS
        /// <summary>
        /// Get the Attack Advancement handler
        /// </summary>
        public AttackRunAdvancements AttackInRunAdvancements { get; private set; }

        /// <summary>
        /// Get the Spear Advancement handler
        /// </summary>
        public SpearRunAdvancements SpearInRunAdvancements { get; private set; }

        /// <summary>
        /// Get the Dodge Advancement handler
        /// </summary>
        public DodgeRunAdvancements DodgeInRunAdvancements { get; private set; }

        /// <summary>
        /// Get the Passive Advancement handler
        /// </summary>
        public PassiveRunAdvancements PassiveInRunAdvancements { get; private set; }

        /// <summary>
        /// Get the Devil Rage Advancement handler
        /// </summary>
        public DevilRageRunAdvancements DevilRageInRunAdvancements { get; private set; }
        #endregion

        private void Awake()
        {
            ManagerHUB.GetManager.SetSlotHandlerReference(this);
            ManagerHUB.GetManager.GameEventsHandler.onPlayerHubEntry += ResetAbilitiesOnDeath;

            EntrySetup();
        }

        /// <summary>
        /// Creates IAdvanceable instances from every handler 
        /// (AttackInRunAdvancements, SpearInRunAdvancements, DodgeInRunAdvancements, 
        /// PassiveInRunAdvancements, DevilRageInRunAdvancements)
        /// </summary>
        void EntrySetup()
        {
            slotHandlerPairs = new Dictionary<SlotType, IAdvanceable>()
            {
                {SlotType.Attack, AttackInRunAdvancements = new AttackRunAdvancements()},
                {SlotType.Throw, SpearInRunAdvancements = new SpearRunAdvancements()},
                {SlotType.DodgeRoll, DodgeInRunAdvancements = new DodgeRunAdvancements()},
                {SlotType.Passive, PassiveInRunAdvancements = new PassiveRunAdvancements()},
                {SlotType.DevilRage, DevilRageInRunAdvancements = new DevilRageRunAdvancements()},
            };
        }

        ///<summary>Returns the active In run advancements of the player as a string array.</summary>
        public string[] GetSlottedAdvancementTypes()
        {
            List<string> parsedTypes = new List<string>();

            foreach (KeyValuePair<SlotType, IAdvanceable> pair in slotHandlerPairs)
            {
                string name = pair.Value.GetActiveName();
                parsedTypes.Add(name);
            }

            return parsedTypes.ToArray();
        }

        ///<summary>Returns the active In run advancements types of the player as a dictionary.</summary>
        public Dictionary<SlotType, AdvancementTypes> GetInRunAdvancementTypes()
        {
            Dictionary<SlotType, AdvancementTypes> pairs = new Dictionary<SlotType, AdvancementTypes>();

            foreach (KeyValuePair<SlotType, IAdvanceable> pair in slotHandlerPairs)
            {
                SlotType slotType = pair.Key;
                AdvancementTypes advType = Enum.Parse<AdvancementTypes>(pair.Value.GetActiveName());

                pairs.TryAdd(slotType, advType);
            }

            return pairs;
        }

        ///<summary>Sets the passed type to the corresponding slot AdvancementSlot passed.</summary>
        public void SetAdvancement(SlotType slot, AdvancementTypes type)
        {
            slotHandlerPairs[slot].SetActiveAdvancement(type);
        }

        /*FUTURE DEVELOPMENT*/
        void ResetAbilitiesOnDeath()
        {
            foreach (KeyValuePair<SlotType, IAdvanceable> pair in slotHandlerPairs)
            {
                pair.Value.SetActiveAdvancement(AdvancementTypes.None);
            }
        }

        private void OnDestroy()
        {
            ManagerHUB.GetManager.GameEventsHandler.onPlayerHubEntry -= ResetAbilitiesOnDeath;
        }
    }
}