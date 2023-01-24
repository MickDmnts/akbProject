using System.Collections;
using UnityEngine;

using akb.Core.Managing;
using akb.Entities.Interactions;
using akb.Entities.Player.Interactions;

namespace akb.Projectiles
{
    public enum Type
    {
        Normal,
        Exploding,
        //add more...
    }

    public class ProjectileBehaviour : MonoBehaviour, IRangedDemonSphere
    {
        [Header("Set in inspector")]
        [SerializeField] Type projectileType;
        [SerializeField] LayerMask playerLayer;
        [SerializeField] GameObject circleGraphic;
        [SerializeField] Vector3 circleGraphicSize;

        [SerializeField] int projectileDamage;
        [SerializeField] float projectileSpeed = 1f;
        [SerializeField, Range(0f, 1f)] float projectileDodgeFactor;
        [SerializeField] ParticleSystem projectileHitParticle;

        GameObject tempCircleGraphic;

        #region BEZIER_VARS
        float uCurrent = 0;
        float uMax = 1f;

        Vector3 p012 = Vector3.zero;
        bool moving;
        float startTime;

        Vector3 startPos, midPos;
        Transform target;
        #endregion

        public void StartLerping(Vector3 startPos, Vector3 bezierHint)
        {
            this.startPos = startPos;
            midPos = bezierHint;

            target = ManagerHUB.GetManager.PlayerEntity.transform;

            //Start lerping
            StartCoroutine(LerpToTarget());
        }

        public IEnumerator LerpToTarget()
        {
            tempCircleGraphic = Instantiate(circleGraphic, transform.position, Quaternion.Euler(90f, 0f, 0f));
            tempCircleGraphic.transform.localScale = Vector3.one;
            tempCircleGraphic.SetActive(true);

            moving = true;
            startTime = Time.time;
            Vector3 posCache = Vector3.zero;

            while (moving)
            {
                Vector3 targetPos = posCache;

                uCurrent = (Time.time - startTime) / projectileSpeed;

                if (uCurrent >= uMax)
                {
                    uCurrent = uMax;
                    moving = false;

                    Destroy(tempCircleGraphic);

                    ExitBehaviour();
                }
                else if (uCurrent >= 0f && uCurrent <= 1 - projectileDodgeFactor)
                {
                    targetPos = target.position;
                    posCache = targetPos;
                }

                Vector3 p01, p12;
                p01 = (1 - uCurrent) * startPos + uCurrent * midPos;
                p12 = (1 - uCurrent) * midPos + uCurrent * targetPos;

                p012 = (1 - uCurrent) * p01 + uCurrent * p12;

                if (tempCircleGraphic == null) StopAllCoroutines();

                if (tempCircleGraphic != null)
                    tempCircleGraphic.transform.position = p012;

                transform.position = p012;

                yield return null;
            }

            uCurrent = 0;
            p012 = startPos = midPos = Vector3.zero;
            target = null;

            StopAllCoroutines();
        }

        private void OnCollisionEnter(Collision collision)
        {
            Destroy(tempCircleGraphic);

            ExitBehaviour(collision);
        }

        void ExitBehaviour(Collision collision = null)
        {
            //Universal behaviour
            Instantiate(projectileHitParticle.gameObject, transform.position, projectileHitParticle.transform.rotation);

            if (collision != null)
            {
                if (collision.transform.CompareTag("Player"))
                {
                    IInteractable interactable = collision.transform.GetComponent<IInteractable>();

                    if (interactable != null)
                    {
                        interactable.AttackInteraction(projectileDamage);
                    }
                }
            }

            switch (projectileType)
            {
                case Type.Normal:
                    //nop...
                    break;

                case Type.Exploding:
                    //create aoe
                    Collider[] hits = Physics.OverlapSphere(transform.position, 3, playerLayer.value);

                    PlayerInteractable interactable;

                    foreach (Collider hit in hits)
                    {
                        interactable = hit.GetComponent<PlayerInteractable>();

                        if (interactable != null)
                        {
                            interactable.ApplyStatusEffect(ManagerHUB.GetManager.StatusEffectManager.GetNeededEffect(EffectType.Shocked));
                            break;
                        }
                    }

                    break;
            }

            CacheProjectileToPool();
        }

        void CacheProjectileToPool()
        {
            ManagerHUB.GetManager.ProjectilePools.CacheProjectile(gameObject, ProjectileType.Normal);
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, 3);
        }
    }
}