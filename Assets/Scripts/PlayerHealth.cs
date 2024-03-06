using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public Animator animator;
    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        // rb.GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        // TakeDamage(100);
    }

    public void TakeDamage(int amount) 
    {
        currentHealth -= amount;
        if (currentHealth <= 0) 
        {
            Die();
        }
    }

    public void Heal(int amount) 
    {
        currentHealth += amount;
        if (currentHealth > maxHealth) 
        {
            currentHealth = maxHealth;
        }
    }

    public void Die() 
    {
        rb.bodyType = RigidbodyType2D.Static;
        animator.SetTrigger("Death");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
