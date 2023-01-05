using akb.Core.Database.Monsters;
using akb.Core.Managing;
using akb.Entities.Interactions;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace akb.Entities.AI.Implementations.Status_Demon
{
    public class StatusDemon : AI_Entity, IInteractable,
        IStunnable, IShockable
    {
        [Header("Set in inspector")]
        [SerializeField] EffectType effectType;
        [SerializeField] GameObject circleGraphic;

        [Header("- Teleport Specifics")]
        [SerializeField] float maxDistanceFromTarget;
        [SerializeField] int maxAttackBeforeAutoTeleport;
        [SerializeField] float timeUntilTPout;
        [SerializeField] float teleportCooldown;

        StatusDemonAOEHandler AOEHandler;
        Transform target;

        bool isShocked = false;
        float teleportCooldownCache;

        private void Awake()
        {
            CacheNeededComponents();
        }

        /// <summary>
        /// Call to cache the gameobject needed components.
        /// </summary>
        void CacheNeededComponents()
        {
            ai_agent = GetComponent<NavMeshAgent>();
            ai_entityAnimations = GetComponentInChildren<StatusDemonAnimations>();

            AOEHandler = GetComponentInChildren<StatusDemonAOEHandler>();
        }

        #region NODE_DATA_CREATION
        void Start()
        {
            target = ManagerHUB.GetManager.PlayerEntity.transform;

            entityNodeData = SetupNodeData<StatusDemonNodeData>();

            CreateAppropriateBTHandler(out ai_BTHandler);
        }

        /// <summary>
        /// Call to create a SimpleDemonNodeData instance and mutate the needed fields.
        /// </summary>
        /// <returns>A completely initialised SimpleDemonNodeData instance.</returns>
        protected override T SetupNodeData<T>()
        {
            StatusDemonNodeData tempData = new T() as StatusDemonNodeData;

            tempData.SetEnemyEntity(this);
            tempData.SetEnemyAnimations(GetDemonAnimations());
            tempData.SetNavMeshAgent(ai_agent);

            tempData.SetIsStunned(false);

            tempData.SetCanMove(true);
            tempData.SetCanRotate(true);
            tempData.SetTarget(target);

            tempData.SetEffectType(effectType);

            tempData.SetTeleportCooldown(teleportCooldown);
            tempData.SetTimeUntilTPout(timeUntilTPout);

            tempData.SetMaxDistanceFromTarget(maxDistanceFromTarget);

            return (T)Convert.ChangeType(tempData, typeof(T));
        }
        #endregion
        /// <summary>
        /// Call to create the corresponding status demon variant based on demonType passed
        /// and assign it to the ai_BTHandler variable.
        /// </summary>
        protected override void CreateAppropriateBTHandler(out IBehaviourTreeHandler handlerVar)
        {
            handlerVar = new StatusDemonBTHandler(this, entityNodeData);
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
        public void Initiate_TeleportCloseToPlayer(Vector3 center, float range)
        {
            StopAllCoroutines();

            maxAttackBeforeAutoTeleport = 2;

            GetDemonAnimations().PlayAttackAnimation();

            StartCoroutine(SearchNewPos(center, range, WarpAgentTo));
        }

        IEnumerator SearchNewPos(Vector3 center, float range, Action<Vector3> generatedPositionHandler)
        {
            int searchReps = 15;
            Vector3 possiblePos = Vector3.zero;

            for (int i = 0; i < searchReps; i++)
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
                        Debug.DrawRay(hit.position, Vector3.up, Color.red, 1.0f);
                        possiblePos = hit.position;
                    }
                    yield return null;
                }
            }

            //Call the method callback
            generatedPositionHandler(possiblePos);
        }

        public void Initiate_TeleportAwayFromPlayer(Vector3 center, float range)
        {
            StopAllCoroutines();

            StartCoroutine(SearchNewPos(center, range, WarpAgentTo));
        }
        #endregion

        #region ENTITY_INTERACTIONS
        public void AttackInteraction(float damageValue)
        {
            if (GetDemonData().GetIsDead()) return;

            GetDemonAnimations().PlayGotHitAnimation();

            SubtractHealth(damageValue);
            isDead = CheckIfDead(EntityLife);
            maxAttackBeforeAutoTeleport--;

            if (maxAttackBeforeAutoTeleport < 0)
            {
                Initiate_TeleportAwayFromPlayer(target.position, 25f);
                maxAttackBeforeAutoTeleport = 2;
            }

            if (isDead)
            {
                UpdateNodeDataIsDead(isDead);

                GetDemonAnimations().PlayDeathAnimation();

                MoveLayerOnDeath();

                ManagerHUB.GetManager.GameEventsHandler.OnEnemyDeath();
                UpdateDatabaseEntry();
            }
        }

        public void ApplyStatusEffect(GameObject effect)
        {
            Instantiate<GameObject>(effect, transform);
        }

        void UpdateDatabaseEntry()
        {
            if (effectType == EffectType.Charmed)
            { ManagerHUB.GetManager.GameEventsHandler.OnEnemyEntryUpdate(MonsterIDs.CharmDemon); } // charmed demon
            else
            { ManagerHUB.GetManager.GameEventsHandler.OnEnemyEntryUpdate(MonsterIDs.ConfuseDemon); } // confused demon
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
            teleportCooldownCache = teleportCooldown;

            GetDemonData().SetTeleportCooldown(teleportCooldown * 2);
        }

        public void RemoveShockInteraction()
        {
            isShocked = false;

            GetDemonData().SetTeleportCooldown(teleportCooldownCache);
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
            StopAllCoroutines();

            GetDemonData().SetIsDead(value);
            isDead = GetDemonData().GetIsDead();
        }
        #endregion

        public StatusDemonAnimations GetDemonAnimations()
        {
            return ai_entityAnimations as StatusDemonAnimations;
        }

        StatusDemonNodeData GetDemonData()
        {
            return (StatusDemonNodeData)entityNodeData;
        }

        public StatusDemonAOEHandler GetAOEHandler() => AOEHandler;

        void WarpAgentTo(Vector3 newPos)
        {
            ai_agent.Warp(newPos);
        }

        public EffectType GetEffectType() => effectType;

        public GameObject GetCircleGraphic() => circleGraphic;
    }
}