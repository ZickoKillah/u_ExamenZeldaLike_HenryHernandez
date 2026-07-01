using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    [SerializeField] private Image healthFillImage;
    private float maxHealth;

    public void SetMaxHealth(float maxHealth)
    {   
        this.maxHealth = maxHealth;
        if (healthFillImage != null)
            healthFillImage.fillAmount = 1f;
    }

    public void UpdateHealth(float currentHealth)
    {
        if (healthFillImage != null && maxHealth > 0)
        {
            // Asumimos que maxHealth está guardado en el Boss, o lo pasamos como parámetro
            // Por simplicidad, lo normalizaremos desde el Boss
            healthFillImage.fillAmount = currentHealth / 300f; // Ajusta el máximo según tu Golem
        }
    }
}