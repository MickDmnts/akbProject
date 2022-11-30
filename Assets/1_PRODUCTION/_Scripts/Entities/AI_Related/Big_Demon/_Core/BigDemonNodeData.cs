namespace AKB.Entities.AI.Implementations.Big_Demon
{
    public class BigDemonNodeData : AI_NodeData
    {
        //Universal values
        float chargeRange;
        float chargeDuration;
        float chargeCooldown;

        //Big demon specifics
        float slamRange;
        float slamTime;
        float slamCooldown;

        bool isRushing = false;
        bool isSlamming = false;

        #region MUTATORS
        public void SetEnemyEntity(BigDemon value) => ai_entity = value;
        public void SetEnemyAnimations(BigDemonAnimations value) => ai_animations = value;

        //Universal values
        public void SetChargeRange(float value) => chargeRange = value;
        public void SetChargeDuration(float time) => chargeDuration = time;
        public void SetChargeCooldown(float time) => chargeCooldown = time;

        //Big demon specifics
        public void SetSlamRange(float value) => slamRange = value;
        public void SetSlamTime(float value) => slamTime = value;
        public void SetSlamCooldown(float value) => slamCooldown = value;

        public void SetIsRushing(bool value) => isRushing = value;
        public void SetIsSlamming(bool value) => isSlamming = value;
        #endregion

        #region ACCESSORS
        public BigDemon GetEnemyEntity() => ai_entity as BigDemon;
        public BigDemonAnimations GetDemonAnimations() => ai_animations as BigDemonAnimations;

        //Universal values
        public float GetChargeRange() => chargeRange;
        public float GetChargeDuration() => chargeDuration;
        public float GetChargeCooldown() => chargeCooldown;

        //Big demon specifics
        public float GetSlamRange() => slamRange;
        public float GetSlamTime() => slamTime;
        public float GetSlamCooldown() => slamCooldown;

        public bool GetIsRushing() => isRushing;
        public bool GetIsSlamming() => isSlamming;
        #endregion
    }
}
