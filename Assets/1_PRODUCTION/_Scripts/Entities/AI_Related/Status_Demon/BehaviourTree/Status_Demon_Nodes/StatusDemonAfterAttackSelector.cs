using UnityEngine;

namespace AKB.Entities.AI.Implementations.Status_Demon
{
    public class StatusDemonAfterAttackSelector : Selector
    {
        StatusDemonNodeData _data;

        float distanceThreshold = 4f;

        public StatusDemonAfterAttackSelector(StatusDemonNodeData nodeData, INode nodeOne, INode nodeTwo) : base(nodeData, nodeOne, nodeTwo)
        {
            _data = nodeData;
        }

        public override bool Run()
        {
            Vector3 agentPos = _data.GetAgentPosition();
            Vector3 targetPos = _data.GetTarget().position;

            if ((agentPos - targetPos).magnitude <= distanceThreshold)
            {
                //Teleport away
                nodeOne.Run();
                return false;
            }

            //reset AOE
            nodeTwo.Run();
            return false;
        }
    }
}