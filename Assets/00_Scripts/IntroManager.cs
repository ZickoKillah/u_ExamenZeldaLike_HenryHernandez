using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
    public void LoadGameScene()
    {
        SceneManager.LoadScene(1); // Asegúrate de que la escena del juego tenga índice 1 en Build Settings
    }
}