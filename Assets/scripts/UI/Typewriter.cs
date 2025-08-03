using UnityEngine;
using TMPro;
using System.Collections;

public class Typewriter : MonoBehaviour
{
    public TMP_Text textDisplay;
    public float typeSpeed = 0.1f;
    public string defaultText = "";

    void Start()
    {
        StartTyping(defaultText);
    }

    public void StartTyping(string text)
    {
        textDisplay.text = ""; 
        StartCoroutine(TypeText(text));
    }

    IEnumerator TypeText(string text)
    {
        foreach (char c in text.ToCharArray())
        {
            textDisplay.text += c; 
            yield return new WaitForSeconds(typeSpeed); 
        }
    }


}