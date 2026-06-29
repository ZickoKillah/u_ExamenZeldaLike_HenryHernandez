using System;
using UnityEngine;

public class AnimationCharacter : MonoBehaviour
{
   public Animator animator;
   private Rigidbody rb;
   public bool isAttacking = false;


    private void Awake()
    {
         rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        animator.SetBool("Attack", isAttacking);
        if(isAttacking) return; // Evitar actualizar la velocidad durante el ataque
        Vector3 rbVel = rb.linearVelocity;
        float speed = MathF.Abs(rbVel.x) + MathF.Abs(rbVel.z);
        animator.SetFloat("Speed", speed);
    }
}
