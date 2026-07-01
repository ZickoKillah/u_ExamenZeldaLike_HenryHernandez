using UnityEngine;
using System.Collections;
using UnityEngine.Playables;
public class AltarTrigger : MonoBehaviour, Interactuable
{
    [Header("Referencias")]
    [SerializeField] private GameObject altarSphere;   // La esfera visual
    [SerializeField] private NarrativeEventSO altarDialog; // SO con el primer diálogo
    [SerializeField] private NarrativeEventSO golemDialog; // SO con el diálogo del Golem
    [SerializeField] private GameObject golem;         // El Golem (inactivo al inicio)
    [SerializeField] private GameObject bossHealthBar; // Barra de vida del boss (opcional)
    [SerializeField] private PlayableDirector bossDirector;
    private bool activated = false;

    public void Interact()
    {
        if (activated) return;
        activated = true;

        // Desactivar input del jugador durante la secuencia
        InputHandler.instance.canUseInput = false;

        StartCoroutine(AltarSequence());
    }

    private IEnumerator AltarSequence()
    {
        // --- Diálogo 1: Reflexión del jugador ---
        if (altarDialog != null)
            UIManager.Instance.ShowDialogueForSeconds(altarDialog.narrativeText, 4f);
        yield return new WaitForSeconds(4.5f); // El tiempo del diálogo + 0.5s de pausa

        // --- Pausa para la cinemática 
        altarSphere?.SetActive(false); // La esfera del altar desaparece
        Debug.Log("Cinemática: El Golem se está ensamblando...");
        if (bossDirector != null)
        {
            bossDirector.Play();
            yield return new WaitForSeconds((float)bossDirector.duration);
        }
        else
        {
            Debug.LogWarning("No se asignó Boss Director. Usando simulación.");
            yield return new WaitForSeconds(3f);
        }

        // --- Aparición del Golem ---
        if (golem != null)
            golem.SetActive(true);
        if (bossHealthBar != null)
            bossHealthBar.SetActive(true);
        InputHandler.instance.canUseInput = true;
        Debug.Log("¡Comienza la batalla contra el Golem!");

        // --- Diálogo 2: Amenaza del Golem ---
        if (golemDialog != null)
            UIManager.Instance.ShowDialogueForSeconds(golemDialog.narrativeText, 3f);
        yield return new WaitForSeconds(3.5f);

        
        
    }
}