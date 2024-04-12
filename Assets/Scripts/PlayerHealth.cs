using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public static int currentHealth = 100;
    public Animator animator;
    public Rigidbody2D rb;
    private Healthbar healthbar;

    // Start is called before the first frame update
    void Start()
    {
        // currentHealth = maxHealth;
        GameData.healthKeep = currentHealth;
        GameObject healthBarFillingObject = GameObject.Find("HP/Healthbar/HealthBar Filling");
        if (healthBarFillingObject != null)
        {
            healthbar = healthBarFillingObject.GetComponent<Healthbar>();
            if (healthbar == null)
            {
                Debug.LogError("Healthbar component not found in the HealthBar Filling object.");
            }
        }
        else
        {
            Debug.LogError("HealthBar Filling object not found in the scene.");
        }
        healthbar.healthData();
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        healthbar.SetHealth(currentHealth);
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
            healthbar.SetHealth(currentHealth);
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
