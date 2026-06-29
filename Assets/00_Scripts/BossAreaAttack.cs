using UnityEngine;

public class BossAreaAttack : MonoBehaviour
{
    public float damage = 20f;
    public GameObject owner;

    void Start()
    {
        // Destruir el área después de 1 segundo
        Destroy(gameObject, 1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IDamageable damageable = other.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(damage);
            }
        }
    }
}