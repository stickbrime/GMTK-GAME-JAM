using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SpriteRenderer))] // ȷ����SpriteRenderer���
public class TeleportZone : MonoBehaviour
{
    [Header("��������")]
    public string targetScene = "Level 02";
    public float fadeDuration = 1.5f;
    public KeyCode interactKey = KeyCode.E;

    [Header("�Ӿ�����")]
    public Color highlightColor = new Color(1, 0.8f, 0.2f);

    private Color originalColor;
    private SpriteRenderer spriteRenderer;
    private bool playerInRange;

    void Start()
    {
        // ��ȫ��ȡ���
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer component missing!", this);
            enabled = false; // ���ýű�
            return;
        }

        originalColor = spriteRenderer.color;

        // ȷ����ײ���Ǵ�����
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

            if ( PlayerInventory.instance.getkeys())
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
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // ����ȫ�ı�ǩ���
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
        // ���ӻ�������Χ
        Gizmos.color = Color.cyan;
        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null)
        {
            Gizmos.DrawWireSphere(transform.position,
                collider.bounds.extents.magnitude);
        }
    }
}