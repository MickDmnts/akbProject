using AKB.Core.Managing;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AKB.Entities.Player
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
            GameManager.S.GameEventsHandler.onEnemyHit += IncreaseRageFill;

            playerEntity = transform.root.GetComponent<PlayerEntity>();

            rageInput = playerEntity.PlayerInputs.Player.Rage;
            rageInput.Enable();

            rageInput.started += _ => ActivateRage();
        }

        private void Update()
        {
            if (rageActive)
            {
                rageFill -= rageDecreaseValue * Time.deltaTime;

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
            //if (!isUnlocked) return;

            rageFill += rageFillRate;

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

                int currentDamage = GameManager.S.PlayerEntity.PlayerAttack.GetAttackDamage();

                currentDamage *= rageDamageMultiplier;

                GameManager.S.PlayerEntity.PlayerAttack.SetAttackDamage(currentDamage, true);
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
            GameManager.S.PlayerEntity.PlayerAttack.SetAttackDamageToCache();
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

        private void OnDestroy()
        {
            if (GameManager.S != null)
                GameManager.S.GameEventsHandler.onEnemyHit -= IncreaseRageFill;
        }
    }
}