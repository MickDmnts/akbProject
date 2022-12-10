using System.Collections.Generic;
using UnityEngine;

namespace AKB.Core.Managing.LevelLoading
{
    /// <summary>
    /// This class is responsible for passing the UI scene panels to the 
    /// UI Manager present in the Game Entry Scene.
    /// </summary>
    [DefaultExecutionOrder(100)]
    public class PanelReferences : MonoBehaviour
    {
        /// <summary>
        /// The panels present in the Main Canvas
        /// </summary>
        [Header("Set in inspector")]
        [SerializeField, Tooltip("The panels present in the Main Canvas.")] List<GameObject> uiPanels;

        private void Awake()
        {
            PassPanelReferences();
        }

        /// <summary>
        /// Passes the uiPanels (inspector set) to the UI Manager in game entry scene.
        /// </summary>
        void PassPanelReferences()
        {
            ManagerHUB.GetManager.UIManager.SetUIPanels(uiPanels);
        }

        private void Start()
        {
            EnableMainMenuPanel();
        }

        /// <summary>
        /// Enables the Main menu panel
        /// </summary>
        void EnableMainMenuPanel()
        {
            ManagerHUB.GetManager.UIManager.EnablePanel("MainMenuScreen_UI_Panel");
        }
    }
}