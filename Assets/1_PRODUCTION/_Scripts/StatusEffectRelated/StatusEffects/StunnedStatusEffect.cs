using UnityEngine;

namespace AKB.Entities.Interactions
{
    public class StunnedStatusEffect : StatusEffect
    {
        float timeCache;

        IStunnable stunnable;

        public override void OnEnable()
        {
            base.AttachToEntity();

            EffectBehaviour();

            timeCache = activeTime;
        }

        public override void EffectBehaviour()
        {
            stunnable = GetAttachedEntity().GetComponent<IStunnable>();

            if (stunnable != null)
            {
                if (stunnable.IsAlreadyStunned())
                {
                    base.DestroyEffect();
                }
                else
                {
                    stunnable.InflictStunnedInteraction();

                    ApplyVFXToEntity(GetAttachedEntity());

                    isActive = true;
                }
            }
        }

        public override void Update()
        {
            if (isActive)
            {
                timeCache -= Time.deltaTime;
                if (timeCache <= 0)
                {
                    StopEffect();
                }
            }
        }

        public override void StopEffect()
        {
            base.StopEffect();

            stunnable.RemoveStunnedInteraction();

            base.DestroyEffect();
        }

        public override void OnDisable()
        {
            isActive = false;
        }
    }
}