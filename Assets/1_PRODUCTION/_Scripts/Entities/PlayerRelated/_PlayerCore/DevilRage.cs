using UnityEngine;
using UnityEngine.InputSystem;

using akb.Core.Managing;
using akb.Core.Managing.LevelLoading;

namespace akb.Entities.Player
{
    public class DevilRage : MonoBehaviour
    {
        const float RAGE_MIN = 0f;

        [Header("Set in inspector")]
        [SerializeField] float rageFill = 1;

        [SerializeField] float rageFillRate = 0.05f;
        [SerializeField] float rageDecreaseValue = 0.08f;

        [SerializeField] float rageDuration = 6f;
        [SerializeField] int rageDamageMultiplier = 2;

        PlayerEntity playerEntity;
        InputAction rageInput;

        float currentRageMax = 1;

        bool isUnlocked = false;
        bool rageActive;

        private void Start()
        {
            EntrySetup();
        }

        void EntrySetup()
        {
            ManagerHUB.GetManager.GameEventsHandler.onEnemyHit += IncreaseRageFill;
            ManagerHUB.GetManager.GameEventsHandler.onSceneChanged += ResetRage;

            playerEntity = transform.root.GetComponent<PlayerEntity>();

            rageInput = playerEntity.PlayerInputs.Player.Rage;
            rageInput.Enable();

            rageInput.started += _ => ActivateRage();
        }

        void ResetRage(GameScenes scene)
        {
            if (scene != GameScenes.PlayerHUB) { return; }

            rageFill = RAGE_MIN;
            rageActive = false;

            ResetDamageToDefault();
        }

        private void Update()
        {
            if (rageActive)
            {
                rageFill -= rageDecreaseValue * Time.deltaTime;
                ManagerHUB.GetManager.GameEventsHandler.OnPlayerRageChange(rageFill);

                if (rageFill <= 0f)
                {
                    rageFill = RAGE_MIN;
                    rageActive = false;

                    ResetDamageToDefault();
                }
            }
        }

        /// <summary>
        /// Call to increase the rage fill by rage file rate
        /// </summary>
        void IncreaseRageFill()
        {
            if (!isUnlocked) return;

            rageFill += rageFillRate;
            ManagerHUB.GetManager.GameEventsHandler.OnPlayerRageChange(rageFill);

            if (rageFill >= currentRageMax)
            {
                rageFill = currentRageMax;
            }
        }

        /// <summary>
        /// Sets rageActive to true if CanActivate() returns true.
        /// </summary>
        void ActivateRage()
        {
            if (!isUnlocked) return;

            if (CanActivate())
            {
                rageActive = true;

                int currentDamage = ManagerHUB.GetManager.PlayerEntity.PlayerAttack.GetAttackDamage();

                currentDamage *= rageDamageMultiplier;

                ManagerHUB.GetManager.PlayerEntity.PlayerAttack.SetAttackDamage(currentDamage, true);
            }
        }

        /// <summary>
        /// Returns true if rageFill == RAGE_MAX, false otherwise.
        /// </summary>
        bool CanActivate()
        {
            return rageFill == currentRageMax;
        }

        void ResetDamageToDefault()
        {
            ManagerHUB.GetManager.PlayerEntity.PlayerAttack.SetAttackDamageToCache();
        }

        #region UTILITIES
        public void SetIsUnlockedState(bool state) => isUnlocked = state;
        public bool GetIsUnlocked() => isUnlocked;

        public float GetRageFillRate() => rageFillRate;
        public float GetRageDuration() => rageDuration;

        public void SetRageDuration(float value) => rageDuration = value;
        public void SetRageFillValue(float value)
        {
            rageFillRate = value;
        }
        #endregion

        private void OnDisable()
        {
            if (ManagerHUB.GetManager != null)
            {
                ManagerHUB.GetManager.GameEventsHandler.onEnemyHit -= IncreaseRageFill;
                ManagerHUB.GetManager.GameEventsHandler.onSceneChanged -= ResetRage;
            }
        }
    }
}