using System.Collections;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public float damage = 10f;
    public Collider damageCollider;
    AnimationCharacter animationCharacter;
    void Start()
    {
        animationCharacter = GetComponent<AnimationCharacter>();
    }

    public float attackCooldown = 1f;
    
    public void PerformAttack()
    {
        if (animationCharacter.isAttacking) return; // Evitar ataques consecutivos 
        animationCharacter.isAttacking = true;
        animationCharacter.animator.SetBool("Attack", animationCharacter.isAttacking);
        Collider[]hits = Physics.OverlapBox(damageCollider.bounds.center, 
        damageCollider.bounds.extents, damageCollider.transform.rotation, 
        Physics.AllLayers, QueryTriggerInteraction.Ignore);
        foreach (Collider hit in hits)      
        {
            if (hit.gameObject != gameObject) // Evitar dañarse a sí mismo
            {
                IDamageable damageable = hit.GetComponent<IDamageable>();
                if (damageable != null)
                {
                    damageable.TakeDamage(damage);
                    Debug.Log($"{hit.gameObject.name} ha recibido {damage} de daño.");
                }
            }
        }      
        StartCoroutine(AttackCooldown());  
    }
    IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        EndAttack();
        yield break;
    }
    public void EndAttack()
    {
        animationCharacter.isAttacking = false;
        if (animationCharacter.animator != null)
            animationCharacter.animator.SetBool("Attack", false);
        StopAllCoroutines(); // Fuerza la parada de cualquier cooldown activo
    }
}
