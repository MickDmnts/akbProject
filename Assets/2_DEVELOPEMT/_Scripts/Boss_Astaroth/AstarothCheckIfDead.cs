namespace akb.Entities.AI
{
    public class AstarothCheckIfDead : INode
    {
        protected INodeData nodeData;
        protected INode child;

        public AstarothCheckIfDead(INodeData nodeData,INode child)
        {
            this.nodeData = nodeData;
            this.child = child;
        }

        public INodeData GetNodeData()
        {
            return nodeData;
        }

        public bool Run()
        {
            if(!(nodeData as AI_NodeData).GetIsDead())
            {
                return child.Run();
            }
            return false;
        }
    }
}
