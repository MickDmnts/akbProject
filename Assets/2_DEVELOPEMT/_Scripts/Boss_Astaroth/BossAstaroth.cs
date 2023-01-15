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
        [Header("Astaroth specific")]
        [SerializeField] GameObject projectilePrefab;
        [SerializeField] float phase1ProjectileCd = 3f;
        [SerializeField] float phase2EnemiesCd = 5f;
        [SerializeField] float phase3ProjectileCd = 10f;

        [Header("GFXs")]
        [SerializeField] GameObject secondPhaseShield;

        [Header("Distance Check Specifics")]
        [SerializeField] float maxDistanceFromTarget;

        AstarothAttackHandler attackHandler;
        Transform target;

        float maxHealth;
        bool canTakeDamage = false;
        GameObject activeShield;

        private void Awake()
        {
            ManagerHUB.GetManager.GameEventsHandler.onAllRocksBroken += EnableTakeDamage;
            ManagerHUB.GetManager.GameEventsHandler.onAllRocksBroken += DisableSecondPhaseShield;

            CacheNeededComponents();

            maxHealth = EntityLife;
            canTakeDamage = true;
        }

        /// <summary>
        /// Call to cache the gameobject needed components.
        /// </summary>
        void CacheNeededComponents()
        {
            ai_agent = GetComponent<NavMeshAgent>();
            ai_entityAnimations = GetComponentInChildren<BossAstarothAnimations>();

            attackHandler = GetComponentInChildren<AstarothAttackHandler>();
        }

        #region NODE_DATA_CREATION
        void Start()
        {
            target = ManagerHUB.GetManager.PlayerEntity.transform;

            entityNodeData = SetupNodeData<AstarothNodeData>();

            CreateAppropriateBTHandler(out ai_BTHandler);
        }

        /// <summary>
        /// Call to create a BossAstarothNodeData instance and mutate the needed fields.
        /// </summary>
        /// <returns>A completely initialised BossAstarothNodeData instance.</returns>
        protected override T SetupNodeData<T>()
        {
            AstarothNodeData tempData = new T() as AstarothNodeData;

            tempData.SetEnemyEntity(this);
            tempData.SetEnemyAnimations(GetDemonAnimations());
            tempData.SetNavMeshAgent(ai_agent);

            //Add more things here
            tempData.SetCurrentPhase(AstarothPhases.Phase1);

            tempData.SetMaxDistance(maxDistanceFromTarget);
            tempData.SetCanRotate(true);
            tempData.SetTarget(target);

            return (T)Convert.ChangeType(tempData, typeof(T));
        }
        #endregion

        protected override void CreateAppropriateBTHandler(out IBehaviourTreeHandler handlerVar)
        {
            handlerVar = new AstarothBTHandler(this, entityNodeData);
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

            if (!canTakeDamage) { return; }

            SubtractHealth(damageValue);
            GetDemonAnimations().PlayGotHitAnimation();

            //Updates the bosses phase if the health is in phase change.
            CheckForPhaseChange();

            isDead = CheckIfDead(EntityLife);

            if (isDead)
            {
                UpdateNodeDataIsDead(isDead);
                GetDemonAnimations().PlayDeathAnimation();

                MoveLayerOnDeath();

                //DO EXTRA STUFF HERE 
                //REWARD SOULS AND OPEN TRANSISTOR.

                //Notify subs for an agent death
                ManagerHUB.GetManager.GameEventsHandler.OnEnemyDeath();
                UpdateDatabaseEntry();
            }
        }

        ///<summary>Changes the phase of the boss based on its health value
        /// <para>Phase 1: Max Helth - 75%</para>
        /// <para>Phase 2: 74% - 25%</para>
        /// <para>Phase 3: 25% - death</para>
        /// </summary>
        void CheckForPhaseChange()
        {
            float currentLifePercent = (EntityLife / maxHealth) * 100;

            if (currentLifePercent < 75 && currentLifePercent > 25 && GetDemonData().GetCurrentPhase() == AstarothPhases.Phase1)
            {
                GetDemonData().SetCurrentPhase(AstarothPhases.Phase2);
                ManagerHUB.GetManager.GameEventsHandler.OnAstarothSecondPhase();

                canTakeDamage = false;

                activeShield = Instantiate(secondPhaseShield, transform.position + new Vector3(0f, 3f, 0f), Quaternion.identity);
            }
            else if (currentLifePercent <= 25 && GetDemonData().GetCurrentPhase() == AstarothPhases.Phase2)
            {
                GetDemonData().SetCurrentPhase(AstarothPhases.Phase3);
                ManagerHUB.GetManager.GameEventsHandler.OnAstarothThirdPhase();
                Debug.Log("Changed to phase 3");
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

        void EnableTakeDamage() => canTakeDamage = true;

        void DisableSecondPhaseShield() => Destroy(activeShield);
        #endregion

        public BossAstarothAnimations GetDemonAnimations()
        {
            return ai_entityAnimations as BossAstarothAnimations;
        }

        public AstarothAttackHandler GetAttackHandler() => attackHandler;

        AstarothNodeData GetDemonData()
        {
            return (AstarothNodeData)entityNodeData;
        }

        private void OnDestroy()
        {
            ManagerHUB.GetManager.GameEventsHandler.onAllRocksBroken -= EnableTakeDamage;
            ManagerHUB.GetManager.GameEventsHandler.onAllRocksBroken -= DisableSecondPhaseShield;
        }
    }
}