using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class BlockDataManager : MonoBehaviour
{
    public static BlockDataManager Instance;

    [System.Serializable]
    public class SceneBlockData
    {
        public string sceneName;
        public List<Vector2> blockPositions = new List<Vector2>();
        public List<GameObject> spawnedBlocks = new List<GameObject>();
    }

    public List<SceneBlockData> allSceneData = new List<SceneBlockData>();
    public GameObject blockPrefab;
    public LayerMask blockLayerMask;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
            Debug.Log("BlockDataManager initialized");
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        if (Instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"Scene loaded: {scene.name}");
        CleanInvalidReferences();
        UpdateBlocksVisibility();
        LoadMissingBlocks(scene.name);
    }

    public SceneBlockData GetSceneData(string sceneName)
    {
        SceneBlockData data = allSceneData.Find(d => d.sceneName == sceneName);
        if (data == null)
        {
            data = new SceneBlockData { sceneName = sceneName };
            allSceneData.Add(data);
            Debug.Log($"Created new scene data for {sceneName}");
        }
        return data;
    }

    void UpdateBlocksVisibility()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        foreach (var sceneData in allSceneData)
        {
            bool isCurrentScene = sceneData.sceneName == currentScene;

            foreach (var block in sceneData.spawnedBlocks)
            {
                if (block != null)
                {
                    block.SetActive(isCurrentScene);
                }
            }
        }
    }

    void LoadMissingBlocks(string sceneName)
    {
        if (blockPrefab == null)
        {
            Debug.LogError("Block prefab is not assigned!");
            return;
        }

        var sceneData = GetSceneData(sceneName);

        // 确保方块数量和位置数量一致
        while (sceneData.spawnedBlocks.Count < sceneData.blockPositions.Count)
        {
            sceneData.spawnedBlocks.Add(null);
        }

        for (int i = 0; i < sceneData.blockPositions.Count; i++)
        {
            if (sceneData.spawnedBlocks[i] == null)
            {
                // 检查该位置是否已有其他方块
                Collider2D overlap = Physics2D.OverlapCircle(
                    sceneData.blockPositions[i],
                    0.4f,
                    blockLayerMask
                );

                if (overlap == null)
                {
                    GameObject newBlock = Instantiate(
                        blockPrefab,
                        sceneData.blockPositions[i],
                        Quaternion.identity
                    );

                    var blockCtrl = newBlock.GetComponent<BlockController>();
                    if (blockCtrl != null)
                    {
                        blockCtrl.InitializeBlock(sceneData.blockPositions[i]);
                    }

                    sceneData.spawnedBlocks[i] = newBlock;
                    DontDestroyOnLoad(newBlock);
                    Debug.Log($"Loaded block at {sceneData.blockPositions[i]} in {sceneName}");
                }
                else
                {
                    Debug.LogWarning($"Position {sceneData.blockPositions[i]} already occupied in {sceneName}");
                    sceneData.blockPositions.RemoveAt(i);
                    sceneData.spawnedBlocks.RemoveAt(i);
                    i--; // 因为移除了一个元素，需要调整索引
                }
            }
        }
    }

    public void CleanInvalidReferences()
    {
        foreach (var sceneData in allSceneData)
        {
            for (int i = sceneData.spawnedBlocks.Count - 1; i >= 0; i--)
            {
                if (sceneData.spawnedBlocks[i] == null)
                {
                    if (i < sceneData.blockPositions.Count)
                    {
                        sceneData.blockPositions.RemoveAt(i);
                    }
                    sceneData.spawnedBlocks.RemoveAt(i);
                }
            }
        }
    }

    public void DebugLogAllBlocks()
    {
        foreach (var sceneData in allSceneData)
        {
            Debug.Log($"Scene: {sceneData.sceneName}");
            for (int i = 0; i < sceneData.blockPositions.Count; i++)
            {
                string status = i < sceneData.spawnedBlocks.Count && sceneData.spawnedBlocks[i] != null
                    ? "Exists" : "Missing";
                Debug.Log($"  Block {i}: {sceneData.blockPositions[i]} ({status})");
            }
        }
    }
}