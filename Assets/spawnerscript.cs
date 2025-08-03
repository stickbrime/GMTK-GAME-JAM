using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnerscript : MonoBehaviour
{
    public GameObject enemy;
    public float timer=10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timer <= 0)
        {
            timer += 10;
            Instantiate(enemy, this.transform.position, this.transform.rotation);
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }
}
