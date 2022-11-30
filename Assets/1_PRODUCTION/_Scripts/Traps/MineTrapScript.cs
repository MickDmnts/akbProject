using UnityEngine;

public class MineTrapScript : MonoBehaviour
{
    Animator animator;
    ParticleSystem explode;

    private void Start()
    {
        explode = GetComponent<ParticleSystem>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            animator.SetTrigger("Shake");
        }
    }

    //Animation event
    void Explode()
    {
        explode.Play();
    }
}
