using System.Collections;
using System.Collections.Generic;
using AKB.Core.Managing;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] Button closePauseMenu;
    [SerializeField] Button hellsGrimoire;
    [SerializeField] Button options;
    [SerializeField] Button abandonRun;
    [SerializeField] Button saveExit;


    private void Start()
    {
        EntrySetup();
    }

    /// <summary>
    /// Call to set up the default script behaviour.
    /// </summary>
    void EntrySetup()
    {
        closePauseMenu.onClick.AddListener(ClosePauseMenu);
        hellsGrimoire.onClick.AddListener(HellsGrimoire);
        options.onClick.AddListener(Options);
        abandonRun.onClick.AddListener(AbandonRun);
        saveExit.onClick.AddListener(SaveExit);
    }

    void ClosePauseMenu()
    {
        //close pause menu
        GameManager.S.UIManager.DisablePanel();
    }

    void HellsGrimoire()
    {
        GameManager.S.UIManager.EnablePanel("HellsGrimoire_UI_Panel");
    }

    void Options()
    {
        GameManager.S.UIManager.EnablePanel("SaveSlot_UI_Panel");
    }

    void AbandonRun()
    {
        GameManager.S.LevelManager.TransitToPlayerHub();
    }

    void SaveExit()
    {

    }
}
