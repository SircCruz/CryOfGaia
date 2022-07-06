using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class C2Mission1 : MonoBehaviour
{
    public float progressSpeed;

    private float duration = 4.5f;

    public Slider progress;
    public TextMeshProUGUI progressText;

    public GameObject killEnemies;

    GenerateEnemy genEnemy;

    GameObject player;
    Player playerScript;

    public Grip[] grip;

    public GameObject buttons, leftGrip, rightGrip;
    public GameObject missionSuccess;

    public GameObject missionFailed;
    Canvas youFailedCanvas;
    public Text description;
    private void Start()
    {
        genEnemy = GameObject.Find("Game Master").GetComponent<GenerateEnemy>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<Player>();

        youFailedCanvas = missionFailed.GetComponent<Canvas>();

        progress.maxValue = 100;
        progress.value = 40;
    }
    private void FixedUpdate()
    {
        CheckIndicators();

        if(progress.value == progress.maxValue)
        {
            genEnemy.enabled = false;
            buttons.SetActive(false);
            player.layer = LayerMask.NameToLayer("Task Item");

            duration -= Time.fixedDeltaTime;
            if(duration <= 0)
            {
                if(killEnemies != null)
                {
                    killEnemies.SetActive(true);
                }
                missionSuccess.SetActive(true);
            }
        }
        else if(progress.value <= 0)
        {
            genEnemy.enabled = false;
            buttons.SetActive(false);
            leftGrip.SetActive(false);
            rightGrip.SetActive(false);
            player.layer = LayerMask.NameToLayer("Task Item");
            playerScript.MoveStop();

            duration -= Time.fixedDeltaTime;
            if(duration <= 0)
            {
                missionFailed.SetActive(true);
                youFailedCanvas.enabled = true;
                description.text = "Ozone Layer thickness level reaches 0%.";
            }

            progressText.text = "0%";
        }
    }
    public void CheckIndicators()
    {
        if(grip[0].isActive && grip[1].isActive)
        {
            progress.value -= progressSpeed * Time.fixedDeltaTime;
        }
        else if (grip[0].isActive || grip[1].isActive)
        {

        }
        else
        {
            progress.value += progressSpeed * Time.fixedDeltaTime;
        }
        progressText.text = progress.value.ToString("#.##") + "%";
    }
}
