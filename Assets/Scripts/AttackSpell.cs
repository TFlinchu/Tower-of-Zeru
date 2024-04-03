using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public AttackSpellItem item;
    void OnTriggerEnter2D(Collider2D other)
    {
        // Damage an enemy
        if (other.gameObject.tag == "Enemy" && !other.isTrigger)
        {
            Skeleton enemyScript = other.gameObject.GetComponent<Skeleton>();
            if (enemyScript != null)
            {
                enemyScript.TakeDamage(item.damage);
            }
        }
    }
}