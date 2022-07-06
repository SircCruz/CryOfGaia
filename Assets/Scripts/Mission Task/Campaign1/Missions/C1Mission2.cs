using System;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class C1Mission2 : MonoBehaviour
{
    //Mission Complete
    Player playerScript;
    GameObject player;
    public GenerateEnemy genEnemy;
    public CinemachineVirtualCamera vcam;
    CameraFollow cam;
    public GameObject missionSuccess, buttons;
    public GameObject killEnemies;
    public Transform target1, target2, target3;
    bool isCompleted;

    float duration = 4.5f;
    public GameObject tree1layer, tree2layer, tree3layer;

    //Mission Failed
    public bool isFailed;
    public GameObject youDied;
    Canvas youDiedCanvas;
    public Text description;

    public Slider slider;
    public float timer = 300;
    public Text txtTimer;
    public bool isTimeZero = false;
    bool switchColor;
    float greenTextDur, restoreGreenTextDur;

    public Text txtTree1HP;
    public Text txtTree2HP;
    public Text txtTree3HP;

    public TreeHP tree1HP;
    public TreeHP tree2HP;
    public TreeHP tree3HP;
    int tree1, tree2, tree3;

    int coins;
    int collectedcoins;
    bool isRewarded;

    public BadgeShow badge;
    public Skill2 skill2;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Character");
        playerScript = player.GetComponent<Player>();

        txtTimer.text = timer.ToString();

        coins = PlayerPrefs.GetInt("Coins", 0);
        collectedcoins = PlayerPrefs.GetInt("Collected Coins", 0);

        youDiedCanvas = youDied.GetComponent<Canvas>();
        youDied.SetActive(false);

        greenTextDur = 0.5f;
        restoreGreenTextDur = greenTextDur;

        cam = GameObject.Find("CM vcam1").GetComponent<CameraFollow>();

        slider.maxValue = timer;
        skill2.tutorialMode = false;
    }
    private void Reward()
    {
        if (!isRewarded)
        {
            coins += 500;
            collectedcoins += 500;
            PlayerPrefs.SetInt("Coins", coins);
            PlayerPrefs.SetInt("Collected Coins", collectedcoins);
            isRewarded = true;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isTimeZero && !isFailed)
        {
            timer -= Time.fixedDeltaTime;
            slider.value = timer;
            if (timer <= 11)
            {
                if (!switchColor)
                {
                    txtTimer.color = new Color(0, 255, 0);
                }
                else
                {
                    txtTimer.color = new Color(255, 255, 255);
                }
                greenTextDur -= Time.fixedDeltaTime;
                if(greenTextDur <= 0)
                {
                    greenTextDur = restoreGreenTextDur;
                    switchColor = !switchColor;
                }
            }
        }
        TimeSpan timeFormat = TimeSpan.FromSeconds(timer);
        txtTimer.text = timeFormat.ToString("mm':'ss");
        if(timer <= 0)
        {
            PlayerPrefs.SetInt("C1 Mission2", 1);
            PlayerPrefs.SetInt("C1Mission2", 1);

            isTimeZero = true;
            Reward();
            genEnemy.enabled = false;
            buttons.SetActive(false);
            isCompleted = true;
            player.layer = LayerMask.NameToLayer("Task Item");
            tree1layer.layer = LayerMask.NameToLayer("Task Item");
            tree2layer.layer = LayerMask.NameToLayer("Task Item");
            tree3layer.layer = LayerMask.NameToLayer("Task Item");
            playerScript.MoveStop();
        }
        if (isCompleted)
        {
            duration -= Time.fixedDeltaTime;
            if (duration <= 0)
            {
                if(killEnemies != null)
                {
                    killEnemies.SetActive(true);
                }
                missionSuccess.SetActive(true);
            }
        }
        if (isFailed)
        {
            duration -= Time.fixedDeltaTime;
            genEnemy.enabled = false;
            buttons.SetActive(false);
            player.layer = LayerMask.NameToLayer("Task Item");
            tree1layer.layer = LayerMask.NameToLayer("Task Item");
            tree2layer.layer = LayerMask.NameToLayer("Task Item");
            tree3layer.layer = LayerMask.NameToLayer("Task Item");
            playerScript.MoveStop();
            if (duration <= 0)
            {
                youDied.SetActive(true);
                youDiedCanvas.enabled = true;
                description.text = "One of the trees has been destroyed.";
            }
        }
    }
    void Update()
    {
        txtTree1HP.text = tree1HP.healthPoints.ToString() + " HP";
        txtTree2HP.text = tree2HP.healthPoints.ToString() + " HP";
        txtTree3HP.text = tree3HP.healthPoints.ToString() + " HP";

        tree1 = int.Parse(tree1HP.healthPoints.ToString());
        tree2 = int.Parse(tree2HP.healthPoints.ToString());
        tree3 = int.Parse(tree3HP.healthPoints.ToString());

        if(tree1 <= 0)
        {
            vcam.m_LookAt = target1.transform;
            vcam.m_Follow = target1.transform;
            txtTree1HP.text = 0.ToString();
            cam.isFailed = true;
            isFailed = true;
        }
        if (tree2 <= 0)
        {
            vcam.m_LookAt = target2.transform;
            vcam.m_Follow = target2.transform;
            txtTree2HP.text = 0.ToString();
            cam.isFailed = true;
            isFailed = true;
        }
        if (tree3 <= 0)
        {
            vcam.m_LookAt = target3.transform;
            vcam.m_Follow = target3.transform;
            txtTree3HP.text = 0.ToString();
            cam.isFailed = true;
            isFailed = true;
        }

        //achievements
        if (collectedcoins >= 100000)
        {
            badge.ShowBadge("Treasurer II");
        }
        else if (collectedcoins >= 10000)
        {
            badge.ShowBadge("Treasurer I");
        }
    }
}
