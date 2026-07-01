using UnityEngine;
using System.Collections;

public class SpikeTrap : MonoBehaviour, IActivatable
{
    [Header("Configuración")]
    [SerializeField] private float activeDuration = 2f;
    [SerializeField] private float inactiveDuration = 2f;
    [SerializeField] private float damage = 10f;
    [SerializeField] private GameObject spikeModel; // El hijo visual

    private bool isActive = false;
    private Coroutine trapCycle;

    private void Start()
    {
        Deactivate(); // Empieza oculto
        trapCycle = StartCoroutine(TrapCycle());
    }

    private IEnumerator TrapCycle()
    {
        while (true)
        {
            yield return new WaitForSeconds(inactiveDuration);
            Activate();
            yield return new WaitForSeconds(activeDuration);
            Deactivate();
        }
    }

    public void Activate()
    {
        isActive = true;
        spikeModel?.SetActive(true);
    }

    public void Deactivate()
    {
        isActive = false;
        spikeModel?.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (!isActive) return;
    
        // --- Logs de diagnóstico ---
        Debug.Log($"Trampa activa - Detectado: {other.name}");
        IDamageable damageable = other.GetComponent<IDamageable>();
        Debug.Log($"¿Tiene IDamageable? {damageable != null}");
        // -------------------------
    
        if (other.CompareTag("Player") && damageable != null)
        {                                                      //Debo gestionar el cambio de la health a float del player.
            damageable.TakeDamage(damage * Time.deltaTime);  //daño es proporción a los frames y no en unidades 
            Debug.Log($"Aplicando daño: {damage * Time.deltaTime}");
        }
    }
}