using UnityEngine;

public class NarrativeManager : Singleton<NarrativeManager>
{
    [Header("Eventos Narrativos")]
    [SerializeField] private NarrativeEventSO introEvent;
    [SerializeField] private NarrativeEventSO room1Event;
    [SerializeField] private NarrativeEventSO room2Event;    
    [SerializeField] private NarrativeEventSO bossEvent;
    [SerializeField] private NarrativeEventSO victoryEvent;
    [SerializeField] private NarrativeEventSO defeatEvent;

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

    private void OnRoomCleared(string roomId)
    {
        if (roomId == "Sala1" && room1Event != null)
        {
            UIManager.Instance.ShowDialogueForSeconds(room1Event.narrativeText, 4f);
        }
        else if (roomId == "Sala2" && room2Event != null)
        {
            UIManager.Instance.ShowDialogueForSeconds(room2Event.narrativeText, 4f);
        }
    }

    private void OnItemCollected(ItemDataSO item)
    {
        // Mostrar el mensaje específico del objeto, si tiene uno.
        if (!string.IsNullOrEmpty(item.pickupMessage))
        {
            UIManager.Instance.ShowDialogueForSeconds(item.pickupMessage, 2f);
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