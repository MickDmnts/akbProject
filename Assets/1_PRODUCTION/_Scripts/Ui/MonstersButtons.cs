using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

using akb.Core.Database.Monsters;
using System;
using akb.Core.Sounds;

namespace akb.Core.Managing.UI
{
    public class MonstersButtons : MonoBehaviour
    {
        [SerializeField] Image monsterSpriteTemplate;
        [SerializeField] TextMeshProUGUI monsterDescriptionTempalte;

        [SerializeField] Sprite lockedMonstersSprite;
        [SerializeField] string lockedMonstersDescripton;

        //[SerializeField] List<Sprite> monsterSprite;
        List<string> monsterDescription = new List<string>();

        List<Button> monsterButtons = new List<Button>();

        //Monsters Buttons
        [SerializeField] Button basicDemonButton;
        [SerializeField] Button fireBasicDemonButton;
        [SerializeField] Button bigDemonButton;
        [SerializeField] Button bigChargersButton;
        [SerializeField] Button rangedDemonsButton;
        [SerializeField] Button electroDemonsButton;
        [SerializeField] Button charmDemonsButton;
        [SerializeField] Button confuseDemonsButton;
        [SerializeField] Button beelzebubButton;
        [SerializeField] Button astarothButton;

        private void Start()
        {
            AddButtonsToList();


            EntrySetup();

            ManagerHUB.GetManager.GameEventsHandler.onMonsterButtonPanelOpen += SetDefault;
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
            monsterButtons.Add(charmDemonsButton);
            monsterButtons.Add(confuseDemonsButton);
            monsterButtons.Add(beelzebubButton);
            monsterButtons.Add(astarothButton);
        }

        void Setter(int buttonIndex)
        {
            ManagerHUB.GetManager.SoundsHandler.PlayOneShot(GameAudioClip.UIClickSound);

            if (GameManager.GetManager.Database.GetIsMonsterFoundValue(GameManager.GetManager.ActiveFileID, buttonIndex) == 0)
            {
                monsterDescriptionTempalte.text = lockedMonstersDescripton;
                monsterSpriteTemplate.sprite = lockedMonstersSprite;  
            }
            else
            {
                monsterDescriptionTempalte.text = GameManager.GetManager.Database.GetMonsterDescription(GameManager.GetManager.ActiveFileID, buttonIndex);
                Image(buttonIndex);
            }
        }

        void Image(int buttonIndex)
        {
            // Texture size does not matter, since sprite will replace with with incoming size.
            string str = GameManager.GetManager.Database.GetMonsterImage(buttonIndex);

            // Convert from Base64String to a byte array
            byte[] imageBytes = Convert.FromBase64String(str);
            // Create a new Texture2D object
            Texture2D tex = new Texture2D(2, 2);
            // Load the image data into the texture
            tex.LoadImage(imageBytes);
            // Create a new Sprite object
            Sprite sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);
            //Set the sprite as the converted one
            monsterSpriteTemplate.sprite = sprite;
        }


        void SetDefault()
        {
            if (GameManager.GetManager.Database.GetIsMonsterFoundValue(GameManager.GetManager.ActiveFileID, MonsterIDs.BasicDemon) == 0)
            {
                monsterDescriptionTempalte.text = lockedMonstersDescripton;
                monsterSpriteTemplate.sprite = lockedMonstersSprite;
            }
            else
            {
                monsterDescriptionTempalte.text = GameManager.GetManager.Database.GetMonsterDescription(GameManager.GetManager.ActiveFileID, MonsterIDs.BasicDemon);
                Image(0);
            }
        }

        private void OnDestroy()
        {
            ManagerHUB.GetManager.GameEventsHandler.onMonsterButtonPanelOpen -= SetDefault;
        }
    }
}