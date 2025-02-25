using System.Runtime.CompilerServices;
using UnityEngine;

public class WandInventoryUI : MonoBehaviour
{
    
    public Transform itemsParent;
    public GameObject inventoryUI;
    WandInventory inventory;
    WandInventorySlot[] slots;

    // Start is called before the first frame update
    void Start()
    {
        inventory = WandInventory.instance;
        inventory.onItemChangedCallback += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<WandInventorySlot>();

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].setIndex(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }
    }

    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {

            if (i < inventory.items.Length && inventory.items[i] != null)
            {
                slots[i].AddItem(inventory.items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}
