using UnityEngine;

namespace AKB.Entities.Interactions
{
    public sealed class EnflamedStatusEffect : StatusEffect
    {
        [Header("Set in inspector")]
        [SerializeField] int effectDamage;
        [SerializeField] float delayUntilEffectStart;

        float timeCache;
        float delayCache;

        float updateInterval = 1f;
        float nextUpdate;

        IInteractable interactable;

        public override void OnEnable()
        {
            base.AttachToEntity();

            interactable = GetAttachedEntity().GetComponent<IInteractable>();

            isActive = true;

            timeCache = activeTime;
            delayCache = Time.time + delayUntilEffectStart;

            ApplyVFXToEntity(GetAttachedEntity());
        }

        public override void Update()
        {
            if (Time.time <= delayCache) return;

            if (isActive)
            {
                timeCache -= Time.deltaTime;
                if (timeCache <= 0)
                {
                    StopEffect();
                }
                else
                {
                    EffectBehaviour();
                }
            }
        }

        public override void EffectBehaviour()
        {
            if (Time.time >= nextUpdate)
            {
                interactable.AttackInteraction(effectDamage);

                nextUpdate = Time.time + updateInterval;
            }
        }

        public override void StopEffect()
        {
            base.StopEffect();

            timeCache = Time.time;

            base.DestroyEffect();
        }

        public override void OnDisable()
        {
            isActive = false;
        }
    }
}