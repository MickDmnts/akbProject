using System.Collections.Generic;
using UnityEngine;

using AKB.Core.Managing.LevelLoading;

namespace AKB.Core.Managing.UI
{
    /* [CLASS DOCUMENTATION]
     * 
     * [Variable specific]
     * Dynamically changed: Theses values change in runtime.
     * 
     * [Must Know]
     * 1. The uiPanels are set from the UIRender scene when it loads, not the opposite.
     */

    [DefaultExecutionOrder(50)]
    public class UI_Manager : MonoBehaviour
    {
        public PlayerUIBars playerUIBars { get; private set; }

        //Dynamically changed
        List<GameObject> uiPanels;

        bool isPaused = false;

        public bool IsPaused
        {
            get { return isPaused; }
        }

        #region AWAKE_CALLED_EXTERNALLY
        /// <summary>
        /// Call to set the uiPanels list to the passed reference.
        /// </summary>
        public void SetUIPanels(List<GameObject> panels)
        {
            uiPanels = panels;
        }

        public void AddExtraPanelsToManager(List<GameObject> panels, bool deactivateOnAdd = false)
        {
            foreach (GameObject panel in panels)
            {
                uiPanels.Add(panel);

                if (deactivateOnAdd)
                {
                    panel.SetActive(false);
                }
            }
        }
        #endregion

        private void Awake()
        {
            ManagerHUB.GetManager.SetUI_ManagerReference(this);
        }

        private void Update()
        {
            //For game pausing
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PauseGame();
            }
        }

        /// <summary>
        /// Call to activate the Options panel if the user is not in the MainMenu.
        /// Sets isPaused to true if successfully paused or false if unpaused.
        /// </summary>
        public void PauseGame()
        {
            //if (IsInInvalidScene()) return;

            if (!isPaused)
            {
                //TODO: Handle pausing
                isPaused = true;
                EnablePanel("PauseMenu_UI_Panel");
            }
            else
            {
                //TODO: Handle un-pausing
                isPaused = false;
                DisableAllPanels();
            }
        }

        /// <summary>
        /// Call to iterate through the currently active scenes, if one of them is the MainMenu then return true
        /// false otherwise.
        /// </summary>
        bool IsInInvalidScene()
        {
            if (ManagerHUB.GetManager.LevelManager.FocusedScene == GameScenes.PlayerScene
                || ManagerHUB.GetManager.LevelManager.FocusedScene == GameScenes.TutorialArena)
                return true;

            return false;
        }

        /// <summary>
        /// Call to enable the passed UI panel.
        /// </summary>
        public void EnablePanel(string panelName)
        {
            foreach (GameObject panel in uiPanels)
            {
                panel.SetActive(panelName.Equals(panel.name));
            }
        }

        /// <summary>
        /// Call to disable the passed UI panel.
        /// </summary>
        public void DisableAllPanels()
        {
            foreach (GameObject panel in uiPanels)
            {
                panel.SetActive(false);
            }
        }

        void QuitGame()
        {
            Application.Quit();
        }
    }
}