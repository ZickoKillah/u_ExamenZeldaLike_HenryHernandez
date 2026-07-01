using UnityEngine;
using UnityEngine.AI;

public enum EnemyState
{
    Idle,
    Chasing,
    Attacking
}

// Hereda de Entity, que ya tiene IDamageable
public class Enemy : Entity
{
    [Header("Referencias")] public NavMeshAgent navMeshAgent;
    public Transform player;
    public Attack attackComponent; // Referencia al script Attack

    [Header("Estado")] public EnemyState currentState = EnemyState.Idle;

    [Header("Datos desde ScriptableObject")] [SerializeField]
    protected EnemyDataSO enemyData;

    // Distancia a la que empieza a atacar (se configura en el Inspector o desde EnemyDataSO)
    [SerializeField] protected float attackRange = 2f;

    [Header("Loot")] [SerializeField] private GameObject potionPrefab; // Prefab de la poción
    [SerializeField] [Range(0f, 1f)] private float potionDropChance = 0.3f; // 30% por defecto
    
    [SerializeField] private GameObject soulPrefab; // El prefab del alma
    [SerializeField] [Range(0f, 1f)] private float soulDropChance = 1f; // 100% por defecto
    protected override void Start()
    {
        base.Start(); // Inicializa currentHealth = maxHealth
        if (enemyData != null)
            ApplyData(enemyData);
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null) player = playerObj.transform;
        }

        if (attackComponent == null)
            attackComponent = GetComponent<Attack>();
    }

    public virtual void ApplyData(EnemyDataSO data)
    {
        enemyData = data;
        maxHealth = data.maxHealth;
        currentHealth = maxHealth;
        if (attackComponent != null)
            attackComponent.damage = data.damage;
        if (navMeshAgent != null)
            navMeshAgent.speed = data.speed;
        attackRange = data.attackRange;
    }

    protected virtual void Update()
    {
        // Si no hay jugador, no hace nada
        if (player == null) return;

        // Máquina de estados simple
        switch (currentState)
        {
            case EnemyState.Idle:
                IdleBehaviour();
                break;
            case EnemyState.Chasing:
                ChasePlayer();
                break;
            case EnemyState.Attacking:
                AttackBehaviour();
                break;
        }
    }

    // --- Comportamientos (virtuales para que Archer/Boss los sobrescriban) ---
    protected virtual void IdleBehaviour()
    {
        // Patrullaje aquí
    }

    protected virtual void ChasePlayer()
    {
        if (navMeshAgent != null)
        {
            navMeshAgent.SetDestination(player.position);
            Debug.Log("Golem persiguiendo. Destino: " + player.position);
        }
        else
        {
            Debug.LogError("NavMeshAgent es null en ChasePlayer");
        }

        // Si se acerca lo suficiente, pasa a atacar
        if (Vector3.Distance(transform.position, player.position) <= attackRange)
        {
            currentState = EnemyState.Attacking;
        }
    }

    protected virtual void AttackBehaviour()
    {
        // Si el jugador se aleja, vuelve a perseguir
        if (Vector3.Distance(transform.position, player.position) > attackRange)
        {
            currentState = EnemyState.Chasing;
            return;
        }

        // Ejecutar ataque
        if (attackComponent != null)
        {
            attackComponent.PerformAttack();
        }
    }

    // --- Triggers para detectar al jugador ---
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            currentState = EnemyState.Chasing;
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            currentState = EnemyState.Idle;
        }
    }
    private void TryDropLoot()
    {
        if (potionPrefab == null) return;

        if (Random.value <= potionDropChance)
        {
            Instantiate(potionPrefab, transform.position + Vector3.up * 0.5f, Quaternion.identity);
            Debug.Log($"{gameObject.name} soltó una poción.");
        }
    }

    protected override void Die()
    {
        Debug.Log($"{gameObject.name} ha muerto.");

        // Intentar soltar poción
        TryDropLoot();
        TryDropSoul();  
        EventBus.InvokeOnEnemyDeath(this);
        Destroy(gameObject);

    }
    private void TryDropSoul()
    {
        if (soulPrefab == null || enemyData == null) return;

        int soulsToDrop = enemyData.soulsToDrop;
        if (soulsToDrop <= 0) return;

        for (int i = 0; i < soulsToDrop; i++)
        {
            // Posición aleatoria alrededor del enemigo 
            Vector3 randomOffset = Random.insideUnitSphere * 1.5f;
            randomOffset.y = 0.5f; // Para que queden sobre el suelo
            Vector3 dropPosition = transform.position + randomOffset;

            Instantiate(soulPrefab, dropPosition, Quaternion.identity);
        }

        Debug.Log($"{gameObject.name} soltó {soulsToDrop} almas.");
    }
}