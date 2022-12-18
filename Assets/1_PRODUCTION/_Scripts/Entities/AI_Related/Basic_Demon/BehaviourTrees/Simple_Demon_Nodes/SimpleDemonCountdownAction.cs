using UnityEngine;

namespace akb.Entities.AI.Implementations.Simple_Demon
{
    public sealed class SimpleDemonCountdownAction : RunAfterCountdown
    {
        public SimpleDemonCountdownAction(SimpleDemonNodeData nodeData, INode nodeToRun) : base(nodeData, nodeToRun)
        {
            this.nodeData = nodeData;

            RefreshTimers(nodeData.GetAttackCooldown());
        }

        public override bool Run()
        {
            currentTimer -= Time.deltaTime;
            if (currentTimer <= 0f)
            {
                //Deactivates movement
                (nodeData as SimpleDemonNodeData).SetCanMove(false);

                bool childRun = nodeToRun.Run();
                if (!childRun)
                {
                    RefreshTimers(initialTime);
                }
            }

            return true;
        }
    }
}