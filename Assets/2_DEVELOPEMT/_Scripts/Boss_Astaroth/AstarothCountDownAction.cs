using UnityEngine;

namespace akb.Entities.AI.Implementations.Astaroth
{
    public sealed class AstarothCountDownAction : RunAfterCountdown
    {
        public AstarothCountDownAction(AstarothNodeData nodeData, INode nodeToRun) : base(nodeData, nodeToRun)
        {
            this.nodeData = nodeData;

            RefreshTimers(nodeData.GetAttackCooldown());
        }
        public override bool Run()
        {
            currentTimer -= Time.deltaTime;
            if (currentTimer <= 0f)
            {
                //Deactivates rotation
                (nodeData as AstarothNodeData).SetCanRotate(false);

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
