using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using akb.Core.Managing;
using akb.Core.Managing.LevelLoading;
using akb.Core.Managing.PCG;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timer;

    float startTime = 0;

    bool canActivateTimer = false;

    // Update is called once per frame
    void Update()
    {
        if (canActivateTimer == true)
        { 
            startTime += Time.deltaTime;
            timer.SetText(startTime.ToString());
        }
    }
    private void Start()
    {
        ManagerHUB.GetManager.GameEventsHandler.onGenerateNextRoom += StartRunTimer;
        ManagerHUB.GetManager.GameEventsHandler.onSceneChanged += StopRunTimer;
    }
    private void OnDestroy()
    {
        ManagerHUB.GetManager.GameEventsHandler.onGenerateNextRoom -= StartRunTimer;
        ManagerHUB.GetManager.GameEventsHandler.onSceneChanged -= StopRunTimer;
    }

    void StartRunTimer(RoomWorld empty)
    {
        _ = empty;
        canActivateTimer = true;
        startTime = 0;
        timer.SetText(startTime.ToString());
    }

    void StopRunTimer(GameScenes empty)
    {
        _ = empty;
        canActivateTimer = false;
        startTime = 0;
        timer.SetText(startTime.ToString());
    }
}
