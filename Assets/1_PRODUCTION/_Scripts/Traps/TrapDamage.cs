//using UnityEngine;


//namespace akb.Entities.Interactions
//{
//    public class TrapDamage : MonoBehaviour
//    {
//        [SerializeField] Collider boxCollider;
//        [SerializeField] float damageValue;
//        private void Start()
//        {
//            boxCollider.enabled = true;
//            boxCollider.isTrigger = true;
//        }

//        private void OnTriggerEnter(Collider other)
//        {
//            if (other.TryGetComponent<IInteractable>(out IInteractable interactable))
//            {
//                Debug.Log("I do damage");
//                interactable.AttackInteraction(damageValue);
//                StopTrapDamageInteraction();
//            }
//        }
//        private void OnTriggerStay(Collider other)
//        {
//            StopTrapDamageInteraction();
//            if (other.TryGetComponent<IInteractable>(out IInteractable interactable))
//            {
//                Debug.Log("I do damage");
//                interactable.AttackInteraction(damageValue);
//            }
//        }
//        public void TrapDamageInteraction()
//        {
//            boxCollider.enabled = true;
//        }
//        public void StopTrapDamageInteraction()
//        {
//            boxCollider.enabled = false;
//        }
//    }
//}
