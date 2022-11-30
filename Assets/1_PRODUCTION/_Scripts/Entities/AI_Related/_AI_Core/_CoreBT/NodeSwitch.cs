namespace AKB.Entities.AI.Implementations
{
    public abstract class NodeSwitch : INode
    {
        protected INodeData nodeData;
        protected INode[] nodesToRun;

        protected bool switchCondition = false;

        public NodeSwitch(INodeData nodeData, INode[] nodesToRun)
        {
            this.nodeData = nodeData;
            this.nodesToRun = nodesToRun;
        }

        public INodeData GetNodeData()
        {
            return nodeData;
        }

        public abstract bool Run();
    }
}