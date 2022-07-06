using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialTask : MonoBehaviour
{
    public Skill2 skill2;

    //Mission Complete
    Player playerScript;
    public GameObject player;

    public Slider progress;

    public GenerateEnemy genEnemy;
    public GameObject missionSuccess, buttons;
    public GameObject killEnemies;
    bool isCompleted;
    float duration = 4.5f;

    void Start()
    {
        skill2.tutorialMode = true;

        playerScript = GameObject.Find("Character").GetComponent<Player>();

        killEnemies.SetActive(false);
        missionSuccess.SetActive(false);

        progress.maxValue = 20;
        progress.value = 0;
        progress.wholeNumbers = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(progress.value == progress.maxValue)
        {
            isCompleted = true;
        }
        if (isCompleted)
        {
            PlayerPrefs.SetInt("Tutorial", 1);

            duration -= Time.fixedDeltaTime;
            player.layer = LayerMask.NameToLayer("Task Item");
            playerScript.MoveStop();
            genEnemy.enabled = false;
            buttons.SetActive(false);
            if (duration <= 0)
            {
                if (killEnemies != null)
                {
                    killEnemies.SetActive(true);
                }
                missionSuccess.SetActive(true);
            }
        }
    }
    public void AddValue()
    {
        progress.value += 1;
    }
}
