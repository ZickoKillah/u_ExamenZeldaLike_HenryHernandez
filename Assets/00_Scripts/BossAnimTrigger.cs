using UnityEngine;

public class BossAnimTrigger : MonoBehaviour
{
    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayAssemble()
    {
        if (animator != null)
            animator.SetTrigger("PlayAssemble");
    }
}