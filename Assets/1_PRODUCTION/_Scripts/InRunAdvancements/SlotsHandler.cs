using UnityEngine;

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

    [DefaultExecutionOrder(150)]
    public class SlotsHandler : MonoBehaviour
    {
        /// <summary>
        /// The advancement slots of the player
        /// </summary>
        /// <returns></returns>
        [Header("Set in inspector")]
        [SerializeField, Tooltip("The advancement slots of the player.")] int slotSize = 5;

        /// <summary>
        /// The player active advancements
        /// </summary>
        IAdvanceable[] slots;

        #region IN_RUN_ADVANCEMENT_HANDLERS
        /// <summary>
        /// Get the Attack Advancement handler
        /// </summary>
        public AttackRunAdvancements AttackAdvancementHandler { get; private set; }

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
        public PassiveRunAdvancements PassiveRunAdvancements { get; private set; }

        /// <summary>
        /// Get the Devil Rage Advancement handler
        /// </summary>
        public DevilRageRunAdvancements DevilRageRunAdvancements { get; private set; }
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

            AttackAdvancementHandler = new AttackRunAdvancements();
            SpearInRunAdvancements = new SpearRunAdvancements();
            DodgeInRunAdvancements = new DodgeRunAdvancements();
            PassiveRunAdvancements = new PassiveRunAdvancements();
            DevilRageRunAdvancements = new DevilRageRunAdvancements();
        }

        /* FUTURE DEVELOPMENT
        void NullifyAbilitiesOnDeath()
        {
            for (int i = 0; i < slots.Length; i++)
            {
                slots[i] = null;
            }
        }

        void UpdateUserHUDSlots()
        {
            throw new System.NotImplementedException();
        }

        void ClearCurrentHUDSlots()
        {
            throw new System.NotImplementedException();
        }*/
    }
}