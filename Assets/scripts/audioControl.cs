using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioControl : MonoBehaviour
{
    public static audioControl instance;


    public int Currentindex;
    void Awake()
    {
        instance = this;
    }
    public List<AudioClip> audioClips;

    public AudioSource audio;

    public void playFloorauio()
    {

        if (Currentindex == 3)
        {

            audio.clip = audioClips[1];
        }
        else if (Currentindex > 3)
        {
            audio.clip = audioClips[Currentindex - 1];
        }
        else
        {
              audio.clip = audioClips[Currentindex];
         }
      
        audio.Play();
     }
}
