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
        // SceneManager.LoadScene("");
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
