using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Vector2 movement;
    Vector2 moveDirection;
    // float horizontalInput;
    // float vertictalInput;
    float moveSpeed = 5f;
    Rigidbody2D rb;
    bool isfacingRight = true;
    public Animator animator;
    public VectorValue startingPostion;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        transform.position = startingPostion.initialValue;
    }

    // Update is called once per frame
    void Update()
    {
        // horizontalInput = Input.GetAxisRaw("Horizontal");
        // vertictalInput = Input.GetAxisRaw("Vertical");
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2( movement.x, movement.y).normalized;

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    void FixedUpdate() {
        //rb.velocity = new Vector2(horizontalInput * moveSpeed, 0f);

        //rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
        if (movement.x > 0 && !isfacingRight) {
            FlipSprite();
        }
        else if (movement.x < 0 && isfacingRight) {
            FlipSprite();
        }
    }

    void FlipSprite() {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        isfacingRight = !isfacingRight;
        // if (isFacingRight && horizontalInput < 0f || !isFacingRight && horizontalInput > 0f) {
        //     isFacingRight = !isFacingRight;
        //     Vector3 ls = transform.localScale;
        //     ls.x *= -1f;
        //     transform.localScale = -ls;
    }
}

