using AKB.Core.Managing;

namespace AKB.Entities.AI.Implementations.Status_Demon
{
    public class StatusDemonNodeData : AI_NodeData
    {
        bool canTeleport;
        float timeUntilTPout;
        float teleportCooldown;

        float maxDistanceFromTarget;

        EffectType effectType;

        #region MUTATORS
        public void SetEnemyEntity(StatusDemon value) => ai_entity = value;
        public void SetEnemyAnimations(StatusDemonAnimations value) => ai_animations = value;

        public void SetEffectType(EffectType value) => effectType = value;

        public void SetTimeUntilTPout(float value) => timeUntilTPout = value;
        public void SetTeleportCooldown(float time) => teleportCooldown = time;
        public void SetCanTeleport(bool value) => canTeleport = value;

        public void SetMaxDistanceFromTarget(float value) => maxDistanceFromTarget = value;
        #endregion

        #region ACCESSORS
        public StatusDemon GetEnemyEntity() => ai_entity as StatusDemon;
        public StatusDemonAnimations GetEnemyAnimations() => ai_animations as StatusDemonAnimations;

        public EffectType GetEffectType() => effectType;

        public float GetTimeUntilTPout() => timeUntilTPout;
        public float GetTeleportCooldown() => teleportCooldown;
        public bool GetCanTeleport() => canTeleport;

        public float GetMaxDistanceFromTarget() => maxDistanceFromTarget;
        #endregion
    }

}