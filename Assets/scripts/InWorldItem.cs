using UnityEngine;

public class InWorldItem : MonoBehaviour
{
    public Item item;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerInventory>().AddItem(item);
            Destroy(gameObject); 
        }
    }
}