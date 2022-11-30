using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

using AKB.Entities.Interactions;
using AKB.Core.Managing;

namespace AKB.Entities.Player
{
    [DefaultExecutionOrder(460)]
    public class PlayerAttack : MonoBehaviour
    {
        [Header("Set in inspector")]
        [SerializeField] int attackDamage;
        [SerializeField] float attackCooldown = 0.2f;
        [SerializeField, Range(0f, 1f)] float rotateToAttackDir = 0.5f;

        PlayerEntity playerEntity;
        InputAction attackAction;

        bool isAttacking = false;
        bool healthRegenActive = false;

        float nextAttack;
        float attackCooldownCache;

        int attackDamageCache;

        // Start is called before the first frame update
        void Start()
        {
            playerEntity = transform.root.GetComponent<PlayerEntity>();

            //Input setup
            attackAction = playerEntity.PlayerInputs.Player.Fire;
            attackAction.Enable();

            attackCooldownCache = attackCooldown;
        }

        // Update is called once per frame
        void Update()
        {
            if (!playerEntity.PlayerSpearThrow.GetHasSpear()
                || playerEntity.PlayerDodgeRoll.GetIsDodging()) return;

            if (attackAction.ReadValue<float>() > 0.5f)
            {
                AttackBehaviour();
            }
        }

        void AttackBehaviour()
        {
            if (Time.time >= nextAttack)
            {
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

                    if (healthRegenActive)
                    {
                        float percentageToHeal = GetAttackDamage() * 0.2f;

                        playerEntity.IncrementPlayerHealthBy((int)percentageToHeal);
                    }

                    GameManager.S.GameEventsHandler.OnEnemyHit();
                }
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
                attackDamageCache = attackDamage;

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

        public void SetInputsActiveState(bool value)
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
            attackAction.Disable();
        }
        #endregion
    }
}
