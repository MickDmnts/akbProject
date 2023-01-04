using UnityEngine;
using TMPro;

using akb.Core.Managing;
using akb.Core.Managing.LevelLoading;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timer;

    float startTime = 0;

    bool canActivateTimer = false;

    private void Start()
    {
        ManagerHUB.GetManager.GameEventsHandler.onSceneChanged += HandleWorldEntranceTimer;
        ManagerHUB.GetManager.GameEventsHandler.onNextRoomEntry += TimerReset;
    }

    void HandleWorldEntranceTimer(GameScenes currentScene)
    {
        if (currentScene == GameScenes.World1Scene || currentScene == GameScenes.World2Scene)
        {
            _ = currentScene;
            canActivateTimer = true;
            startTime = 0;
            timer.SetText("Time: " + startTime.ToString());
        }
        else
        {
            _ = currentScene;
            canActivateTimer = false;
            startTime = 0;
            timer.SetText("");
        }
    }

    void TimerReset()
    {
        startTime = 0;
        timer.SetText("Time: " + startTime.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        if (canActivateTimer == true)
        {
            startTime += Time.deltaTime;
            timer.SetText("Time: " + startTime.ToString());
        }
    }

    private void OnDestroy()
    {
        ManagerHUB.GetManager.GameEventsHandler.onSceneChanged -= HandleWorldEntranceTimer;
        ManagerHUB.GetManager.GameEventsHandler.onNextRoomEntry -= TimerReset;
    }
}
