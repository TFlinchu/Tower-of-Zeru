using UnityEngine;

public class Wand : MonoBehaviour
{
    public GameObject ballPrefab;
    public float speed = 10f; // Speed of the ball

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
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
        }
    }
}