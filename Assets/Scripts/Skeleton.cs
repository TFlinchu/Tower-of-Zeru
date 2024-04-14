using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Skeleton : MonoBehaviour
{
    public Transform target;
    public Rigidbody2D rb;
    public Animator animator;
    protected bool isStopped;
    protected bool isAttacking;
    public float speed = 2f;
    public float hp = 100f;
    private Coroutine attackCoroutine;
    public GameObject hurtbox;

    public GameObject spell1Prefab;
    public GameObject spell2Prefab;
    public GameObject spell3Prefab;
    public GameObject spell4Prefab;
    AudioManager audioManager;

    private void Awake() {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    public struct SpellDrop
    {
        public GameObject spellPrefab;
        public float dropRate;

        public SpellDrop(GameObject spellPrefab, float dropRate)
        {
            this.spellPrefab = spellPrefab;
            this.dropRate = dropRate;
        }
    }

    // Create a list to hold the spells and their drop rates
    public List<SpellDrop> spellDrops = new List<SpellDrop>();

    // Populate the list with your spells and their drop rates
    // Do this in the Start() method or in the Unity editor
    void Start()
    {
        spellDrops.Add(new SpellDrop(spell1Prefab, 0.75f));
        spellDrops.Add(new SpellDrop(spell2Prefab, 0.75f));
        spellDrops.Add(new SpellDrop(spell3Prefab, 0.25f));
        spellDrops.Add(new SpellDrop(spell4Prefab, 0.25f));
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
            audioManager.PlaySFX(audioManager.skeletonDamage3);
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
    protected virtual IEnumerator AttackRepeatedly()
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
        audioManager.PlaySFX(audioManager.skeletonDamage);
        if (hp <= 0)
        {
            if (SceneManager.GetActiveScene () == SceneManager.GetSceneByName ("BossRoom")) {
                audioManager.PlaySFX(audioManager.skeletonDeath);
                audioManager.PlaySFX(audioManager.winSound);
                Die();
            }
            else {
                audioManager.PlaySFX(audioManager.skeletonDeath);
                Die();
            }
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
        disableHurtbox();
        foreach (var component in GetComponents<Component>())
        {
            if (!(component is Renderer) && !(component is Animator))
            {
                if (component is Behaviour behaviour) behaviour.enabled = false;
            }
        }

        // Access the SpawnManager instance and call the EnemyKilled() method
        FindObjectOfType<SpawnManager>().EnemyKilled();

        // Determine if a spell should be dropped
        if (Random.value < 0.5f)
        {
            // Determine which spell should be dropped
            float totalDropRate = spellDrops.Sum(spellDrop => spellDrop.dropRate);
            float randomDrop = Random.value * totalDropRate;

            foreach (var spellDrop in spellDrops)
            {
                if (randomDrop < spellDrop.dropRate)
                {
                    // Drop the spell
                    audioManager.PlaySFX(audioManager.itemSound);
                    Instantiate(spellDrop.spellPrefab, transform.position, Quaternion.identity);
                    break;
                }
                else
                {
                    randomDrop -= spellDrop.dropRate;
                }
            }
        }
    }
}