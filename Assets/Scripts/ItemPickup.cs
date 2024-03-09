using TMPro;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    // reference to the item scriptable object
    public Item item;

    private bool isPlayerinTrigger = false;


public void OnTriggerEnter2D(Collider2D other)
{
    if (other.gameObject.tag == "Player")
    {
        isPlayerinTrigger = true;
        UIManager.instance.ShowPickupText(item.itemName);
    }
}

public void OnTriggerExit2D(Collider2D other)
{
    if (other.gameObject.tag == "Player")
    {
        isPlayerinTrigger = false;
        UIManager.instance.HidePickupText();
    }
}

    void Update()
    {
        if (isPlayerinTrigger && Input.GetKeyDown(KeyCode.E))
        {
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
