using UnityEngine;

public class SurvivalStats : MonoBehaviour
{
    float currentWave;
    float bestWave;

    void Start()
    {
        currentWave = PlayerPrefs.GetFloat("Current Wave", 1);
        bestWave = PlayerPrefs.GetFloat("Best Wave", 0);
    }
}
