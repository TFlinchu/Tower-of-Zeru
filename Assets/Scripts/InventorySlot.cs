using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{

    public Image icon;
    public Button DropButton;
    private GameObject player;

    Item item;

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

    public void ClearSlot()
    {
        item = null;

        icon.sprite = null;
        icon.enabled = false;
        DropButton.interactable = false;
    }

    public void OnDropButton()
    {
        Item droppedItem = Inventory.instance.RemoveItem(item);
        Vector3 dropPosition = player.transform.position + player.transform.forward;
        Instantiate(droppedItem.itemPrefab, dropPosition, Quaternion.identity);
    }

    public void moveItem()
    {
        Debug.Log("Moving " + item.name);
    }
}
