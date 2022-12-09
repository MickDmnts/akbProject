using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AKB.Core.Managing
{
    public class PlayerUIBars : MonoBehaviour
    { 
        //[SerializeField] Slider healthSlider;

        //[SerializeField] Slider rageSlider;

        [SerializeField] Image healthbar;

        [SerializeField] Color maxHealthColor = Color.green;
        [SerializeField] Color lowHealthColor = Color.red;

        //[SerializeField] Gradient healthBarGradient;

        [SerializeField] Image ragebar;

        [SerializeField] Image redSplatterImage;

        //public void SetMaxHealthValue(float maxHealthValue)
        //{
        //    healthSlider.maxValue = maxHealthValue;
        //}

        //public void SetHealthValue(float healthValue)
        //{
        //    healthSlider.value = healthValue;

        //    healthbar.color = healthBarGradient.Evaluate(healthSlider.normalizedValue);
        //}

        //public void SetRageValue(float rageValue)
        //{
        //    rageSlider.value = rageValue;
        //}

        public void SetHealthValue(float currentHealth, float maxHealth)
        {
            healthbar.fillAmount = currentHealth / maxHealth;

            healthbar.color = maxHealthColor;


            if (currentHealth < 30)
            {
                healthbar.color = lowHealthColor;
            }
        }

        public void SetRageValue(float rageValue)
        {
            ragebar.fillAmount = rageValue;
        }
    }
}
