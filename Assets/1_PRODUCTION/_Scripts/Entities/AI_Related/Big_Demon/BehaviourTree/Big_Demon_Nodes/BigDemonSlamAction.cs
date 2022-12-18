using UnityEngine;

namespace akb.Entities.AI.Implementations.Big_Demon
{
    /* [Node documentation]
     * 
     * [Must know]
     *  BigDemonNodeData compatible.
     */
    public class BigDemonSlamAction : AttackAction
    {
        BigDemonNodeData _data;

        bool playedChargeAnimation = false;

        public BigDemonSlamAction(BigDemonNodeData nodeData) : base(nodeData)
        {
            this.nodeData = nodeData;
            this._data = (BigDemonNodeData)nodeData;

            RefreshTimers(nodeData.GetSlamTime());
        }

        public override bool Run()
        {
            //Attack charge here
            if (!playedChargeAnimation)
            {
                _data.GetDemonAnimations().PlaySlamChargeAnimation();
                playedChargeAnimation = true;

                _data.SetCanRotate(false);
            }

            _data.GetEnemyEntity().GetSlamGraphic().transform.localScale += new Vector3(10 * Time.deltaTime, 10 * Time.deltaTime, 10 * Time.deltaTime);

            //Attack animation here
            currentTimer -= Time.deltaTime;
            if (currentTimer <= 0f)
            {
                playedChargeAnimation = false;

                _data.GetEnemyEntity().GetAttackHandler().InitiateAttack();

                _data.SetCanRotate(true);
                _data.SetCanMove(true);
                _data.SetIsSlamming(false);

                _data.GetDemonAnimations().PlayAttackAnimation();

                RefreshTimers(_data.GetSlamTime());

                _data.GetEnemyEntity().GetSlamGraphic().transform.localScale = Vector3.zero;
                return false;
            }

            return true;
        }
    }
}