namespace akb.Entities.AI.Implementations.Astaroth
{
    public class AstarothNodeData : AI_NodeData
    {
        AstarothPhases bossPhase;
        float timeUntilAttack;
        float attackCooldown;
        float maxDistanceFromTarget;


        #region MUTATORS
        public void SetMaxDistance(float value) => maxDistanceFromTarget = value;
        public void SetEnemyEntity(BossAstaroth value) => ai_entity = value;
        public void SetEnemyAnimations(BossAstarothAnimations value) => ai_animations = value;
        public void SetTimeUntilAttack(float time) => timeUntilAttack = time;
        public void SetCurrentPhase(AstarothPhases currentPhase) => bossPhase = currentPhase;
        public void SetAttackCooldown(float time) => attackCooldown = time;
        #endregion

        #region ACCESSORS
        public BossAstaroth GetEnemyEntity() => ai_entity as BossAstaroth;
        public BossAstarothAnimations GetDemonAnimations() => ai_animations as BossAstarothAnimations;
        public float GetTimeUntilAttack() =>timeUntilAttack;
        public float GetAttackCooldown() => attackCooldown;
        public AstarothPhases GetCurrentPhase() => bossPhase;
        public float GetMaxDistance() => maxDistanceFromTarget;
        #endregion
    }
}
