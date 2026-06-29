using UnityEngine;

public abstract class Entity : MonoBehaviour, IDamageable
{
    [Header("Stats")]
    [SerializeField] protected float maxHealth = 100f;
    [SerializeField] protected float currentHealth;

    protected virtual void Start()
    {
        currentHealth = maxHealth;
    }

    // IDamageable: recibe daño. Es virtual para que Boss pueda extenderlo
    public virtual void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
        Debug.Log($"{gameObject.name} recibe {damage} de daño. Vida restante: {currentHealth}");

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    // Método virtual para que cada enemigo muera de forma distinta
    protected virtual void Die()
    {
        Debug.Log($"{gameObject.name} ha muerto.");
        // Aquí se notificará al RoomManager mediante EventBus
        Destroy(gameObject);
    }
}
