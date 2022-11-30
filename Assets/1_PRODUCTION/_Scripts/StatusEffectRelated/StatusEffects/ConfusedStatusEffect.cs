using UnityEngine;

namespace AKB.Entities.Interactions
{
    public class ConfusedStatusEffect : StatusEffect
    {
        float timeCache;

        IConfusable confusable;

        public override void OnEnable()
        {
            base.AttachToEntity();

            EffectBehaviour();

            timeCache = activeTime;
        }

        public override void EffectBehaviour()
        {
            confusable = GetAttachedEntity().GetComponent<IConfusable>();

            if (confusable != null)
            {
                if (confusable.IsConfused())
                {
                    base.DestroyEffect();
                }
                else
                {
                    confusable.ApplyConfusedInteraction();

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

            confusable.RemoveConfusedInteraction();

            base.DestroyEffect();
        }

        public override void OnDisable()
        {
            isActive = false;
        }
    }
}