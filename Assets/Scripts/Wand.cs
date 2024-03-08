using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine;

public class Wand : MonoBehaviour
{
    public GameObject ballPrefab;
    public float speed = 10f; // Speed of the ball
    public float attackDelay = 0.5f; // Delay between attacks in seconds
    private bool canAttack = true; // If the player can attack
    private Coroutine shootingCoroutine; // Reference to the shooting coroutine

    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject()) {
            return;
        }
        if (Input.GetMouseButton(0) && canAttack)
        {
            if (shootingCoroutine == null)
            {
                shootingCoroutine = StartCoroutine(Shoot());
            }
        }
        else if (Input.GetMouseButtonUp(0) && shootingCoroutine != null)
        {
            StopCoroutine(shootingCoroutine);
            shootingCoroutine = null;
        }
    }

    private IEnumerator Shoot()
    {
        attackCooldown();

        // setting shoot direction
        Vector3 shootDirection;
        shootDirection = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        shootDirection.z = 0.0f;

        // Normalize the shoot direction
        shootDirection.Normalize();

        // Create and shoot the ball
        GameObject ball = Instantiate(ballPrefab, transform.position, Quaternion.identity);
        Rigidbody2D ballRigidbody = ball.GetComponent<Rigidbody2D>();
        ballRigidbody.velocity = new Vector2(shootDirection.x * speed, shootDirection.y * speed);

        // Destroy the ball after 1 second
        Destroy(ball, 1f);

        yield return new WaitForSeconds(attackDelay);

        shootingCoroutine = null;
    }

    private void attackCooldown()
    {
        canAttack = false;
        Invoke("resetAttack", attackDelay);
    }

    private void resetAttack()
    {
        canAttack = true;
    }
}