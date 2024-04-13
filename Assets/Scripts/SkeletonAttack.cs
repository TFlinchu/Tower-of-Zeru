using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAttack : MonoBehaviour
{

    public int damage = 10;
    void OnTriggerEnter2D(Collider2D other)
    {
        // Damage the player
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
    }
}