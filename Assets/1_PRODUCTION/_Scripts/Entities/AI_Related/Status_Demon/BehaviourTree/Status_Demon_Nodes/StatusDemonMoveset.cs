using UnityEngine;

namespace akb.Entities.AI.Implementations.Status_Demon
{
    public class StatusDemonMoveset : NavToTarget
    {
        StatusDemonNodeData _data;

        public StatusDemonMoveset(StatusDemonNodeData nodeData) : base(nodeData)
        {
            this._data = nodeData;
        }

        public override bool Run()
        {
            //Forces the branch to return false.
            if (!_data.GetCanMove() || !_data.GetCanTeleport()) return false;

            bool currentRun = true;
            Vector3 targetPos = _data.GetTarget().transform.position;
            Vector3 agentPos = nodeData.GetAgentPosition();

            _data.GetNavMeshAgent().SetDestination(targetPos);

            if ((targetPos - agentPos).magnitude <= _data.GetMaxDistanceFromTarget())
            {
                _data.SetCanTeleport(false);
                _data.SetCanMove(false);

                //The demon is close to the target so move near him.
                _data.GetEnemyEntity().Initiate_TeleportCloseToPlayer(_data.GetTarget().position, 5f);

                currentRun = false;
            }

            EvaluateRunState(currentRun);
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
