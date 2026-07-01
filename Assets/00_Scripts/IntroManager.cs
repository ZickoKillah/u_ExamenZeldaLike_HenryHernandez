using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
    public void LoadGameScene()
    {
        SceneManager.LoadScene(1); 
    }
}