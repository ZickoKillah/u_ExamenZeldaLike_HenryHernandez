using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    [SerializeField] private Image healthFillImage;

    public void SetMaxHealth(float maxHealth)
    {
        if (healthFillImage != null)
            healthFillImage.fillAmount = 1f;
    }

    public void UpdateHealth(float currentHealth)
    {
        if (healthFillImage != null)
        {
            // Asumimos que maxHealth está guardado en el Boss, o lo pasamos como parámetro
            // Por simplicidad, lo normalizaremos desde el Boss
            healthFillImage.fillAmount = currentHealth / 200f; // Ajusta el máximo según tu Golem
        }
    }
}