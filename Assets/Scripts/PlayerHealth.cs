using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public Animator animator;
    public Rigidbody2D rb;
    private Healthbar healthbar;
    public static bool isFirstLoad = true;
    public Item defaultItem;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        GameObject healthBarFillingObject = GameObject.Find("Canvas/Healthbar/HealthBar Filling");
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
        Invoke("LoadScene", 2f); // Call LoadScene after 2 seconds
    }

    void LoadScene()
    {
        isFirstLoad = true;
        SceneManager.LoadScene(1); // Load scene 1
    }

    // Update is called once per frame
    void Update()
    {
        if (isFirstLoad)
        {
            Inventory.instance.AddItem(defaultItem);
            isFirstLoad = false;
        }
    }
}
