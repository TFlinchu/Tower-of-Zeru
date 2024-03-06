using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour
{
    public Transform target;
    public Rigidbody2D rb;
    public Animator animator;
    private bool isStopped = false;
    public float speed = 2f;
    public float hp = 100f;
    private Coroutine attackCoroutine;
    public GameObject hurtbox;
    private bool isAttacking = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Get the Animator component on the child GameObject
        animator = GetComponentInChildren<Animator>();

        // lock rotation
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    private void Update()
    {
        // Find the target
        if (!target)
        {
            FindTarget();
            animator.SetBool("isMoving", true);
        }
    }

    private void FixedUpdate()
    {
        // Move towards target
        if (target && !isStopped && !isAttacking)
        {
            Vector2 direction = ((Vector2)target.position - rb.position).normalized;
            Vector2 newPosition = rb.position + direction * speed * Time.fixedDeltaTime;
            rb.MovePosition(newPosition);

            // Flip the sprite based on direction
            if (direction.x > 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }

    // enters attack range
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            isStopped = true;
            animator.SetBool("isMoving", false);
            // Start the attack coroutine when the player enters the range
            attackCoroutine = StartCoroutine(AttackRepeatedly());
        }
    }

    // leaves attack range
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            // Stop the attack coroutine when the player leaves the range
            isStopped = false;
        }
    }

    // Attack repeatedly
    private IEnumerator AttackRepeatedly()
    {
        // Delay between attacks
        float attackDelay = 1f;
        isStopped = true;

        while (isStopped)
        {
            isAttacking = true;
            // Disable movement
            rb.constraints = RigidbodyConstraints2D.FreezeAll;

            animator.SetTrigger("Attack");

            // Wait for the attack animation to finish
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

            // Re-enable movement
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;

            yield return new WaitForSeconds(attackDelay);
        }
        animator.SetBool("isMoving", true);
        isAttacking = false;
    }

    // collides with body
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            // damage player
            other.gameObject.GetComponent<PlayerHealth>().TakeDamage(10);

            // Testing
            TakeDamage(25);
        }
    }

    private void FindTarget()
    {
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            // Find the target
            target = GameObject.FindGameObjectWithTag("Player").transform;
            if (target)
            {
                isStopped = false;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Die();
        }
    }

    public void enableHurtbox()
    {
        // Enable the Collider2D component on the hurtbox
        hurtbox.GetComponent<Collider2D>().enabled = true;
    }
    public void disableHurtbox()
    {
        hurtbox.GetComponent<Collider2D>().enabled = false;
    }

    private void Die()
    {
        animator.SetTrigger("Die");

        // Disable all components except for the Renderer
        foreach (var component in GetComponents<Component>())
        {
            if (!(component is Renderer) && !(component is Animator))
            {
                if (component is Behaviour behaviour) behaviour.enabled = false;
            }
        }
    }
}
