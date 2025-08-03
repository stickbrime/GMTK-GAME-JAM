using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    [Header("Movement")]
    public float moveSpeed = 5f;
    public float acceleration = 0.5f;

    [Header("Block Spawning")]
    public float spawnRadius = 1.5f;
    public float spawnCooldown = 0.5f;
    public LayerMask blockCheckMask;
    public int maxBlocksPerScene = 20;

    private Rigidbody2D rb;
    private float lastSpawnTime;
    private Vector2 currentVelocity;
    private Camera mainCamera;

    [Header("Teleport")]
    public bool allowMovement = true;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (!allowMovement) return;

        HandleMovement();
        HandleBlockSpawning();
    }

    public void SetMovementEnabled(bool enabled)
    {
        allowMovement = enabled;
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
        }
    }

    void HandleMovement()
    {
        Vector2 input = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        ).normalized;

        Vector2 targetVelocity = input * moveSpeed;
        rb.velocity = Vector2.SmoothDamp(
            rb.velocity,
            targetVelocity,
            ref currentVelocity,
            acceleration
        );
    }

    void HandleBlockSpawning()
    {
        if (Input.GetKeyDown(KeyCode.F) && Time.time > lastSpawnTime + spawnCooldown)
        {
            TrySpawnBlock();
        }
    }

    void TrySpawnBlock()
    {
        if (BlockDataManager.Instance == null || BlockDataManager.Instance.blockPrefab == null)
        {
            Debug.LogWarning("Block system not ready");
            return;
        }

        string currentScene = SceneManager.GetActiveScene().name;
        var sceneData = BlockDataManager.Instance.GetSceneData(currentScene);

        // 检查方块数量限制
        if (sceneData.blockPositions.Count >= maxBlocksPerScene)
        {
            Debug.Log("Max blocks reached in this scene");
            return;
        }

        Vector2 spawnDirection = Random.insideUnitCircle.normalized;
        Vector2 spawnPos = (Vector2)transform.position + spawnDirection * spawnRadius;

        // 确保生成位置在屏幕内
        Vector2 viewportPos = mainCamera.WorldToViewportPoint(spawnPos);
        if (viewportPos.x < 0.1f || viewportPos.x > 0.9f ||
            viewportPos.y < 0.1f || viewportPos.y > 0.9f)
        {
            spawnPos = (Vector2)transform.position + spawnDirection * (spawnRadius * 0.5f);
        }

        Collider2D overlap = Physics2D.OverlapCircle(spawnPos, 0.4f, blockCheckMask);
        if (overlap == null)
        {
            GameObject newBlock = Instantiate(
                BlockDataManager.Instance.blockPrefab,
                spawnPos,
                Quaternion.identity
            );

            var blockCtrl = newBlock.GetComponent<BlockController>();
            if (blockCtrl != null)
            {
                blockCtrl.InitializeBlock(spawnPos);
            }

            lastSpawnTime = Time.time;
            Debug.Log($"Spawned new block at {spawnPos}");
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
}