using UnityEngine;
using AKB.Entities.Interactions;

namespace AKB.Core.Managing
{
    /// <summary>
    /// The available EffectTypes of the game.
    /// </summary>
    public enum EffectType
    {
        Enflamed,
        Shocked,
        Stunned,
        Confused,
        Charmed,

        None,
    }

    /// <summary>
    /// This class is responsible for holing the status effects of the game.
    /// </summary>
    [DefaultExecutionOrder(-395)]
    public class StatusEffectManager : MonoBehaviour
    {
        /// <summary>
        /// The game status effects.
        /// </summary>
        [Header("Set in inspector")]
        [SerializeField, Tooltip("The game status effects.")] StatusEffect[] effects;

        private void Awake()
        {
            ManagerHUB.GetManager.SetStatusEffectRef(this);
        }

        /// <summary>
        /// Returns a gameobject of the passed effect type.
        /// </summary>
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