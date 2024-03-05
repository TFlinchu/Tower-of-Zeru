using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour
{
    public Transform target;
    public float speed = 2f;
    public Rigidbody2D rb;
    private bool isStopping = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // lock rotation
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    private void Update()
    {
        // Find the target
        if (!target)
        {
            FindTarget();
        }
    }

    private void FixedUpdate()
    {
        // Move towards target
        if (target && !isStopping)
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
            // start stopping
            Debug.Log("Player entered trigger area");
            isStopping = true;
        }
    }

    // leaves attack range
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            // stop stopping
            Debug.Log("Player left trigger area");
            isStopping = false;
        }
    }

    // collides with body
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            // damage player
            Debug.Log("player touched enemy");
        }
    }

    private void FindTarget()
    {
        // Find the target
        target = GameObject.FindGameObjectWithTag("Player").transform;
        Debug.Log(target);
    }
    private IEnumerator StopAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        isStopping = true;
    }

}
