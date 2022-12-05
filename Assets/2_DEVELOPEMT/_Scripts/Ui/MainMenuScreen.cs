using System.Collections;
using System.Collections.Generic;
using AKB.Core.Managing;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuScreen : MonoBehaviour
{
    [Header("Set in inspector\n\tMain Menu Panel")]
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
        //Continues from last saved game
        throw new System.NotImplementedException();
    }

    void NewGame()
    {
        //Replace when save-loading system gets introduced.
        //ManagerHUB.GetManager.UIManager.EnablePanel("SaveSlot_UI_Panel");

        //------------------------
        ManagerHUB.GetManager.UIManager.EnablePanel("GamePlayScreenPanel");
        ManagerHUB.GetManager.LevelManager.LoadNext(false);
        //------------------------
    }

    void LoadGame()
    {
        ManagerHUB.GetManager.UIManager.EnablePanel("SaveSlot_UI_Panel");
        throw new System.NotImplementedException();
    }

    void Exit()
    {
        Application.Quit();
    }
}
