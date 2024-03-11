using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine;

public class WandInventory : MonoBehaviour
{
    // singleton pattern, to be called from other scripts
    public static WandInventory instance;

    void Awake()
    {
        instance = this;
    }

    // will be used to update inventory UI
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    // basic inventory space
    public int space = 6;
    public Item[] items; // Changed from List<Item> to Item[]
    public Item itemBeingMoved;
    public int itemBeingMovedIndex;

    void Start()
    {
        items = new Item[space]; // Initialize the array with the size of the inventory space
    }

    // adds an item (only spells for now) to your inventory
    public bool AddItem(Item item)
    {
        // Find the first empty slot
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == null)
            {
                items[i] = item;
                // checks if there is a callback, then invokes it
                onItemChangedCallback?.Invoke();
                return true;
            }
        }

        // If we reach here, it means the inventory is full
        Debug.Log("Not enough room.");
        return false;
    }

    // removes an item from your inventory
    public Item RemoveItem(int index) // Modify this line
    {
        Item item = items[index];
        items[index] = null;
        onItemChangedCallback?.Invoke();
        itemBeingMoved = null;
        itemBeingMovedIndex = -1;

        return item;
    }

    public void MoveItem(int index)
    {
        if (itemBeingMoved == null)
        {
            if (items[index] == null)
            {
                Debug.Log("No item to move");
                return;
            }
            // Set the item being moved
            Debug.Log("Item to move: " + items[index].itemName);
            itemBeingMoved = items[index];
            itemBeingMovedIndex = index;
        }
        else
        {
            if (items[index] == null)
            {
                items[index] = itemBeingMoved;
                Debug.Log("Swapped with " + items[itemBeingMovedIndex].itemName);
                items[itemBeingMovedIndex] = null;
            }
            else
            {
                // Swap the items
                Item temp = items[index];
                items[index] = itemBeingMoved;
                items[itemBeingMovedIndex] = temp;
                Debug.Log("Swapped " + items[index].itemName + " with " + items[itemBeingMovedIndex].itemName);
            }

            // Clear the item being moved
            itemBeingMoved = null;
            itemBeingMovedIndex = 9;
            Debug.Log("Item moved");

            // Update the UI
            onItemChangedCallback?.Invoke();
        }
    }

    // public void MoveItemToOtherInventory(int index)
    // {
    //     Inventory.instance.AddItem(RemoveItem(index));
    // }

    public Item GetItem(int index)
    {
        return items[index];
    }
}