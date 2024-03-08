using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{

    public Image icon;
    public Button DropButton; 

    Item item;

    public void AddItem(Item newItem) 
    {
        item = newItem;
        
        icon.sprite = item.icon;
        icon.enabled = true;
        DropButton.interactable = true;

    }

    public void ClearSlot() 
    {
        item = null;
        
        icon.sprite = null;
        icon.enabled = false;
        DropButton.interactable = false;
    }

    public void OnDropButton() 
    {
        Debug.Log("Dropping " + item.name);
        Inventory.instance.RemoveItem(item);
    }

    public void moveItem() {
        Debug.Log("Moving " + item.name);
    }
}
