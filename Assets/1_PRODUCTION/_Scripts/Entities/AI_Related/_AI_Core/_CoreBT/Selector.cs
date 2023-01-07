namespace akb.Entities.AI.Implementations
{
    /* [Node documentation]
     * 
     * [Custom Selector]
     * Updates the nodeOne implementation continuously until it returns false, and then returns the return value of the nodeTwo Run method.
     * 
     * [Must know]
     *  AI_NodeData compatible.
     */
    public class Selector : INode
    {
        protected INodeData nodeData;

        protected INode nodeOne;
        protected INode nodeTwo;

        public Selector(INodeData nodeData, INode nodeOne, INode nodeTwo)
        {
            this.nodeData = nodeData;
            this.nodeOne = nodeOne;
            this.nodeTwo = nodeTwo;
        }

        public INodeData GetNodeData()
        {
            return nodeData;
        }

        /// <summary>
        /// If the NavToTarget Run method returns false, calls and returns the AttackTargetAction Run method value.
        /// <para>Returns false if none of the above apply.</para>
        /// </summary>
        public virtual bool Run()
        {
            if (!nodeOne.Run())
            {
                return nodeTwo.Run();
            }

            return false;
        }
    }
}
