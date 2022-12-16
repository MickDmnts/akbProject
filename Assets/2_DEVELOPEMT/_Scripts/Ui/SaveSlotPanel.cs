using System;
using UnityEngine;
using UnityEngine.UI;

namespace AKB.Core.Managing.UI
{
    public class SaveSlotPanel : MonoBehaviour
    {
        enum ButtonType
        {
            newGame,
            savedGame,
        }

        [SerializeField] ButtonType buttonType;

        [SerializeField] Button button1;
        [SerializeField] Button button2;
        [SerializeField] Button button3;
        [SerializeField] Button button4;

        private void Start()
        {
            EntrySetup();
        }

        /// <summary>
        /// Call to set up the default script behaviour.
        /// </summary>
        void EntrySetup()
        {
            button1.onClick.AddListener(delegate { Placeholder(0); });
            button2.onClick.AddListener(delegate { Placeholder(1); });
            button3.onClick.AddListener(delegate { Placeholder(2); });
            button4.onClick.AddListener(delegate { Placeholder(3); });
        }

       
        void Placeholder(int buttonIndex)
        {
            switch(buttonType)
            {
                case ButtonType.newGame:
                    GameManager.GetManager.SetFileID(buttonIndex);
                    //Erase previous data from db
                    break;

                case ButtonType.savedGame:
                    //GameManager.GetManager.SetFileID();
                    throw new NotImplementedException(); 
                    break;
            }

            //-Each save slot shows the time played
            //-Each save slot shows the total number of runs made
            //- Each save slot has a number denoting the number of permanent upgrades made
            //-Each save slot shows the number of remaining sinner’s souls
        }
    }
}