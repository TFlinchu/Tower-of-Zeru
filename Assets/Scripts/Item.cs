using UnityEngine;


// Can be used to create a new item in the Unity Editor.
// something like this should be applied to the enemies if we intend to make a new one
[CreateAssetMenu(fileName = "item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string itemName = "item";
    public Sprite icon = null;
    public GameObject itemPrefab = null;
     public float damage = 0;
    public bool isModifier = false;
    public enum ModifierType { None, Damage, Speed }
    public ModifierType modifierType = ModifierType.None;
    public float modifierValue = 0f;
}
