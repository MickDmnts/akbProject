using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using akb.Core.Managing;
using akb.Core.Managing.LevelLoading;

namespace akb.Entities.Player
{
    [DefaultExecutionOrder(420)]
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Set in inspector")]
        [SerializeField] float moveSpeed = 5;
        [SerializeField, Range(0f, 1f)] float rotateToMouseTime = 0.5f;
        [SerializeField, Range(0f, 1f)] float rotateToDirection = 0.5f;

        CinemachineVirtualCamera playerCamera;

        PlayerEntity playerEntity;
        bool orbitOnly = false;
        bool canMove = false;
        bool hasControllerConnected;
        bool controlsInverted = false;

        float moveSpeedCache;

        #region MOVEMENT_SPECIFICS
        InputAction moveAction;
        InputAction mouseInput;

        Vector2 movementInput = Vector2.zero;
        #endregion

        #region STARTUP_SETUP
        private void Start()
        {
            CacheNeededComponents();
            CacheControls();

            //Enable the controls
            moveAction.Enable();
            mouseInput.Enable();

            canMove = true;

            ManagerHUB.GetManager.GameEventsHandler.onSceneChanged += ResetOnHub;
        }

        /// <summary>
        /// Call to cache needed script components.
        /// </summary>
        void CacheNeededComponents()
        {
            playerEntity = GetComponent<PlayerEntity>();
            playerCamera = FindObjectOfType<CinemachineVirtualCamera>();
        }

        /// <summary>
        /// Call to cache needed controls.
        /// </summary>
        void CacheControls()
        {
            moveAction = playerEntity.PlayerInputs.Player.Move;
            mouseInput = playerEntity.PlayerInputs.Player.Look;

            moveSpeedCache = moveSpeed;
        }

        void ResetOnHub(GameScenes scene)
        {
            if (scene != GameScenes.PlayerHUB) { return; }

            moveSpeed = moveSpeedCache;
        }
        #endregion

        #region PLAYER_MOVEMENT_INPUTS
        private void Update()
        {
            hasControllerConnected = Gamepad.current == null ? false : true;

            if (!canMove)
            {
                movementInput = Vector2.zero;
                return;
            }

            //Input caching
            movementInput = controlsInverted ? -moveAction.ReadValue<Vector2>().normalized
                : moveAction.ReadValue<Vector2>().normalized;

            //Update the animator blend tree speed.
            playerEntity.PlayerAnimations.SetRunningBlendValue(movementInput.magnitude);

            //Translate player position based on above input.
            MovePlayer();

            //Rotate the player to face the above input.
            MovementBasedOrbitRotation(rotateToDirection);

            //This disables mouse rotation - maybe add a "Use Gamepad" in settings config.
            if (orbitOnly && !hasControllerConnected)
            {
                MouseBasedOrbitRotation(rotateToMouseTime);
            }
        }

        /// <summary>
        /// Call to move the player game object towards the movement input from the user.
        /// </summary>
        void MovePlayer()
        {
            if (orbitOnly || !canMove) return;

            Vector3 v3Move = XYToXZ(movementInput);
            v3Move = WorldToCameraRelative(v3Move, playerCamera);

            transform.position += v3Move * Time.deltaTime * moveSpeed;
        }

        /// <summary>
        /// Call to rotate the player based on movement inputs.
        /// </summary>
        void MovementBasedOrbitRotation(float rotationTime)
        {
            if (!canMove) return;

            Vector3 rotationDir = XYToXZ(movementInput);

            if (rotationDir == Vector3.zero) return;

            rotationDir = WorldToCameraRelative(rotationDir, playerCamera);

            transform.forward = Vector3.Lerp(transform.forward, rotationDir, rotationTime);
        }

        /// <summary>
        /// Call to rotate the player to face the mouse position on the screen.
        /// </summary>
        public void MouseBasedOrbitRotation(float rotationTime)
        {
            Vector3 mouse2DPos = mouseInput.ReadValue<Vector2>();
            mouse2DPos.z = Camera.main.farClipPlane;

            RaycastHit hitInfo;
            Ray mouseRay = Camera.main.ScreenPointToRay(mouse2DPos);
            if (Physics.Raycast(mouseRay, out hitInfo, 1000))
            { /*Nop...*/ }

            Vector3 direction = transform.position - hitInfo.point;
            direction.y = transform.position.y;

            float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + 180f;

            Quaternion newRot = Quaternion.Euler(0f, angle, 0f);

            transform.rotation = Quaternion.Lerp(transform.rotation, newRot, rotationTime);
        }
        #endregion

        #region UTILITIES
        /// <summary>
        /// Call to translate a vector from XY coordinates to XZ coordinates
        /// </summary>
        Vector3 XYToXZ(Vector2 vectorToTranslate)
        {
            return new Vector3(vectorToTranslate.x, 0f, vectorToTranslate.y);
        }

        /// <summary>
        /// Call to translate a Vector3 coordinates to be relative to passed camera.
        /// </summary>
        Vector3 WorldToCameraRelative(Vector3 vectorToTranslate, CinemachineVirtualCamera cameraRef)
        {
            vectorToTranslate = cameraRef.transform.TransformDirection(vectorToTranslate);
            vectorToTranslate.y = 0f;

            return new Vector3(vectorToTranslate.x, vectorToTranslate.y, vectorToTranslate.z);
        }

        /// <summary>
        /// Call to set the orbiting state of the player to the passed value.
        /// <para>Sets moveDirection to Vector3 zero.</para>
        /// </summary>
        public void SetOrbitState(bool state)
        {
            orbitOnly = state;
        }

        public bool GetOrbitState() => orbitOnly;

        public void SetCanMoveState(bool state)
        {
            canMove = state;
        }

        public void SetCurrentMoveSpeed(float value, bool cachePastSpeed = false)
        {
            if (cachePastSpeed) { moveSpeedCache = moveSpeed; }

            moveSpeed = value;
        }

        public float GetMoveSpeed() => moveSpeed;

        public float GetMoveSpeedCache() => moveSpeedCache;

        public void TeleportEntity(Vector3 cords)
        {
            transform.position = cords;
        }

        public Vector3 GetCurrentMoveInput()
        {
            return XYToXZ(movementInput);
        }

        public bool GetHasControllerConnected() => hasControllerConnected;

        public void SetMovementInputActiveState(bool state)
        {
            if (state)
            {
                moveAction.Enable();
                mouseInput.Enable();
            }
            else
            {
                moveAction.Disable();
                mouseInput.Disable();
            }
        }

        public bool IsInputInverted()
        {
            return controlsInverted;
        }

        public void SetIsInputInverted(bool value)
        {
            controlsInverted = value;
        }

        #endregion

        private void OnDisable()
        {
            moveAction.Disable();
            mouseInput.Disable();
        }

        private void OnDestroy()
        {
            ManagerHUB.GetManager.GameEventsHandler.onSceneChanged -= ResetOnHub;
        }
    }
}