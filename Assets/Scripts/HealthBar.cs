using UnityEngine;
using UnityEngine.UI;

namespace Eco
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Slider healthSlider;

        public void SetMaxHealth(int maxHealth)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = maxHealth;
        }

        public void SetHealth(int currentHealth)
        {
            healthSlider.value = currentHealth;
        }
    }
}
