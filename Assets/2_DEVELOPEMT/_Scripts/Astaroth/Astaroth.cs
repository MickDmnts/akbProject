using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace akb.Entities.AI.Implementations.Astaroth
{
    using akb.Core.Database.Monsters;
    using akb.Core.Managing;

    using akb.Entities.Interactions;

    public class Astaroth : AI_Entity, IInteractable
    {
        public void ApplyStatusEffect(GameObject effect)
        {
            throw new System.NotImplementedException();
        }

        public void AttackInteraction(float damageValue)
        {
            throw new System.NotImplementedException();
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
