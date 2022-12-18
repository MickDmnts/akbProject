using UnityEngine;
using UnityEngine.AI;

namespace akb.Entities.AI
{
    /*
     * This interface is used to ensure type safety of the classes passed throughout the 
     * Behaviour trees.
     */
    public interface INodeData
    {
        public void SetNavMeshAgent(NavMeshAgent agent);
        public void SetTarget(Transform value);

        public NavMeshAgent GetNavMeshAgent();
        public Transform GetTarget();
        public Vector3 GetAgentPosition();
    }
}
