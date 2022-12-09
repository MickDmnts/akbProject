using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace AKB.Core.Managing.UI
{
    public class CameraStacker : MonoBehaviour
    {
        [Header("Set in inspector")]

        ///<summary>The player camera from the Player Scene</summary>
        [SerializeField, Tooltip("The player camera from the Player Scene")] Camera playerCamera;

        ///<summary>The player HUD camera from the Player Scene</summary>
        [SerializeField, Tooltip("The player HUD camera from the Player Scene")] Camera playerHudCamera;

        [Header("Set dynamically")]

        ///<summary>This camera gets auto-retrieved when the Player Scene gets loaded.</summary>
        [SerializeField, Tooltip("This camera gets auto-retrieved when the Player Scene gets loaded.")] Camera uiOverlayRenderer;

        private void Start()
        {
            uiOverlayRenderer = GameObject.FindGameObjectWithTag("UI_Camera").GetComponent<Camera>();

            SetCameraStack(playerCamera, playerHudCamera, uiOverlayRenderer);
        }

        /// <summary>
        /// Sets the inspector-passed cameras as "Stack" cameras inside the Player Camera.
        /// </summary>
        /// <param name="parentCamera">The parent camera to stack other cameras below it.</param>
        /// <param name="cameras">The cameras to stack, order gets preserved.</param>
        void SetCameraStack(Camera parentCamera, params Camera[] cameras)
        {
            for (int i = 0; i < cameras.Length; i++)
            {
                parentCamera.GetUniversalAdditionalCameraData().cameraStack.Add(cameras[i]);
            }
        }
    }
}