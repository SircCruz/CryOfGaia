using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill2SFX : MonoBehaviour
{
    public AudioClip sound;
    public AudioSource source;

    bool isPlayed;

    private void OnEnable()
    {
        if (!isPlayed)
        {
            source.PlayOneShot(sound, PlayerPrefs.GetFloat("Sound Volume", 0.1f));
            isPlayed = true;
        }
    }
}
