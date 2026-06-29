using UnityEngine;
using System.Collections;

public class Boss : Enemy
{
    [Header("Fases del Jefe")]
    [SerializeField] private float phase2Threshold = 0.5f; // 50% de vida
    private bool isPhase2 = false;

    [Header("Ataque Especial (Fase 2)")]
    [SerializeField] private GameObject areaAttackPrefab; // Prefab de un área de daño (ej: círculo en el suelo)
    [SerializeField] private float areaAttackDamage = 20f;
    [SerializeField] private float specialAttackCooldown = 4f;
    private float lastSpecialAttackTime = -999f;
    
    [Header("Ataque Cuerpo a Cuerpo")]
    [SerializeField] private float meleeDamage = 20f;
    [SerializeField] private float meleeCooldown = 1.5f;
    [SerializeField] private Collider meleeCollider; // Un collider hijo para el puño
    private float lastMeleeAttackTime = -999f;

    [Header("Barra de Vida")]
    [SerializeField] private BossHealthBar healthBar;

    protected override void Start()
    {
        base.Start();
        if (navMeshAgent != null && player != null)
        {
            navMeshAgent.SetDestination(player.position);
            Debug.Log("Moviendo Golem hacia: " + player.position);
        }
        // El Golem tiene más vida y ataque que un enemigo normal
        maxHealth = 200f;
        currentHealth = maxHealth;
        attackRange = 3f;
        if (navMeshAgent != null)
        {
            navMeshAgent.speed = 2.5f;
        }
        if (healthBar != null)
        {
            healthBar.SetMaxHealth(maxHealth);
            healthBar.gameObject.SetActive(true);
        }
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        if (healthBar != null)
            healthBar.UpdateHealth(currentHealth);

        // Comprobar transición a Fase 2
        if (!isPhase2 && currentHealth <= maxHealth * phase2Threshold)
        {
            EnterPhase2();
        }
    }

    private void EnterPhase2()
    {
        isPhase2 = true;
        Debug.Log("¡Golem entra en Fase 2!");

        if (navMeshAgent != null) 
            navMeshAgent.speed *= 1.5f;
    
        attackRange = 5f;

        // Cambio de color seguro
        Renderer renderer = GetComponentInChildren<Renderer>();
        if (renderer != null)
            renderer.material.color = Color.red;
    }

    protected override void AttackBehaviour()
    {
        if (player == null) return;
        float dist = Vector3.Distance(transform.position, player.position);

        if (dist > attackRange)
        {
            currentState = EnemyState.Chasing;
            return;
        }

        // Ataque cuerpo a cuerpo (Fase 1 y 2)
        if (Time.time - lastMeleeAttackTime >= meleeCooldown)
        {
            StartCoroutine(MeleeAttackRoutine());
            lastMeleeAttackTime = Time.time;
        }
        

        // Ataque especial en Fase 2 (ya lo tienes)
        if (isPhase2 && Time.time - lastSpecialAttackTime >= specialAttackCooldown)
        {
            StartCoroutine(PerformSpecialAttack());
            lastSpecialAttackTime = Time.time;
        }
    }
    private IEnumerator MeleeAttackRoutine()
    {
        // Activamos el collider del puño
        meleeCollider.enabled = true;

        // Aquí podrías reproducir animación de golpe con Animator (opcional)
        Debug.Log("Golem golpea");

        yield return new WaitForSeconds(0.3f); // Duración del golpe

        // Desactivamos el collider del puño
        meleeCollider.enabled = false;
    }
    private IEnumerator PerformSpecialAttack()
    {
        Debug.Log("¡Golem usa Ataque Especial de Área!");
        // Instanciar área de daño en la posición del jugador
        if (areaAttackPrefab != null && player != null)
        {
            GameObject area = Instantiate(areaAttackPrefab, player.position, Quaternion.identity);
            BossAreaAttack areaScript = area.GetComponent<BossAreaAttack>();
            if (areaScript != null)
            {
                areaScript.damage = areaAttackDamage;
                areaScript.owner = gameObject;
            }
        }
        yield return new WaitForSeconds(0.5f); // Tiempo de anticipación antes del daño
    }

    protected override void Die()
    {
        Debug.Log("¡El Golem ha sido derrotado!");
        if (healthBar != null)
            healthBar.gameObject.SetActive(false);
        // Notificar al EventBus que el Boss ha muerto 
        EventBus.InvokeBossDefeated();
        base.Die();
    }
}