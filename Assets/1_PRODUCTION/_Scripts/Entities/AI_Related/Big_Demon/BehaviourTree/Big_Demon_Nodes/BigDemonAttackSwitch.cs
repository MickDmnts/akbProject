using UnityEngine;

namespace AKB.Entities.AI.Implementations.Big_Demon
{
    public class BigDemonAttackSwitch : NodeSwitch
    {
        INode rushNode;
        INode slamNode;

        INode nodeToRun;

        public BigDemonAttackSwitch(BigDemonNodeData nodeData, INode[] nodesToRun) : base(nodeData, nodesToRun)
        {
            this.nodeData = nodeData;

            if (nodesToRun.Length <= 0 || nodesToRun.Length < 2)
            {
                throw new System.ArgumentException("NodesToRun can't be empty or smaller than 2.");
            }
            else
            {
                rushNode = nodesToRun[0];
                slamNode = nodesToRun[1];
            }
        }

        public override bool Run()
        {
            Vector3 agentPos = (nodeData as AI_NodeData).GetAgentPosition();
            Vector3 targetPos = nodeData.GetTarget().position;

            float distanceToTarget = (agentPos - targetPos).magnitude;

            if (distanceToTarget <= (nodeData as BigDemonNodeData).GetSlamRange())
            {
                nodeToRun = slamNode;
            }
            else
            {
                if (!(nodeData as BigDemonNodeData).GetIsSlamming())
                {
                    nodeToRun = rushNode;
                }
                else
                {
                    nodeToRun = slamNode;
                }
            }

            return nodeToRun.Run();
        }
    }
}