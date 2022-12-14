using UnityEngine;
using UnityEngine.UI;

namespace AKB.Core.Managing.UI
{
    public class BossHealthbar : MonoBehaviour
    {
        [SerializeField] Image BeelzebubHealthbar;

        [SerializeField] Image AstarothHealthbar;

        public void SetHealthValueBeelzebub(float currentHealth, float maxHealth)
        {
            BeelzebubHealthbar.fillAmount = currentHealth / maxHealth;
        }

        public void SetHealthValueAstaroth(float currentHealth, float maxHealth)
        {
            AstarothHealthbar.fillAmount = currentHealth / maxHealth;
        }
    }
}