using UnityEngine;
using System.Collections;

public class Archer : Enemy
{
    [Header("Ataque a Distancia")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private float projectileSpeed = 10f;

    private bool isAttacking = false;
    private Animator animator; 

    protected override void Start()
    {
        base.Start();
        attackRange = 8f;
        animator = GetComponent<Animator>(); // Caché
    }

    protected override void AttackBehaviour()
    {
        if (player == null) return;

        // Si el jugador se aleja, vuelve a perseguir
        if (Vector3.Distance(transform.position, player.position) > attackRange)
        {
            currentState = EnemyState.Chasing;
            return;
        }

        // Dispara si no está ya atacando
        if (!isAttacking)
        {
            StartCoroutine(ShootRoutine());
        }
    }
    private IEnumerator ShootRoutine()
    {
        isAttacking = true;
        Debug.Log($"{gameObject.name}: Iniciando disparo.");

        if (animator != null)
            animator.SetBool("Attack", true);

        // Verificar referencias
        if (projectilePrefab == null)
        {
            Debug.LogError($"{gameObject.name}: No tiene asignado Projectile Prefab.");
            isAttacking = false;
            yield break;
        }
        if (shootPoint == null)
        {
            Debug.LogError($"{gameObject.name}: No tiene asignado Shoot Point.");
            isAttacking = false;
            yield break;
        }

        // Instanciar flecha
        Debug.Log($"{gameObject.name}: Instanciando flecha en {shootPoint.position}.");
        GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);
        Projectile projScript = projectile.GetComponent<Projectile>();
        if (projScript != null)
            projScript.SetOwner(gameObject);

        // Aplicar velocidad
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 direction = (player.position - shootPoint.position).normalized;
            Debug.Log($"{gameObject.name}: Dirección de disparo = {direction}, Velocidad = {projectileSpeed}");
            rb.linearVelocity = direction * projectileSpeed;
        }
        else
        {
            Debug.LogError($"{gameObject.name}: El proyectil no tiene Rigidbody.");
        }

        // Esperar animación de ataque
        yield return new WaitForSeconds(0.5f);

        if (animator != null)
            animator.SetBool("Attack", false);

        Debug.Log($"{gameObject.name}: Disparo completado. Esperando cooldown.");
        yield return new WaitForSeconds(1.0f);
        isAttacking = false;
    }
    /*private IEnumerator ShootRoutine()
    {
        isAttacking = true;

        
        if (animator != null)
            animator.SetBool("Attack", true);

        // Instancia la flecha
        if (projectilePrefab != null && shootPoint != null)
        {
            GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);
            Projectile projScript = projectile.GetComponent<Projectile>();
            if (projScript != null)
                projScript.SetOwner(gameObject);  // <--- ignorar al arquero
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 direction = (player.position - shootPoint.position).normalized;
                rb.linearVelocity = direction * projectileSpeed;
            }
        }

        // Espera la duración del ataque (ajusta según tu animación)
        yield return new WaitForSeconds(0.5f);

        // --- NUEVO: Desactivar animación de ataque ---
        if (animator != null)
            animator.SetBool("Attack", false);

        // Cooldown hasta el próximo disparo
        yield return new WaitForSeconds(1.0f);
        isAttacking = false;
    }*/

    /*private void Update()
    {
        if (player == null)
        {
            Debug.Log($"{gameObject.name}: No tengo referencia al jugador.");
            return;
        }
        Debug.Log($"{gameObject.name}: Estado={currentState}, Distancia={Vector3.Distance(transform.position, player.position)}");
    }*/
}