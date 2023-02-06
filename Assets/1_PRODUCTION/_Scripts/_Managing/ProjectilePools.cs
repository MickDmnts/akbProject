using AKB.Projectiles;
using System.Collections.Generic;
using UnityEngine;

namespace AKB.Core.Managing
{
    /// <summary>
    /// Every available bullet type in the game.
    /// </summary>
    public enum ProjectileType
    {
        Normal,
        Exploding,
    }

    /* [CLASS DOCUMENTATION]
    * 
    * Inspector variable : Must be set from the inspector
    * Private variables: These values change in runtime.
    * 
    * [Class flow]
    * 1. When the game loads the manager pools the inpector projectile gameobjects.
    * 
    * [Must know]
    * 1. The GetPooledProjectileByType(...) returns null in case the pool is empty.
    * 
    */
    [DefaultExecutionOrder(50)]
    public class ProjectilePools : MonoBehaviour
    {
        #region INSPECTOR_VARIABLES
        [Header("Set in inspector - Projectile prefab")]
        [SerializeField] List<GameObject> projectilePrefabs;

        [Header("\tSet in inspector - Total Projectile amount")]
        [SerializeField] int amountToPool;
        #endregion

        #region PRIVATE_VARIABLES
        Transform projectileAnchor;

        List<Queue<GameObject>> projectilePools;
        #endregion

        #region STARTUP_BEHAVIOUR
        private void Awake()
        {
            CreateAnchors();
        }

        /// <summary>
        /// Call to createan anchor with the projectiles as children so the hierarchy is kept clean.
        /// </summary>
        void CreateAnchors()
        {
            GameObject anchor = new GameObject("Projectiles");
            projectileAnchor = anchor.transform;
        }

        private void Start()
        {
            GameManager.S.SetProjectilePoolsReference(this);

            InitializeDataStructures();
        }

        /// <summary>
        /// Call to initiate the projectile pool creation sequence.
        /// </summary>
        void InitializeDataStructures()
        {
            projectilePools = new List<Queue<GameObject>>();

            for (int i = 0; i < projectilePrefabs.Count; i++)
            {
                projectilePools.Add(CreatePool(amountToPool, projectileAnchor, i));
            }
        }

        /// <summary>
        /// Call to populate the passed Queue reference with iterations amount of bulletType bullets.
        /// </summary>
        /// <param name="iterations">How many bullets to create.</param>
        /// <param name="anchor">The parent gameObject in the hierarchy</param>
        Queue<GameObject> CreatePool(int iterations, Transform anchor, int listIndex)
        {
            Queue<GameObject> queue = new Queue<GameObject>();

            for (int i = 0; i < iterations; i++)
            {
                GameObject obj = Instantiate(projectilePrefabs[listIndex]);
                obj.transform.SetParent(anchor, true);
                obj.SetActive(false);

                //Queue the now created projectile
                queue.Enqueue(obj);
            }

            return queue;
        }
        #endregion

        #region UTILITIES
        /// <summary>
        /// Call to get a projectile Gameobject reference based on the passed type.
        /// <para>Returns null in case the pool is empty.</para>
        /// </summary>
        public GameObject GetPooledProjectileByType(ProjectileType projectileType)
        {
            //Grab the first projectile GO
            switch (projectileType)
            {
                case ProjectileType.Normal:
                    GameObject projectile = projectilePools[0].Dequeue();
                    return projectile;

                case ProjectileType.Exploding:
                    GameObject explProjectile = projectilePools[1].Dequeue();
                    return explProjectile;

                default:
                    return null;
            }
        }

        /// <summary>
        /// Call to cache the passed projectile gameObject back to its appropriate Queue based on the bulletType.
        /// </summary>
        /// <param name="projectile">The bullet script attached to the gameObject.</param>
        public void CacheProjectile(GameObject projectile, ProjectileType projectileType)
        {
            switch (projectileType)
            {
                case ProjectileType.Normal:
                    projectile.SetActive(false);
                    projectilePools[0].Enqueue(projectile.gameObject);
                    break;

                case ProjectileType.Exploding:
                    projectile.SetActive(false);
                    projectilePools[1].Enqueue(projectile.gameObject);
                    break;

            }
        }
        #endregion
    }
}