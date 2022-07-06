using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioClick : MonoBehaviour
{
    public void PlayClickedSound(int soundnum)
    {
        if (soundnum == 1)
        {
            FindObjectOfType<AudioManager>().Play("click");
        }
        else if (soundnum == 2)
        {
            FindObjectOfType<AudioManager>().Play("select");
        }
    }
}
