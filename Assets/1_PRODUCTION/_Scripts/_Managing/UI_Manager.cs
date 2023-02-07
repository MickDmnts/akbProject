using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

using akb.Core.Managing.LevelLoading;

namespace akb.Core.Managing.UI
{
    /// <summary>
    /// This class is responsible for managing the activation and deactivation of all in-game
    /// UI Panels.
    /// </summary>
    [DefaultExecutionOrder(-398)]
    public class UI_Manager : MonoBehaviour
    {
        /// <summary>
        ///  All UI panels of the game.
        /// </summary>
        List<GameObject> uiPanels;

        /// <summary>
        /// The games' paused state.
        /// </summary>
        bool isPaused = false;
        /// <summary>
        /// Get the games' paused state.
        /// </summary>
        public bool IsPaused => isPaused;

        public bool CanScreenShake { get; set; }
        public bool CanShowScreenTint { get; set; }
        public bool CanShowUIElements { get; set; }

        UIInputs uiInputs;
        InputAction uiBack;

        #region AWAKE_CALLED_EXTERNALLY
        /// <summary>
        /// Call to set the uiPanels list to the passed reference.
        /// </summary>
        public void SetUIPanels(List<GameObject> panels)
        {
            uiPanels = panels;
        }

        /// <summary>
        /// Pass extra panels to feed in the UI managers panel list.
        /// </summary>
        /// <param name="deactivateOnAdd">If true, the panels gets deactivated once added.</param>
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
            //Pass the UI_Manager reference to the Manager HUB for ease of access.
            ManagerHUB.GetManager.SetUI_ManagerReference(this);
            uiInputs = new UIInputs();
        }

        private void Start()
        {
            uiBack = uiInputs.UI.Cancel;
            uiBack.Enable();

            uiBack.started += _ => CheckForPauseGame();
        }

        void CheckForPauseGame()
        {
            if (IsInMainMenu())
            {
                EnablePanel("MainMenuScreen_UI_Panel");
                return;
            }

            //For game pausing
            if (!IsInInvalidScene())
            {
                PauseGame();
            }
        }

        /// <summary>
        /// Call to activate the Options panel and freeze the game.
        /// </summary>
        public void PauseGame()
        {
            if (!isPaused)
            {
                //---------------------DEBUGING--------------------------
                Time.timeScale = 0f;
                //-------------------------------------------------------

                //TODO: Handle pausing
                isPaused = true;
                EnablePanel("PauseMenu_UI_Panel");
            }
            else
            {
                //---------------------DEBUGING--------------------------
                Time.timeScale = 1f;
                //-------------------------------------------------------

                //TODO: Handle un-pausing
                isPaused = false;
                DisableAllPanels();
                EnablePanel("GamePlayScreenPanel");
            }
        }

        /// <summary>
        /// Call to check if the user can pause in the current scene.
        /// </summary>
        bool IsInInvalidScene()
        {
            if (ManagerHUB.GetManager.LevelManager.FocusedScene == GameScenes.PlayerScene
                || ManagerHUB.GetManager.LevelManager.FocusedScene == GameScenes.TutorialArena)
                return true;

            return false;
        }

        bool IsInMainMenu()
        {
            if (ManagerHUB.GetManager.LevelManager.FocusedScene == GameScenes.PlayerScene)
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

        public void EnablePanels(params string[] panelNames)
        {
            foreach (string param in panelNames)
            {
                for (int i = 0; i != uiPanels.Count; i++)
                {
                    if (uiPanels[i].name.Equals(param))
                    {
                        uiPanels[i].SetActive(true);
                    }
                }
            }
        }

        /// <summary>
        /// Call to disable all UI panels.
        /// </summary>
        public void DisableAllPanels()
        {
            foreach (GameObject panel in uiPanels)
            {
                panel.SetActive(false);
            }
        }

        /// <summary>
        /// Call to shut down the game.
        /// </summary>
        void QuitGame()
        {
            Application.Quit();
        }

        private void OnDestroy()
        {
            uiBack.started -= _ => CheckForPauseGame();
        }
    }
}