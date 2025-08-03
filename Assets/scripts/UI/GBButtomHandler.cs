using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GBButtomHandler : MonoBehaviour
{
    public void OnBeginButtonClick()
    {
        Debug.Log("����˿�ʼ��Ϸ��ť");
        SceneManager.LoadSceneAsync(2);
        audioControl.instance.Currentindex = 1;
        audioControl.instance.playFloorauio();
        PlayerPrefs.SetInt("box", 0);
        PlayerPrefs.SetInt("key",0);
    }

    public void OnQuitButtonClick()
    {
        Debug.Log("������˳���Ϸ��ť");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
