using UnityEngine;

using akb.Core.Managing;

namespace akb.Entities.Interactions
{
    public abstract class StatusEffect : MonoBehaviour
    {
        [Header("Set in inspector")]
        [SerializeField] protected float activeTime;
        [SerializeField] protected EffectType effectType;
        [SerializeField] protected GameObject effectParticle;

        protected GameObject attachedEntity;
        protected GameObject particleHolder;
        protected bool isActive = false;

        public abstract void OnEnable();

        public virtual void AttachToEntity()
        {
            if (transform.parent != null)
            {
                attachedEntity = transform.parent.gameObject;
                Debug.Log(attachedEntity.name);
            }
            else
            {
                return;
            }

            Debug.Log(attachedEntity.name);
        }

        public virtual void ApplyVFXToEntity(GameObject attachedEntity)
        {
            particleHolder = new GameObject("Effect particles");
            particleHolder.transform.SetParent(attachedEntity.transform, false);

            GameObject vfx = Instantiate(effectParticle, particleHolder.transform);
            vfx.transform.position += new Vector3(0f, 2f, 0f);
        }

        public abstract void Update();

        public abstract void EffectBehaviour();

        public virtual void StopEffect()
        {
            attachedEntity = null;

            isActive = false;
        }

        public virtual void DestroyEffect()
        {
            Destroy(particleHolder);
            Destroy(gameObject);
        }

        public abstract void OnDisable();

        public GameObject GetAttachedEntity() => attachedEntity;
    }
}