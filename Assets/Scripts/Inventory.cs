using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // singleton pattern, to be called from other scripts
    public static Inventory instance;

    void Awake()
    {
        instance = this;
    }

    // will be used to update inventory UI
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    // basic inventory space
    public int space = 10;
    public Item[] items; // Changed from List<Item> to Item[]

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
        return item;
    }
}