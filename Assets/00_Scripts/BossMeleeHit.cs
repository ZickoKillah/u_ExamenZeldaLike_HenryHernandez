using UnityEngine;

public class BossMeleeHit : MonoBehaviour
{
    public float damage = 20f;
    public GameObject owner;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IDamageable damageable = other.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(damage);
                Debug.Log($"Golem golpeó al jugador por {damage}");
            }
        }
    }
}