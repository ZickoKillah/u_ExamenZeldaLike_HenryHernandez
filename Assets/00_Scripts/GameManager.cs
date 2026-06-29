using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public enum GameState { Playing, Paused, GameOver, Victory }
    public GameState currentState { get; private set; } = GameState.Playing;

    private void Start()
    {
        currentState = GameState.Playing;
    }
    private void OnEnable()
    {
        EventBus.OnPlayerDeath += SetGameOver;
        EventBus.BossDefeated += SetVictory;
    }

    private void OnDisable()
    {
        EventBus.OnPlayerDeath -= SetGameOver;
        EventBus.BossDefeated -= SetVictory;
    }

    public void SetGameOver()
    {
        currentState = GameState.GameOver;
        Time.timeScale = 0f;
    }

    public void SetVictory()
    {
        currentState = GameState.Victory;
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(0); // Recarga la escena del juego
    }
}