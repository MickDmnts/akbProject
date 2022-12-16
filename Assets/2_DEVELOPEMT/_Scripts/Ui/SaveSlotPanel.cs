using System;
using UnityEngine;
using UnityEngine.UI;

namespace AKB.Core.Managing.UI
{
    public class SaveSlotPanel : MonoBehaviour
    {
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

        //theloume na kanei new game
        //theloume na krataei ta saves mas
        //ta save mas prepei na kratane kapoies plhrofories tis opoies kai prepei na deiksoume
        //theloume na kanei override to prohgoumo run an exei pathsei new game se ena hdh yparxon
        void Placeholder(int buttonIndex)
        {
            //gia twra,  apla na se phgainei se mia pista
            //GameManager.S.LevelManager.TransitToPlayerHub();
            Debug.Log("New game button on save slots screen was pressed");
            if(buttonIndex == 0)
            {

            }

            if(buttonIndex == 1)
            {

            }

            if (buttonIndex == 2)
            {

            }

            if (buttonIndex == 3)
            {

            }

            //-Each save slot shows the time played
            //-Each save slot shows the total number of runs made
            //- Each save slot has a number denoting the number of permanent upgrades made
            //-Each save slot shows the number of remaining sinner’s souls
        }
    }
}