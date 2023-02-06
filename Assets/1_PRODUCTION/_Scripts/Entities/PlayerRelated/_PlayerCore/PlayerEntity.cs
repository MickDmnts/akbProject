using UnityEngine;

using AKB.Core.Managing;
using AKB.Entities.Player.SpearHandling;
using AKB.Entities.Interactions;
using AKB.Entities.Player.Interactions;

namespace AKB.Entities.Player
{
    [DefaultExecutionOrder(400)]
    public sealed class PlayerEntity : Entity
    {
        [Header("Set in inspector")]
        [SerializeField] float playerMaxHealth;
        [SerializeField, Range(0, 1f)] float mitigateDamageAfter;

        bool _isActive = false;
        public bool IsActive
        {
            get { return _isActive; }
            set { _isActive = value; }
        }

        bool _isDead = false;
        public bool IsDead
        {
            get { return _isDead; }
            set { _isDead = value; }
        }

        #region BEHAVIOUR_CACHING
        public PlayerInteractable PlayerInteractable { get; private set; }

        public PlayerAnimations PlayerAnimations { get; private set; }
        public PlayerMovement PlayerMovement { get; private set; }
        public PlayerSpearTeleporting PlayerSpearTeleporting { get; private set; }
        public PlayerSpearThrow PlayerSpearThrow { get; private set; }
        public PlayerDodgeRoll PlayerDodgeRoll { get; private set; }
        public PlayerAttack PlayerAttack { get; private set; }
        public DevilRage DevilRage { get; private set; }

        public PlayerInputs PlayerInputs { get; private set; }
        public EntityAttackFOV PlayerAttackFOV { get; private set; }

        public Rigidbody Rigidbody { get; private set; }
        #endregion

        private void Awake()
        {
            GameManager.S.SetPlayerReference(this);

            PlayerInputs = new PlayerInputs();
            PlayerInteractable = GetComponent<PlayerInteractable>();

            PlayerAttackFOV = GetComponentInChildren<EntityAttackFOV>();
            PlayerMovement = GetComponent<PlayerMovement>();
            PlayerAnimations = GetComponent<PlayerAnimations>();
            PlayerSpearTeleporting = GetComponentInChildren<PlayerSpearTeleporting>();
            PlayerSpearThrow = GetComponentInChildren<PlayerSpearThrow>();
            PlayerDodgeRoll = GetComponent<PlayerDodgeRoll>();
            PlayerAttack = GetComponentInChildren<PlayerAttack>();
            DevilRage = GetComponentInChildren<DevilRage>();

            Rigidbody = GetComponent<Rigidbody>();

            _isDead = false;
            _isActive = true;
        }

        public int GetPlayerHealth() => (int)EntityLife;
        public void SetPlayerHealth(float value) => EntityLife = value;
        public int GetPlayerMaxHealth() => (int)playerMaxHealth;

        public void IncrementPlayerHealthBy(int value)
        {
            value = value > GetPlayerMaxHealth() ? GetPlayerMaxHealth() : value;

            EntityLife += value;
        }

        public void IncrementPlayerMaxHealthBy(int incrementValue)
        {
            playerMaxHealth += incrementValue;
        }

        public int GetMitigateDamageAfter() => (int)mitigateDamageAfter;
    }
}
