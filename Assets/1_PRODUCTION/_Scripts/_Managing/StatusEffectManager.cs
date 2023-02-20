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
            switch (effectType)
            {
                case EffectType.Enflamed:
                    return Instantiate(effects[0]);

                case EffectType.Shocked:
                    return Instantiate(effects[1]);

                case EffectType.Stunned:
                    return Instantiate(effects[2]);


                case EffectType.Confused:
                    return Instantiate(effects[3]);

                case EffectType.Charmed:
                    return Instantiate(effects[4]);
            }

            return null;
        }
    }
}