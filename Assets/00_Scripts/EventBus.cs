public static class EventBus
{
    // Eventos
    public static event System.Action<Enemy> OnEnemyDeath;
    public static event System.Action BossDefeated;
    public static event System.Action OnPlayerDeath;
    public static event System.Action<string> RoomCleared;
    public static event System.Action<ItemDataSO> OnItemCollected;

    // Métodos para disparar los eventos desde otras clases
    public static void InvokeRoomCleared(string roomId) => RoomCleared?.Invoke(roomId);
    public static void InvokeOnEnemyDeath(Enemy enemy) => OnEnemyDeath?.Invoke(enemy);
    public static void InvokeOnItemCollected(ItemDataSO item) => OnItemCollected?.Invoke(item);
    public static void InvokeBossDefeated() => BossDefeated?.Invoke();
    public static void InvokeOnPlayerDeath() => OnPlayerDeath?.Invoke();
}