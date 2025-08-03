using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider2D))]
public class BlockController : MonoBehaviour
{
    private bool isRegistered = false;
    private string originalScene;

    void Start()
    {
        originalScene = SceneManager.GetActiveScene().name;
        RegisterBlock();
    }

    void OnEnable()
    {
        if (!isRegistered && !string.IsNullOrEmpty(originalScene))
        {
            RegisterBlock();
        }
    }

    void OnDestroy()
    {
        if (Application.isPlaying) // 只在运行时取消注册
        {
            UnregisterBlock();
        }
    }

    void RegisterBlock()
    {
        if (BlockDataManager.Instance == null || isRegistered) return;

        var sceneData = BlockDataManager.Instance.GetSceneData(originalScene);
        if (sceneData != null)
        {
            // 检查位置是否已存在（带容差）
            bool positionExists = false;
            foreach (var pos in sceneData.blockPositions)
            {
                if (Vector2.Distance(pos, transform.position) < 0.1f)
                {
                    positionExists = true;
                    break;
                }
            }

            if (!positionExists)
            {
                sceneData.blockPositions.Add(transform.position);
                sceneData.spawnedBlocks.Add(gameObject);
                isRegistered = true;
                DontDestroyOnLoad(gameObject);
                Debug.Log($"Registered block at {transform.position} in {originalScene}");
            }
            else
            {
                Debug.LogWarning($"Duplicate block position at {transform.position}");
                Destroy(gameObject);
            }
        }
    }

    void UnregisterBlock()
    {
        if (!Application.isPlaying || BlockDataManager.Instance == null || !isRegistered) return;

        var sceneData = BlockDataManager.Instance.GetSceneData(originalScene);
        if (sceneData != null)
        {
            int index = sceneData.spawnedBlocks.IndexOf(gameObject);
            if (index >= 0)
            {
                sceneData.blockPositions.RemoveAt(index);
                sceneData.spawnedBlocks.RemoveAt(index);
                Debug.Log($"Unregistered block from {originalScene}");
            }
        }
    }

    public void InitializeBlock(Vector2 pos)
    {
        transform.position = pos;
        originalScene = SceneManager.GetActiveScene().name;
        RegisterBlock();
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, GetComponent<Collider2D>().bounds.size);
        Gizmos.DrawIcon(transform.position + Vector3.up * 0.5f, "BlockIcon");
    }
}