using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace akb.Core.Managing.UI
{
    public class MonstersButtons : MonoBehaviour
    {
        [SerializeField] Image monsterSpriteTemplate;
        [SerializeField] TextMeshProUGUI monsterDescriptionTempalte;

        [SerializeField] List<Sprite> monsterSprite;
        [SerializeField] List<string> monsterDescription;

        List<Button> monsterButtons = new List<Button>();

        //Monsters Buttons
        [SerializeField] Button basicDemonButton;
        [SerializeField] Button fireBasicDemonButton;
        [SerializeField] Button bigDemonButton;
        [SerializeField] Button bigChargersButton;
        [SerializeField] Button rangedDemonsButton;
        [SerializeField] Button electroDemonsButton;
        [SerializeField] Button statusDemonsButton;
        [SerializeField] Button charmDemonsButton;
        [SerializeField] Button confuseDemonsButton;
        [SerializeField] Button beelzebubButton;
        [SerializeField] Button astarothButton;

        private void Start()
        {
            AddButtonsToList();


            EntrySetup();
        }

        /// <summary>
        /// Call to set up the default script behaviour.
        /// </summary>
        void EntrySetup()
        {
            //Monsters Buttons
            basicDemonButton.onClick.AddListener(delegate { Setter(0); });
            fireBasicDemonButton.onClick.AddListener(delegate { Setter(1); });
            bigDemonButton.onClick.AddListener(delegate { Setter(2); });
            bigChargersButton.onClick.AddListener(delegate { Setter(3); });
            rangedDemonsButton.onClick.AddListener(delegate { Setter(4); });
            electroDemonsButton.onClick.AddListener(delegate { Setter(5); });
            statusDemonsButton.onClick.AddListener(delegate { Setter(6); });
            charmDemonsButton.onClick.AddListener(delegate { Setter(7); });
            confuseDemonsButton.onClick.AddListener(delegate { Setter(8); });
            beelzebubButton.onClick.AddListener(delegate { Setter(9); });
            astarothButton.onClick.AddListener(delegate { Setter(10); });
        }

        void AddButtonsToList()
        {
            monsterButtons.Add(basicDemonButton);
            monsterButtons.Add(fireBasicDemonButton);
            monsterButtons.Add(bigDemonButton);
            monsterButtons.Add(bigDemonButton);
            monsterButtons.Add(rangedDemonsButton);
            monsterButtons.Add(electroDemonsButton);
            monsterButtons.Add(statusDemonsButton);
            monsterButtons.Add(charmDemonsButton);
            monsterButtons.Add(confuseDemonsButton);
            monsterButtons.Add(beelzebubButton);
            monsterButtons.Add(astarothButton);
        }

        void Setter(int buttonIndex)
        {
            monsterSpriteTemplate.sprite = monsterSprite[buttonIndex];
            monsterDescriptionTempalte.text = monsterDescription[buttonIndex];
        }
    }
}