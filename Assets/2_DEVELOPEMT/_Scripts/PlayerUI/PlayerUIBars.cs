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

        private void Start()
        {
            ManagerHUB.GetManager.GameEventsHandler.onPlayerHealthChange += SetHealthBarValue;
            ManagerHUB.GetManager.GameEventsHandler.onPlayerRageChange += SetRageBarValue;

            ManagerHUB.GetManager.GameEventsHandler.onSceneChanged += ResetOnHub;
        }

        void ResetOnHub(GameScenes scene)
        {
            if (scene != GameScenes.PlayerHUB) { return; }

            healthbar.fillAmount = 1;
            healthbar.color = maxHealthColor;

            ManagerHUB.GetManager.UIManager.EnablePanel("GamePlayScreenPanel");

        }

        void SetHealthBarValue(float currentHealth, float maxHealth)
        {
            healthbar.fillAmount = currentHealth / maxHealth;

            healthbar.color = maxHealthColor;


            if (healthbar.fillAmount < 0.25f)
            {
                healthbar.color = lowHealthColor;

                ManagerHUB.GetManager.UIManager.EnablePanels("BloodEffectPanel","GamePlayScreenPanel"); 
            }
            else
            {
                ManagerHUB.GetManager.UIManager.EnablePanel("GamePlayScreenPanel");
            }
        }

        void SetRageBarValue(float rageValue)
        {
            ragebar.fillAmount = rageValue;
            // i can use this method  to play the animation too
        }

        private void OnDestroy()
        {
            ManagerHUB.GetManager.GameEventsHandler.onPlayerHealthChange -= SetHealthBarValue;
            ManagerHUB.GetManager.GameEventsHandler.onPlayerRageChange -= SetRageBarValue;
            ManagerHUB.GetManager.GameEventsHandler.onSceneChanged -= ResetOnHub;
        }
    }
}
