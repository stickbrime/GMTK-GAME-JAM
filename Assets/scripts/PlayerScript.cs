using System.Collections;

using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    public GameObject player;
    public float movespeed = 5;
    public int scenei = 1;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public float attackCooldown = 1f;
    public float attackDuration = 0.2f;
    private float lastAttackTime;
    private PlayerInventory inventory;
    private SpriteRenderer spriteRenderer;
    public Rigidbody2D playerphys;
    public Animator playeranims;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        inventory = GetComponent<PlayerInventory>();
        playerphys = this.GetComponent<Rigidbody2D>();
        playeranims = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        playeranims.SetFloat("movespeed", movespeed);
        if (Input.GetKey(KeyCode.W))
        {
            // player.transform.position += (Vector3.up * movespeed) * Time.deltaTime;
            playerphys.AddForce((Vector3.up * movespeed), ForceMode2D.Impulse);
        }
        if (Input.GetKey(KeyCode.A))
        {
            // player.transform.position += (Vector3.left * movespeed) * Time.deltaTime;
            playerphys.AddForce((Vector3.left * movespeed), ForceMode2D.Impulse);
            player.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        }
        if (Input.GetKey(KeyCode.S))
        {
            // player.transform.position += (Vector3.down * movespeed) * Time.deltaTime;
            playerphys.AddForce((Vector3.down * movespeed), ForceMode2D.Impulse);
        }
        if (Input.GetKey(KeyCode.D))
        {
            // player.transform.position += (Vector3.right * movespeed) * Time.deltaTime;
            playerphys.AddForce((Vector3.right * movespeed), ForceMode2D.Impulse);
            player.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
        if (Input.GetKey(KeyCode.Space) && Time.time - lastAttackTime >= attackCooldown)
        {
            if (HasWeapon())
            {
                StartCoroutine(HandleAttack());
                lastAttackTime = Time.time;
            }
            else
            {
                Debug.Log("No weapon equipped.");
            }
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            movespeed = 2;
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            movespeed = 0.5f;
        }
        else
        {
            movespeed = 1;
        }
        // Debug.Log(scenei);
    }
    bool HasWeapon()
    {
        return inventory.items.Exists(i => i.itemType == ItemType.Weapon);
    }
    private IEnumerator HandleAttack()
    {
        if (spriteRenderer == null) yield break;

        // Change color to white during attack
        spriteRenderer.color = Color.white;

        // Run attack logic
        Attack();

        // Wait for full duration (attack + cooldown)
        yield return new WaitForSeconds(attackCooldown);

        // Restore color
        spriteRenderer.color = HasWeapon() ? Color.red : Color.white;
    }


    void Attack()
    {
      
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "enemy")
        {
            playeranims.SetTrigger("ihurt");
        }
    }
}