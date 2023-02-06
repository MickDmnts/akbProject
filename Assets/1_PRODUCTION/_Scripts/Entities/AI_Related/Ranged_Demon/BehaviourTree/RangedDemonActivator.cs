namespace AKB.Entities.AI.Implementations.Ranged_Demon
{
    public sealed class RangedDemonActivator : INode
    {
        RangedDemonNodeData nodeData;
        INode nodeToRun;

        public RangedDemonActivator(RangedDemonNodeData nodeData, INode nodeToRun)
        {
            this.nodeData = nodeData;
            this.nodeToRun = nodeToRun;
        }

        public INodeData GetNodeData()
        {
            return nodeData;
        }

        public bool Run()
        {
            if (!nodeData.GetSearchingForPos())
            {
                return nodeToRun.Run();
            }

            //Locks node update.
            return true;
        }
    }
}