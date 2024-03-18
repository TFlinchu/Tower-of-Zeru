using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine;

public class Wand : MonoBehaviour
{
    public GameObject ballPrefab;
    public float speed = 10f; // Speed of the ball
    public float attackDelay = 0.5f; // Delay between attacks in seconds
    public float resetWandDelay = 1f; // Delay between attacks in seconds
    private bool canAttack = true; // If the player can attack
    private Coroutine shootingCoroutine; // Reference to the shooting coroutine

    int currentSlot = 0;

    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
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

        // Get the item in the first slot of the inventory
        Item item = WandInventory.instance.GetItem(currentSlot);
        if (item == null)
        {
            // Debug.Log("No item in the first slot.");
            // yield break;

            Debug.Log("slot: " + currentSlot + " was empty. Moving to next slot.");
            currentSlot++;

            if (currentSlot >= WandInventory.instance.space)
            {
                Debug.Log("current slot: " + currentSlot + " No more slots to shoot from. Resetting to slot 0.");
                currentSlot = 0;
                yield return new WaitForSeconds(resetWandDelay);
            } else {
                yield return StartCoroutine(Shoot());
            }

            shootingCoroutine = null;
            yield break;
        }

        Debug.Log("Shooting " + item.name);

        if (item.isModifier)
        {
            Debug.Log("Modifier: " + item.name);
        }

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

        currentSlot++;

        if (currentSlot >= WandInventory.instance.space)
        {
            Debug.Log("current slot:" + currentSlot + " No more slots to shoot from. Resetting to slot 0.");
            currentSlot = 0;
            yield return new WaitForSeconds(attackDelay); // should be changed to wand cooldown
        }
        else
        {
            yield return new WaitForSeconds(attackDelay);
        }

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