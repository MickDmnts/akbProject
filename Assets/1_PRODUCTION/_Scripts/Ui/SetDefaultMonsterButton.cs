using System.Collections;
using System.Collections.Generic;
using akb.Core.Managing;
using UnityEngine;
using UnityEngine.UI;

public class SetDefaultMonsterButton : MonoBehaviour
{
    [SerializeField] Button defaultMonsterButton;

    private void OnEnable()
    {
        defaultMonsterButton.Select();

        ManagerHUB.GetManager.GameEventsHandler.OnMonsterButtonPanelOpen();

    }
}
