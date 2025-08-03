using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GEButtomHandler : MonoBehaviour
{
    public void OnRestartButtonClick()
    {
        Debug.Log("点击了重新开始按钮");
        //要改加载其他画面这里改下就行，记得build setting、
        SceneManager.LoadScene("GameBegin");
    }

    public void OnEndButtonClick()
    {
        Debug.Log("点击了退出按钮");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

    }
}
