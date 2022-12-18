using System;
using System.Collections;

using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

using akb.Core.Managing;
using akb.Entities.Interactions;

namespace akb.Entities.AI.Implementations.Ranged_Demon
{
    public class RangedDemon : AI_Entity, IInteractable,
        IStunnable, IShockable
    {
        [Header("Set in inspector")]
        [SerializeField] Transform target;

        [Header("- Jumping specifics")]
        [SerializeField] float jumpSpeed = 1f;
        [SerializeField] float maxJumpDistance;

        [SerializeField] float minTargetDistance;
        [SerializeField] float maxTargetDistance;

        [SerializeField] float maxTimeNearTarget;

        [Header("- Attack behaviour specifics.")]
        [SerializeField] GameObject lineGraphic;
        [SerializeField] float timeUntilAttack;
        [SerializeField] float attackCooldown;

        [Header("- Set projectile info")]
        [SerializeField] ProjectileType projectileType;
        [SerializeField] Transform bezierStart;
        [SerializeField] Transform bezierHint;

        bool isShocked = false;
        float jumpSpeedCache;
        float attackSpeedCache;


        private void Awake()
        {
            CacheNeededComponents();
        }

        /// <summary>
        /// Call to cache the gameObject needed components.
        /// </summary>
        void CacheNeededComponents()
        {
            ai_agent = GetComponent<NavMeshAgent>();
            ai_entityAnimations = GetComponentInChildren<RangedDemonAnimations>();
        }

        #region NODE_DATA_CREATION
        void Start()
        {
            entityNodeData = SetupNodeData<RangedDemonNodeData>();

            CreateAppropriateBTHandler(out ai_BTHandler);
        }

        /// <summary>
        /// Call to create a RangedDemonNodeData instance and mutate the needed fields.
        /// </summary>
        /// <returns>A completely initialised RangedDemonNodeData instance.</returns>
        protected override T SetupNodeData<T>()
        {
            RangedDemonNodeData tempData = new T() as RangedDemonNodeData;

            tempData.SetEnemyEntity(this);
            tempData.SetEnemyAnimations(GetDemonAnimations());
            tempData.SetNavMeshAgent(ai_agent);

            tempData.SetIsStunned(false);

            tempData.SetMinTargetDistance(minTargetDistance);
            tempData.SetMaxTargetDistance(maxTargetDistance);

            tempData.SetMaxTimeNearTarget(maxTimeNearTarget);

            tempData.SetMaxJumpDistance(maxJumpDistance);

            tempData.SetAttackCooldown(attackCooldown);

            tempData.SetProjectileType(projectileType);
            tempData.SetBezierStartTransform(bezierStart);
            tempData.SetBezierHintTransform(bezierHint);

            tempData.SetCanMove(true);
            tempData.SetCanRotate(true);
            tempData.SetTarget(target);

            return (T)Convert.ChangeType(tempData, typeof(T));
        }
        #endregion

        /// <summary>
        /// Call to create the appropriate BT handler type
        /// and assign it to the ai_BTHandler variable.
        /// </summary>
        protected override void CreateAppropriateBTHandler(out IBehaviourTreeHandler handlerVar)
        {
            handlerVar = new RangedDemonBTHandler(this, entityNodeData);
        }

        //Sole purpose is to update the behaviour tree of the entity
        private void Update()
        {
            if (ai_BTHandler != null)
            {
                ai_BTHandler.UpdateBT();
            }
        }


        #region CALLED_THROUGH_BT
        /// <summary>
        /// Call to initiate the new position searching and agent moving to it.
        /// </summary>
        /// <param name="range">The maximum range to search a position.</param>
        public void SearchNewPos(float range)
        {
            StopAllCoroutines();
            StartCoroutine(SearchPosition(transform.position, range));
        }

        IEnumerator SearchPosition(Vector3 center, float range)
        {
            int reps = 30;
            Vector3 possiblePos = Vector3.zero;

            for (int i = 0; i < reps; i++)
            {
                //Translate XY to XZ
                Vector2 randomHit = Random.insideUnitCircle;
                Vector3 xzTranslation = new Vector3(randomHit.x, 0f, randomHit.y);

                //set random point
                Vector3 randomPoint = center + xzTranslation * range;

                NavMeshHit hit;
                if (NavMesh.SamplePosition(randomPoint, out hit, ai_agent.height * 2, NavMesh.AllAreas))
                {
                    if ((hit.position - transform.position).magnitude >= range / 1.5f)
                    {
                        Debug.DrawRay(hit.position, Vector3.up, UnityEngine.Color.blue, 1.0f);
                        possiblePos = hit.position;
                    }
                }

                yield return null;
            }

            //Continue to jump sequence when position sampling gets finished
            StopAllCoroutines();
            StartCoroutine(MoveToNewPos(possiblePos));
        }

        IEnumerator MoveToNewPos(Vector3 newPos)
        {
            //Start playing flying anim
            GetDemonAnimations().SetIsFlyingState(true);

            //Stops the agent from rotating while flying
            GetDemonData().SetCanRotate(false);
            ai_agent.isStopped = true;

            #region BEZIER_VARIABLES
            float uCurrent;
            float uMax = 1f;
            bool moving = true;
            float startTime = Time.time;

            Vector3 startPos = transform.position;
            Vector3 virtualMidPos = (startPos + newPos) / 2;
            virtualMidPos.y += 10f;

            Vector3 p012;
            #endregion

            //Bezier function
            while (moving)
            {
                Vector3 targetPosCache = newPos;

                uCurrent = (Time.time - startTime) / jumpSpeed;

                if (uCurrent >= uMax)
                {
                    uCurrent = uMax;
                    moving = false;
                }

                //Bezier curve calcs
                Vector3 p01, p12;
                p01 = (1 - uCurrent) * startPos + uCurrent * virtualMidPos;
                p12 = (1 - uCurrent) * virtualMidPos + uCurrent * targetPosCache;

                p012 = (1 - uCurrent) * p01 + uCurrent * p12;

                Debug.DrawLine(virtualMidPos, p012, Color.red, 10f);

                transform.position = p012;

                yield return null;
            }

            //Reset agent to former state
            ResetAgentVariables();

            //Stop playing the flying animation.
            GetDemonAnimations().SetIsFlyingState(false);

            StopAllCoroutines();
        }

        /// <summary>
        /// Call to reset the agent back to its initial state after jumping to a new position.
        /// </summary>
        void ResetAgentVariables()
        {
            ai_agent.isStopped = false;

            GetDemonData().SetSearchingForPos(false);
            GetDemonData().SetCanRotate(true);
        }
        #endregion

        #region INTERACTIONS
        public void AttackInteraction(float damageValue)
        {
            //Early exit if the entity is dead.
            if (GetDemonData().GetIsDead()) return;

            GetDemonAnimations().PlayDamagedAnimation();

            SubtractHealth(damageValue);
            isDead = CheckIfDead(EntityLife);

            if (isDead)
            {
                UpdateNodeDataIsDead(isDead);

                GetDemonAnimations().SetIsFlyingState(false);
                GetDemonAnimations().PlayDeathAnimation();

                MoveLayerOnDeath();
            }
        }

        public void ApplyStatusEffect(GameObject effect)
        {
            Instantiate<GameObject>(effect, transform);
        }

        #region SHOCKED_INTERACTION
        public bool IsGettingShocked()
        {
            return isShocked;
        }

        public void InflictShockInteraction()
        {
            if (isShocked) return;

            isShocked = true;

            //Cache default speeds
            jumpSpeedCache = jumpSpeed;
            attackSpeedCache = attackCooldown;

            GetDemonData().SetAttackCooldown(attackCooldown * 2);
            jumpSpeed /= 2;
        }

        public void RemoveShockInteraction()
        {
            isShocked = false;

            GetDemonData().SetAttackCooldown(attackSpeedCache);
            jumpSpeed = jumpSpeedCache;
        }
        #endregion

        #region STUNNED_INTERACTION
        public bool IsAlreadyStunned()
        {
            return GetDemonData().GetIsStunned();
        }

        public void InflictStunnedInteraction()
        {
            if (GetDemonData().GetIsStunned()) return;

            GetDemonData().SetIsStunned(true);
        }

        public void RemoveStunnedInteraction()
        {
            GetDemonData().SetIsStunned(false);
        }
        #endregion

        protected override void UpdateNodeDataIsDead(bool value)
        {
            GetDemonData().SetIsDead(value);
        }
        #endregion

        #region UTILITIES
        /// <summary>
        /// Call to get THIS demons animations script.
        /// </summary>
        /// <returns></returns>
        public RangedDemonAnimations GetDemonAnimations()
        {
            return ai_entityAnimations as RangedDemonAnimations;
        }

        /// <summary>
        /// Call to get THIS demons data object.
        /// </summary>
        /// <returns></returns>
        RangedDemonNodeData GetDemonData()
        {
            return entityNodeData as RangedDemonNodeData;
        }

        public GameObject GetLineGraphic() => lineGraphic;
        #endregion
    }
}