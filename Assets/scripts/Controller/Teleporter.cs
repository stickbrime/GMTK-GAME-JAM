using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Teleporter : MonoBehaviour
{
    [Header("传送设置")]
    public string targetSceneName;
    public KeyCode interactKey = KeyCode.E;
    public float activationRange = 1.5f;
    public float fadeDuration = 1f;

    [Header("视觉反馈")]
    public GameObject activationIndicator;
    public ParticleSystem teleportEffect;

    private bool playerInRange;
    private Transform playerTransform;

    void Update()
    {
        // 确保玩家引用有效
        if (playerTransform == null && PlayerController.Instance != null)
        {
            playerTransform = PlayerController.Instance.transform;
        }

        // 检测玩家距离
        if (playerTransform != null)
        {
            float distance = Vector2.Distance(transform.position, playerTransform.position);
            playerInRange = distance <= activationRange;

            // 更新视觉反馈
            UpdateVisualFeedback();
        }

        // 处理传送输入
        if (playerInRange && Input.GetKeyDown(interactKey))
        {
            TriggerTeleport();
        }
    }

    void UpdateVisualFeedback()
    {
        if (activationIndicator != null)
        {
            activationIndicator.SetActive(playerInRange);
        }
    }

    void TriggerTeleport()
    {
        if (SceneTransitionManager.Instance == null)
        {
            Debug.LogError("SceneTransitionManager 未初始化！");
            return;
        }

        // 播放传送特效
        if (teleportEffect != null)
        {
            teleportEffect.Play();
        }

        // 开始传送流程
        SceneTransitionManager.Instance.TransitionToScene(targetSceneName, fadeDuration);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, activationRange);
    }
}