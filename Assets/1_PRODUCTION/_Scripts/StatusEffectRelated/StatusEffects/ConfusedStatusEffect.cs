using UnityEngine;
using akb.Core.Managing;
using akb.Core.Sounds;

namespace akb.Entities.Interactions
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
            if (GetAttachedEntity() != null)
            {
                confusable = GetAttachedEntity().GetComponent<IConfusable>();
            }

            if (confusable != null)
            {
                if (confusable.IsConfused())
                {
                    Destroy(gameObject);
                    base.DestroyEffect();
                }
                else
                {
                    confusable.ApplyConfusedInteraction();

                    ApplyVFXToEntity(GetAttachedEntity());

                    ManagerHUB.GetManager.SoundsHandler.PlayOneShot(GameAudioClip.GetConfused);

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