using System.Collections.Generic;
using UnityEngine;

namespace AKB.Core.Managing
{
    public class SpearPool : MonoBehaviour
    {
        [SerializeField] GameObject spearPrefab;
        [SerializeField] int amountToPool;

        Queue<GameObject> spearQueue = new Queue<GameObject>();
        Transform spearParent;

        private void Awake()
        {
            SetParent();
        }

        void SetParent()
        {
            GameObject spearAnchor = new GameObject("Projectiles");
            spearAnchor.transform.SetParent(transform);
            spearParent = spearAnchor.transform;
        }

        // Start is called before the first frame update
        void Start()
        {
            GameManager.S.SetSpearPoolReference(this);

            PopulatePool(ref spearQueue, amountToPool, spearParent);
        }

        /// <param name="spearQueue">The Queue to populate.</param>
        /// <param name="amountToPool">How many bullets to create.</param>
        /// <param name="spearParent">The parent gameObject in the hierarchy</param>
        void PopulatePool(ref Queue<GameObject> spearQueue, int amountToPool, Transform spearParent)
        {
            for (int i = 0; i < amountToPool; i++)
            {
                GameObject obj = Instantiate(spearPrefab);
                obj.transform.SetParent(spearParent, true);
                obj.SetActive(false);

                //Queue the now created spear
                spearQueue.Enqueue(obj);
            }
        }

        /// <summary>
        /// Call to get Spear Gameobject reference.
        /// </summary>
        public GameObject GetPooledSpear()
        {
            GameObject spear = spearQueue.Dequeue();
            return spear;
        }

        /// <summary>
        /// Call to cache the passed Bullet gameObject back to its appropriate Queue.
        /// </summary>
        public void CacheSpear(GameObject spear)
        {
            spear.SetActive(false);
            spearQueue.Enqueue(spear.gameObject);
        }
    }
}
