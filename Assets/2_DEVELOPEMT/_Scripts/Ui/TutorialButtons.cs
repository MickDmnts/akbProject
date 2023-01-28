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
        [SerializeField] Button meleeAttackButton;
        [SerializeField] Button dodgeRollButton;
        [SerializeField] Button throwButton;
        [SerializeField] Button teleportationButton;
        [SerializeField] Button recallButton;

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
            meleeAttackButton.onClick.AddListener(delegate { Setter(0); });
            dodgeRollButton.onClick.AddListener(delegate { Setter(1); });
            throwButton.onClick.AddListener(delegate { Setter(2); });
            teleportationButton.onClick.AddListener(delegate { Setter(3); });
            recallButton.onClick.AddListener(delegate { Setter(4); });
        }

        void AddButtonsToList()
        {
            tutorialButtons.Add(meleeAttackButton);
            tutorialButtons.Add(dodgeRollButton);
            tutorialButtons.Add(throwButton);
            tutorialButtons.Add(teleportationButton);
            tutorialButtons.Add(recallButton);
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