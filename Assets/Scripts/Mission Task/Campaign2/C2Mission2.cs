using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class C2Mission2 : MonoBehaviour
{
    public float progressSpeed;

    private float duration = 4.5f;

    public Slider progress;
    public TextMeshProUGUI progressText;

    public GameObject buttons;
    public GameObject killEnemies;

    GenerateEnemy genEnemy;

    GameObject player;
    Player playerScript;

    public GameObject missionSuccess;

    public SpriteRenderer trashInventory;
    public bool isHoldingTrash;
    private void Start()
    {
        genEnemy = GameObject.Find("Game Master").GetComponent<GenerateEnemy>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<Player>();

        progress.maxValue = 200;
        progress.value = 0;
    }

    private void Update()
    {
        if (isHoldingTrash)
        {
            trashInventory.enabled = true;
        }
        else
        {
            trashInventory.enabled = false;
        }

        progressText.text = progress.value + "/" + progress.maxValue;

        if (progress.value == progress.maxValue)
        {
            genEnemy.enabled = false;
            buttons.SetActive(false);
            player.layer = LayerMask.NameToLayer("Task Item");
            playerScript.MoveStop();

            duration -= Time.fixedDeltaTime;
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
    public void AddCount()
    {
        progress.value += 1;
    }
    public void DeductCount()
    {
        progress.value -= 1;
    }
}
