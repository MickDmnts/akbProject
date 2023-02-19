using UnityEngine;
using akb.Core.Sounds;
using akb.Core.Managing;
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
                    ManagerHUB.GetManager.SoundsHandler.PlayOneShot(GameAudioClip.ArcAttack);
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
    }
}