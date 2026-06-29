using UnityEngine;

public class NarrativeManager : Singleton<NarrativeManager>
{
    [Header("Eventos Narrativos")]
    [SerializeField] private NarrativeEventSO introEvent;    // Ya no se usa aquí, pero lo dejamos por si acaso
    [SerializeField] private NarrativeEventSO room1Event;    // Para Sala 1
    [SerializeField] private NarrativeEventSO room2Event;    // Para Sala 2 (llave)
    [SerializeField] private NarrativeEventSO bossEvent;     // Para el Golem
    [SerializeField] private NarrativeEventSO victoryEvent;  // Victoria
    [SerializeField] private NarrativeEventSO defeatEvent;   // Derrota

    // Ya NO mostramos el intro en Start. Lo hace UIManager.

    private void OnEnable()
    {
        EventBus.RoomCleared += OnRoomCleared;
        EventBus.OnItemCollected += OnItemCollected;
        EventBus.BossDefeated += OnBossDefeated;
        EventBus.OnPlayerDeath += OnPlayerDeath;
    }

    private void OnDisable()
    {
        EventBus.RoomCleared -= OnRoomCleared;
        EventBus.OnItemCollected -= OnItemCollected;
        EventBus.BossDefeated -= OnBossDefeated;
        EventBus.OnPlayerDeath -= OnPlayerDeath;
    }

    // Ahora recibe el parámetro string (el ID de la sala)
    private void OnRoomCleared(string roomId)
    {
        // Según la sala que se limpió, mostramos un diálogo distinto
        if (roomId == "Sala1" && room1Event != null)
        {
            UIManager.Instance.ShowDialogueForSeconds(room1Event.narrativeText, 4f);
        }
        else if (roomId == "Sala2" && room2Event != null)
        {
            UIManager.Instance.ShowDialogueForSeconds(room2Event.narrativeText, 4f);
        }
        // Si tienes más salas, puedes añadir más condiciones aquí
    }

    private void OnItemCollected(ItemType type)
    {
        if (type == ItemType.Key && room2Event != null)
        {
            UIManager.Instance.ShowDialogueForSeconds(room2Event.narrativeText, 4f);
        }
    }

    private void OnBossDefeated()
    {
        if (victoryEvent != null)
            UIManager.Instance.ShowVictory(victoryEvent.narrativeText);
    }

    private void OnPlayerDeath()
    {
        if (defeatEvent != null)
            UIManager.Instance.ShowGameOver(defeatEvent.narrativeText);
    }
}