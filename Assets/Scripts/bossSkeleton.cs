using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSkeleton : Skeleton
{
    public Animator effectAnimator;

    // Override the AttackRepeatedly method
    protected override IEnumerator AttackRepeatedly()
    {
        // Delay between attacks
        float attackDelay = 2f;
        isStopped = true;

        while (isStopped)
        {
            isAttacking = true;
            // Disable movement
            rb.constraints = RigidbodyConstraints2D.FreezeAll;

            yield return new WaitForSeconds(0.5f);

            animator.SetTrigger("Attack");

            // Trigger the special effect
            effectAnimator.SetTrigger("Attack");

            // Wait for the attack animation to finish
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

            // Re-enable movement
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;

            yield return new WaitForSeconds(attackDelay);
        }
        animator.SetBool("isMoving", true);
        isAttacking = false;
    }
}