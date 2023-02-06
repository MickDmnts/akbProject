using UnityEngine;

namespace AKB.Entities.AI.Implementations
{
    public class FaceTargetAction : INode
    {
        INodeData nodeData;
        Transform target;

        public FaceTargetAction(INodeData nodeData, Transform target)
        {
            this.nodeData = nodeData;
            this.target = target;
        }

        public INodeData GetNodeData()
        {
            return nodeData;
        }

        public bool Run()
        {
            if (!(nodeData as AI_NodeData).GetCanRotate()) return false;

            Quaternion rotation = Quaternion.LookRotation(target.position - nodeData.GetNavMeshAgent().transform.position);

            Quaternion newRot = Quaternion.Slerp(nodeData.GetNavMeshAgent().transform.rotation, rotation, 10f * Time.deltaTime);

            nodeData.GetNavMeshAgent().transform.rotation = newRot;

            return true;
        }
    }
}