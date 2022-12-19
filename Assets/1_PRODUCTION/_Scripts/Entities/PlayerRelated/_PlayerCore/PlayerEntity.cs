using UnityEngine;

using akb.Core.Managing;
using akb.Entities.Player.SpearHandling;
using akb.Entities.Interactions;
using akb.Entities.Player.Interactions;
using akb.Core.Managing.LevelLoading;

namespace akb.Entities.Player
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
            ManagerHUB.GetManager.SetPlayerReference(this);

            PlayerInputs = new PlayerInputs();
            PlayerInteractable = GetComponent<PlayerInteractable>();

            Rigidbody = GetComponent<Rigidbody>();

            PlayerMovement = GetComponent<PlayerMovement>();
            PlayerAnimations = GetComponent<PlayerAnimations>();
            PlayerSpearThrow = GetComponentInChildren<PlayerSpearThrow>();
            PlayerSpearTeleporting = GetComponentInChildren<PlayerSpearTeleporting>();
            PlayerDodgeRoll = GetComponent<PlayerDodgeRoll>();
            PlayerAttack = GetComponentInChildren<PlayerAttack>();
            PlayerAttackFOV = GetComponentInChildren<EntityAttackFOV>();
            DevilRage = GetComponentInChildren<DevilRage>();

            _isDead = false;
            _isActive = true;

            ManagerHUB.GetManager.GameEventsHandler.onSceneChanged += SetMovementStatesBasedOnRoom;
        }

        void SetMovementStatesBasedOnRoom(GameScenes gameScenes)
        {
            if (gameScenes == GameScenes.PlayerScene)
            {
                PlayerAttack.SetAttackInputActiveState(false);
                PlayerMovement.SetMovementInputActiveState(false);
                PlayerSpearThrow.SetThrowInputActiveState(false);
                PlayerDodgeRoll.SetDodgeInputActiveState(false);
            }
            else if (gameScenes == GameScenes.PlayerHUB)
            {
                PlayerMovement.SetMovementInputActiveState(true);
                PlayerAttack.SetAttackInputActiveState(false);
                PlayerSpearThrow.SetThrowInputActiveState(false);
                PlayerDodgeRoll.SetDodgeInputActiveState(false);
            }
            else if (gameScenes == GameScenes.World1Scene
                || gameScenes == GameScenes.TutorialArena)
            {
                PlayerAttack.SetAttackInputActiveState(true);
                PlayerMovement.SetMovementInputActiveState(true);
                PlayerSpearThrow.SetThrowInputActiveState(true);
                PlayerDodgeRoll.SetDodgeInputActiveState(true);
            }
        }

        public int GetPlayerHealth() => (int)EntityLife;

        public void SetPlayerHealth(float value)
        {
            EntityLife = value;

            ManagerHUB.GetManager.GameEventsHandler.OnPlayerHealthChange(EntityLife, playerMaxHealth);
        }

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

        private void OnDisable()
        {
            ManagerHUB.GetManager.GameEventsHandler.onSceneChanged -= SetMovementStatesBasedOnRoom;
        }
    }
}
