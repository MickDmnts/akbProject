using UnityEngine;

namespace AKB.Entities.AI.Implementations.Status_Demon
{
    public class StatusDemonAOECountdown : RunAfterCountdown
    {
        StatusDemonNodeData _data;

        float attackThreshold = 0.5f;

        public StatusDemonAOECountdown(StatusDemonNodeData nodeData, INode nodeToRun) : base(nodeData, nodeToRun)
        {
            _data = nodeData;

            RefreshTimers(_data.GetTimeUntilTPout());
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

                    _data.GetEnemyEntity().GetCircleGraphic().transform.localScale = Vector3.zero;

                    return false;
                }
            }

            //Attack in intervals instead of attacking in Update() frequency
            //We want the attack to be as close to the interval as possible, thus the 0.01f value.
            if (currentTimer % attackThreshold <= 0.01f)
            {
                _data.GetEnemyEntity().GetAOEHandler().StartAttack();

                _data.GetEnemyEntity().GetCircleGraphic().transform.localScale = new Vector3(10, 10, 10);
            }
            else
            {
                _data.GetEnemyEntity().GetCircleGraphic().transform.localScale -= new Vector3(10 * Time.deltaTime, 10 * Time.deltaTime, 10 * Time.deltaTime);
            }

            return true;
        }
    }
}