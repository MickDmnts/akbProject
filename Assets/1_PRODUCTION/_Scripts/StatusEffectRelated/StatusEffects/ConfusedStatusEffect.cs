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

            if (attachedEntity == null)
            {
                Destroy(gameObject);
                return;
            }

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