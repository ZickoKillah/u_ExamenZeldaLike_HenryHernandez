using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [Header("Enemigos de esta sala")]
    [SerializeField] private List<Enemy> enemiesInRoom = new List<Enemy>();

    [Header("Puerta que se abrirá al limpiar la sala")]
    [SerializeField] private DoorController door;
    
    [Header("Identificador de la sala")]
    [SerializeField] private string roomId;
    private void OnEnable()
    {
        EventBus.OnEnemyDeath += HandleEnemyDeath;
    }

    private void OnDisable()
    {
        EventBus.OnEnemyDeath -= HandleEnemyDeath;
    }

    private void HandleEnemyDeath(Enemy deadEnemy)
    {
        if (enemiesInRoom.Contains(deadEnemy))
        {
            enemiesInRoom.Remove(deadEnemy);
            Debug.Log($"Enemigo eliminado de la sala. Quedan {enemiesInRoom.Count}.");

            if (enemiesInRoom.Count == 0)
            {
                EventBus.InvokeRoomCleared(roomId);
                Debug.Log("¡Sala limpia! Puerta abierta.");
            }
        }
    }
}