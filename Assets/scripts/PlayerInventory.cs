using System.Collections.Generic;
using TMPro;
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
        equipboxnum = 0;
        spriteRenderer = GetComponent<SpriteRenderer>();

        equipboxnum = PlayerPrefs.GetInt("box", 0);
        tMP_Text.text = equipboxnum.ToString();
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
        Debug.Log(equipboxnum);
        equipboxnum += 1;
        PlayerPrefs.SetInt("box", equipboxnum);
        tMP_Text.text = equipboxnum.ToString();

    }


    public TMP_Text tMP_Text;
    public bool checkequip()
    {
        bool isok = false;

        if (equipboxnum >= 4)
        {

            isok = true;
         } 

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
        if (other.CompareTag("box"))
        {
            Saveequipbox();
            Destroy(other.gameObject);
         }
    }
}