namespace akb.Entities.AI.Implementations
{
    /* [CLASS DOCUMENTATION]
     *
     * The AI_EntityBTHandler abstract class will be used and stored inside AI_Entities to create
     * and handle the update of the behaviour tree.
     */
    public abstract class AI_EntityBTHandler : IBehaviourTreeHandler
    {
        protected AI_Entity ai_parentEntity;

        protected INodeData ai_nodeData;
        protected INode ai_entryNode;

        protected abstract void CreateBehaviourTree(ref INode entryNode);

        public abstract void UpdateBT();

        protected virtual T GetParentEntity<T>() where T : class
        {
            return ai_parentEntity as T;
        }

        public virtual T GetDemonNodeData<T>() where T : class, INodeData
        {
            return ai_nodeData as T;
        }
    }
}