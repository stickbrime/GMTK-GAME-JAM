using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class GOButtomHandler : MonoBehaviour
{
    public void OnRetryButtonClick()
    {
        Debug.Log("��������¿�ʼ��ť��������ĳ��л�����������");
    }

    public void OnQuitButtonClick()
    {
        Debug.Log("������˳���ť");
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
