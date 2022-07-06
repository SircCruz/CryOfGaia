using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectIterator : MonoBehaviour
{
    public GameObject[] gameobjects;

    private void Start()
    {
        StartCoroutine(Iterate());
    }
    IEnumerator Iterate()
    {
        for(int i = 0; i < gameobjects.Length; i++)
        {
            yield return new WaitForEndOfFrame();
            gameobjects[i].SetActive(true);
        }
    }
}
