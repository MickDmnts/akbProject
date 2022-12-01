using System.Collections;
using System.Collections.Generic;
using AKB.Core.Managing;
using UnityEngine;
using UnityEngine.UI;


public class SaveSlotPanel : MonoBehaviour
{
    [SerializeField] Button button1;
    [SerializeField] Button button2;
    [SerializeField] Button button3;
    [SerializeField] Button button4;


    private void Start()
    {
        EntrySetup();
    }

    /// <summary>
    /// Call to set up the default script behaviour.
    /// </summary>
    void EntrySetup()
    {
        button1.onClick.AddListener(Placeholder);
        button2.onClick.AddListener(Placeholder);
        button3.onClick.AddListener(Placeholder);
        button4.onClick.AddListener(Placeholder);
    }

    void Placeholder()
    {
        //gia twra,  apla na se phgainei se mia pista
        GameManager.S.LevelManager.TransitToPlayerHub();
    }
}
