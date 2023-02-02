using UnityEngine;
using UnityEngine.UI;

namespace akb.Core.Managing.UI
{
    public class MainMenuScreen : MonoBehaviour
    {
        [Header("Set in inspector\n\tMain Menu Panel")]
        [SerializeField] Button continueButton;
        [SerializeField] Button newGameButton;
        [SerializeField] Button loadGamedButton;
        [SerializeField] Button exitButton;

        private void Start()
        {
            EntrySetup();
        }

        /// <summary>
        /// Call to set up the default script behaviour.
        /// </summary>
        void EntrySetup()
        {
            continueButton.onClick.AddListener(Continue);
            newGameButton.onClick.AddListener(NewGame);
            loadGamedButton.onClick.AddListener(LoadGame);
            exitButton.onClick.AddListener(Exit);
        }

        void Continue()
        {
            int lastSavedFile = GameManager.GetManager.Database.GetLastUsedFileID();

            GameManager.GetManager.Database.SetLastUsedFileID(lastSavedFile);
            GameManager.GetManager.SetActiveFileID(lastSavedFile);
            ManagerHUB.GetManager.GameEventsHandler.OnLoadGame(lastSavedFile);

            //Load Hub scene
            ManagerHUB.GetManager.LevelManager.TransitToHub();

            //Open the GamePlayScreenPanel (Health, rage etc)
            ManagerHUB.GetManager.UIManager.EnablePanel("GamePlayScreenPanel");
            //if isnt nothing to load make the button is interactable == false , else is interactable == true

            throw new System.NotImplementedException();
            //TODO: Load save file ID from coresponding db field, pass it through the same methods of the save buttons
            //to load the game info correctly.
        }

        void NewGame()
        {
            ManagerHUB.GetManager.UIManager.EnablePanel("NewGameSlots_UI_Panel");
        }

        void LoadGame()
        {
            ManagerHUB.GetManager.UIManager.EnablePanel("SaveSlot_UI_Panel");
        }

        void Exit()
        {
            Application.Quit();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }
}