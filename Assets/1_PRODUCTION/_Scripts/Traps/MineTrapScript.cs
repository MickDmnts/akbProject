using UnityEngine;
using akb.Core.Sounds;
using akb.Core.Managing;

namespace akb.Entities.Interactions
{
    public class MineTrapScript : MonoBehaviour
    {
        Animator animator;
        ParticleSystem explode;
        [SerializeField] float damageValue;

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
                ManagerHUB.GetManager.SoundsHandler.PlayOneShot(GameAudioClip.MineTrigger);
                animator.SetTrigger("Shake");
            }
        }

        //Animation event
        public void Explode()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, 5);

            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].TryGetComponent<IInteractable>(out IInteractable interactable))
                {
                    interactable.AttackInteraction(damageValue);
                }
            }
            ManagerHUB.GetManager.SoundsHandler.PlayOneShot(GameAudioClip.MineExplode);
            explode.Play();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position, 5);
        }
    }
}
