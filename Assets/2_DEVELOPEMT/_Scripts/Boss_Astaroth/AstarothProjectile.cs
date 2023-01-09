using UnityEngine;

using akb.Entities.Interactions;

namespace akb.Entities.AI.Implementations.Astaroth
{
    public class AstarothProjectile : MonoBehaviour
    {
        [Header("Set in inspector")]
        [SerializeField] float projectileSpeed;
        [SerializeField] int projectileDamage;

        private void Start()
        {
            Destroy(gameObject, 10f);
        }

        private void Update()
        {
            transform.position += transform.forward * Time.deltaTime * projectileSpeed;
        }

        private void OnCollisionEnter(Collision other)
        {
            IInteractable interactable = other.gameObject.GetComponent<IInteractable>();

            if (interactable != null)
            {
                interactable.AttackInteraction(projectileDamage);
            }

            Destroy(gameObject);
        }
    }
}