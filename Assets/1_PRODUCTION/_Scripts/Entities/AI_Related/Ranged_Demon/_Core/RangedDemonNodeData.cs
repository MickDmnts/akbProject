using AKB.Core.Managing;
using UnityEngine;

namespace AKB.Entities.AI.Implementations.Ranged_Demon
{
    public class RangedDemonNodeData : AI_NodeData
    {
        float minTargetDistance;
        float maxTargetDistance;

        float maxTimeNearTarget;

        float maxJumpDistance;

        float timeUntilAttack;
        float attackCooldown;

        bool searchingForPos;

        //Projectile spawning
        ProjectileType projectileType;
        Transform bezierStart;
        Transform bezierHint;

        #region MUTATORS
        public void SetEnemyEntity(RangedDemon value) => ai_entity = value;
        public void SetEnemyAnimations(RangedDemonAnimations value) => ai_animations = value;

        public void SetMinTargetDistance(float value) => minTargetDistance = value;
        public void SetMaxTargetDistance(float value) => maxTargetDistance = value;

        public void SetMaxTimeNearTarget(float value) => maxTimeNearTarget = value;

        public void SetMaxJumpDistance(float value) => maxJumpDistance = value;

        public void SetTimeUntilAttack(float time) => timeUntilAttack = time;
        public void SetAttackCooldown(float time) => attackCooldown = time;

        public void SetSearchingForPos(bool value) => searchingForPos = value;

        public void SetProjectileType(ProjectileType value) => projectileType = value;
        public void SetBezierStartTransform(Transform transform) => bezierStart = transform;
        public void SetBezierHintTransform(Transform transform) => bezierHint = transform;
        #endregion

        #region ACCESSORS
        public RangedDemon GetEnemyEntity() => ai_entity as RangedDemon;
        public RangedDemonAnimations SetEnemyAnimations() => ai_animations as RangedDemonAnimations;

        public float GetMinTargetDistance() => minTargetDistance;
        public float GetMaxTargetDistance() => maxTargetDistance;

        public float GetMaxTimeNearTarget() => maxTimeNearTarget;

        public float GetMaxJumpDistance() => maxJumpDistance;

        public float GetAttackCooldown() => attackCooldown;
        public float GetTimeUntilAttack() => timeUntilAttack;

        public bool GetSearchingForPos() => searchingForPos;

        public ProjectileType GetProjectileType() => projectileType;
        public Transform GetBezierStartTransform() => bezierStart;
        public Transform GetBezierHintTransform() => bezierHint;
        #endregion
    }
}