using UnityEngine;


namespace akb.Entities.Interactions
{
    public class ProjectileScript : MonoBehaviour
    {
        [SerializeField] float moveSpeed = 10f;
        [SerializeField] float damageValue = 10f;
        // Update is called once per frame
        void Update()
        {
            Shoot();
        }
        public void Shoot()
        {
            transform.position += moveSpeed * transform.forward * Time.deltaTime;
            Destroy(gameObject, 5f);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<IInteractable>(out IInteractable interactable))
            {
                interactable.AttackInteraction(damageValue);
            }
        }
    }
}
