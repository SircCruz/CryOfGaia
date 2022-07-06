using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class C1Mission1 : MonoBehaviour
{
    //Mission Complete
    Player playerScript;
    public GenerateEnemy genEnemy;
    public CinemachineVirtualCamera vcam;
    public GameObject missionSuccess, buttons;
    public GameObject killEnemies;
    public Transform target1, target2, target3;
    bool lookAtTree1, lookAtTree2, lookAtTree3;
    bool isCompleted;
    float duration = 4.5f;

    //Audio
    public AudioClip grow2, grow3, grow4;
    AudioSource source;

    public Animator treeAnim1;
    public Animator treeAnim2;
    public Animator treeAnim3;
    public GameObject seedIcon;

    public GameObject player;
    public Text tree1Num;
    public Text tree2Num;
    public Text tree3Num;

    private float tree1Position;
    private float tree2Position;
    private float tree3Position;

    private int tree1PhaseCounter = 0;
    private int tree2PhaseCounter = 0;
    private int tree3PhaseCounter = 0;
    private bool tree1AcceptSeed = true;
    private bool tree2AcceptSeed = true;
    private bool tree3AcceptSeed = true;
    private int phases = 4;

    public bool isHoldingSeed;

    int coins;
    int collectedcoins;
    bool isRewarded;
    public BadgeShow badge;

    public GameObject trivia;
    public Skill2 skill2;
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
    private void Start()
    {
        tree1Num.text = tree1PhaseCounter.ToString();
        tree2Num.text = tree1PhaseCounter.ToString();
        tree3Num.text = tree1PhaseCounter.ToString();

        tree1Position = treeAnim1.transform.position.x;
        tree2Position = treeAnim2.transform.position.x;
        tree3Position = treeAnim3.transform.position.x;

        source = gameObject.GetComponentInParent<AudioSource>();

        playerScript = GameObject.Find("Character").GetComponent<Player>();

        coins = PlayerPrefs.GetInt("Coins", 0);
        collectedcoins = PlayerPrefs.GetInt("Collected Coins", 0);

        killEnemies.SetActive(false);
        missionSuccess.SetActive(false);
        seedIcon.SetActive(false);

        skill2.tutorialMode = false;
    }
    private void Update()
    {
        //trees' locations
        if ((player.transform.position.x >= tree1Position - .5f && player.transform.position.x <= tree1Position + .5f) && isHoldingSeed && tree1AcceptSeed)
        {
            GrowTree(1);
            isHoldingSeed = false;
        }
        if ((player.transform.position.x >= tree2Position - .5f && player.transform.position.x <= tree2Position + .5f) && isHoldingSeed && tree2AcceptSeed)
        {
            GrowTree(2);
            isHoldingSeed = false;
        }
        if ((player.transform.position.x >= tree3Position - .5f && player.transform.position.x <= tree3Position + .5f) && isHoldingSeed && tree3AcceptSeed)
        {
            GrowTree(3);
            isHoldingSeed = false;
        }
        //if all trees are fully grown
        if (!tree1AcceptSeed && !tree2AcceptSeed && !tree3AcceptSeed)
        {
            PlayerPrefs.SetInt("C1 Mission1", 1);
            PlayerPrefs.SetInt("C1Mission1", 1);

            Reward();
            genEnemy.enabled = false;
            buttons.SetActive(false);
            isCompleted = true;
            player.layer = LayerMask.NameToLayer("Task Item");
            playerScript.MoveStop();
            if (lookAtTree1)
            {
                vcam.m_LookAt = target1.transform;
                vcam.m_Follow = target1.transform;
            }
            else if (lookAtTree2)
            {
                vcam.m_LookAt = target2.transform;
                vcam.m_Follow = target2.transform;
            }
            else if (lookAtTree3)
            {
                vcam.m_LookAt = target3.transform;
                vcam.m_Follow = target3.transform;
            }
        }
        if (isCompleted)
        {
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
        if (isHoldingSeed)
        {
            seedIcon.SetActive(true);
        }
        else
        {
            seedIcon.SetActive(false);
        }

        if (collectedcoins >= 100000)
        {
            badge.ShowBadge("Grand Collector");
        }
        else if (collectedcoins >= 10000)
        {
            badge.ShowBadge("Gatherer");
        }
    }
    void GrowTree(int treeNumber)
    {
        lookAtTree1 = false;
        lookAtTree2 = false;
        lookAtTree3 = false;
        if(treeNumber == 1)
        {
            if (tree1PhaseCounter != phases)
            {
                if(tree1PhaseCounter == 4)
                {
                    source.PlayOneShot(grow4);
                }
                else if(tree1PhaseCounter == 3)
                {
                    source.PlayOneShot(grow3);
                }
                else if(tree1PhaseCounter <= 2)
                {
                    source.PlayOneShot(grow2);
                }
                tree1PhaseCounter += 1;
                treeAnim1.SetFloat("Grow", tree1PhaseCounter);
                tree1Num.text = tree1PhaseCounter.ToString();
                if (tree1PhaseCounter == phases)
                {
                    if(PlayerPrefs.GetInt("Trivia" + 0, 0) == 0)
                    {
                        Instantiate(trivia, new Vector2(tree1Position, treeAnim1.transform.position.y + 3f), transform.rotation);
                        PlayerPrefs.SetInt("Trivia" + 0, 1);
                    }
                    tree1Num.color = new Color(0, 255, 0);
                    tree1AcceptSeed = false;
                    lookAtTree1 = true;
                }
            }
        }
        else if (treeNumber == 2)
        {
            if (tree2PhaseCounter != phases)
            {
                if (tree2PhaseCounter == 4)
                {
                    source.PlayOneShot(grow4);
                }
                else if (tree2PhaseCounter == 3)
                {
                    source.PlayOneShot(grow3);
                }
                else if (tree2PhaseCounter <= 2)
                {
                    source.PlayOneShot(grow2);
                }
                tree2PhaseCounter += 1;
                treeAnim2.SetFloat("Grow", tree2PhaseCounter);
                tree2Num.text = tree2PhaseCounter.ToString();
                if (tree2PhaseCounter == phases)
                {
                    if (PlayerPrefs.GetInt("Trivia" + 1, 0) == 0)
                    {
                        Instantiate(trivia, new Vector2(tree2Position, treeAnim2.transform.position.y + 3f), transform.rotation);
                        PlayerPrefs.SetInt("Trivia" + 1, 1);
                    }
                    tree2Num.color = new Color(0, 255, 0);
                    tree2AcceptSeed = false;
                    lookAtTree2 = true;
                }
            }
        }
        else if (treeNumber == 3)
        {
            if (tree3PhaseCounter != phases)
            {
                if (tree3PhaseCounter == 4)
                {
                    source.PlayOneShot(grow4);
                }
                else if (tree3PhaseCounter == 3)
                {
                    source.PlayOneShot(grow3);
                }
                else if (tree3PhaseCounter <= 2)
                {
                    source.PlayOneShot(grow2);
                }
                tree3PhaseCounter += 1;
                treeAnim3.SetFloat("Grow", tree3PhaseCounter);
                tree3Num.text = tree3PhaseCounter.ToString();
                if (tree3PhaseCounter == phases)
                {
                    if (PlayerPrefs.GetInt("Trivia" + 2, 0) == 0)
                    {
                        Instantiate(trivia, new Vector2(tree3Position, treeAnim3.transform.position.y + 3f), transform.rotation);
                        PlayerPrefs.SetInt("Trivia" + 2, 1);
                    }
                    tree3Num.color = new Color(0, 255, 0);
                    tree3AcceptSeed = false;
                    lookAtTree3 = true;
                }
            }
        }
    }
}

