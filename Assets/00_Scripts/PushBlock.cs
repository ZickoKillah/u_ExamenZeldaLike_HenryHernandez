using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class PushBlock : MonoBehaviour, Interactuable
{
    [Header("Empuje")]
    [SerializeField] private float pushForce = 5f;
    [SerializeField] private float pushDistance = 1f;
    private bool isMoving = false;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true; // Seguimos usando kinematic para control total
    }

    public void Interact()
    {
        if (isMoving) return;

        PlayerController player = FindFirstObjectByType<PlayerController>();
        if (player == null) return;

        // Dirección del empuje = hacia donde mira el jugador
        Vector3 pushDir = player.transform.forward;
        pushDir.y = 0f;
        pushDir.Normalize();

        // Verificar si hay espacio para mover la caja
        if (!Physics.Raycast(transform.position, pushDir, pushDistance, LayerMask.GetMask("Default"), QueryTriggerInteraction.Ignore))
        {
            StartCoroutine(PushRoutine(pushDir));
        }
        else
        {
            Debug.Log("La caja no puede moverse en esa dirección.");
        }
    }

    private IEnumerator PushRoutine(Vector3 direction)
    {
        isMoving = true;

        Vector3 startPos = rb.position; // Usamos la posición del Rigidbody
        Vector3 endPos = startPos + direction * pushDistance;
        float elapsed = 0f;
        float duration = 0.3f; // Un poco más lento para asegurar detección

        while (elapsed < duration)
        {
            // Movemos el Rigidbody, no el Transform
            rb.MovePosition(Vector3.Lerp(startPos, endPos, elapsed / duration));
            elapsed += Time.deltaTime;
            yield return null;
        }

        rb.MovePosition(endPos); // Aseguramos la posición final
        isMoving = false;
    }
}