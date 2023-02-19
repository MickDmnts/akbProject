using akb.Core.Managing;
using akb.Entities.Interactions;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

namespace akb.Entities.Player.SpearHandling
{
    [DefaultExecutionOrder(430)]
    public class PlayerSpearTeleporting : MonoBehaviour
    {
        [Header("Set in inspector")]
        [SerializeField] float spearTeleportationCD;
        [SerializeField] int teleportationCharges;

        [Header("Damage at teleport skill")]
        [SerializeField] int damageAtTeleportPoint = 10;
        [SerializeField] float overlapSphereRadius = 5f;
        [SerializeField] LayerMask demonsHitLayer;

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

        public float CurrentTeleportationCD => currentTeleportationCD;

        private void Start()
        {
            CacheNeededComponents();

            SetupInput();

            EntrySetup();

            canTeleport = true;

            ManagerHUB.GetManager.GameEventsHandler.onPlayerHubEntry += EntrySetup;
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

            if (spearThrowHandler.GetThrownSpearGameobject() != null)
            {
                if (spearThrowHandler.GetThrownSpearGameobject().transform.position.y <= 0)
                {
                    StartCoroutine(SearchPosition(spearThrowHandler.GetThrownSpearGameobject().transform.position, 15f));
                    return;
                }
            }

            if (teleportAction.ReadValue<float>() > 0.1f)
            {
                TeleportToSpear();
            }
        }

        IEnumerator SearchPosition(Vector3 center, float range)
        {
            int reps = 30;
            Vector3 possiblePos = Vector3.zero;

            for (int i = 0; i < reps; i++)
            {
                //Translate XY to XZ
                Vector2 randomHit = Random.insideUnitCircle;
                Vector3 xzTranslation = new Vector3(randomHit.x, 0f, randomHit.y);

                //set random point
                Vector3 randomPoint = center + xzTranslation * range;

                NavMeshHit hit;
                if (NavMesh.SamplePosition(randomPoint, out hit, 15, NavMesh.AllAreas))
                {
                    if ((hit.position - transform.position).magnitude >= range / 1.5f)
                    {
                        Debug.DrawRay(hit.position, Vector3.up, UnityEngine.Color.blue, 1.0f);
                        possiblePos = hit.position;
                    }
                }

                yield return null;
            }

            StopAllCoroutines();
            BringSpearToGround(possiblePos);
        }

        void BringSpearToGround(Vector3 possiblePos)
        {
            spearThrowHandler.GetThrownSpearGameobject().transform.position = possiblePos;
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

                if (ManagerHUB.GetManager.SlotsHandler.SpearInRunAdvancements.GetIsAdvancementActive(Core.Managing.InRunUpdates.AdvancementTypes.DamageAtTeleportPoint))
                {
                    StartCoroutine(DamageOnTeleport());
                }

                StartCoroutine(SpearTeleportationRecharge());
            }
        }

        IEnumerator DamageOnTeleport()
        {
            yield return new WaitForFixedUpdate();

            Collider[] hits = new Collider[50];
            int hitCount = Physics.OverlapSphereNonAlloc(transform.position, overlapSphereRadius, hits, demonsHitLayer.value);

            for (int i = 0; i < hitCount; i++)
            {
                if (hits[i] == null) continue;

                IInteractable interactable;

                if (hits[i].TryGetComponent<IInteractable>(out interactable))
                {
                    interactable.AttackInteraction(damageAtTeleportPoint);
                }

                yield return null;
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
                    currentTeleportationCharges++;
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

            ManagerHUB.GetManager.GameEventsHandler.OnTeleportCooldown(currentCD);
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

            ManagerHUB.GetManager.GameEventsHandler.onPlayerHubEntry -= EntrySetup;
        }
    }
}
