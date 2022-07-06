using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unlocker : MonoBehaviour
{
    public MissionChooser mission;

    public Button c1Mission2;
    public Button c1BossMission;
    public GameObject tutorial;

    public Button plantita;
    public Button survival;

    private void Start()
    {
        if(PlayerPrefs.GetInt("Tutorial", 0) == 1)
        {
            tutorial.SetActive(true);
        }
        else
        {
            tutorial.SetActive(false);
        }

        if(PlayerPrefs.GetInt("C1 Mission1", 0) == 1)
        {
            c1Mission2.interactable = true;
            plantita.interactable = true;
            survival.interactable = true;
        }
        else
        {
            c1Mission2.interactable = false;
            plantita.interactable = false;
            survival.interactable = false;
        }

        if(PlayerPrefs.GetInt("C1 Mission2", 0) == 1)
        {
            c1BossMission.interactable = true;
        }
        else
        {
            c1BossMission.interactable = false;
        }
    }
    public void FirstPlay()
    {
        if (PlayerPrefs.GetInt("Tutorial", 0) == 0)
        {
            mission.StartMission();
        }
    }
}
