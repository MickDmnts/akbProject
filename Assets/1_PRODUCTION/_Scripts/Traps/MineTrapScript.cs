using UnityEngine;
namespace akb.Entities.Interactions
{
    public class MineTrapScript : MonoBehaviour
    {
        Animator animator;
        ParticleSystem explode;
        [SerializeField] float damageValue;
        bool exploded = false;
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
        public void Explode()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, 5);

            for(int i =0; i < colliders.Length; i++)
            {
                if(colliders[i].TryGetComponent<IInteractable>(out IInteractable interactable))
                {
                    interactable.AttackInteraction(damageValue);
                }
            }
            explode.Play();
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position, 5);
        }
    }
}
