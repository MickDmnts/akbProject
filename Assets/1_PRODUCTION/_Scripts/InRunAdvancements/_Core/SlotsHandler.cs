using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AKB.Core.Managing.InRunUpdates
{
    /// <summary>
    /// The available player slots.
    /// Each slot type represents an array index.
    /// </summary>
    public enum SlotType
    {
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
        //PullEnemyOnSpearRecall = 5,

        //Dodge Roll Specific
        MovementSpeed = 6,
        PushAway = 7,
        ShockOnTouch = 8,

        //Passives
        IgnoreFirstHit = 9,
        MultiplyCoins = 10,
        RegenHealthOnRoomEntry = 11,

        //Devil Rage Specific
        EnflameSuroundings = 12,
        DoubleSpeed = 13,
        TeleportOnDodge = 14,
    }

    public class SlotsHandler : MonoBehaviour
    {
        /// <summary>
        /// The advancement slots number of the player.
        /// </summary>
        /// <returns></returns>
        [Header("Set in inspector")]
        [SerializeField, Tooltip("The advancement slots number of the player.")] int slotSize = 5;
        /// <summary>
        /// The images holding the slots in the user HUD.
        /// </summary>
        /// <returns></returns>
        [SerializeField, Tooltip("The images holding the slots in the user HUD.")] List<Image> slotImages;

        /// <summary>
        /// The player slots containg the advancement handlers.
        /// </summary>
        IAdvanceable[] slots;

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

            EntrySetup();
        }

        /// <summary>
        /// Creates IAdvanceable instances from every handler 
        /// (AttackAdvancementHandler, SpearInRunAdvancements, DodgeInRunAdvancements, 
        /// PassiveRunAdvancements, PassiveRunAdvancements, DevilRageRunAdvancements)
        /// </summary>
        void EntrySetup()
        {
            slots = new IAdvanceable[slotSize];

            AttackInRunAdvancements = new AttackRunAdvancements();
            SpearInRunAdvancements = new SpearRunAdvancements();
            DodgeInRunAdvancements = new DodgeRunAdvancements();
            PassiveInRunAdvancements = new PassiveRunAdvancements();
            DevilRageInRunAdvancements = new DevilRageRunAdvancements();

            slots[0] = AttackInRunAdvancements;
            slots[1] = SpearInRunAdvancements;
            slots[2] = DodgeInRunAdvancements;
            slots[3] = PassiveInRunAdvancements;
            slots[4] = DevilRageInRunAdvancements;
        }

        public string[] GetSlottedAdvancementTypes()
        {
            List<string> parsedTypes = new List<string>();

            for (int i = 0; i < slots.Length; i++)
            {
                string name = slots[i].GetActiveName();
                parsedTypes.Add(name);
            }

            return parsedTypes.ToArray();
        }

        /*FUTURE DEVELOPMENT*/
        void NullifyAbilitiesOnDeath()
        {
            for (int i = 0; i < slots.Length; i++)
            {
                slots[i].SetActiveAdvancement(AdvancementTypes.None);
            }
        }

        void UpdateUserHUDSlots()
        {
            throw new System.NotImplementedException();
        }

        void ClearCurrentHUDSlots()
        {
            throw new System.NotImplementedException();
        }
    }
}