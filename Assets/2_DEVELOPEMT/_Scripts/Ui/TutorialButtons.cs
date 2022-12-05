using System.Collections;
using System.Collections.Generic;
using AKB.Core.Managing;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialButtons : MonoBehaviour
{
    [SerializeField] Image imageTemplate;
    [SerializeField] TextMeshProUGUI textTempalte;

    //Tutorials Buttons
    [SerializeField] Button meleeAttackButton;
    [SerializeField] Button dodgeRollButton;
    [SerializeField] Button throwButton;
    [SerializeField] Button teleportationButton;
    [SerializeField] Button recallButton;

    //Parent gameobject
    [SerializeField] GameObject Tutorials_UI_Panel;

    [SerializeField] List<Sprite> tutorialButtonsSprite;
    [SerializeField] List<string> tutorialButtonsDescription;


    private void Start()
    {
        EntrySetup();

        if (Tutorials_UI_Panel.activeSelf == true)
        {
            meleeAttackButton.Select();
        }
    }

    /// <summary>
    /// Call to set up the default script behaviour.
    /// </summary>
    void EntrySetup()
    {
        //Tutorials Buttons
        meleeAttackButton.onClick.AddListener(Setter);
        dodgeRollButton.onClick.AddListener(Setter);
        throwButton.onClick.AddListener(Setter);
        teleportationButton.onClick.AddListener(Setter);
        recallButton.onClick.AddListener(Setter);
    }

    void Setter()
    {
        //imageTemplate.sprite = GameManager.S.UIManager.testing.sprite;
        //textTempalte.text = GameManager.S.UIManager.testing.description;
    }
}
