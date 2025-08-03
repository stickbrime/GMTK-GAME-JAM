using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 2f;
    public float activationDistance = 10f;

    [Header("Health Settings")]
    public int maxHealth = 3;

    [Header("Key Drop Settings")]
    public GameObject keyPrefab;
    [Range(0, 1)] public float keyDropChance = 0.3f;

    private Transform player;
    private int currentHealth;
    private bool isActive = false;

    void Start()
    {
        // Ѱ����Ҷ���
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // ��ʼ������ֵ
        currentHealth = maxHealth;

        // ��ʼ���
        isActive = false;
    }

    void Update()
    {
        // ����Ƿ�Ӧ�ü���
        if (!isActive && player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);
            if (distanceToPlayer <= activationDistance)
            {
                isActive = true;
            }
        }

        // �����������Ҵ��ڣ�������ƶ�
        if (isActive && player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            transform.position += (Vector3)direction * moveSpeed * Time.deltaTime;
        }
    }

    // �ܵ��˺��ķ������ɴ������ű����ã�
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // ��������Ƿ����Կ��
        if (Random.value <= keyDropChance && keyPrefab != null)
        {
            Instantiate(keyPrefab, transform.position, Quaternion.identity);
        }

        // ���ٵ��˶���
        Destroy(gameObject);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("weapon"))
        {
            TakeDamage(currentHealth);
        }
    }
}