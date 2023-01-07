using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

using akb.Core.Database.Monsters;

namespace akb.Core.Managing.UI
{
    public class MonstersButtons : MonoBehaviour
    {
        [SerializeField] Image monsterSpriteTemplate;
        [SerializeField] TextMeshProUGUI monsterDescriptionTempalte;

        [SerializeField] Sprite lockedMonstersSprite;
        [SerializeField] string lockedMonstersDescripton;

        [SerializeField] List<Sprite> monsterSprite;
        List<string> monsterDescription = new List<string>();

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
            basicDemonButton.onClick.AddListener(delegate { Setter(MonsterIDs.BasicDemon); });
            fireBasicDemonButton.onClick.AddListener(delegate { Setter(MonsterIDs.FireDemon); });
            bigDemonButton.onClick.AddListener(delegate { Setter(MonsterIDs.BigDemon); });
            bigChargersButton.onClick.AddListener(delegate { Setter(MonsterIDs.ChargerDemon); });
            rangedDemonsButton.onClick.AddListener(delegate { Setter(MonsterIDs.RangedDemon); });
            electroDemonsButton.onClick.AddListener(delegate { Setter(MonsterIDs.ElectroDemon); });
            statusDemonsButton.onClick.AddListener(delegate { Setter(MonsterIDs.StatusDemon); });
            charmDemonsButton.onClick.AddListener(delegate { Setter(MonsterIDs.CharmDemon); });
            confuseDemonsButton.onClick.AddListener(delegate { Setter(MonsterIDs.ConfuseDemon); });
            beelzebubButton.onClick.AddListener(delegate { Setter(MonsterIDs.BossBeelzebub); });
            astarothButton.onClick.AddListener(delegate { Setter(MonsterIDs.BossAstaroth); });
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
            if (GameManager.GetManager.Database.GetIsMonsterFoundValue(GameManager.GetManager.ActiveFileID, buttonIndex) == 0)
            {
                monsterSpriteTemplate.sprite = lockedMonstersSprite;
                monsterDescriptionTempalte.text = lockedMonstersDescripton;
            }
            else
            {
                monsterSpriteTemplate.sprite = monsterSprite[buttonIndex];

                monsterDescriptionTempalte.text = GameManager.GetManager.Database.GetMonsterDescription(GameManager.GetManager.ActiveFileID, buttonIndex);
            }
        }
    }
}