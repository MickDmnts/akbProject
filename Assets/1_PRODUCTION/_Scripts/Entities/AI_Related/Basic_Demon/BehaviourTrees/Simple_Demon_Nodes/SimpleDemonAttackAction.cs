using UnityEngine;

namespace akb.Entities.AI.Implementations.Simple_Demon
{
    /* [Node documentation]
     * 
     * [Must know]
     *  SimpleDemonNodeData compatible.
     */
    public class SimpleDemonAttackAction : AttackAction
    {
        bool playedChargeAnimation = false;

        SimpleDemonNodeData _data;

        public SimpleDemonAttackAction(SimpleDemonNodeData nodeData) : base(nodeData)
        {
            this.nodeData = nodeData;
            this._data = (SimpleDemonNodeData)nodeData;

            RefreshTimers(nodeData.GetTimeUntilAttack());
        }

        public override bool Run()
        {
            //Attack charge here
            if (!playedChargeAnimation)
            {
                _data.GetDemonAnimations().PlayChargeAnimation();
                playedChargeAnimation = true;

                _data.SetCanRotate(false);
            }

            //Increase cone attack graphic
            _data.GetEnemyEntity().GetConeGraphicTransform().localScale += new Vector3(10 * Time.deltaTime, 10 * Time.deltaTime, 10 * Time.deltaTime);

            //Attack animation here
            currentTimer -= Time.deltaTime;
            if (currentTimer <= 0f)
            {
                //Attack here
                _data.GetEnemyEntity().GetAttackHandler().InitiateAttack();

                _data.SetCanRotate(true);
                _data.SetCanMove(true);

                _data.GetDemonAnimations().PlayAttackAnimation();

                playedChargeAnimation = false;

                RefreshTimers(_data.GetTimeUntilAttack());

                _data.GetEnemyEntity().GetConeGraphicTransform().localScale = Vector3.zero;
                return false;
            }

            return true;
        }
    }
}