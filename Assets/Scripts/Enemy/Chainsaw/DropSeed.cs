using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropSeed : MonoBehaviour
{
    public GameObject seed;
    int generateSeed;
    public bool dropSeedOnDeath;

    SceneLoader app;

    private void OnApplicationQuit()
    {
        app.isQuitting = true;
    }
    private void OnDestroy()
    {
        if (!app.isQuitting)
        {
            if (dropSeedOnDeath)
            {
                Instantiate(seed, transform.position, transform.rotation);
            }
        }
    }
    void Start()
    {
        app = GameObject.Find("Game Master").GetComponent<SceneLoader>();

        generateSeed = Random.Range(1, 6);
        if (generateSeed <= 2)
        {
            dropSeedOnDeath = true;
        }
        else
        {
            dropSeedOnDeath = false;
        }
    }
}
