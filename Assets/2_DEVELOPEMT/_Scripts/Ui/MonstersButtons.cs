using System.Collections;
using System.Collections.Generic;
using AKB.Core.Managing;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MonstersButtons : MonoBehaviour
{
    [SerializeField] Image imageTemplate;
    [SerializeField] TextMeshProUGUI textTempalte;

    [SerializeField] List<Button> enemiesButtons = new List<Button>();

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

    //Parent gameobject
    [SerializeField] GameObject Monsters_UI_Panel;

    private void Start()
    {
        EntrySetup();

        if (Monsters_UI_Panel.activeSelf == true)
        {
            basicDemonButton.Select();
        }
    }

    /// <summary>
    /// Call to set up the default script behaviour.
    /// </summary>
    void EntrySetup()
    {
        //Monsters Buttons
        basicDemonButton.onClick.AddListener(Setter);
        fireBasicDemonButton.onClick.AddListener(Setter);
        bigDemonButton.onClick.AddListener(Setter);
        bigChargersButton.onClick.AddListener(Setter);
        rangedDemonsButton.onClick.AddListener(Setter);
        electroDemonsButton.onClick.AddListener(Setter);
        statusDemonsButton.onClick.AddListener(Setter);
        charmDemonsButton.onClick.AddListener(Setter);
        confuseDemonsButton.onClick.AddListener(Setter);
        beelzebubButton.onClick.AddListener(Setter);
        astarothButton.onClick.AddListener(Setter);
    }

    void AddButtonsToList()
    {
        enemiesButtons.Add(basicDemonButton);
        enemiesButtons.Add(fireBasicDemonButton);
        enemiesButtons.Add(bigDemonButton);
        enemiesButtons.Add(bigDemonButton);
        enemiesButtons.Add(rangedDemonsButton);
        enemiesButtons.Add(electroDemonsButton);
        enemiesButtons.Add(statusDemonsButton);
        enemiesButtons.Add(charmDemonsButton);
        enemiesButtons.Add(confuseDemonsButton);
        enemiesButtons.Add(beelzebubButton);
        enemiesButtons.Add(astarothButton);
    }

    void Setter()
    {
        imageTemplate.sprite = GameManager.S.UIManager.testing.sprite;
        textTempalte.text = GameManager.S.UIManager.testing.description;
    }

    //playerListGameobject.transform.Find("PlayerNameText").GetComponent<Text>().text = player.NickName;

}
