using UnityEngine;

namespace AKB.Entities.AI.Implementations
{
    /* [Node documentation]
     * 
     * [Must know]
     *  INodeData compatible.
     */
    public abstract class AttackAction : INode
    {
        protected INodeData nodeData;

        protected float initialTime;
        protected float currentTimer;

        public AttackAction(INodeData nodeData)
        {
            this.nodeData = nodeData;
        }

        protected virtual void RefreshTimers(float initialTime)
        {
            this.initialTime = initialTime;
            this.currentTimer = this.initialTime;
        }

        public INodeData GetNodeData() => nodeData;

        public abstract bool Run();
    }
}
