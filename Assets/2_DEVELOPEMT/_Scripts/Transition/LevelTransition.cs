using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour
{
    public Animator animator;
    private int levelToLoad;
    // Update is called once per frame
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            FadeOut();
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            FadeIn();
        }
    }
    public void FadeOut()
    {
        animator.SetTrigger("FadeOut");
    }
    public void FadeIn()
    {
        animator.SetTrigger("FadeIn");
    }
}
