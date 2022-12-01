using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AKB.Entities.Player.SpearHandling
{
    [DefaultExecutionOrder(430)]
    public class PlayerSpearTeleporting : MonoBehaviour
    {
        [Header("Set in inspector")]
        [SerializeField] float spearTeleportationCD;
        [SerializeField] int teleportationCharges;

        PlayerSpearThrow spearThrowHandler;

        #region PLAYER_INPUTS
        PlayerEntity playerEntity;
        InputAction teleportAction;
        #endregion

        bool hasTeleported;
        bool canTeleport;
        float currentTeleportationCD;

        int currentTeleportationCharges;
        int teleportationChargesCache;

        private void Start()
        {
            CacheNeededComponents();

            SetupInput();

            EntrySetup();

            canTeleport = true;
        }

        void CacheNeededComponents()
        {
            playerEntity = GetComponentInParent<PlayerEntity>();

            spearThrowHandler = transform.root.GetComponentInChildren<PlayerSpearThrow>();
        }

        void SetupInput()
        {
            teleportAction = playerEntity.PlayerInputs.Player.TeleportToSpear;
            teleportAction.Enable();
        }

        void EntrySetup()
        {
            //Teleportation cooldown.
            currentTeleportationCD = spearTeleportationCD;

            //Teleportation charges.
            currentTeleportationCharges = teleportationCharges;
            teleportationChargesCache = teleportationCharges;
        }

        private void Update()
        {
            if (currentTeleportationCharges <= 0) return;

            if (teleportAction.ReadValue<float>() > 0.1f)
            {
                TeleportToSpear();
            }
        }

        void TeleportToSpear()
        {
            if (spearThrowHandler.GetThrownSpearGameobject() && canTeleport)
            {
                //Play tp animation
                playerEntity.PlayerAnimations.PlayFallingAfterTPAnim();

                //Notify the player mover to teleport the character controller to the spear position.
                playerEntity.PlayerMovement.TeleportEntity(spearThrowHandler.GetThrownSpearPosition());

                //Simulate the spear pickup
                playerEntity.PlayerSpearThrow.EnableSpearThrowing();

                currentTeleportationCharges--;

                hasTeleported = true;
                canTeleport = false;

                StartCoroutine(SpearTeleportationRecharge());
            }
            else
            {
                Debug.Log("No spear to teleport to");
            }
        }

        IEnumerator SpearTeleportationRecharge()
        {
            while (hasTeleported)
            {
                DecreaseTeleportationCooldown(ref currentTeleportationCD);

                canTeleport = CheckHasTeleportedState(currentTeleportationCD);
                if (canTeleport)
                {
                    currentTeleportationCD = spearTeleportationCD;
                    hasTeleported = false;
                }
                yield return null;
            }

            StopCoroutine(SpearTeleportationRecharge());
        }

        void DecreaseTeleportationCooldown(ref float currentCD)
        {
            if (currentCD <= 0f) currentCD = 0;

            currentCD -= Time.deltaTime;
        }

        bool CheckHasTeleportedState(float currentTPCooldown)
        {
            if (currentTPCooldown > 0f)
            {
                return false;
            }

            return true;
        }

        public void ResetTeleportationCharges()
        {
            currentTeleportationCharges = teleportationChargesCache;
        }

        public void IncreaseTeleportChargesBy(int value)
        {
            teleportationCharges += value;

            ResetTeleportationCharges();
        }

        private void OnDestroy()
        {
            StopAllCoroutines();

            teleportAction.Disable();
        }
    }
}