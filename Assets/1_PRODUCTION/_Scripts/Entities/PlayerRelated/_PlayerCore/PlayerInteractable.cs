using UnityEngine;

using akb.Entities.Interactions;
using akb.Core.Managing;
using akb.Core.Managing.LevelLoading;
using System.Collections;

namespace akb.Entities.Player.Interactions
{
    public class PlayerInteractable : MonoBehaviour,
        IInteractable, IShockable, IStunnable, IConfusable,
        ICharmable
    {
        [Header("Set in inspector")]
        [SerializeField, Range(0f, 1f)] float healthRegenOnRoomEntry = 0.1f;

        PlayerEntity playerEntity;

        float mitigateDamageAfter;

        bool currentlyShocked = false;
        bool currentlyStunned = false;
        bool currentlyCharmed = false;

        Vector3 inflictedFromDirection;

        bool ignoreHit = false;

        private void Start()
        {
            ManagerHUB.GetManager.GameEventsHandler.onSceneChanged += OnSceneEntryPassive;

            playerEntity = GetComponent<PlayerEntity>();
        }

        void OnSceneEntryPassive(GameScenes activeScene)
        {
            if (ManagerHUB.GetManager.SlotsHandler.PassiveInRunAdvancements.GetIsAdvancementActive(Core.Managing.InRunUpdates.AdvancementTypes.RegenHealthOnRoomEntry))
            {
                RegenHealthOnRoomEntry();
            }

            if (ManagerHUB.GetManager.SlotsHandler.PassiveInRunAdvancements.GetIsAdvancementActive(Core.Managing.InRunUpdates.AdvancementTypes.IgnoreFirstHit))
            {
                ignoreHit = true;
            }
        }

        private void RegenHealthOnRoomEntry()
        {
            float percentage = playerEntity.GetPlayerMaxHealth() * healthRegenOnRoomEntry;

            playerEntity.SetPlayerHealth(playerEntity.GetPlayerHealth() + percentage);
        }

        public void AttackInteraction(float damageValue)
        {
            if (playerEntity.IsDead || ignoreHit) return;

            ignoreHit = false;

            Debug.Log($"Attacked player for {damageValue}");

            if (playerEntity.GetPlayerHealth() < mitigateDamageAfter * playerEntity.GetPlayerMaxHealth())
            {
                damageValue /= 2;
            }

            SubtractHealth(damageValue);

            playerEntity.IsDead = CheckIfDead(playerEntity.GetPlayerHealth());

            if (playerEntity.IsDead)
            {
                GameObject deathGfx = Instantiate(playerEntity.DeathGFX);
                deathGfx.transform.position = transform.position;
                deathGfx.transform.SetParent(ManagerHUB.GetManager.RoomSelector.CurrentRoomGO.transform);

                ManagerHUB.GetManager.LevelManager.TransitToHub();
            }
        }

        void SubtractHealth(float subtractionValue)
        {
            playerEntity.SetPlayerHealth(playerEntity.GetPlayerHealth() - subtractionValue);

            //Notify coin multiplyer reset
            ManagerHUB.GetManager.GameEventsHandler.OnPlayerHit();
        }

        bool CheckIfDead(float healthValue)
        {
            if (healthValue <= 0)
            {
                return true;
            }

            return false;
        }

        public void ApplyStatusEffect(GameObject effect)
        {
            GameObject temp = GameObject.Instantiate(effect, playerEntity.transform);
        }

        #region SHOCKED_INTERACTION
        public bool IsGettingShocked() => currentlyShocked;

        public void InflictShockInteraction()
        {
            if (currentlyShocked) return;

            currentlyShocked = true;

            #region MOVEMENT_EFFECT
            float currentMoveSpeed = playerEntity.PlayerMovement.GetMoveSpeed();

            playerEntity.PlayerMovement.SetCurrentMoveSpeed(currentMoveSpeed / 2, true);
            #endregion

            #region ATTACK_EFFECT
            float currentAttackSpeed = playerEntity.PlayerAttack.GetAttackCooldown();

            playerEntity.PlayerAttack.SetAttackCooldown(currentAttackSpeed * 2, true);
            #endregion
        }

        public void RemoveShockInteraction()
        {
            currentlyShocked = false;

            #region MOVEMENT_EFFECT
            float cachedSpeed = playerEntity.PlayerMovement.GetMoveSpeedCache();

            playerEntity.PlayerMovement.SetCurrentMoveSpeed(cachedSpeed, false);
            #endregion

            #region ATTACK_EFFECT
            float cachedAttack = playerEntity.PlayerAttack.GetAttackCooldownCache();

            playerEntity.PlayerAttack.SetAttackCooldown(cachedAttack, false);
            #endregion
        }
        #endregion

        #region STUNNED_INTERACTION
        public bool IsAlreadyStunned() => currentlyStunned;

        public void InflictStunnedInteraction()
        {
            if (currentlyStunned) return;
            currentlyStunned = true;

            #region MOVEMENT
            playerEntity.PlayerMovement.SetMovementInputActiveState(false);
            #endregion

            #region ATTACK
            playerEntity.PlayerAttack.SetAttackInputActiveState(false);
            #endregion
        }

        public void RemoveStunnedInteraction()
        {
            currentlyStunned = false;

            #region MOVEMENT
            playerEntity.PlayerMovement.SetMovementInputActiveState(true);
            #endregion

            #region ATTACK
            playerEntity.PlayerAttack.SetAttackInputActiveState(true);
            #endregion
        }
        #endregion

        #region CONFUSED_INTERACTION
        public bool IsConfused() => playerEntity.PlayerMovement.IsInputInverted();

        public void ApplyConfusedInteraction()
        {
            if (IsConfused()) return;

            playerEntity.PlayerMovement.SetIsInputInverted(true);
        }

        public void RemoveConfusedInteraction()
        {
            playerEntity.PlayerMovement.SetIsInputInverted(false);
        }
        #endregion

        #region CHARMED_INTERACTION
        public bool IsAlreadyCharmed()
        {
            return currentlyCharmed;
        }

        public void SetInflictedFromDirection(Vector3 transform)
        {
            if (IsAlreadyCharmed()) return;

            inflictedFromDirection = transform;
        }

        public Vector3 GetInflictedFromDirection() => inflictedFromDirection;

        public void DeactivateEntityControls()
        {
            currentlyCharmed = true;

            playerEntity.PlayerMovement.SetMovementInputActiveState(false);
        }

        public void ActivateEntityControls()
        {
            currentlyCharmed = false;

            playerEntity.PlayerMovement.SetMovementInputActiveState(true);
        }
        #endregion

        private void OnDisable()
        {
            ManagerHUB.GetManager.GameEventsHandler.onSceneChanged -= OnSceneEntryPassive;
        }
    }
}