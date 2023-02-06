using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using AKB.Core.Managing;

public class HellsGrimoire : MonoBehaviour
{
    [SerializeField]Image imageTemplate;
    [SerializeField]TextMeshProUGUI textTempalte;

    [SerializeField] Button tutorialsButton;
    [SerializeField] Button monstersButton;
    [SerializeField] Button pastRunsButton;

    //Tutorials Buttons
    [SerializeField] Button meleeAttackButton;
    [SerializeField] Button dodgeRollButton;
    [SerializeField] Button throwButton;
    [SerializeField] Button teleportationButton;
    [SerializeField] Button recallButton;

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
        EntrySetup();
    }

    /// <summary>
    /// Call to set up the default script behaviour.
    /// </summary>
    void EntrySetup()
    {
        tutorialsButton.onClick.AddListener(Tutorials);
        monstersButton.onClick.AddListener(Monsters);
        pastRunsButton.onClick.AddListener(PastRuns);

        //Tutorials Buttons
        meleeAttackButton.onClick.AddListener(Setter);
        dodgeRollButton.onClick.AddListener(Setter);
        throwButton.onClick.AddListener(Setter);
        teleportationButton.onClick.AddListener(Setter);
        recallButton.onClick.AddListener(Setter);

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

    void Tutorials()
    {
        GameManager.S.UIManager.EnablePanel("Tutorials Body");
    }

    void Monsters()
    {
        GameManager.S.UIManager.EnablePanel("Monsters Body");
    }

    void PastRuns()
    {
        GameManager.S.UIManager.EnablePanel("Past Runs Body");
    }

    void Setter()
    {
        imageTemplate.sprite = GameManager.S.UIManager.testing.sprite;
        textTempalte.text = GameManager.S.UIManager.testing.description;
    }
}