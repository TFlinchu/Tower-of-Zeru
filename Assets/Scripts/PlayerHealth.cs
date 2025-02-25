using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public static int currentHealth = 100;
    public Animator animator;
    public Rigidbody2D rb;
    private Healthbar healthbar;
    public static bool isFirstLoad = true;
    public Item defaultItem;
    AudioManager audioManager;

    private void Awake() {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

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
        audioManager.PlaySFX(audioManager.playerDamage);
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
        GameObject inventory = GameObject.Find("Inventory");
        GameObject gameManager = GameObject.Find("GameManager");

        if (inventory != null)
        {
            Destroy(inventory);
        }

        if (gameManager != null)
        {
            Destroy(gameManager);
        }

        isFirstLoad = true;
        currentHealth = maxHealth;
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
