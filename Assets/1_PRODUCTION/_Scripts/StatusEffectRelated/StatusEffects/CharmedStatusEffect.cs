using UnityEngine;
using akb.Core.Managing;
using akb.Core.Sounds;
namespace akb.Entities.Interactions
{
    public class CharmedStatusEffect : StatusEffect
    {
        [SerializeField] float moveSpeed;

        float timeCache;

        ICharmable charmable;
        Vector3 shotFromEntity;

        Vector3 directionToMove;

        public override void OnEnable()
        {
            base.AttachToEntity();

            if (attachedEntity == null)
            {
                Destroy(gameObject);
                return;
            }

            timeCache = activeTime;

            charmable = transform.parent.gameObject.GetComponent<ICharmable>();

            if (charmable != null)
            {
                if (charmable.IsAlreadyCharmed())
                {
                    base.DestroyEffect();
                }
                else
                {
                    shotFromEntity = charmable.GetInflictedFromDirection();

                    charmable.DeactivateEntityControls();

                    ApplyVFXToEntity(GetAttachedEntity());
                    ManagerHUB.GetManager.SoundsHandler.PlayOneShot(GameAudioClip.GetCharmed);
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
                else
                {
                    EffectBehaviour();
                }
            }
        }

        public override void EffectBehaviour()
        {
            directionToMove = shotFromEntity - transform.root.position;

            directionToMove.y = 0f;

            transform.root.position += directionToMove * moveSpeed * Time.deltaTime;
        }

        public override void StopEffect()
        {
            base.StopEffect();

            charmable.ActivateEntityControls();

            base.DestroyEffect();
        }

        public override void OnDisable()
        {
            charmable = null;
            attachedEntity = null;
            shotFromEntity = Vector3.zero;

            isActive = false;
        }
    }
}