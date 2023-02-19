using System.Collections;
using System.Collections.Generic;
using akb.Core.Managing;
using UnityEngine;

public class SetDefaultTab : MonoBehaviour
{
    private void OnEnable()
    {
        ManagerHUB.GetManager.GameEventsHandler.OnHellsGrimoirePanelOpen();
    }
}
