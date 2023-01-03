namespace akb.Entities.AI.Implementations.Simple_Demon
{
    /* [CLASS DOCUMENTATION]
     *
     * The SimpleDemonNodeData implementation of AI_NodeData adds extra needed data for the SimpleDemon AI Entity to work.
     * 
     * Stored in: SimpleDemon.cs
     */
    [System.Serializable]
    public class SimpleDemonNodeData : AI_NodeData
    {
        float timeUntilAttack;
        float attackCooldown;
        float attackRange;

        #region MUTATORS
        public void SetEnemyEntity(SimpleDemon value) => ai_entity = value;
        public void SetEnemyAnimations(SimpleDemonAnimations value) => ai_animations = value;

        public void SetTimeUntilAttack(float time) => timeUntilAttack = time;
        public void SetAttackCooldown(float time) => attackCooldown = time;
        public void SetAttackRange(float value) => attackRange = value;
        #endregion

        #region ACCESSORS
        public SimpleDemon GetEnemyEntity() => ai_entity as SimpleDemon;
        public SimpleDemonAnimations GetDemonAnimations() => ai_animations as SimpleDemonAnimations;

        public float GetAttackCooldown() => attackCooldown;
        public float GetTimeUntilAttack() => timeUntilAttack;
        public float GetAttackRange() => attackRange;
        #endregion
    }
}