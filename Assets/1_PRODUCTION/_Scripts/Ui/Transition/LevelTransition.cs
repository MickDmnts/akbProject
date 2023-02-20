using UnityEngine;

using akb.Core.Managing;

public class LevelTransition : MonoBehaviour
{
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();

        ManagerHUB.GetManager.GameEventsHandler.onSceneLoaded += FadeOut;
        ManagerHUB.GetManager.GameEventsHandler.onNextRoomEntry += FadeOut;
    }

    public void FadeOut()
    {
        animator.SetTrigger("FadeOut");
    }

    public void FadeIn()
    {
        animator.SetTrigger("FadeIn");
    }

    private void OnDestroy()
    {
        ManagerHUB.GetManager.GameEventsHandler.onSceneLoaded -= FadeOut;
        ManagerHUB.GetManager.GameEventsHandler.onNextRoomEntry -= FadeOut;
    }
}