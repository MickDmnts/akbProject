using UnityEngine;

using AKB.Projectiles;
using AKB.Core.Managing;

namespace AKB.Entities.AI.Implementations.Ranged_Demon
{
    public class RangedDemonAttackAction : AttackAction
    {
        RangedDemonNodeData _data;

        public RangedDemonAttackAction(RangedDemonNodeData nodeData) : base(nodeData)
        {
            this._data = nodeData;
        }

        public override bool Run()
        {
            SpawnProjectile();

            //Return false to kick off the cooldown timer node.
            return false;
        }

        void SpawnProjectile()
        {
            GameObject temp = ManagerHUB.GetManager.ProjectilePools.GetPooledProjectileByType(_data.GetProjectileType());

            //Play attack animation
            _data.GetEnemyEntity().GetDemonAnimations().PlayAttackAnimation();

            if (temp != null)
            {
                temp.transform.position = _data.GetBezierStartTransform().position;

                temp.SetActive(true);
                temp.GetComponentInChildren<IRangedDemonSphere>().StartLerping(_data.GetBezierStartTransform().position, _data.GetBezierHintTransform().position);
            }
            else
                throw new System.NullReferenceException("Projectile pool for normal projectiles returned null");
        }
    }
}