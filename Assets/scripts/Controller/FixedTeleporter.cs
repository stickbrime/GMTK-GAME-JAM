using UnityEngine;

public class FixedTeleporter : MonoBehaviour
{
    [Header("Teleport Settings")]
    public string targetScene = "Level2";
    public float fadeDuration = 1f;
    public Vector2 viewportPosition = new Vector2(0.9f, 0.9f);
    public float activationRadius = 1f;

    [Header("Visuals")]
    public GameObject teleportEffect;
    public SpriteRenderer indicator;
    public Color activeColor = Color.yellow;
    public Color inactiveColor = Color.blue;

    private Transform player;
    private bool playerInRange;
    private bool isTeleporting;

    void Start()
    {
        // 定位到屏幕右上角
        UpdatePosition();
        Camera.onPreRender += OnCameraPreRender;
    }

    void OnDestroy()
    {
        Camera.onPreRender -= OnCameraPreRender;
    }

    void OnCameraPreRender(Camera cam)
    {
        if (cam.CompareTag("MainCamera"))
        {
            UpdatePosition();
        }
    }

    void UpdatePosition()
    {
        Vector3 worldPos = Camera.main.ViewportToWorldPoint(
            new Vector3(viewportPosition.x, viewportPosition.y, 10f)
        );
        transform.position = new Vector3(worldPos.x, worldPos.y, 0f);
    }

    void Update()
    {
        if (isTeleporting) return;

        if (player == null && PlayerController.Instance != null)
        {
            player = PlayerController.Instance.transform;
        }

        if (player != null)
        {
            float distance = Vector2.Distance(transform.position, player.position);
            bool nowInRange = distance <= activationRadius;

            if (nowInRange != playerInRange)
            {
                playerInRange = nowInRange;
                UpdateVisuals();
            }

            if (playerInRange && Input.GetKeyDown(KeyCode.E))
            {
                StartTeleport();
            }
        }
    }

    void UpdateVisuals()
    {
        if (indicator != null)
        {
            indicator.color = playerInRange ? activeColor : inactiveColor;
        }
    }

    void StartTeleport()
    {
        if (isTeleporting || SceneTransitionManager.Instance == null) return;

        isTeleporting = true;

        if (teleportEffect != null)
        {
            Instantiate(teleportEffect, transform.position, Quaternion.identity);
        }

        // 禁用玩家输入
        if (PlayerController.Instance != null)
        {
            PlayerController.Instance.enabled = false;
        }

        // 开始传送流程
        SceneTransitionManager.Instance.TransitionToScene(
            targetScene,
            fadeDuration,
            OnTeleportComplete
        );
    }

    void OnTeleportComplete()
    {
        isTeleporting = false;

        // 重新启用玩家输入
        if (PlayerController.Instance != null)
        {
            PlayerController.Instance.enabled = true;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, activationRadius);
        Gizmos.DrawIcon(transform.position + Vector3.up * 0.5f, "TeleportIcon");
    }
}