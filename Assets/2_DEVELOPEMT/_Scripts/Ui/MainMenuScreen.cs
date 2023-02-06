using System.Collections;
using System.Collections.Generic;
using AKB.Core.Managing;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuScreen : MonoBehaviour
{
    [SerializeField] Button continueButton;
    [SerializeField] Button newGameButton;
    [SerializeField] Button loadGamedButton;
    [SerializeField] Button exitButton;


    private void Start()
    {
        EntrySetup();
    }

    /// <summary>
    /// Call to set up the default script behaviour.
    /// </summary>
    void EntrySetup()
    {
        continueButton.onClick.AddListener(Continue);
        newGameButton.onClick.AddListener(NewGame);
        loadGamedButton.onClick.AddListener(LoadGame);
        exitButton.onClick.AddListener(Exit);
    }

    void Continue()
    {
        //Continues from  last saved game
    }

    void NewGame()
    {
        GameManager.S.UIManager.EnablePanel("SaveSlot_UI_Panel");
    }

    void LoadGame()
    {
        GameManager.S.UIManager.EnablePanel("SaveSlot_UI_Panel");
    }

    void Exit()
    {
        Application.Quit();
    }
}
