using UnityEngine;

public class LockedDoor : DoorController, Interactuable
{
    [Header("Mensajes")]
    [SerializeField] private string messageNoKey = "Necesitas una llave para abrir esta puerta.";
    [SerializeField] private string messageOpen = "Has usado la llave. La puerta se abre.";

    // Sobrescribimos para que NO se abra al limpiar la sala
    protected override void OnRoomCleared()
    {
        // No hacemos nada. Esta puerta solo se abre con interacción.
    }

    // Se ejecuta cuando el jugador pulsa 'E' cerca de la puerta
    public void Interact()
    {
        PlayerController player = FindFirstObjectByType<PlayerController>();
        if (player == null) return;

        if (player.HasKey())
        {
            Debug.Log(messageOpen);
            UIManager.Instance.ShowDialogueForSeconds(messageOpen, 2f);
            base.OpenDoor(); // Llama al método del padre que desactiva la puerta
        }
        else
        {
            Debug.Log(messageNoKey);
            UIManager.Instance.ShowDialogueForSeconds(messageNoKey, 2f);
        }
    }
}