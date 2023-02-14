using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

namespace akb.Core.Managing.UI
{
    public class UISlotsButtons : MonoBehaviour
    {
        enum ButtonType
        {
            newGame,
            savedGame,
        }

        [Header("Set in inspector")]
        [SerializeField] ButtonType buttonType;

        [Header("Assign buttons")]
        [SerializeField] Button[] buttons;
        [SerializeField] TextMeshProUGUI[] buttonTimePlayed;
        [SerializeField] TextMeshProUGUI[] buttonTotalRuns;
        [SerializeField] TextMeshProUGUI[] buttonTotalUpgradesMade;
        [SerializeField] TextMeshProUGUI[] buttonSinnerSoulsInHand;

        private void Start()
        {
            EntrySetup();
        }

        /// <summary>
        /// Call to set up the default script behaviour.
        /// </summary>
        void EntrySetup()
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                int assigne = i;

                buttons[assigne].onClick.AddListener(new UnityAction(() => ButtonFileID(assigne)));
                LoadButtonInfo(assigne, i);
                _ = assigne;
            }

            if (buttonType == ButtonType.savedGame)
            {
                for (int i = 0; i < buttons.Length; i++)
                {
                    buttons[i].interactable = !(GameManager.GetManager.Database.GetLastRoom(i) == -2);
                }
            }
        }

        void LoadButtonInfo(int saveFileID, int buttonIndex)
        {
            buttonTimePlayed[buttonIndex].SetText("Time Played: File" + saveFileID);
            buttonTotalRuns[buttonIndex].SetText("Total Runs: " + GameManager.GetManager.Database.GetTotalRunsValue(saveFileID));
            buttonTotalUpgradesMade[buttonIndex].SetText("Total Advancements: File" + saveFileID);
            buttonSinnerSoulsInHand[buttonIndex].SetText("Sinner Souls: " + GameManager.GetManager.Database.GetSoulsValue(saveFileID));
        }

        void ButtonFileID(int buttonIndex)
        {
            switch (buttonType)
            {
                case ButtonType.newGame:
                    NewGameActions(buttonIndex);
                    break;

                case ButtonType.savedGame:
                    LoadGameActions(buttonIndex);
                    break;
            }
        }

        void NewGameActions(int buttonIndex)
        {
            //Erase previous data from db
            GameManager.GetManager.Database.EraseDataFromFile(buttonIndex);
            GameManager.GetManager.Database.SetLastUsedFileID(buttonIndex);

            GameManager.GetManager.SetActiveFileID(buttonIndex);

            //Notify handlers of new game
            int id = GameManager.GetManager.Database.GetLastUsedFileID();
            ManagerHUB.GetManager.GameEventsHandler.OnNewGame(id);

            //Load Hub scene
            ManagerHUB.GetManager.LevelManager.LoadNext(false);

            //Open the GamePlayScreenPanel (Health, rage etc)
            ManagerHUB.GetManager.UIManager.EnablePanel("GamePlayScreenPanel");
        }

        void LoadGameActions(int buttonIndex)
        {
            GameManager.GetManager.Database.SetLastUsedFileID(buttonIndex);
            GameManager.GetManager.SetActiveFileID(buttonIndex);
            ManagerHUB.GetManager.GameEventsHandler.OnLoadGame(buttonIndex);

            //Load Hub scene
            ManagerHUB.GetManager.LevelManager.TransitToHub();

            //Open the GamePlayScreenPanel (Health, rage etc)
            ManagerHUB.GetManager.UIManager.EnablePanel("GamePlayScreenPanel");
        }
    }
}