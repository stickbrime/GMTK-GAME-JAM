using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour
{
    public bool IsOnControl;

    public MirrorType mirrorType;

    public Sprite[] mirrorSprites;

    public SpriteRenderer mirrorRenderer;

    public MirrorType RightType;
    public enum MirrorType
    {
        Mirror1,
        Mirror2,
        Mirror3,
        Mirror4
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            IsOnControl = true;         
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            IsOnControl = false;
        }
    }

    public void Update()
    {
        if (IsOnControl) 
        {
            if (Input.GetKeyDown(KeyCode.E)) 
            {
                if ((int)mirrorType > 3)
                {
                    mirrorType = MirrorType.Mirror1;
                }
                else 
                {
                    mirrorType = (MirrorType)((int)mirrorType + 1);
                }

                switch (mirrorType) 
                {
                
                    case MirrorType.Mirror1:
                        Debug.Log("Mirror 1 is now active.");
                        this.transform.eulerAngles = new Vector3(0, 0, 0);
                        mirrorRenderer.sprite = mirrorSprites[0];
                        break;
                        case MirrorType.Mirror2:
                        mirrorRenderer.sprite = mirrorSprites[1];
                        Debug.Log("Mirror 2 is now active.");
                        break;
                    case MirrorType.Mirror3:
                        mirrorRenderer.sprite = mirrorSprites[2];
                        Debug.Log("Mirror 3 is now active.");
                        break;
                    case MirrorType.Mirror4:
                        mirrorRenderer.sprite = mirrorSprites[1];

                        this.transform.eulerAngles = new Vector3(0, 180,0);
                        break;    


                }
            }
        }
    }
}
