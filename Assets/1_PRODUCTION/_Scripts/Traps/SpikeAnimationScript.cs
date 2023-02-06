using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeAnimationScript : MonoBehaviour
{
    [SerializeField] float spikeHitRate = 2f;

    float nextSpikeHit;
    Animator animator;
    bool played = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        nextSpikeHit = Time.time + spikeHitRate;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Time.time > nextSpikeHit)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                animator.SetTrigger("Up");
                played = true;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        animator.SetTrigger("Down");
        if (Time.time > nextSpikeHit)
        {
            nextSpikeHit = Time.time + spikeHitRate;
            animator.SetTrigger("Up");
            played = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!played) return;

        animator.SetTrigger("Down");
    }
}
