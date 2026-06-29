using UnityEngine;

public class DoorController : MonoBehaviour
{
    [Header("Sala a la que pertenece esta puerta")]
    [SerializeField] private string roomId;
    public virtual void OpenDoor()
    {
        Debug.Log($"{gameObject.name}: Puerta abierta.");
        gameObject.SetActive(false);
    }

    protected virtual void OnEnable()
    {
        EventBus.RoomCleared += HandleRoomCleared;
    }

    protected virtual void OnDisable()
    {
        EventBus.RoomCleared -= HandleRoomCleared;
    }

    // Método protegido y virtual para que las subclases puedan sobrescribirlo
    protected virtual void OnRoomCleared()
    {
        OpenDoor();
    }

    // El método privado que maneja el evento y llama al virtual
    protected virtual void HandleRoomCleared(string clearedRoomId)
    {
        if (clearedRoomId == roomId)
        {
            OpenDoor();
        }
    }
}