using UnityEngine;

using AKB.Entities.Interactions;

namespace AKB.Entities.Player.Interactions
{
    /* CLASS DOCUMENTATION *\
     * 
     * [Variable Specifics]
     * 
     * 
     * [Class Flow]
     * 1. ....
     * 2. ....
     * 
     * [Must Know]
     * 1. ...
     */

    public class PlayerInteractable : MonoBehaviour,
        IInteractable, IShockable, IStunnable, IConfusable,
        ICharmable
    {
        PlayerEntity playerEntity;

        float mitigateDamageAfter;

        bool currentlyShocked = false;
        bool currentlyStunned = false;
        bool currentlyCharmed = false;
        Vector3 inflictedFromDirection;

        private void Start()
        {
            playerEntity = GetComponent<PlayerEntity>();
        }

        public void AttackInteraction(float damageValue)
        {
            if (playerEntity.IsDead) return;

            Debug.Log($"Attacked player for {damageValue}");

            if (playerEntity.GetPlayerHealth() < mitigateDamageAfter * playerEntity.GetPlayerMaxHealth())
            {
                damageValue /= 2;
            }

            SubtractHealth(damageValue);

            //SHOW HURT AND DO STUFF

            playerEntity.IsDead = CheckIfDead(playerEntity.GetPlayerHealth());

            if (playerEntity.IsDead)
            {
                //PLAY DEATH ANIMA AND DO TRANSITION STUFF TO HUB
            }
        }

        void SubtractHealth(float subtractionValue)
        {
            playerEntity.SetPlayerHealth(playerEntity.GetPlayerHealth() - subtractionValue);
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
            playerEntity.PlayerMovement.SetMovementInputsState(false);
            #endregion

            #region ATTACK
            playerEntity.PlayerAttack.SetInputsActiveState(false);
            #endregion
        }

        public void RemoveStunnedInteraction()
        {
            currentlyStunned = false;

            #region MOVEMENT
            playerEntity.PlayerMovement.SetMovementInputsState(true);
            #endregion

            #region ATTACK
            playerEntity.PlayerAttack.SetInputsActiveState(true);
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

            playerEntity.PlayerMovement.SetMovementInputsState(false);
        }

        public void ActivateEntityControls()
        {
            currentlyCharmed = false;

            playerEntity.PlayerMovement.SetMovementInputsState(true);
        }
        #endregion
    }
}