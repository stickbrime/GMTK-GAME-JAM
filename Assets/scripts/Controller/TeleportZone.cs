using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SpriteRenderer))] // 确保有SpriteRenderer组件
public class TeleportZone : MonoBehaviour
{
    [Header("传送设置")]
    public string targetScene = "Level 02";
    public float fadeDuration = 1.5f;
    public KeyCode interactKey = KeyCode.E;

    [Header("视觉反馈")]
    public Color highlightColor = new Color(1, 0.8f, 0.2f);

    private Color originalColor;
    private SpriteRenderer spriteRenderer;
    private bool playerInRange;

    void Start()
    {
        // 安全获取组件
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer component missing!", this);
            enabled = false; // 禁用脚本
            return;
        }

        originalColor = spriteRenderer.color;

        // 确保碰撞体是触发器
        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null)
        {
            collider.isTrigger = true;
        }
        else
        {
            Debug.LogError("Collider2D component missing!", this);
        }
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(interactKey))
        {
            if (SceneTransitionManager.Instance != null)
            {
                SceneTransitionManager.Instance.TransitionToScene(targetScene, fadeDuration);
            }
            else
            {
                Debug.LogWarning("SceneTransitionManager instance missing");
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // 更安全的标签检查
        if (other != null && other.CompareTag("Player"))
        {
            playerInRange = true;
            if (spriteRenderer != null)
            {
                spriteRenderer.color = highlightColor;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other != null && other.CompareTag("Player"))
        {
            playerInRange = false;
            if (spriteRenderer != null)
            {
                spriteRenderer.color = originalColor;
            }
        }
    }

    void OnDrawGizmos()
    {
        // 可视化触发范围
        Gizmos.color = Color.cyan;
        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null)
        {
            Gizmos.DrawWireSphere(transform.position,
                collider.bounds.extents.magnitude);
        }
    }
}