using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    // reference to the item scriptable object
    public Item item;

    // pick up
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Picked up " + item.name + "!");
            PickUp();
        }
    }

    public void PickUp()
    {
        // add to inventory
        bool wasPickedUp = Inventory.instance.AddItem(item);
        // play sound
        // destroy item
        if (wasPickedUp)
        {
            Destroy(gameObject);
        }
    }
}
