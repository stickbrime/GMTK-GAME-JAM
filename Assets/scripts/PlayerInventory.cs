using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor.U2D;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory instance;
    void Awake()
    {
        instance = this;
    }


    public List<Item> items = new List<Item>();
    private SpriteRenderer spriteRenderer;
    public GameObject weapon;

    public int equipboxnum;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        equipboxnum = PlayerPrefs.GetInt("box", 0);

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
            weapon.SetActive(true);
        }
    }

    public bool HasKey(string keyName)
    {
        return items.Exists(i => i.itemType == ItemType.Key && i.itemName == keyName);
    }

    public void Saveequipbox()
    {
        equipboxnum += 1;
        PlayerPrefs.SetInt("box", equipboxnum);
    }

    public bool checkequip()
    {
        bool isok = false;

        // if(equipboxnum==4 && 

        return isok;
    }

    public bool getkeys()
    {
        int keynum = PlayerPrefs.GetInt("key", 0);
        if (keynum == 1)
        {
            PlayerPrefs.SetInt("key", 0);
            return true;

        }
        else
        {
            return false;
        }

    }
    void OnTriggerEnter2D(Collider2D other)
    {
          if (other.CompareTag("key"))
        {
            PlayerPrefs.SetInt("key", 1);
            Destroy(other.gameObject);
         }
    }
}