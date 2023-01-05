using UnityEngine;
using UnityEngine.UI;

namespace akb.Core.Managing.UI
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] Button closePauseMenu;
        [SerializeField] Button hellsGrimoire;
        [SerializeField] Button options;
        [SerializeField] Button abandonRun;
        [SerializeField] Button saveExit;

        private void Start()
        {
            EntrySetup();
        }

        /// <summary>
        /// Call to set up the default script behaviour.
        /// </summary>
        void EntrySetup()
        {
            closePauseMenu.onClick.AddListener(ClosePauseMenu);
            hellsGrimoire.onClick.AddListener(HellsGrimoire);
            options.onClick.AddListener(Options);
            abandonRun.onClick.AddListener(AbandonRun);
            saveExit.onClick.AddListener(SaveExit);
        }

        void ClosePauseMenu()
        {
            ManagerHUB.GetManager.UIManager.PauseGame();
        }

        void HellsGrimoire()
        {
            ManagerHUB.GetManager.UIManager.EnablePanel("HellsGrimoire_UI_Panel");
        }

        void Options()
        {
            ManagerHUB.GetManager.UIManager.EnablePanel("Options_UI_Panel");
        }

        void AbandonRun()
        {
            ManagerHUB.GetManager.UIManager.DisableAllPanels();
            ManagerHUB.GetManager.UIManager.PauseGame();
            ManagerHUB.GetManager.LevelManager.TransitToHub();
        }

        void SaveExit()
        {
            Debug.Log("Wow i saved");
            Application.Quit();
        }
    }
}