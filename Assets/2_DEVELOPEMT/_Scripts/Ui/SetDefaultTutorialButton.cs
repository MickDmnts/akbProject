using System.Collections;
using System.Collections.Generic;
using akb.Core.Managing;
using UnityEngine;
using UnityEngine.UI;

public class SetDefaultTutorialButton : MonoBehaviour
{
    [SerializeField] Button defaultTutorialButton;

    private void OnEnable()
    {
        defaultTutorialButton.Select();

        ManagerHUB.GetManager.GameEventsHandler.OnTutorialButtonPanelOpen();
    }
}
