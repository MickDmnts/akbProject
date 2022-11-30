namespace AKB.Entities.AI.Implementations
{
    public class IsStunnedSelector : INode
    {
        AI_NodeData nodeData;

        INode nextNode;

        public IsStunnedSelector(AI_NodeData nodeData, INode nextNode)
        {
            this.nodeData = nodeData;
            this.nextNode = nextNode;
        }

        public INodeData GetNodeData()
        {
            return nodeData;
        }

        public bool Run()
        {
            if (nodeData.GetIsStunned())
            {
                return false;
            }

            return nextNode.Run();
        }
    }
}