using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoalMissileManager : MonoBehaviour
{
    public GetCharacterPos[] characterPos;

    public Transform[] spawnLocation;
    public GameObject missile;

    private float cooldown = 2f;
    private void OnEnable()
    {
        int count = Random.Range(1, 10);
        StartCoroutine(LaunchMissile(count));
    }
    private IEnumerator LaunchMissile(int missileCount)
    {
        for(int i = 1; i <= missileCount; i++)
        {
            int dropMode = Random.Range(1, 3);

            if (dropMode == 1)
            {
                DropToPlayer();
            }
            else if (dropMode == 2)
            {
                DropToSides();
            }
            yield return new WaitForSeconds(cooldown);
        }
        gameObject.SetActive(false);
    }
    private void DropToPlayer()
    {
        for(int i = 0; i < characterPos.Length; i++)
        {
            if (characterPos[i].isStep)
            {
                Instantiate(missile, spawnLocation[i].position, transform.rotation);
            }
        }
    }
    private void DropToSides()
    {
        for (int i = 1; i < characterPos.Length - 1; i++)
        {
            if (characterPos[i].isStep)
            {
                Instantiate(missile, spawnLocation[i - 1].position, transform.rotation);
                Instantiate(missile, spawnLocation[i + 1].position, transform.rotation);
            }
        }
        if (characterPos[0].isStep)
        {
            Instantiate(missile, spawnLocation[1].position, transform.rotation);
        }
        if (characterPos[characterPos.Length - 1].isStep)
        {
            Instantiate(missile, spawnLocation[characterPos.Length - 2].position, transform.rotation);
        }
    }
}
