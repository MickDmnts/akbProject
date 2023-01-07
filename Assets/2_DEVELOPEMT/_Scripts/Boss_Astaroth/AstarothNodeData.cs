namespace akb.Entities.AI.Implementations.Astaroth
{
    public class AstarothNodeData : AI_NodeData
    {
        public void SetEnemyEntity(BossAstaroth value) => ai_entity = value;
        public void SetEnemyAnimations(BossAstarothAnimations value) => ai_animations = value;
        public BossAstaroth GetEnemyEntity() => ai_entity as BossAstaroth;
        public BossAstarothAnimations GetDemonAnimations() => ai_animations as BossAstarothAnimations
    }
}
