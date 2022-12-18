using System.Collections.Generic;
using UnityEngine;

namespace akb.Core.Managing
{
    /// <summary>
    /// The player spear pool manager.
    /// Creates an X amount of spears for the player to use.
    /// </summary>
    public class SpearPool : MonoBehaviour
    {
        /// <summary>
        /// The player spear prefab.
        /// </summary>
        [Header("Set in inspector")]
        [SerializeField, Tooltip("The player spear prefab")] GameObject spearPrefab;
        /// <summary>
        /// The created amount of spears to preload.
        /// </summary>
        [SerializeField, Tooltip("The created amount of spears to preload.")] int amountToPool;

        /// <summary>
        /// The queue used for spear caching.
        /// </summary>
        Queue<GameObject> spearQueue = new Queue<GameObject>();
        /// <summary>
        /// The hierarchy parent to keep it clean.
        /// </summary>
        Transform spearParent;

        private void Awake()
        {
            SetParent();
        }

        /// <summary>
        /// Creates a new empty game object in the hierarchy to hold the spears.
        /// </summary>
        void SetParent()
        {
            GameObject spearAnchor = new GameObject("Projectiles");
            spearAnchor.transform.SetParent(transform);
            spearParent = spearAnchor.transform;
        }

        void Start()
        {
            ManagerHUB.GetManager.SetSpearPoolReference(this);

            PopulatePool(ref spearQueue, amountToPool, spearParent);
        }

        /// <summary>
        /// Creates a pool of spears based on the amountToPool value for the player to use.
        /// </summary>
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
        /// Call to get the Spear Gameobject reference.
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
