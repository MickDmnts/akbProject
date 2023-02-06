using System;
using UnityEngine;

namespace AKB.Core.Managing
{
    using AKB.Entities.Interactions;

    public enum EffectType
    {
        Enflamed,
        Shocked,
        Stunned,
        Confused,
        Charmed,

        None,
    }

    [DefaultExecutionOrder(100)]
    public class StatusEffectManager : MonoBehaviour
    {
        [Header("Set in inspector")]
        [SerializeField] StatusEffect[] effects;

        private void Awake()
        {
            GameManager.S.SetStatusEffectRef(this);
        }

        private void Start()
        {
            CheckEffectsArrayLength();
        }

        void CheckEffectsArrayLength()
        {
            if (effects.Length < Enum.GetValues(typeof(EffectType)).Length - 1)
            {
                throw new ArgumentException("Not many effects in the effects array");
            }
        }

        public GameObject GetNeededEffect(EffectType effectType)
        {
            GameObject tempEffect = null;

            switch (effectType)
            {
                case EffectType.Enflamed:
                    tempEffect = effects[0].gameObject;
                    break;

                case EffectType.Shocked:
                    tempEffect = effects[1].gameObject;
                    break;

                case EffectType.Stunned:
                    tempEffect = effects[2].gameObject;
                    break;

                case EffectType.Confused:
                    tempEffect = effects[3].gameObject;
                    break;

                case EffectType.Charmed:
                    tempEffect = effects[4].gameObject;
                    break;

                default:
                    tempEffect = null;
                    break;
            }

            return tempEffect;
        }
    }
}