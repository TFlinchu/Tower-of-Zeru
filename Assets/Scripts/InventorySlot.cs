using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
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
        //GameData.itemData = item;

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
        //GameData.itemData = null;

        icon.sprite = null;
        icon.enabled = false;
        DropButton.interactable = false;
    }

    public void OnDropButton()
    {
        Item droppedItem = Inventory.instance.RemoveItem(index);
        Vector3 dropPosition = player.transform.position + player.transform.forward;
        Instantiate(droppedItem.itemPrefab, dropPosition, Quaternion.identity);
    }

    public void OnSlotButton()
    {
        if (WandInventory.instance.itemBeingMoved != null)
        {
            Item item = WandInventory.instance.GetItem(WandInventory.instance.itemBeingMovedIndex);
            bool happened = Inventory.instance.AddItem(item);
            if (happened)
                WandInventory.instance.RemoveItem(WandInventory.instance.itemBeingMovedIndex);
        }
        else
        {
            Inventory.instance.MoveItem(index);
        }
    }
}
