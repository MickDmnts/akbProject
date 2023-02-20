using UnityEngine;
using akb.Core.Managing;
using akb.Core.Sounds;

namespace akb.Entities.Interactions
{
    public sealed class ShockedStatusEffect : StatusEffect
    {
        float timeCache;

        IShockable shockable;

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
            shockable = GetAttachedEntity().gameObject.GetComponent<IShockable>();

            if (shockable != null)
            {
                if (shockable.IsGettingShocked())
                {
                    base.DestroyEffect();
                }
                else
                {
                    shockable.InflictShockInteraction();

                    ApplyVFXToEntity(GetAttachedEntity());

                    ManagerHUB.GetManager.SoundsHandler.PlayOneShot(GameAudioClip.GetElectrified);

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

            shockable.RemoveShockInteraction();

            base.DestroyEffect();
        }

        public override void OnDisable()
        {
            isActive = false;
        }
    }
}