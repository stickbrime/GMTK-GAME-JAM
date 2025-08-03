using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyControl : MonoBehaviour
{
    public BoxCollider2D box;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("showbox",3);
    }

    void showbox()
    {

        box = this.gameObject.AddComponent<BoxCollider2D>();

        box.isTrigger = true;
     }

    // Update is called once per frame
    void Update()
    {
        
    }
}
