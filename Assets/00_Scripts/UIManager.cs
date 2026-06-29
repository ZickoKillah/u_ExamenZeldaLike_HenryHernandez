using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
public class UIManager : Singleton<UIManager>
{
    [Header("Corazones")]
    [SerializeField] private Image[] hearts; // Array de 3 imágenes de corazón
    [SerializeField] private Sprite heartFull;
    [SerializeField] private Sprite heartThreeQuarters;
    [SerializeField] private Sprite heartHalf;
    [SerializeField] private Sprite heartQuarter;
    [SerializeField] private Sprite heartEmpty;

    [Header("Ítems")]
    [SerializeField] private Image keyIcon;
    [SerializeField] private TextMeshProUGUI keyCountText;
    [SerializeField] private Image potionIcon;
    [SerializeField] private TextMeshProUGUI potionCountText;

    [Header("Diálogo")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;
    
    [Header("Paneles de fin")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] private GameObject victoryPanel;
    [SerializeField] private TextMeshProUGUI victoryText;
    
    [Header("Mejora de espada")]
    [SerializeField] private Image swordUpgradeIcon;
    
    [Header("Almas")]
    [SerializeField] private TextMeshProUGUI soulsText;

    [Header("Intro")] [SerializeField] private NarrativeEventSO introEvent;
    private void Start()
    {
        UpdateHearts(12, 3, 4); // Vida inicial: 3 corazones llenos
        UpdateKeyCount(0);
        UpdatePotionCount(0);
        dialoguePanel.SetActive(false);
        if (introEvent != null)
        {
            ShowDialogueForSeconds(introEvent.narrativeText, 4f);
        }
    }

    public void UpdateHearts(int currentHealth, int maxHearts, int healthPerHeart)
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            int heartStart = i * healthPerHeart;
            int heartFill = Mathf.Clamp(currentHealth - heartStart, 0, healthPerHeart);
            float fillAmount = (float)heartFill / healthPerHeart;
            hearts[i].fillAmount = fillAmount;
        }
    }

    public void UpdateKeyCount(int count)
    {
        keyCountText.text = count.ToString();
        keyIcon.gameObject.SetActive(count > 0);
    }

    public void UpdatePotionCount(int count)
    {
        potionCountText.text = count.ToString();
        potionIcon.gameObject.SetActive(count > 0);
    }
    public void ShowSwordUpgradeIcon()
    {
        if (swordUpgradeIcon != null)
            swordUpgradeIcon.gameObject.SetActive(true);
    }

    public void ShowDialogue(string message)
    {
        dialogueText.text = message;
        dialoguePanel.SetActive(true);
    }

    public void HideDialogue()
    {
        dialoguePanel.SetActive(false);
    }
    private Coroutine dialogueCoroutine; // Para cancelar diálogos anteriores si se solapan

    public void ShowDialogueForSeconds(string message, float seconds)
    {
        dialogueText.text = message;
        dialoguePanel.SetActive(true);

        // Si ya había un diálogo en curso, lo cancelamos para evitar que se oculte antes de tiempo
        if (dialogueCoroutine != null)
            StopCoroutine(dialogueCoroutine);

        dialogueCoroutine = StartCoroutine(HideAfterSeconds(seconds));
    }

    private IEnumerator HideAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        dialoguePanel.SetActive(false);
        dialogueCoroutine = null;
    }

    public void UpdateSoulsCount(int count)
    {
        if (soulsText != null)
            soulsText.text = count.ToString();
    }
    
    
    
    
    
    
    
    public void ShowGameOver(string message)
    {
        if (gameOverText != null) 
            gameOverText.text = message;
        gameOverPanel?.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ShowVictory(string message)
    {
        if (victoryText != null) 
            victoryText.text = message;
        victoryPanel?.SetActive(true);
        Time.timeScale = 0f;
    }
}