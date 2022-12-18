using System;
using UnityEngine;

namespace akb.Entities.AI.Implementations.Big_Demon
{
    public sealed class BigDemonCountdownAction : RunAfterCountdown
    {
        Action<bool> methodCallOnTimerStart;

        BigDemonNodeData _data;

        public BigDemonCountdownAction(BigDemonNodeData nodeData, INode nodeToRun, Action<bool> methodCallOnTimerStart = null) : base(nodeData, nodeToRun)
        {
            this._data = (BigDemonNodeData)nodeData;
            this.methodCallOnTimerStart = methodCallOnTimerStart;

            RefreshTimers(nodeData.GetChargeCooldown());
        }

        public override bool Run()
        {
            methodCallOnTimerStart(true);
            currentTimer -= Time.deltaTime;
            if (currentTimer <= 0f)
            {
                //Deactivates movement
                _data.SetCanMove(false);

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