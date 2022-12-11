using System.Collections.Generic;
using UnityEngine;

namespace AKB.Core.Managing
{
    /// <summary>
    /// Every available Projectile type in the game.
    /// </summary>
    public enum ProjectileType
    {
        Normal,
        Exploding,
    }

    /// <summary>
    /// 
    /// </summary>
    [DefaultExecutionOrder(-394)]
    public class ProjectilePools : MonoBehaviour
    {
        #region INSPECTOR_VARIABLES
        /// <summary>
        /// The projectile prefabs the enemies can shoot.
        /// </summary>
        [Header("Set in inspector - Projectile prefab")]
        [SerializeField, Tooltip("The projectile prefabs the enemies can shoot.")] List<GameObject> projectilePrefabs;

        /// <summary>
        /// The amount of per-projectile to preload on game start.
        /// </summary>
        [Header("\tSet in inspector - Total Projectile amount")]
        [SerializeField, Tooltip("The amount of per-projectile to preload on game start.")] int amountToPool;
        #endregion

        #region PRIVATE_VARIABLES
        /// <summary>
        /// The spawned projectiles hierarchy anchor.
        /// </summary>
        Transform projectileAnchor;

        /// <summary>
        /// The created projectile pools.
        /// </summary>
        List<Queue<GameObject>> projectilePools;
        #endregion

        #region STARTUP_BEHAVIOUR
        private void Awake()
        {
            CreateAnchors();
        }

        /// <summary>
        /// Call to create an anchor with the projectiles as children so the hierarchy is kept clean.
        /// </summary>
        void CreateAnchors()
        {
            GameObject anchor = new GameObject("Projectiles");
            projectileAnchor = anchor.transform;
        }

        private void Start()
        {
            ManagerHUB.GetManager.SetProjectilePoolsReference(this);

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