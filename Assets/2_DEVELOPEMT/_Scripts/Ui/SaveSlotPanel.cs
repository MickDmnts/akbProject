using System;
using UnityEngine;
using UnityEngine.UI;

namespace akb.Core.Managing.UI
{
    public class SaveSlotPanel : MonoBehaviour
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
            for (int i = 0; i != buttons.Length; i++)
            {
                buttons[i].onClick.AddListener(delegate { ButtonFileID(i); });
                LoadButtonInfo(i, buttons[i]);
            }
        }

        void LoadButtonInfo(int saveFileID, Button button)
        {
            //-Each save slot shows the time played
            //-Each save slot shows the total number of runs made
            //- Each save slot has a number denoting the number of permanent upgrades made
            //-Each save slot shows the number of remaining sinner’s souls
        }

        void ButtonFileID(int buttonIndex)
        {
            Debug.Log(buttonIndex); ;

            switch (buttonType)
            {
                case ButtonType.newGame:
                    GameManager.GetManager.SetFileID(buttonIndex);
                    //Erase previous data from db
                    break;

                case ButtonType.savedGame:
                    throw new NotImplementedException();
            }
        }
    }
}