using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

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
                LoadButtonInfo(assigne, buttons[assigne]);
                _ = assigne;
            }
        }

        void LoadButtonInfo(int saveFileID, Button button)
        {
            //-Each save slot shows the time played
            //-Each save slot shows the total number of runs made
            //-Each save slot has a number denoting the number of permanent upgrades made
            //-Each save slot shows the number of remaining sinner’s souls
        }

        void ButtonFileID(int buttonIndex)
        {
            Debug.Log(buttonIndex); ;

            switch (buttonType)
            {
                case ButtonType.newGame:
                    GameManager.GetManager.SetActiveFileID(buttonIndex);
                    //Erase previous data from db
                    break;

                case ButtonType.savedGame:
                    throw new NotImplementedException();
            }
        }
    }
}