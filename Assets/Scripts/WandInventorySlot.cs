using UnityEngine;
using UnityEngine.UI;

public class WandInventorySlot : MonoBehaviour
{
    public Image icon;
    public Button DropButton;
    public Button SlotButton;
    private GameObject player;

    Item item;
    int index;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void AddItem(Item newItem)
    {
        item = newItem;

        icon.sprite = item.icon;
        icon.enabled = true;
        DropButton.interactable = true;
    }

    public void setIndex(int i)
    {
        index = i;
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
        Item droppedItem = WandInventory.instance.RemoveItem(index);
        Vector3 dropPosition = player.transform.position + player.transform.forward;
        Instantiate(droppedItem.itemPrefab, dropPosition, Quaternion.identity);
    }

    public void OnSlotButton()
    {
        if (Inventory.instance.itemBeingMoved != null)
        {
            Item item = Inventory.instance.GetItem(Inventory.instance.itemBeingMovedIndex);
            bool happened = WandInventory.instance.AddItem(item);
            if (happened)
                Inventory.instance.RemoveItem(Inventory.instance.itemBeingMovedIndex);
        }
        else
        {
            WandInventory.instance.MoveItem(index);
        }
    }
}
