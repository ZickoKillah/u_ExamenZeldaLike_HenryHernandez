using UnityEngine;

public class Interact : MonoBehaviour
{
    public Collider interactCollider;

    public void PerformInteract()
    {
        Collider[] hits = Physics.OverlapBox(interactCollider.bounds.center, 
            interactCollider.bounds.extents, interactCollider.transform.rotation);
        foreach (Collider hit in hits)
        {
            if (hit.gameObject == gameObject) continue;

            // Prioridad: Interactuable (cofre, altar)
            Interactuable interactuable = hit.GetComponent<Interactuable>();
            if (interactuable != null)
            {
                interactuable.Interact();
                Debug.Log($"Interactuado con {hit.gameObject.name}.");
                return; // Solo una interacción por pulsación
            }
        
            // Si no es Interactuable, buscar ICollectable (objetos recogibles)
            ICollectable collectable = hit.GetComponent<ICollectable>();
            if (collectable != null)
            {
                collectable.Collect();
                Debug.Log($"Recogido: {hit.gameObject.name}.");
                return;
            }
        }        
    }
    }
    

