
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

using akb.Entities.Interactions;
using akb.Core.Managing;
using akb.Core.Sounds;

namespace akb.Entities.Player
{
    [DefaultExecutionOrder(460)]
    public class PlayerAttack : MonoBehaviour
    {
        #region INSPECTOR_VARS
        [Header("Set in inspector")]
        [SerializeField] int attackDamage = 25;
        [SerializeField] float attackCooldown = 0.2f;
        [SerializeField, Range(0f, 1f)] float rotateToAttackDir = 0.5f;
        #endregion

        #region PRIVATE_VARS
        PlayerEntity playerEntity;

        InputAction attackAction;

        #region ATTACK_VARS
        bool isAttacking = false;
        bool healthRegenActive = false;

        float nextAttack;
        float attackCooldownCache;
        int statusEffectCounter = 0;
        float resetEffectCounter = 1f;
        int attackDamageCache;
        #endregion
        #endregion

        private void Awake()
        {
            attackDamageCache = attackDamage;
        }

        // Start is called before the first frame update
        void Start()
        {
            EntrySetup();
        }

        /// <summary>
        /// Call to set up player attacking when the game gets loaded.
        /// </summary>
        void EntrySetup()
        {
            playerEntity = transform.root.GetComponent<PlayerEntity>();

            //Input setup
            attackAction = playerEntity.PlayerInputs.Player.Fire;
            attackAction.canceled += _ => AttackReleasedCallback();
            attackAction.Enable();

            attackCooldownCache = attackCooldown;
        }

        void Update()
        {
            if (!playerEntity.PlayerSpearThrow.GetHasSpear()
                || playerEntity.PlayerDodgeRoll.GetIsDodging()) return;

            if (!isAttacking)
            {
                resetEffectCounter -= Time.deltaTime;
                if (resetEffectCounter <= 0)
                {
                    statusEffectCounter = 0;
                }
            }
            else
            {
                resetEffectCounter = 1f;
            }

            if (attackAction.ReadValue<float>() > 0.5f)
            {
                //If the user is pivoting
                if (playerEntity.PlayerMovement.GetOrbitState())
                {
                    //Charge callback here
                    playerEntity.PlayerSpearThrow.OnChargePressed(attackAction);
                }
                else
                {
                    AttackBehaviour();
                }
            }
        }

        //Subed to canceled attackAction event
        void AttackReleasedCallback()
        {
            playerEntity.PlayerSpearThrow.OnChargeReleased();
        }

        /// <summary>
        /// Call to start the player attack sequence
        /// </summary>
        void AttackBehaviour()
        {
            if (Time.time >= nextAttack)
            {
                statusEffectCounter++;
                nextAttack = Time.time + attackCooldown;

                playerEntity.PlayerMovement.SetCanMoveState(false);

                playerEntity.PlayerAnimations.PlayAttackAnimation();

                CallAttackInteraction();

                StartCoroutine(AttackReset());

                isAttacking = true;
            }
            else
            {
                if (!playerEntity.PlayerMovement.GetHasControllerConnected())
                {
                    playerEntity.PlayerMovement.MouseBasedOrbitRotation(rotateToAttackDir);
                }
            }

        }

        void CallAttackInteraction()
        {
            List<Transform> hits = playerEntity.PlayerAttackFOV.GetHitsInsideFrustrum(0);
            foreach (Transform hit in hits)
            {
                IInteractable interactable = hit.GetComponent<IInteractable>();
                if (interactable != null)
                {
                    interactable.AttackInteraction(attackDamage);
                    //Stabflesh

                    //Apply status effect on third hit
                    ApplyAdvancementEffect(interactable);

                    OmvivampEffect();

                    ManagerHUB.GetManager.GameEventsHandler.OnEnemyHit();
                }
                else
                {
                    ManagerHUB.GetManager.SoundsHandler.PlayerRandomSwing();
                }
            }

            if (hits.Count <= 0) statusEffectCounter = 0;
        }

        private void ApplyAdvancementEffect(IInteractable interactable)
        {
            if (statusEffectCounter >= 3)
            {
                EffectType attackEffect = ManagerHUB.GetManager.SlotsHandler.AttackInRunAdvancements.GetCurrentAdvancementEffect();
                GameObject effect = ManagerHUB.GetManager.StatusEffectManager.GetNeededEffect(attackEffect);

                if (effect != null)
                {
                    interactable.ApplyStatusEffect(effect);
                }

                statusEffectCounter = 0;
            }
        }

        private void OmvivampEffect()
        {
            if (healthRegenActive)
            {
                float percentageToHeal = GetAttackDamage() * 0.2f;

                playerEntity.IncrementPlayerHealthBy((int)percentageToHeal);
            }
        }

        IEnumerator AttackReset()
        {
            yield return new WaitForFixedUpdate();

            while (IsExecutingAnimation() || playerEntity.PlayerAnimations.IsInTransition(1))
            {
                playerEntity.PlayerSpearThrow.SetHoldInputToZero();

                yield return null;
            }
            isAttacking = false;
            playerEntity.PlayerMovement.SetCanMoveState(true);
        }

        bool IsExecutingAnimation()
        {
            if (playerEntity.PlayerAnimations.IsAnimationRunning(1, "SpearAttack1") || playerEntity.PlayerAnimations.IsAnimationRunning(1, "SpearAttack2"))
            {
                return true;
            }

            return false;
        }

        #region UTILITIES
        public bool GetIsAttacking() => isAttacking;
        public void SetHealthRegenState(bool state) => healthRegenActive = state;

        public void SetAttackDamage(int value, bool cachePastValue = false)
        {
            if (cachePastValue)
            { attackDamageCache = attackDamage; }

            attackDamage = value;
        }

        public void SetAttackDamageToCache() => attackDamage = attackDamageCache;

        public int GetAttackDamage() => attackDamage;

        public void SetAttackCooldown(float value, bool cachePastValue = false)
        {
            if (cachePastValue) { attackCooldownCache = attackCooldown; }

            attackCooldown = value;
        }

        public float GetAttackCooldown() => attackCooldown;
        public float GetAttackCooldownCache() => attackCooldownCache;

        public void SetAttackInputActiveState(bool value)
        {
            if (value)
            {
                attackAction.Enable();
            }
            else
            {
                attackAction.Disable();
            }
        }

        private void OnDisable()
        {
            attackAction.canceled -= _ => AttackReleasedCallback();

            attackAction.Disable();
        }
        #endregion
    }
}
