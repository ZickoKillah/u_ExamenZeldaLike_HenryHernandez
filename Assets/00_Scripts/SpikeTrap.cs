using UnityEngine;
using System.Collections;

public class SpikeTrap : MonoBehaviour, IActivatable
{
    [Header("Configuración")]
    [SerializeField] private float activeDuration = 2f;
    [SerializeField] private float inactiveDuration = 2f;
    [SerializeField] private float damage = 10f;
    [SerializeField] private GameObject spikeModel; // El hijo visual (los pinchos)

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
        if (other.CompareTag("Player"))
        {
            IDamageable damageable = other.GetComponent<IDamageable>();
            damageable?.TakeDamage(damage * Time.deltaTime);
        }
    }
}