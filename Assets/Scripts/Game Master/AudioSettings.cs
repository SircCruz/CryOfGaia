using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSettings : MonoBehaviour
{
    public AudioManager audioManager;
    public AudioSource music;
    AudioSource sound;
    void Start()
    {
        music.volume = PlayerPrefs.GetFloat("Music Volume", 0.1f);

        sound = FindObjectOfType<AudioManager>().GetComponent<AudioSource>();
        sound.volume = PlayerPrefs.GetFloat("Sound Volume", 0.1f);

        for (int i = 0; i < audioManager.sounds.Length; i++)
        {
            audioManager.sounds[i].volume = PlayerPrefs.GetFloat("Sound Volume", 0.1f);
        }
    }
}
