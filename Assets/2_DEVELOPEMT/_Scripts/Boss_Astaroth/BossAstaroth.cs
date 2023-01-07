using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using akb.Entities.Interactions;

namespace akb.Entities.AI.Implementations.Astaroth
{
    public class BossAstaroth : AI_Entity, IInteractable
    {
        public void AttackInteraction(float damageValue)
        {
            throw new System.NotImplementedException();
        }

        public void ApplyStatusEffect(GameObject effect)
        {
            //nop...
        }

        protected override void CreateAppropriateBTHandler(out IBehaviourTreeHandler handlerVar)
        {
            throw new System.NotImplementedException();
        }

        protected override T SetupNodeData<T>()
        {
            throw new System.NotImplementedException();
        }

        protected override void UpdateNodeDataIsDead(bool value)
        {
            throw new System.NotImplementedException();
        }
    }
}