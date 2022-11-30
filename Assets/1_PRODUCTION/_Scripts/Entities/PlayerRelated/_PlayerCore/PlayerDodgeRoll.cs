using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AKB.Entities.Player
{
    [DefaultExecutionOrder(450)]
    public class PlayerDodgeRoll : MonoBehaviour
    {
        [Header("Set in inspector")]
        [SerializeField] int nonAttackableLayerIndex = 31;
        [SerializeField] int startingDodgeCount = 1;
        [SerializeField] int maxStartingDodgeCount = 1;
        [SerializeField] float dodgeCooldown = 0.8f;
        [SerializeField] float pushForce = 50f;
        [SerializeField] float movementThresholdToDodge = 1f;

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

            while (playerEntity.PlayerAnimations.IsInTransition(0)
                || playerEntity.PlayerAnimations.IsAnimationRunning(0, "DodgeRoll"))
            {
                yield return null;
            }

            gameObject.layer = 6;
            playerEntity.PlayerMovement.SetCanMoveState(true);
            playerEntity.Rigidbody.velocity = Vector3.zero;

            isDodging = false;

            ResetDodgesCount();
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
    }
}