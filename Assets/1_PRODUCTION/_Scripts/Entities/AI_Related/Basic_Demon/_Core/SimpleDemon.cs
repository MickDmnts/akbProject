using System;
using UnityEngine;
using UnityEngine.AI;

namespace akb.Entities.AI.Implementations.Simple_Demon
{
    using akb.Core.Database.Monsters;
    using akb.Core.Managing;

    using akb.Entities.Interactions;
    using Fire_Demon;

    /// <summary>
    /// The available simple demon variations.
    /// </summary>
    public enum SimpleDemonType
    {
        SimpleDemon = 0,
        FireDemon = 1,
    }

    public class SimpleDemon : AI_Entity, IInteractable,
        IStunnable, IShockable
    {
        [Header("Set in inspector")]
        [SerializeField] SimpleDemonType demonType;

        [SerializeField] GameObject coneGraphic;
        [SerializeField] float timeUntilAttack;
        [SerializeField] float attackCooldown;
        [SerializeField] float attackRange;

        [Header("Fire Demon Settings")]
        [SerializeField] GameObject circleGraphic;
        [SerializeField] float fireAoeRadious = 7f;

        SimpleDemonAttackHandler attackHandler;
        Transform target;

        bool isShocked = false;
        float agentSpeedCache;
        float attackSpeedCache;

        private void Awake()
        {
            CacheNeededComponents();

            agentSpeedCache = ai_agent.speed;
            attackSpeedCache = attackCooldown;
        }

        /// <summary>
        /// Call to cache the gameobject needed components.
        /// </summary>
        void CacheNeededComponents()
        {
            ai_agent = GetComponent<NavMeshAgent>();
            ai_entityAnimations = GetComponentInChildren<SimpleDemonAnimations>();

            attackHandler = GetComponentInChildren<SimpleDemonAttackHandler>();
        }

        #region NODE_DATA_CREATION
        void Start()
        {
            target = ManagerHUB.GetManager.PlayerEntity.transform;

            entityNodeData = SetupNodeData<SimpleDemonNodeData>();

            CreateAppropriateBTHandler(out ai_BTHandler);
        }

        /// <summary>
        /// Call to create a SimpleDemonNodeData instance and mutate the needed fields.
        /// </summary>
        /// <returns>A completely initialised SimpleDemonNodeData instance.</returns>
        protected override T SetupNodeData<T>()
        {
            SimpleDemonNodeData tempData = new T() as SimpleDemonNodeData;

            tempData.SetEnemyEntity(this);
            tempData.SetEnemyAnimations(GetDemonAnimations());
            tempData.SetNavMeshAgent(ai_agent);

            tempData.SetIsStunned(false);

            tempData.SetTimeUntilAttack(timeUntilAttack);
            tempData.SetAttackCooldown(attackCooldown);

            tempData.SetAttackRange(attackRange);
            tempData.SetCanMove(true);
            tempData.SetCanRotate(true);
            tempData.SetTarget(target);

            return (T)Convert.ChangeType(tempData, typeof(T));
        }
        #endregion

        /// <summary>
        /// Call to create the corresponding Simple demon variant based on demonType passed
        /// and assign it to the ai_BTHandler variable.
        /// </summary>
        protected override void CreateAppropriateBTHandler(out IBehaviourTreeHandler handlerVar)
        {
            switch (demonType)
            {
                case SimpleDemonType.SimpleDemon:
                    handlerVar = new SimpleDemonBTHandler(this, entityNodeData);
                    break;

                case SimpleDemonType.FireDemon:
                    handlerVar = new FireDemonBTHandler(this, entityNodeData);
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

        #region ENTITY_INTERACTIONS
        public void AttackInteraction(float damageValue)
        {
            if (GetDemonData().GetIsDead()) return;

            SubtractHealth(damageValue);
            GetDemonAnimations().PlayGotHitAnimation();

            isDead = CheckIfDead(EntityLife);

            if (isDead)
            {
                UpdateNodeDataIsDead(isDead);
                GetDemonAnimations().PlayDeathAnimation();

                MoveLayerOnDeath();

                Destroy(coneGraphic);

                GameObject gfx = Instantiate(deathGFX);
                gfx.transform.position = transform.position;

                if (ManagerHUB.GetManager.RoomSelector.CurrentRoomGO != null)
                { gfx.transform.SetParent(ManagerHUB.GetManager.RoomSelector.CurrentRoomGO.transform); }

                //Notify subs for an agent death
                ManagerHUB.GetManager.GameEventsHandler.OnEnemyDeath();
                ManagerHUB.GetManager.GameEventsHandler.OnCoinReceive(coinsOnDeath);
                UpdateDatabaseEntry();
            }
        }

        public void ApplyStatusEffect(GameObject effect)
        {
            GameObject gfx = Instantiate<GameObject>(effect, transform);
        }

        void UpdateDatabaseEntry()
        {
            if (demonType == SimpleDemonType.SimpleDemon)
            { ManagerHUB.GetManager.GameEventsHandler.OnEnemyEntryUpdate(MonsterIDs.BasicDemon); } //Simple demon
            else
            { ManagerHUB.GetManager.GameEventsHandler.OnEnemyEntryUpdate(MonsterIDs.FireDemon); } //Fire demon
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

            GetDemonData().SetAttackCooldown(attackCooldown * 2);
            ai_agent.speed /= 2;
        }

        public void RemoveShockInteraction()
        {
            isShocked = false;

            GetDemonData().SetAttackCooldown(attackSpeedCache);
            ai_agent.speed = agentSpeedCache;
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

        public Transform GetConeGraphicTransform() => coneGraphic.transform;

        //Fire demon specific
        public void FireDemonDeathAction()
        {
            circleGraphic.transform.localScale = new Vector3(fireAoeRadious, fireAoeRadious, fireAoeRadious);

            //PLAY EXPLOSION ANIMATION
            //SPAWN FIRE PARTICLES
            //DO A FOREACH PLAYER HIT -> CALL ENFLAMED STATUS EFFECT ON PLAYER
        }

        public SimpleDemonAnimations GetDemonAnimations()
        {
            return ai_entityAnimations as SimpleDemonAnimations;
        }

        public SimpleDemonAttackHandler GetAttackHandler() => attackHandler;

        SimpleDemonNodeData GetDemonData()
        {
            return (SimpleDemonNodeData)entityNodeData;
        }
    }
}