using UnityEngine;

public class InWorldItem : MonoBehaviour
{
    public Item item;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("player"))
        {
            collision.GetComponent<PlayerInventory>().AddItem(item);
            Destroy(gameObject); 
        }
    }
}