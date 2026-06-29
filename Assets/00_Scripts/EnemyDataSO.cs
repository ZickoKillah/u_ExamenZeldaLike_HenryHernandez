using UnityEngine;

[CreateAssetMenu(fileName = "Nuevo Enemigo", menuName = "Templo de Zoion/Enemy Data")]
public class EnemyDataSO : ScriptableObject
{
    public string enemyName;
    public float maxHealth = 100f;
    public float damage = 10f;
    public float speed = 3.5f;
    public float attackRange = 2f;
    public float attackCooldown = 1.5f;
    public EnemyType enemyType;
    public int soulsToDrop;
    public enum EnemyType
    {
        Melee,
        Archer,
        Boss
    }
}
        

