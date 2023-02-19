using UnityEngine;

using akb.Core.Managing;

public class LevelTransition : MonoBehaviour
{
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();

        //ManagerHUB.GetManager.GameEventsHandler.onFadeOut += FadeOut;
        ManagerHUB.GetManager.GameEventsHandler.onSceneLoaded += FadeOut;
        ManagerHUB.GetManager.GameEventsHandler.onNextRoomEntry += FadeIn;
    }

    public void FadeOut()
    {
        animator.SetTrigger("FadeOut");
        Debug.Log("fadeout");
    }

    public void FadeIn()
    {
        animator.SetTrigger("FadeIn");
        Debug.Log("fadein");
    }

    private void OnDestroy()
    {
        //ManagerHUB.GetManager.GameEventsHandler.onFadeOut -= FadeOut;
        ManagerHUB.GetManager.GameEventsHandler.onSceneLoaded -= FadeOut;
        ManagerHUB.GetManager.GameEventsHandler.onNextRoomEntry -= FadeIn;
    }
}
