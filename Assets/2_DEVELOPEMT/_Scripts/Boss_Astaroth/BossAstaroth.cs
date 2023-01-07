using System;
using UnityEngine;
using UnityEngine.AI;

using akb.Core.Managing;
using akb.Core.Database.Monsters;
using akb.Entities.Interactions;

namespace akb.Entities.AI.Implementations.Astaroth
{
    public enum AstarothPhases
    {
        Phase1,
        Phase2,
        Phase3
    }

    public class BossAstaroth : AI_Entity, IInteractable
    {
        [SerializeField] GameObject projectilePrefab;
        [SerializeField] ParticleSystem flamePillarPs;
        [SerializeField] float phase1ProjectileRps = 3f;

        //BossAstarothAttackHandler attackHandler;
        Transform target;

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
            ai_entityAnimations = GetComponentInChildren<BossAstarothAnimations>();

            //attackHandler = GetComponentInChildren<BossAstarothAttackHandler>();
        }

        #region NODE_DATA_CREATION
        void Start()
        {
            target = ManagerHUB.GetManager.PlayerEntity.transform;

            entityNodeData = SetupNodeData<AstarothNodeData>();

            //CreateAppropriateBTHandler(out ai_BTHandler);
        }

        /// <summary>
        /// Call to create a BossAstarothNodeData instance and mutate the needed fields.
        /// </summary>
        /// <returns>A completely initialised BossAstarothNodeData instance.</returns>
        protected override T SetupNodeData<T>()
        {
            AstarothNodeData tempData = new T() as AstarothNodeData;

            //tempData.SetEnemyEntity(this);
            //tempData.SetEnemyAnimations(GetDemonAnimations());
            tempData.SetNavMeshAgent(ai_agent);

            tempData.SetIsStunned(false);

            tempData.SetCanRotate(true);
            tempData.SetTarget(target);

            return (T)Convert.ChangeType(tempData, typeof(T));
        }
        #endregion

        /* protected override void CreateAppropriateBTHandler(out IBehaviourTreeHandler handlerVar)
        {
            handlerVar = new AstarothNodeData(this, entityNodeData);
        } */

        protected override void CreateAppropriateBTHandler(out IBehaviourTreeHandler handlerVar)
        {
            throw new NotImplementedException();
        }

        //Sole purpose is to update the behaviour tree of the entity
        private void Update()
        {
            if (ai_BTHandler != null)
            {
                ai_BTHandler.UpdateBT();
            }
        }

        public void AttackInteraction(float damageValue)
        {
            if (GetDemonData().GetIsDead()) return;

            SubtractHealth(damageValue);
            //GetDemonAnimations().PlayGotHitAnimation();

            isDead = CheckIfDead(EntityLife);

            if (isDead)
            {
                UpdateNodeDataIsDead(isDead);
                //GetDemonAnimations().PlayDeathAnimation();

                MoveLayerOnDeath();

                //Notify subs for an agent death
                ManagerHUB.GetManager.GameEventsHandler.OnEnemyDeath();
                UpdateDatabaseEntry();
            }
        }

        public void ApplyStatusEffect(GameObject effect)
        {
            //nop...
        }

        void UpdateDatabaseEntry()
        {
            ManagerHUB.GetManager.GameEventsHandler.OnEnemyEntryUpdate(MonsterIDs.BossAstaroth);
        }

        protected override void UpdateNodeDataIsDead(bool value)
        {
            GetDemonData().SetIsDead(value);
        }

        public BossAstarothAnimations GetDemonAnimations()
        {
            return ai_entityAnimations as BossAstarothAnimations;
        }

        AstarothNodeData GetDemonData()
        {
            return (AstarothNodeData)entityNodeData;
        }
    }
}