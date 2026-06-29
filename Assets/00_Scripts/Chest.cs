using UnityEngine;
using DG.Tweening;

public class Chest : MonoBehaviour, Interactuable
{
    public Transform top;
    public bool isOpen = false;

    [Header("Spawneo de llave")]
    [SerializeField] private GameObject keyPrefab; // Prefab de la llave (PickupItem)
    [SerializeField] private Transform spawnPoint; // Punto desde donde aparece (opcional)

    public void Interact()
    {
        if (isOpen) return;
        isOpen = true;

        // Si no se asigna spawnPoint, usaremos la posición del cofre un poco más arriba
        Vector3 spawnPos = spawnPoint != null ? spawnPoint.position : transform.position + Vector3.up * 1.5f;

        Sequence sequence = DOTween.Sequence();
        sequence.AppendCallback(() => InputHandler.instance.canUseInput = false);
        sequence.Append(top.DOLocalRotate(new Vector3(-120, 0, 0), 0.5f).SetEase(Ease.OutBack));

        // Spawnear la llave justo al terminar la animación de la tapa (0.5 seg después)
        sequence.AppendCallback(() =>
        {
            if (keyPrefab != null)
            {
                Instantiate(keyPrefab, spawnPos, Quaternion.identity);
                Debug.Log("Llave spawneada desde el cofre.");
            }
        });

        sequence.AppendInterval(1f);
        sequence.AppendCallback(() => InputHandler.instance.canUseInput = true);
    }
}