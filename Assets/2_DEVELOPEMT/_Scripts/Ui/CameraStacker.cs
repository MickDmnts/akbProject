using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace AKB.Core.Managing.UI
{
    public class CameraStacker : MonoBehaviour
    {
        [Header("Set in inspector")]
        [SerializeField] Camera playerCamera;
        [SerializeField] Camera playerHudCamera;

        [Header("Set dynamically")]
        [SerializeField] Camera uiOverlayRenderer;

        private void Awake()
        {
            uiOverlayRenderer = GameObject.FindGameObjectWithTag("UI_Camera").GetComponent<Camera>();

            SetCameraStack();
        }

        void SetCameraStack()
        {
            playerCamera.GetUniversalAdditionalCameraData().cameraStack.Add(playerHudCamera);
            playerCamera.GetUniversalAdditionalCameraData().cameraStack.Add(uiOverlayRenderer);
        }
    }
}