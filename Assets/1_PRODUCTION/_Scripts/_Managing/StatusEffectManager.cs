using UnityEngine;

namespace akb.Core.Managing
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
        [SerializeField, Tooltip("The game status effects.")] GameObject[] effects;

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
                    tempEffect = Instantiate(effects[0]);
                    break;

                case EffectType.Shocked:
                    tempEffect = Instantiate(effects[1]);
                    break;

                case EffectType.Stunned:
                    tempEffect = Instantiate(effects[2]);
                    break;

                case EffectType.Confused:
                    tempEffect = Instantiate(effects[3]);
                    break;

                case EffectType.Charmed:
                    tempEffect = Instantiate(effects[4]);
                    break;

                default:
                    tempEffect = null;
                    break;
            }

            return tempEffect;
        }
    }
}