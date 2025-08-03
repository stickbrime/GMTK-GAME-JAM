using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoController : MonoBehaviour
{
    public VideoPlayer videoPlayer; 
    public string startSceneName = "GameBegin"; // Build Settings都没加

    // Start is called before the first frame update
    void Start()
    {
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    // Update is called once per frame
    void Update()
    {

    }

    // 添加缺失的OnVideoEnd方法
    private void OnVideoEnd(VideoPlayer source)
    {
        SceneManager.LoadScene(startSceneName);
    }
}
