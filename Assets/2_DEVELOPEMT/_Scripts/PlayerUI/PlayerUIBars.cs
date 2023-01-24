using UnityEngine;
using UnityEngine.UI;

using akb.Core.Managing.LevelLoading;

namespace akb.Core.Managing
{
    public class PlayerUIBars : MonoBehaviour
    {
        [Header("Set player UI bars fills.")]
        [SerializeField] Image healthbar;

        [SerializeField] Color maxHealthColor = Color.green;
        [SerializeField] Color lowHealthColor = Color.red;

        [SerializeField] Image ragebar;

        [SerializeField] Image redSplatterImage;

        private void Start()
        {
            ManagerHUB.GetManager.GameEventsHandler.onPlayerHealthChange += SetHealthValue;
            ManagerHUB.GetManager.GameEventsHandler.onPlayerRageChange += SetRageValue;

            ManagerHUB.GetManager.GameEventsHandler.onSceneChanged += ResetOnHub;
        }

        void ResetOnHub(GameScenes scene)
        {
            if (scene != GameScenes.PlayerHUB) { return; }

            healthbar.fillAmount = 1;
            healthbar.color = maxHealthColor;
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

        private void OnDestroy()
        {
            ManagerHUB.GetManager.GameEventsHandler.onPlayerHealthChange -= SetHealthValue;
            ManagerHUB.GetManager.GameEventsHandler.onPlayerRageChange -= SetRageValue;
            ManagerHUB.GetManager.GameEventsHandler.onSceneChanged -= ResetOnHub;
        }

        void UpdateBloodEffect(float currentHealth)
        {
            Color splatterAlpha = redSplatterImage.color;
            splatterAlpha.a = 1 - (currentHealth / 30);
            redSplatterImage.color = splatterAlpha;
        }
    }
}
