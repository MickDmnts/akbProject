using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using akb.Entities.Interactions;

namespace akb.Entities.AI.Implementations.Astaroth
{
    public enum AstarothPhases
    {
        Phase1,
        Phase2,
        Phase3
    }

    public class BossAstaroth : AI_Entity, IInteractable
    {
        [SerializeField] GameObject projectilePrefab;
        [SerializeField] ParticleSystem flamePillarPs;
        [SerializeField] float phase1ProjectileRps = 3f;

        Transform target;

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