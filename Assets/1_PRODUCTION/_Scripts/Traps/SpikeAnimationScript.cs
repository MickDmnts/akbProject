using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace akb.Entities.Interactions
{
    public class SpikeAnimationScript : MonoBehaviour
    {
        [SerializeField] float spikeHitRate = 2f;
        [SerializeField] float damageValue = 10f;
        float nextSpikeHit;
        Animator animator;
        bool played = false;
        void Start()
        {
            animator = GetComponent<Animator>();
            nextSpikeHit = Time.time + spikeHitRate;
        }
        //private void OnTriggerEnter(Collider other)
        //{
        //    if (Time.time > nextSpikeHit)
        //    {
        //        if (other.gameObject.CompareTag("Player"))
        //        {
        //            animator.SetTrigger("Up");
        //            played = true;
        //        }
        //    }
        //}
        private void OnTriggerStay(Collider other)
        {
            played = false;
            animator.SetTrigger("Down");
            if (Time.time > nextSpikeHit)
            {
                if (other.gameObject.CompareTag("Player"))
                {
                    animator.SetTrigger("Up");
                    nextSpikeHit = Time.time + spikeHitRate;
                    played = true;
                    if (played == true)
                    {
                        other.TryGetComponent<IInteractable>(out IInteractable interactable);
                        interactable.AttackInteraction(damageValue);
                    }
                }
            }
        }
        //public void TrapDamage()
        //{
        //    Collider[] colliders = Physics.OverlapSphere(transform.position,2);
        //    for(int i = 0; i< colliders.Length; i++)
        //    {
        //        if(colliders[i].TryGetComponent<IInteractable>(out IInteractable interactable))
        //        {
        //            interactable.AttackInteraction(damageValue);
        //        }
        //    }
        //}
        private void OnTriggerExit(Collider other)
        {
            if (!played) return;

            animator.SetTrigger("Down");
        }
    }
}
