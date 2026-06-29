public static class EventBus
{
    // Eventos (solo se pueden invocar desde dentro de esta clase)
    public static event System.Action<Enemy> OnEnemyDeath;
    public static event System.Action BossDefeated;
    public static event System.Action OnPlayerDeath;
    public static event System.Action<string> RoomCleared;
   
    
    // Métodos públicos para disparar los eventos desde otras clases
    
    public static void InvokeRoomCleared(string roomId) => RoomCleared?.Invoke(roomId);

    public static void InvokeOnEnemyDeath(Enemy enemy)
    {
        OnEnemyDeath?.Invoke(enemy);
    }
    public static event System.Action<ItemType> OnItemCollected;
    public static void InvokeOnItemCollected(ItemType type) => OnItemCollected?.Invoke(type);
    
    public static void InvokeBossDefeated() => BossDefeated?.Invoke();
    
    public static void InvokeOnPlayerDeath() => OnPlayerDeath?.Invoke();
}