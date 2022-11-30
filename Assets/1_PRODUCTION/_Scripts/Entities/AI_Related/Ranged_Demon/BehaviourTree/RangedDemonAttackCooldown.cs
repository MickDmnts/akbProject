using UnityEngine;

namespace AKB.Entities.AI.Implementations.Ranged_Demon
{
    public class RangedDemonAttackCooldown : RunAfterCountdown
    {
        RangedDemonNodeData _data;

        public RangedDemonAttackCooldown(RangedDemonNodeData nodeData, INode nodeToRun) : base(nodeData, nodeToRun)
        {
            _data = nodeData;

            RefreshTimers(nodeData.GetAttackCooldown());
        }

        public override bool Run()
        {
            currentTimer -= Time.deltaTime;
            if (currentTimer <= 0)
            {
                bool result = nodeToRun.Run();
                if (!result)
                {
                    RefreshTimers(initialTime);

                    _data.GetEnemyEntity().GetLineGraphic().transform.localScale = Vector3.one;
                }
            }

            _data.GetEnemyEntity().GetLineGraphic().transform.localScale += new Vector3(0f, 10 * Time.deltaTime, 0f);

            //Soft-locks node update
            return true;
        }
    }
}