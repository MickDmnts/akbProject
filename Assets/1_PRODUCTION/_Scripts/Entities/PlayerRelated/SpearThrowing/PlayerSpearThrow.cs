using System;
using UnityEngine;
using UnityEngine.InputSystem;

using AKB.Core.Managing;
using UnityEngine.Rendering.Universal;

namespace AKB.Entities.Player.SpearHandling
{
    [DefaultExecutionOrder(440)]
    public class PlayerSpearThrow : MonoBehaviour
    {
        [Header("Set in inspector")]
        [SerializeField] GameObject playerDummySpear;
        [SerializeField] Transform spearSpawnPoint;

        [SerializeField] int spearThrowDamage = 25;
        [SerializeField] int spearRecallDamage = 25;

        [SerializeField] float maxHoldTime = 2f;
        [SerializeField] float initialSpearSpeed = 5f;
        [SerializeField] float spearThrowMinimumPressTime = 0.5f;
        [SerializeField] float triggerHoldDeadzone = 0.5f;

        [Header("Set spear graphic settings")]
        [SerializeField] Vector3 spearGraphicSize;

        #region PLAYER_INPUTS
        PlayerEntity playerEntity;

        InputAction throwAction;
        InputAction recallAction;
        #endregion

        #region PRIVATE_VALUES
        GameObject thrownSpear;
        DecalProjector spearGraphic;

        SpearHandler activeSpearHandler;

        bool hasSpear = true;

        float holdCounter = 0f;
        float holdCounterMultiplier = 1.5f;
        float holdCounterCache = 0;
        float throwActionValue;
        #endregion

        #region ENTRY_SETUP
        private void Start()
        {
            StartupSetup();
            InputSetup();
        }

        /// <summary>
        /// Call to cache PlayerEntity and needed InputActions.
        /// </summary>
        void StartupSetup()
        {
            playerEntity = transform.root.GetComponent<PlayerEntity>();
            spearGraphic = GetComponentInChildren<DecalProjector>();

            throwAction = playerEntity.PlayerInputs.Player.Throw;

            recallAction = playerEntity.PlayerInputs.Player.Recall;

            throwAction.Enable();
            recallAction.Enable();
        }

        /// <summary>
        /// Call to set up input actions for started and canceled Unity actions.
        /// </summary>
        void InputSetup()
        {
            throwAction.started += _ => StartedAction();
            recallAction.started += _ => RecallSpear();

            throwAction.canceled += _ => CancelAction();
            recallAction.canceled += _ => RecallCancelAction();
        }
        #endregion

        #region SPEAR_CONTROLLS
        void StartedAction()
        {
            if (playerEntity.PlayerDodgeRoll.GetIsDodging()) return;

            if (hasSpear)
            {
                StartOrbit();
                PlayChargeAnimation();
            }
        }

        /// <summary>
        /// <para>Subscribed to throwAction.started Unity event.</para>
        /// Call to play the spear charge animation from PlayerAnimations and set orbiting to true
        /// from PlayerMovement.
        /// </summary>
        void StartOrbit()
        {
            playerEntity.PlayerMovement.SetOrbitState(true);
        }

        void PlayChargeAnimation()
        {
            playerEntity.PlayerAnimations.SetSpearChargeState(true);
        }

        private void Update()
        {
            if (!hasSpear || playerEntity.PlayerDodgeRoll.GetIsDodging()) return;

            //Trigger - RMB input caching
            throwActionValue = throwAction.ReadValue<float>();

            if (DetermineIsHoldingState(throwActionValue))
            {
                IncreaseHoldTimer();
            }
            else
            {
                DecreaseHoldTimer();
            }
        }

