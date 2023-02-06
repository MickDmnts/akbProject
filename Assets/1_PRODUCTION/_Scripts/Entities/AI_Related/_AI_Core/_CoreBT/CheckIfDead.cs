using System;

namespace AKB.Entities.AI
{
    /* [Node Documentation]
     * 
     * [Custom switch]
     * If the agent is not marked as dead through the node data, update the INode passed.
     * 
     * [Must know]
     * AI_NodeData compatible.
     */
    public class CheckIfDead : INode
    {
        protected INodeData nodeData;
        protected INode child;

        public CheckIfDead(INodeData nodeData, INode child)
        {
            this.nodeData = nodeData;
            this.child = child;
        }

        public INodeData GetNodeData()
        {
            return nodeData;
        }

        /// <summary>
        /// Call to update the passed child node ONLY if the agent in not marked as
        /// dead in the node data.
        /// </summary>
        /// <returns>The child .Run() value if the agent in not dead
        /// ELSE false.</returns>
        public virtual bool Run()
        {
            if (!(nodeData as AI_NodeData).GetIsDead())
            {
                return child.Run();
            }

            return false;
        }
    }
}