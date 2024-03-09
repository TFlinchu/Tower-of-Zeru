using UnityEngine;


// Can be used to create a new item in the Unity Editor.
// something like this should be applied to the enemies if we intend to make a new one
[CreateAssetMenu(fileName = "item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string itemName = "item";
    public Sprite icon = null;
    public GameObject itemPrefab = null;
}
