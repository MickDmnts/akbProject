using UnityEngine;
using UnityEngine.AI;

namespace akb.Entities.AI
{
    /* [Node documentation]
     * 
     * [Must know]
     *  INodeData compatible.
     */
    public abstract class NavToTarget : INode
    {
        protected INodeData nodeData;

        protected NavMeshAgent agent;
        protected Transform target;

        protected bool previousRun = false;

        public NavToTarget(INodeData nodeData)
        {
            this.nodeData = nodeData;

            this.agent = nodeData.GetNavMeshAgent();
            this.target = nodeData.GetTarget();
        }

        public INodeData GetNodeData()
        {
            return nodeData;
        }

        public abstract bool Run();

        /// <summary>
        /// Call to call the OnNodeEntry() or OnNodeExit() methods based on current run value.
        /// </summary>
        /// <param name="currentRun"></param>
        protected virtual void EvaluateRunState(bool currentRun)
        {
            if (!previousRun && currentRun)
            {
                OnNodeEntry();
            }

            if (previousRun && !currentRun)
            {
                OnNodeExit();
            }
        }

        protected abstract void OnNodeEntry();

        protected abstract void OnNodeExit();
    }
}