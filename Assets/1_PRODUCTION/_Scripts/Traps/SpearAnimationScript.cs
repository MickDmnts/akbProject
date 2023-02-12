using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace akb.Entities.Interactions
{
    public class SpearAnimationScript : MonoBehaviour
    {
        [SerializeField] float spearHitRate = 2f;

        float nextSpikeHit;
        Animator animator;
        bool played = false;

        void Start()
        {
            animator = GetComponent<Animator>();
            nextSpikeHit = Time.time + spearHitRate;
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
                if (other.gameObject.CompareTag("Player"))
                {
                    nextSpikeHit = Time.time + spearHitRate;
                    animator.SetTrigger("TrapAttack");
                    played = false;
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (!played) return;

            animator.ResetTrigger("TrapAttack");
        }
        //public void TrapDamageInteraction(Collider other)
        //{
        //    if (other.TryGetComponent<IInteractable>(out IInteractable interactable))
        //    {
        //        Debug.Log("I do damage");
        //        interactable.AttackInteraction(damageValue);
        //    }
        //    //if (other.TryGetComponent<IInteractable>(out IInteractable interactable))
        //    //{
        //    //    Debug.Log("I do damage");
        //    //    interactable.AttackInteraction(damageValue);
        //    //}
        //}
    }
}