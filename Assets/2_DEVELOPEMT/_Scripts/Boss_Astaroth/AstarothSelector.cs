namespace akb.Entities.AI.Implementations
{
    public class AstarothSelector : INode
    {
        protected INodeData nodeData;
        protected INode Phase1;
        protected INode Phase2;
        protected INode Phase3;

        public AstarothSelector(INodeData nodeData,INode nodeOne,INode nodeTwo,INode nodeThree)
        {
            this.nodeData = nodeData;
            this.Phase1 = nodeOne;
            this.Phase2 = nodeTwo;
            this.Phase3 = nodeThree;
        }
        public INodeData GetNodeData()
        {
            return nodeData;
        }

        public bool Run()
        {
            if (!Phase1.Run())
            {
                return Phase2.Run();
            }
            if (!Phase2.Run())
            {
                return Phase3.Run();
            }
            if (!Phase3.Run())
            {
                return false;
            }
            return false;
        }
    }
}