using System.Collections.Generic;
using UnityEditor.U2D;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<Item> items = new List<Item>();
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void AddItem(Item item)
    {
        items.Add(item);
        Debug.Log($"Picked up: {item.itemName}");

        if (item.itemType == ItemType.Weapon)
        {
            EquipWeapon(item);
        }
    }

    private void EquipWeapon(Item weaponItem)
    {
        Debug.Log($"Equipped weapon: {weaponItem.itemName}");
        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.red;
        }
    }

    public bool HasKey(string keyName)
    {
        return items.Exists(i => i.itemType == ItemType.Key && i.itemName == keyName);
    }
}