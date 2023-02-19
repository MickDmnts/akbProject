using System.Collections;
using UnityEngine;

using akb.Entities.Interactions;

namespace akb.Entities.AI.Implementations.Astaroth
{
    public class FlamePillar : MonoBehaviour
    {
        [Header("Set in inspector")]
        [SerializeField] int playerLayer;
        [SerializeField] GameObject pillarGfx;
        [SerializeField] Vector3 damageCenter;
        [SerializeField] float damageRadius;
        [SerializeField] int damageValue;
        [SerializeField] float timeUntilDmg = 2f;

        bool spawnedPillar = false;
        float currentInterval = 0;

        private void Update()
        {
            currentInterval += Time.deltaTime;
            if (currentInterval >= timeUntilDmg && !spawnedPillar)
            {
                spawnedPillar = true;
                GameObject temp = Instantiate(pillarGfx, transform.position + new Vector3(0f, 2f, 0f), pillarGfx.transform.rotation);
                temp.transform.SetParent(transform);
                Destroy(transform.root.gameObject, 2f);

                StartCoroutine(DamageSurroundings());
            }
        }

        IEnumerator DamageSurroundings()
        {
            Collider[] collisions = Physics.OverlapSphere(damageCenter, damageRadius);

            foreach (Collider collision in collisions)
            {
                IInteractable interactable = collision.GetComponent<IInteractable>();

                if (interactable != null)
                {
                    interactable.AttackInteraction(damageValue);
                }

                yield return null;
            }

            yield return null;
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(damageCenter, damageRadius);
        }
    }
}