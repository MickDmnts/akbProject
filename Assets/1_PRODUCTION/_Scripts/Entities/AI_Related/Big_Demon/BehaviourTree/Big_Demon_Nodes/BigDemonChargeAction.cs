using System;
using UnityEngine;

namespace akb.Entities.AI.Implementations.Big_Demon
{
    public sealed class BigDemonChargeAction : AttackAction
    {
        BigDemonNodeData _data;

        bool playedChargeAnimation = false;

        Action rushCallback;

        public BigDemonChargeAction(BigDemonNodeData nodeData, Action rushCallback) : base(nodeData)
        {
            this.nodeData = nodeData;
            this._data = (BigDemonNodeData)nodeData;
            this.rushCallback = rushCallback;

            RefreshTimers(nodeData.GetChargeDuration());
        }

        public override bool Run()
        {
            //Attack charge here
            if (!playedChargeAnimation)
            {
                //Initiate rush animation
                _data.GetDemonAnimations().PlayChargeAnimation();
                playedChargeAnimation = true;

                _data.SetCanRotate(false);

                //Push entity and activate FOV in front of it
                rushCallback();
            }

            //Attack animation here
            currentTimer -= Time.deltaTime;
            if (currentTimer <= 0f)
            {
                _data.GetDemonAnimations().ResetAttackVariables();

                playedChargeAnimation = false;

                RefreshTimers(_data.GetChargeDuration());
                return false;
            }

            return true;
        }
    }
}