namespace akb.Entities.AI.Implementations.Astaroth
{
    public class AstarothNodeData : AI_NodeData
    {
        #region MUTATORS
        public void SetEnemyEntity(BossAstaroth value) => ai_entity = value;
        public void SetEnemyAnimations(BossAstarothAnimations value) => ai_animations = value;
        #endregion

        #region ACCESSORS
        public BossAstaroth GetEnemyEntity() => ai_entity as BossAstaroth;
        public BossAstarothAnimations GetDemonAnimations() => ai_animations as BossAstarothAnimations;
        #endregion
    }
}