        /// <summary>
        /// <para>Returns True if the value is greater than triggerHoldDeadzone
        /// , false otherwise</para>
        /// </summary>
        bool DetermineIsHoldingState(float throwActionValue)
        {
            if (throwActionValue > triggerHoldDeadzone)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Call to increase holdCounter by Time.DeltaTime
        /// </summary>
        void IncreaseHoldTimer()
        {
            if (holdCounter < maxHoldTime)
            {
                holdCounter += Time.deltaTime;

                spearGraphic.size = new Vector3(spearGraphicSize.x, spearGraphicSize.y * holdCounter, spearGraphicSize.z);
            }
        }

        /// <summary>
        /// Call to decrease holdCounter by Time.DeltaTime
        /// </summary>
        void DecreaseHoldTimer()
        {
            if (holdCounter < 0)
            {
                holdCounter = 0;
                return;
            }

            holdCounter -= Time.deltaTime;
            spearGraphic.size = new Vector3(spearGraphicSize.x, 0f, spearGraphicSize.z);
        }
        #endregion

        #region SPEAR_ACTIONS_AND_ANIMATIONS
        /// <summary>
        /// Called when the unity canceled event gets called to determine the correct actions based on player inputs.
        /// <para>If hasSpear is false, invokes StopSpearRecall() and StopOrbit(), returns after.</para>
        /// <para>If holdCounter is greater/equal to spearThrowMinimumPressTime then invokes PlayThrowSpearAnim()
        /// and sets holdCounterCache to holdCounter, and holdCounter to 0.</para>
        /// <para>else invokes PlayChargeCancelationAnim() and StopOrbit()</para>
        /// </summary>
        void CancelAction()
        {
            if (!hasSpear || playerEntity.PlayerDodgeRoll.GetIsDodging()) return;

            if (holdCounter >= spearThrowMinimumPressTime)
            {
                PlayThrowSpearAnim();

                holdCounterCache = holdCounter;
                holdCounter = 0f;
            }
            else
            {
                StopOrbit();
            }
        }

        void RecallCancelAction()
        {
            if (hasSpear)
            {
                playerEntity.PlayerAnimations.SetSpearPullAnimationState(true);
            }
            else
            {
                StopSpearRecall();
            }

            StopOrbit();
        }

        /// <summary>
        /// <para>Internally called from CancelAction().</para>
        /// <para>Call to play the throwing animation from PlayerAnimations.</para>
        /// </summary>
        void PlayThrowSpearAnim()
        {
            playerEntity.PlayerAnimations.PlayThrowAnimation();
        }

        /// <summary>
        /// <para>Internally called from CancelAction().</para>
        /// <para>Plays the spear charge reversed animation from PlayerAnimations.</para>
        /// </summary>
        void PlayChargeCancelationAnim()
        {
            playerEntity.PlayerAnimations.PlayChargeCancelAnim();
        }

        /// <summary>
        /// <para>Internally called from CancelAction().</para>
        /// <para>Sets the orbit to false from PlayerMovement.</para>
        /// <para>Sets the SetSpearChargeState() to false from PlayerAnimations</para>
        /// </summary>
        public void StopOrbit()
        {
            playerEntity.PlayerMovement.SetOrbitState(false);
            playerEntity.PlayerAnimations.SetSpearChargeState(false);
        }
        #endregion

        #region SPEAR_INSTANTIATION
        /// <summary>
        /// <para>Animation Event inside "SpearThrow"</para>
        /// <para>Call to instantiate a spear prefab and set up its internal speeds.</para>
        /// </summary>
        public void SpawnSpear()
        {
            thrownSpear = GameManager.S.SpearPool.GetPooledSpear();

            thrownSpear.transform.position = spearSpawnPoint.position;
            thrownSpear.transform.rotation = spearSpawnPoint.rotation;
            thrownSpear.SetActive(true);

            if (!thrownSpear.Equals(null))
            {
                activeSpearHandler = thrownSpear.GetComponent<SpearHandler>();
            }
            else
                throw new NullReferenceException("Thrown spear obj in null");


            if (!activeSpearHandler.Equals(null))
            {
                activeSpearHandler.StartSpearThrowSimulation(holdCounterCache * holdCounterMultiplier * initialSpearSpeed, 1f, this);

                SetPlayerSpearActiveState(false);

                hasSpear = false;
            }
            else
                throw new NullReferenceException("Thrown spear handler in null");
        }

        /// <summary>
        /// Call to set the dummy spear the player model holds to the passed active state.
        /// </summary>
        /// <param name="state"></param>
        void SetPlayerSpearActiveState(bool state)
        {
            if (playerDummySpear != null)
                playerDummySpear.SetActive(state);
        }
        #endregion

        #region OFF_HAND_SPEAR_MANAGEMENT
        /// <summary>
        /// Called from the performed unity event
        /// <para>Early returns if hasSpear is true.</para>
        /// <para>Invokes StartOrbit()</para>
        /// <para>Calls SetSpearPullAnimationState to true from PlayerAnimations</para>
        /// <para>Calls the StartSpearRetraction(...) from the thrownSpearHandler.</para>
        /// </summary>
        /// <exception cref="NullReferenceException">Throw error if the thrownSpearHandler is null</exception>
        public void RecallSpear()
        {
            if (hasSpear || playerEntity.PlayerDodgeRoll.GetIsDodging()) return;

            StartOrbit();
            playerEntity.PlayerAttack.SetInputsActiveState(false);

            playerEntity.PlayerAnimations.SetSpearPullAnimationState(false);

            if (activeSpearHandler != null)
                activeSpearHandler.StartSpearRetraction(spearSpawnPoint);
            else
                throw new NullReferenceException("Throw spear handler is null");
        }

        /// <summary>
        /// Sets SetSpearPullAnimationState() to true from PlayerAnimations.
        /// <para>Calls StopSpearRetraction() from thrownSpearHandler</para>
        /// </summary>
        /// <exception cref="NullReferenceException">Throw error if the thrownSpearHandler is null</exception>
        public void StopSpearRecall()
        {
            playerEntity.PlayerAnimations.SetSpearPullAnimationState(true);

            if (activeSpearHandler != null)
                activeSpearHandler.StopSpearRetraction();
            else
                throw new NullReferenceException("Throw spear handler is null");
        }
        #endregion

        /// <summary>
        /// Call to get the thrown spear gameObject.
        /// </summary>
        public GameObject GetThrownSpearGameobject()
        {
            return thrownSpear;
        }

        /// <summary>
        /// Call to get the position of the thrown spear in world transform.
        /// </summary>
        /// <returns></returns>
        public Vector3 GetThrownSpearPosition()
        {
            return thrownSpear.transform.position;
        }

        /// <summary>
        /// Call to enable the player spear throwing ability.
        /// </summary>
        public void EnableSpearThrowing()
        {
            hasSpear = true;

            playerEntity.PlayerAttack.SetInputsActiveState(true);

            SetPlayerSpearActiveState(true);

            NullifySpearReferences();
        }

        /// <summary>
        /// Call to set thrownSpearHandler, thrownSpear to null.
        /// <para>Sets the thrownSpear gameObject active state to false.</para>
        /// </summary>
        void NullifySpearReferences()
        {
            GameManager.S.SpearPool.CacheSpear(thrownSpear.gameObject);

            activeSpearHandler = null;
            thrownSpear = null;
        }

        public bool GetHasSpear() => hasSpear;
        public void SetHoldInputToZero()
        {
            holdCounter = 0f;
            throwActionValue = 0f;
        }

        public int GetSpearRecallDamage() => spearRecallDamage;
        public void SetSpearRecallDamage(int value) => spearRecallDamage = value;

        public int GetSpearThrowDamage() => spearThrowDamage;
        public void SetSpearThrowDamage(int value) => spearThrowDamage = value;

        public void SetHoldCounterMultiplier(float value) => holdCounterMultiplier = value;
        public float GetHoldCounterMultiplier() => holdCounterMultiplier;

        private void OnDisable()
        {
            throwAction.Disable();
        }

        private void OnDestroy()
        {
            throwAction.started -= _ => StartedAction();
            throwAction.performed -= _ => RecallSpear();
            throwAction.canceled -= _ => CancelAction();
        }
    }
}
