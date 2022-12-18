using UnityEngine;

namespace akb.Entities.AI.Implementations.Ranged_Demon
{
    public class RangedDemonMoveset : NavToTarget
    {
        float initialTime = 2f;
        float currentTimer = 0f;

        RangedDemonNodeData _data;

        public RangedDemonMoveset(INodeData nodeData) : base(nodeData)
        {
            this._data = (RangedDemonNodeData)nodeData;
        }

        public override bool Run()
        {
            //this runs outcome
            bool currentRun = true;

            //Cache positions.
            Vector3 targetPos = nodeData.GetTarget().transform.position;
            Vector3 agentPos = nodeData.GetAgentPosition();

            //Jump away only if the agent in not actively searching for another pos.
            if ((targetPos - agentPos).magnitude <= _data.GetMinTargetDistance()
                && !_data.GetSearchingForPos())
            {
                currentTimer -= Time.deltaTime;
                if (currentTimer <= 0f)
                {
                    currentTimer = initialTime;

                    //Initiate jumping sequence
                    _data.SetSearchingForPos(true);
                    _data.GetEnemyEntity().SearchNewPos(_data.GetMaxJumpDistance());
                    currentRun = false;
                }
            }

            EvaluateRunState(currentRun);
            return currentRun;
        }

        protected override void OnNodeEntry()
        {
            previousRun = true;
        }

        protected override void OnNodeExit()
        {
            previousRun = false;
        }
    }
}