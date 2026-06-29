using UnityEngine;
using UnityEngine.Events;

public class PressurePlate : MonoBehaviour
{
    public UnityEvent OnActivated;
    public UnityEvent OnDeactivated;

    private int objectsOnPlate = 0;

    private void OnTriggerEnter(Collider other)
    {    Debug.Log($"Trigger con: {other.name}, Tag: {other.tag}");
        if (other.CompareTag("PushBlock") || other.CompareTag("Player"))
        {
            objectsOnPlate++;
            if (objectsOnPlate == 1)
                OnActivated?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PushBlock") || other.CompareTag("Player"))
        {
            objectsOnPlate--;
            if (objectsOnPlate == 0)
                OnDeactivated?.Invoke();
        }
    }
}