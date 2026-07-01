using UnityEngine;

[CreateAssetMenu(fileName = "Nuevo Item", menuName = "Templo de Zoion/Item Data")]
public class ItemDataSO : ScriptableObject
{
    public string itemName;
    public ItemType itemType;
    public Sprite icon;        // Para mostrarlo en la UI (HUD de ítems)
    [TextArea(2, 4)]
    public string pickupMessage; 

    // El valor numérico (ej: cantidad de curación, aumento de daño)
    public float effectValue;
}