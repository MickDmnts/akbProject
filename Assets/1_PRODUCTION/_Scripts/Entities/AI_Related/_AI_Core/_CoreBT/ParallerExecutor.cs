namespace AKB.Entities.AI.Implementations
{
    public class ParallerExecutor : INode
    {
        INode[] nodesToRun;
        INodeData nodeData;

        public ParallerExecutor(INode[] nodesToRun, INodeData nodeData)
        {
            this.nodesToRun = nodesToRun;
            this.nodeData = nodeData;
        }

        public INodeData GetNodeData()
        {
            return nodeData;
        }

        public bool Run()
        {
            bool currentRun = false;

            foreach (INode node in nodesToRun)
            {
                currentRun = node.Run();
            }

            return currentRun;
        }
    }
}