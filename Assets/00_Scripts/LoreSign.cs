using UnityEngine;

public class LoreSign : MonoBehaviour, Interactuable
{
    [Header("Mensaje del cartel")] [TextArea(3, 10)] 
    
    [SerializeField] string message;

    [SerializeField] private float displaySeconds = 4f;


    public void Interact()
    {
        if (UIManager.Instance != null)
        {
            UIManager.Instance.ShowDialogueForSeconds(message, displaySeconds);
        }
    }
}
