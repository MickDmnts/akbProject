using UnityEngine;
using UnityEngine.UI;

namespace akb.Core.Managing
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


        private void Start()
        {
            ManagerHUB.GetManager.GameEventsHandler.onPlayerHealthChange += SetHealthValue;
            ManagerHUB.GetManager.GameEventsHandler.onPlayerRageChange += SetRageValue;

        }

        private void OnDestroy()
        {
            ManagerHUB.GetManager.GameEventsHandler.onPlayerHealthChange -= SetHealthValue;
            ManagerHUB.GetManager.GameEventsHandler.onPlayerRageChange -= SetRageValue;
        }

        void SetHealthValue(float currentHealth, float maxHealth)
        {
            healthbar.fillAmount = currentHealth / maxHealth;

            healthbar.color = maxHealthColor;


            if (currentHealth < 30)
            {
                healthbar.color = lowHealthColor;
            }
        }

        void SetRageValue(float rageValue)
        {
            ragebar.fillAmount = rageValue;
        }
    }
}
