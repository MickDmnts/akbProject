using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AKB.Core.Managing
{
    public class PlayerUIBars : MonoBehaviour
    { 
        [SerializeField] Slider healthSlider;

        [SerializeField] Slider rageSlider;

        [SerializeField] Image healthBarFill;
        [SerializeField] Gradient healthBarGradient;

        [SerializeField] Image redSplatterImage;

        public void SetMaxHealthValue(float maxHealthValue)
        {
            healthSlider.maxValue = maxHealthValue;
        }

        public void SetHealthValue(float healthValue)
        {
            healthSlider.value = healthValue;

            healthBarFill.color = healthBarGradient.Evaluate(healthSlider.normalizedValue);
        }

        public void SetRageValue(float rageValue)
        {
            rageSlider.value = rageValue;
        }
    }
}
