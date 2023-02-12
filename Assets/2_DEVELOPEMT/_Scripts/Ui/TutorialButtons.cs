using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace akb.Core.Managing.UI
{
    public class TutorialButtons : MonoBehaviour
    {
        [SerializeField] Image tutorialSpriteTemplate;
        [SerializeField] TextMeshProUGUI tutorialDescriptionTempalte;

        [SerializeField] List<Sprite> tutorialSprite;
        [SerializeField] List<string> tutorialDescription;

        List<Button> tutorialButtons = new List<Button>();

        //Tutorials Buttons
        [SerializeField] Button movementButton;
        [SerializeField] Button attackButton;
        [SerializeField] Button dodgeRollButton;
        [SerializeField] Button throwButton;
        [SerializeField] Button teleportButton;
        [SerializeField] Button recallButton;
        [SerializeField] Button devilRageButton;
        [SerializeField] Button abilitiesButton;
        [SerializeField] Button battleRoomButton;
        [SerializeField] Button healRoomButton;
        [SerializeField] Button storeRoomButton;
        [SerializeField] Button hellCoinsButton;
        [SerializeField] Button sinnersSoulButton;



        private void Start()
        {
            AddButtonsToList();

            EntrySetup();

            ManagerHUB.GetManager.GameEventsHandler.onTutorialButtonPanelOpen += SetDefault;
        }

        /// <summary>
        /// Call to set up the default script behaviour.
        /// </summary>
        void EntrySetup()
        {
            //Tutorials Buttons
            movementButton.onClick.AddListener(delegate { Setter(0); });
            attackButton.onClick.AddListener(delegate { Setter(1); });
            dodgeRollButton.onClick.AddListener(delegate { Setter(2); });
            throwButton.onClick.AddListener(delegate { Setter(3); });
            teleportButton.onClick.AddListener(delegate { Setter(4); });
            recallButton.onClick.AddListener(delegate { Setter(5); });
            devilRageButton.onClick.AddListener(delegate { Setter(6); });
            abilitiesButton.onClick.AddListener(delegate { Setter(7); });
            battleRoomButton.onClick.AddListener(delegate { Setter(8); });
            healRoomButton.onClick.AddListener(delegate { Setter(9); });
            storeRoomButton.onClick.AddListener(delegate { Setter(10); });
            hellCoinsButton.onClick.AddListener(delegate { Setter(11); });
            sinnersSoulButton.onClick.AddListener(delegate { Setter(12); });
        }

        void AddButtonsToList()
        {
            tutorialButtons.Add(movementButton);
            tutorialButtons.Add(attackButton);
            tutorialButtons.Add(dodgeRollButton);
            tutorialButtons.Add(throwButton);
            tutorialButtons.Add(teleportButton);
            tutorialButtons.Add(recallButton);
            tutorialButtons.Add(devilRageButton);
            tutorialButtons.Add(abilitiesButton);
            tutorialButtons.Add(battleRoomButton);
            tutorialButtons.Add(healRoomButton);
            tutorialButtons.Add(storeRoomButton);
            tutorialButtons.Add(hellCoinsButton);
            tutorialButtons.Add(sinnersSoulButton);
        }


        void Setter(int buttonIndex)
        {
            tutorialSpriteTemplate.sprite = tutorialSprite[buttonIndex];
            tutorialDescriptionTempalte.text = tutorialDescription[buttonIndex];
        }

        void SetDefault()
        {
            tutorialSpriteTemplate.sprite = tutorialSprite[0];
            tutorialDescriptionTempalte.text = tutorialDescription[0];
        }

        private void OnDestroy()
        {
            ManagerHUB.GetManager.GameEventsHandler.onTutorialButtonPanelOpen -= SetDefault;
        }
    }
}