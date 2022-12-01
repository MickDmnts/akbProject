using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearAnimationScript : MonoBehaviour
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
                animator.SetTrigger("TrapAttack");
                played = true;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (Time.time > nextSpikeHit)
        {
            nextSpikeHit = Time.time + spikeHitRate;
            animator.SetTrigger("TrapAttack");
            played = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!played) return;

        animator.ResetTrigger("TrapAttack");
    }
}