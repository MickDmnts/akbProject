using UnityEngine;


namespace akb.Entities.Interactions
{
    public class SpearDamage : MonoBehaviour
    {
        [SerializeField] float damageValue; 

        private void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent<IInteractable>(out IInteractable interactable))
            {
                interactable.AttackInteraction(damageValue);
            }
        }
    }
}
