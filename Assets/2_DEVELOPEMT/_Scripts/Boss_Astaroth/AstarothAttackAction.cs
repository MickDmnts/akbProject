using UnityEngine;

namespace akb.Entities.AI.Implementations.Astaroth
{


    public class AstarothAttackAction : AttackAction
    {
        bool playedAttackAnimation = false;
        AstarothNodeData _data;
        public AstarothAttackAction(AstarothNodeData nodeData) : base(nodeData)
        {
            this.nodeData = nodeData;
            this._data = (AstarothNodeData)nodeData;

            RefreshTimers(nodeData.GetTimeUntilAttack());
        }
        public override bool Run()
        {
            if (!playedAttackAnimation)
            {
                _data.GetDemonAnimations().PlayAttackAnimation();
                playedAttackAnimation = true;

                _data.SetCanRotate(false);
            }

            if(currentTimer <= 0f)
            {
                _data.GetEnemyEntity().GetAttackHandler().InitiateAttack();

                _data.SetCanRotate(true);
                _data.SetCanMove(true);

                _data.GetDemonAnimations().PlayAttackAnimation();

                playedAttackAnimation = false;

                RefreshTimers(_data.GetTimeUntilAttack());

                return false;
            }
            return true;
        }
    }
}
