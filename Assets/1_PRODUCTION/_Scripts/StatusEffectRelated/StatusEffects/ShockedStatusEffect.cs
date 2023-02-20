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

            EffectBehaviour();

            timeCache = activeTime;
        }

        public override void EffectBehaviour()
        {
            if (GetAttachedEntity() != null)
            {
                shockable = GetAttachedEntity().GetComponent<IShockable>();
            }

            if (shockable != null)
            {
                if (shockable.IsGettingShocked())
                {
                    Destroy(gameObject);
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