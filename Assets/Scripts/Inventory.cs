using System.Collections;
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
    public List<Item> items = new List<Item>();

    // adds an item (only spells for now) to your inventory
    public bool AddItem(Item item)
    {
        // inventory is full
        if (items.Count >= space)
        {
            Debug.Log("Not enough room.");
            return false;
        }
        // inventory has room
        items.Add(item);
        // checks if there is a callback, then invokes it
        onItemChangedCallback?.Invoke();
        return true;
    }

    // removes an item from your inventory
    public void RemoveItem(Item item)
    {
        items.Remove(item);
        // drop item

        onItemChangedCallback?.Invoke();
    }

}
