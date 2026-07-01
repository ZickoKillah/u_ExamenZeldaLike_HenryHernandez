using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PickupItem : MonoBehaviour, ICollectable
{
    [Header("Configuración del Ítem")]
    [SerializeField] private ItemDataSO itemData;
    [SerializeField] private bool destroyOnCollect = true; // Desaparece al recogerse

    public void Collect()
    {
        // Buscamos al jugador 
        PlayerController player = FindFirstObjectByType<PlayerController>();
        if (player != null)
        {
            player.AddItem(itemData);
            Debug.Log($"Recogido: {itemData.itemName}");

            if (destroyOnCollect)
            {
                Destroy(gameObject, 0.1f); // Pequeño delay para que termine el sonido/efecto
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Collect();
        }
    }
}