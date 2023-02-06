using System;
using System.Collections;

using UnityEngine;
using UnityEngine.AI;

namespace AKB.Entities.AI.Implementations.Big_Demon
{
    using AKB.Entities.Interactions;
    using Charge_Only;

    public enum BigDemonType
    {
        BigDemonChargeSlam = 0,
        BigDemonChargeOnly = 1,
    }

    public class BigDemon : AI_Entity, IInteractable,
        IStunnable
    {
        const float RUSH_VELOCITY = 50f;

        [Header("Set in inspector")]
        [SerializeField] BigDemonType demonType;
        [SerializeField] Transform target;

        [Header("Universal Settings")]
        [SerializeField] GameObject lineGraphic;
        [SerializeField] Vector3 lineGraphicSize;
        [SerializeField] float chargeRange;
        [SerializeField] float chargeDuration;
        [SerializeField] float stopChargeAfter;
        [SerializeField] float chargeCooldown;

        [Header("Big Demon Slam Specific")]
        [SerializeField] GameObject circleGraphic;
        [SerializeField] float slamRange;
        [SerializeField] float slamTime;
        [SerializeField] float slamCooldown;

        BigDemonNodeData _nodeData;
        BigDemonAttackHandler attackHandler;

        private void Awake()
        {
            CacheNeededComponents();
        }

        void CacheNeededComponents()
        {
            ai_agent = GetComponent<NavMeshAgent>();
            ai_entityAnimations = GetComponentInChildren<BigDemonAnimations>();

            attackHandler = GetComponentInChildren<BigDemonAttackHandler>();
        }

        private void Start()
        {
            entityNodeData = SetupNodeData<BigDemonNodeData>();
            _nodeData = entityNodeData as BigDemonNodeData;

            CreateAppropriateBTHandler(out ai_BTHandler);
        }

        protected override T SetupNodeData<T>()
        {
            BigDemonNodeData tempData = new T() as BigDemonNodeData;

            tempData.SetEnemyEntity(this);
            tempData.SetEnemyAnimations(GetDemonAnimations());
            tempData.SetNavMeshAgent(ai_agent);

            tempData.SetIsStunned(false);

            tempData.SetChargeDuration(chargeDuration);
            tempData.SetChargeCooldown(chargeCooldown);
            tempData.SetChargeRange(chargeRange);

            if (demonType.Equals(BigDemonType.BigDemonChargeSlam))
            {
                //Big demon specific
                NodeDataExtendedSetup(ref tempData);
            }

            tempData.SetCanMove(true);
            tempData.SetCanRotate(true);
            tempData.SetTarget(target);

            return (T)Convert.ChangeType(tempData, typeof(T));
        }

        void NodeDataExtendedSetup(ref BigDemonNodeData tempData)
        {
            tempData.SetSlamRange(slamRange);
            tempData.SetSlamTime(slamTime);
            tempData.SetSlamCooldown(slamCooldown);
        }

        /// <summary>
        /// Call to create the corresponding big demon variant based on demonType passed
        /// and assign it to the ai_BTHandler variable.
        /// </summary>
        protected override void CreateAppropriateBTHandler(out IBehaviourTreeHandler handlerVar)
        {
            switch (demonType)
            {
                case BigDemonType.BigDemonChargeSlam:
                    handlerVar = new BigDemonBTHandler(this, entityNodeData);
                    break;

                case BigDemonType.BigDemonChargeOnly:
                    handlerVar = new BigChargerBTHandler(this, entityNodeData);
                    break;

                default:
                    //Invalid demon type
                    handlerVar = null;
                    break;
            }
        }

        //Sole purpose is to update the behaviour tree of the entity
        private void Update()
        {
            if (ai_BTHandler != null)
            {
                ai_BTHandler.UpdateBT();
            }
        }

        #region CHARGE_ACTION
        public void ActivateRushFOV()
        {
            StartCoroutine(BigDemonRush());
        }

        IEnumerator BigDemonRush()
        {
            //hold graphic in place here
            Vector3 graphicPos = transform.position;
            GameObject tempGraphic = Instantiate(lineGraphic, graphicPos, transform.rotation * Quaternion.Euler(90f, 0f, 0f));
            tempGraphic.transform.localScale = lineGraphicSize;
            tempGraphic.SetActive(true);

            ai_agent.isStopped = true;
            ai_agent.velocity = ai_agent.transform.forward * RUSH_VELOCITY;

            float stopCache = stopChargeAfter;
            while (stopCache > 0)
            {
                GetAttackHandler().InitiateAttack();

                stopCache -= Time.deltaTime;
                yield return null;
            }

            Destroy(tempGraphic);

            ai_agent.velocity = Vector3.zero;
            ai_agent.isStopped = false;

            _nodeData.SetCanRotate(true);
            _nodeData.SetCanMove(true);
            _nodeData.SetIsRushing(false);

            StopAllCoroutines();
        }
        #endregion

        #region ENTITY_INTERACTIONS
        public void AttackInteraction(float damageValue)
        {
            if (GetDemonData().GetIsDead()) return;

            SubtractHealth(damageValue);
            GetDemonAnimations().PlayGetHitAnimation();

            isDead = CheckIfDead(EntityLife);

            if (isDead)
            {
                UpdateNodeDataIsDead(isDead);
                GetDemonAnimations().PlayDeathAnimation();

                MoveLayerOnDeath();
            }
        }

        public void ApplyStatusEffect(GameObject effect)
        {
            Instantiate<GameObject>(effect, transform);
        }

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
            _nodeData.SetIsDead(value);
        }
        #endregion

        public BigDemonAnimations GetDemonAnimations()
        {
            return ai_entityAnimations as BigDemonAnimations;
        }

        /// <summary>
        /// Call to get THIS demons data object.
        /// </summary>
        BigDemonNodeData GetDemonData()
        {
            return entityNodeData as BigDemonNodeData;
        }

        public BigDemonAttackHandler GetAttackHandler() => attackHandler;

        public GameObject GetSlamGraphic() => circleGraphic;

        private void OnDisable()
        {
            StopAllCoroutines();
        }
    }
}
