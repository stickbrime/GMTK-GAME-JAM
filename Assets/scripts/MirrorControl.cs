using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorControl : MonoBehaviour
{
    public List<Mirror> mirrors;

    public SpriteRenderer Box;

    public Sprite OpenBox;

    public GameObject keyPrefab;

    public GameObject valuePrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public bool IsOpen = false;
    // Update is called once per frame
    void Update()
    {
        if (!IsOpen) 
        {
            for (int i = 0; i < mirrors.Count; i++)
            {
                if (mirrors[i].mirrorType != mirrors[i].RightType)
                {
                    return;
                }
            }

            Box.sprite = OpenBox;
            IsOpen = true;

            keyPrefab.gameObject.SetActive(true);

            valuePrefab.gameObject.SetActive(true);
        }
     


    }
}
