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

        public dummy testing { get; private set; }

        //Dynamically changed
        List<GameObject> uiPanels;

        bool isPaused = false;

        public bool IsPaused
        {
            get { return isPaused; }
        }

        public GameObject MainMenuScreen_UI_Panel;
        public GameObject SaveSlot_UI_Panel;
        public GameObject PauseMenu_UI_Panel;
        public GameObject Options_UI_Panel;
        public GameObject HellsGrimoire_UI_Panel;
        public GameObject Attack_UI_Panel;

        public GameObject SceneFader_UI_Panel;



        #region AWAKE_CALLED_EXTERNALLY
        /// <summary>
        /// Call to set the uiPanels list to the passed reference.
        /// </summary>
        public void SetUIPanels(List<GameObject> panels)
        {
            this.uiPanels = panels;
        }
        #endregion

        private void Start()
        {
            GameManager.S.SetUI_ManagerReference(this);
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
        void PauseGame()
        {
            if (IsInInvalidScene()) return;

            if (!isPaused)
            {
                //TODO: Handle pausing
            }
            else
            {
                //TODO: Handle un-pausing
            }
        }

        /// <summary>
        /// Call to iterate through the currently active scenes, if one of them is the MainMenu then return true
        /// false otherwise.
        /// </summary>
        bool IsInInvalidScene()
        {
            if (GameManager.S.LevelManager.FocusedScene == GameScenes.MainMenu
                || GameManager.S.LevelManager.FocusedScene == GameScenes.TutorialArena)
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
        public void DisablePanel()
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

        //public void EnablePanel(string panelToBeActivated)
        //{
        //    MainMenuScreen_UI_Panel.SetActive(panelToBeActivated.Equals(MainMenuScreen_UI_Panel.name));
        //    SaveSlot_UI_Panel.SetActive(panelToBeActivated.Equals(SaveSlot_UI_Panel.name));
        //    PauseMenu_UI_Panel.SetActive(panelToBeActivated.Equals(PauseMenu_UI_Panel.name));
        //    Options_UI_Panel.SetActive(panelToBeActivated.Equals(Options_UI_Panel.name));
        //    HellsGrimoire_UI_Panel.SetActive(panelToBeActivated.Equals(HellsGrimoire_UI_Panel.name));
        //    Attack_UI_Panel.SetActive(panelToBeActivated.Equals(Attack_UI_Panel.name));

        //    SceneFader_UI_Panel.SetActive(panelToBeActivated.Equals(SceneFader_UI_Panel.name));
        //}

        //public void DisablePanel(string panelToBeActivated)
        //{
        //    MainMenuScreen_UI_Panel.SetActive(false);
        //    SaveSlot_UI_Panel.SetActive(panelToBeActivated.Equals(SaveSlot_UI_Panel.name));
        //    PauseMenu_UI_Panel.SetActive(panelToBeActivated.Equals(PauseMenu_UI_Panel.name));
        //    Options_UI_Panel.SetActive(panelToBeActivated.Equals(Options_UI_Panel.name));
        //    HellsGrimoire_UI_Panel.SetActive(panelToBeActivated.Equals(HellsGrimoire_UI_Panel.name));
        //    Attack_UI_Panel.SetActive(panelToBeActivated.Equals(Attack_UI_Panel.name));

        //    SceneFader_UI_Panel.SetActive(panelToBeActivated.Equals(SceneFader_UI_Panel.name));
        //}
    }
}