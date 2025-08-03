using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BlockManager : MonoBehaviour
{
    public static BlockManager Instance;

    [System.Serializable]
    public class SceneBlockData
    {
        public string sceneName;
        public List<Vector3> blockPositions = new List<Vector3>();
    }

    public List<SceneBlockData> allSceneData = new List<SceneBlockData>();
    public GameObject blockPrefab;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddBlock(Vector3 position)
    {
        string currentScene = SceneManager.GetActiveScene().name;
        var sceneData = GetSceneData(currentScene);

        // 防止重复记录
        if (!sceneData.blockPositions.Contains(position))
        {
            sceneData.blockPositions.Add(position);
        }
    }

    public void LoadBlocksForCurrentScene()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        var sceneData = GetSceneData(currentScene);

        // 清除可能残留的方块引用
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        // 重新生成所有方块
        foreach (Vector3 pos in sceneData.blockPositions)
        {
            GameObject newBlock = Instantiate(blockPrefab, pos, Quaternion.identity);
            newBlock.transform.SetParent(transform);
        }
    }

    private SceneBlockData GetSceneData(string sceneName)
    {
        var data = allSceneData.Find(d => d.sceneName == sceneName);
        if (data == null)
        {
            data = new SceneBlockData { sceneName = sceneName };
            allSceneData.Add(data);
        }
        return data;
    }
}