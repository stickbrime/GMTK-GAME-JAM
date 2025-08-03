using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;

public class checkgameover : MonoBehaviour
{
    private bool Canover;
    public TMP_Text tMP;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (PlayerInventory.instance.checkequip())
            {
                Canover = true;
                tMP.color = Color.green;
            }

        }
    }
  
  void Update()
  {
        if (Canover)
        {

            if (Input.GetKeyDown(KeyCode.E))
            {
                audioControl.instance.Currentindex = 5;
                audioControl.instance.playFloorauio();
                SceneManager.LoadSceneAsync(7); 
             }

        }
  }
}
