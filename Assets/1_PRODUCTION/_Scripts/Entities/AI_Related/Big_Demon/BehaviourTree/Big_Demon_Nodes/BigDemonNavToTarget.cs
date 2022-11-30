using UnityEngine;

namespace AKB.Entities.AI.Implementations.Big_Demon
{
    public class BigDemonNavToTarget : NavToTarget
    {
        new BigDemonNodeData nodeData;

        public BigDemonNavToTarget(BigDemonNodeData nodeData) : base(nodeData)
        {
            this.nodeData = nodeData;
        }

        public override bool Run()
        {
            if (!nodeData.GetCanMove()) return false;

            bool currentRun = true;

            nodeData.GetNavMeshAgent().SetDestination(nodeData.GetTarget().position);

            Vector3 pos1 = agent.transform.position;
            Vector3 pos2 = target.position;

            if ((pos1 - pos2).magnitude <= nodeData.GetChargeRange()
                || (pos1 - pos2).magnitude <= nodeData.GetChargeRange())
            {
                agent.isStopped = true;
                currentRun = false;
            }

            base.EvaluateRunState(currentRun);
            return currentRun;
        }

        protected override void OnNodeEntry()
        {
            agent.isStopped = false;
            previousRun = true;
        }

        protected override void OnNodeExit()
        {
            agent.isStopped = false;
            previousRun = false;
        }
    }
}