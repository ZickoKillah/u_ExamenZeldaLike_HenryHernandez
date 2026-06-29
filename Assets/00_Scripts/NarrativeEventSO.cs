using UnityEngine;

[CreateAssetMenu(fileName = "Nuevo Evento Narrativo", menuName = "Templo de Zoion/Evento Narrativo")]
public class NarrativeEventSO : ScriptableObject
{
    [TextArea(3, 10)]
    public string narrativeText;
    public enum TriggerType { OnRoomEnter, OnItemPickup, OnBossSpawn, OnVictory, OnDefeat }
    public TriggerType triggerType;
}