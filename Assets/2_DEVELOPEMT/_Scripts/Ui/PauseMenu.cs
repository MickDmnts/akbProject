using UnityEngine;
using UnityEngine.UI;

namespace AKB.Core.Managing.UI
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

        //prepei na kratane kapoies plhrofories tis opoies kai prepei na deiksoume on runtime

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
            //GameManager.S.LevelManager.TransitToPlayerHub();
            Debug.Log("Abandon run button on pause menu screen was pressed");
        }

        void SaveExit()
        {
            Debug.Log("Save & Exit button on pause menu screen was pressed");
        }
    }
}