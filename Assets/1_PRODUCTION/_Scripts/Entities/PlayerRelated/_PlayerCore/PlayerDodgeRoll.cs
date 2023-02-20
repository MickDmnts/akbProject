using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

using akb.Core.Managing;
using akb.Entities.Interactions;

using UnityEngine.AI;

namespace akb.Entities.Player
{
    using akb.Core.Managing.InRunUpdates;
    [DefaultExecutionOrder(450)]
    public class PlayerDodgeRoll : MonoBehaviour
    {
        [Header("Set in inspector")]
        [SerializeField] int playerLayer = 6;
        [SerializeField] int nonAttackableLayerIndex = 31;
        [SerializeField] int startingDodgeCount = 1;
        [SerializeField] int maxStartingDodgeCount = 1;
        [SerializeField] float dodgeCooldown = 0.8f;
        [SerializeField] float pushForce = 50f;
        [SerializeField] float movementThresholdToDodge = 1f;

        [Header("Shock on touch specifics")]
        [SerializeField] EffectType effectToApply;
        [SerializeField] LayerMask demonsHitLayer;
        [SerializeField] float effectiveRadius = 5f;

        [Header("Push on dodge specifics")]
        [SerializeField] int damageOnPush = 5;
        [SerializeField] float surroundingsPushForce = 7f;
        [SerializeField] float pushForceRadius = 5f;

        PlayerEntity playerEntity;
        InputAction dodgeAction;

        int maxDodges;
        int currentDodges;

        float nextDodge;
        bool isDodging;

        private void Start()
        {
            playerEntity = GetComponent<PlayerEntity>();

            dodgeAction = playerEntity.PlayerInputs.Player.DodgeRoll;
            dodgeAction.Enable();

            maxDodges = maxStartingDodgeCount;
            currentDodges = startingDodgeCount;
        }

        private void Update()
        {
            if (playerEntity.PlayerMovement.GetOrbitState() ||
                playerEntity.PlayerAttack.GetIsAttacking()) return;

            if (CanDodge())
            {
                if (dodgeAction.ReadValue<float>() >= 0.5f
                        && playerEntity.PlayerMovement.GetCurrentMoveInput().magnitude >= movementThresholdToDodge)
                {
                    DecreaseDodges();

                    playerEntity.PlayerMovement.SetCanMoveState(false);
                    playerEntity.PlayerAnimations.PlayDodgeRollAnimation();

                    //change layer here - nonattackable
                    gameObject.layer = nonAttackableLayerIndex;

                    playerEntity.Rigidbody.AddForce(transform.forward * pushForce, ForceMode.VelocityChange);

                    nextDodge = Time.time + dodgeCooldown;

                    StartCoroutine(DodgeReset());

                    isDodging = true;

                }
            }
        }

        IEnumerator DodgeReset()
        {
            yield return new WaitForFixedUpdate();

            if (ManagerHUB.GetManager.SlotsHandler.DodgeInRunAdvancements.GetIsAdvancementActive(AdvancementTypes.PushAway))
            {
                PushSurroundings();
            }

            while (playerEntity.PlayerAnimations.IsInTransition(0)
                || playerEntity.PlayerAnimations.IsAnimationRunning(0, "DodgeRoll"))
            {
                if (ManagerHUB.GetManager.SlotsHandler.DodgeInRunAdvancements.GetIsAdvancementActive(AdvancementTypes.ShockOnTouch))
                {
                    ShockSurroundings();
                }
                yield return null;
            }

            gameObject.layer = playerLayer;
            playerEntity.PlayerMovement.SetCanMoveState(true);
            playerEntity.Rigidbody.velocity = Vector3.zero;

            isDodging = false;

            ResetDodgesCount();
            if (ManagerHUB.GetManager.SlotsHandler.DodgeInRunAdvancements.GetIsAdvancementActive(AdvancementTypes.MovementSpeed))
            {
                StartCoroutine(IncreaseMoveSpeed());
            }
        }

        IEnumerator IncreaseMoveSpeed()
        {
            playerEntity.PlayerMovement.SetCurrentMoveSpeed(playerEntity.PlayerMovement.GetMoveSpeed() * 1.1f, true);
            yield return new WaitForSeconds(2f);

            playerEntity.PlayerMovement.SetCurrentMoveSpeed(playerEntity.PlayerMovement.GetMoveSpeedCache());

            StopCoroutine(IncreaseMoveSpeed());
        }

        void ShockSurroundings()
        {
            Collider[] hits = new Collider[50];
            int hitCount = Physics.OverlapSphereNonAlloc(transform.position, effectiveRadius, hits, demonsHitLayer.value);

            for (int i = 0; i < hitCount; i++)
            {
                if (hits[i] == null) continue;

                IInteractable interactable;

                if (hits[i].TryGetComponent<IInteractable>(out interactable))
                {
                    interactable.ApplyStatusEffect(ManagerHUB.GetManager.StatusEffectManager.GetNeededEffect(effectToApply));
                }
            }
        }

        void PushSurroundings()
        {
            Collider[] hits = new Collider[50];
            int hitCount = Physics.OverlapSphereNonAlloc(transform.position, pushForceRadius, hits, demonsHitLayer.value);

            for (int i = 0; i < hitCount; i++)
            {
                if (hits[i] == null) continue;

                NavMeshAgent hitAgent = null;
                IInteractable interaction;

                if (hits[i].TryGetComponent<NavMeshAgent>(out hitAgent) && hits[i].TryGetComponent<IInteractable>(out interaction))
                {
                    hitAgent.velocity = (hits[i].transform.position - transform.position).normalized * surroundingsPushForce;
                    interaction.AttackInteraction(damageOnPush);
                }
            }
        }

        bool CanDodge()
        {
            if (currentDodges > startingDodgeCount)
            {
                return true;
            }

            return Time.time >= nextDodge;
        }

        void DecreaseDodges()
        {
            currentDodges--;

            if (currentDodges < 0)
            {
                currentDodges = 0;
            }
        }

        void ResetDodgesCount()
        {
            currentDodges = currentDodges == 0 ? maxDodges : currentDodges;
        }

        public bool GetIsDodging() => isDodging;
        public void SetMaxDodges(int value) => maxDodges = value;

        public void SetDodgeInputActiveState(bool state)
        {
            if (state == true)
            {
                dodgeAction.Enable();
            }
            else
            {
                dodgeAction.Disable();
            }
        }
    }
}