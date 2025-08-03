using System.Collections;
using UnityEngine;
using TMPro;

public class TextController : MonoBehaviour
{
    public TMP_Text targetText;
    public string customText;

    IEnumerator TypeText(string text, float interval)
    {
        for (int i = 0; i < text.Length; i++)
        {
            targetText.text += text[i]; 
            yield return new WaitForSeconds(interval);
        }
    }

    private void Start()
    {
        StartCoroutine(TypeText(customText, 0.15f));
    }
}