using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreeHP : MonoBehaviour
{
    public GameObject trivia;
    TreeLight treeLight;

    AudioSource source;
    SpriteRenderer render;
    public AudioClip dead, hurt;
    bool isPlayed;

    public float healthPoints;
    public Chainsaw2 chainsaw;
    public GameObject leaf1, leaf2, leaf3, leaf4;
    private Animator tree;
    public Text hpTxt;
    float redTextDur, restoreRedtextDur;
    bool isHit = false;

    private float hurtDur, restoreHurtDur;
    private bool isHurt;
    private bool transition1, transition2, transition3;
    private void Start()
    {
        tree = gameObject.GetComponent<Animator>();
        treeLight = gameObject.GetComponentInChildren<TreeLight>();
        source = gameObject.GetComponent<AudioSource>();
        render = gameObject.GetComponent<SpriteRenderer>();

        transition1 = false;
        transition2 = false;
        transition3 = false;

        redTextDur = 0.5f;
        restoreRedtextDur = redTextDur;

        hurtDur = 0.1f;
        restoreHurtDur = hurtDur;
    }
    private void FixedUpdate()
    {
        if (healthPoints <= 250)
        {
            if (PlayerPrefs.GetInt("Trivia" + 3, 0) == 0)
            {
                Instantiate(trivia, new Vector2(transform.position.x, transform.position.y + 2f), transform.rotation);
                PlayerPrefs.SetInt("Trivia" + 3, 1);
            }
        }
        if (healthPoints <= 50)
        {
            if(PlayerPrefs.GetInt("Trivia" + 4, 0) == 0)
            {
                Instantiate(trivia, new Vector2(transform.position.x, transform.position.y + 2f), transform.rotation);
                PlayerPrefs.SetInt("Trivia" + 4, 1);
            }
        }
        if (healthPoints <= 0)
        {
            tree.SetTrigger("tree-dead");
            if (!isPlayed)
            {
                source.PlayOneShot(dead);
                isPlayed = true;
            }
        }
        if (isHurt)
        {
            if (healthPoints >= 201)
            {
                SpawnLeaves();
                tree.SetBool("tree-damage-300", true);
            }
            else if (healthPoints >= 101)
            {
                if (!transition1)
                {
                    SpawnLeavesTransition();
                    tree.SetBool("tree-transition-300-200", true);
                    transition1 = true;
                }
                else
                {
                    SpawnLeaves2();
                    tree.SetBool("tree-damage-200", true);
                }
            }
            else if (healthPoints >= 51)
            {
                if (!transition2)
                {
                    SpawnLeavesTransition2();
                    tree.SetBool("tree-transition-200-100", true);
                    transition2 = true;
                }
                else
                {
                    SpawnLeaves3();
                    tree.SetBool("tree-damage-100", true);
                }
            }
            else if (healthPoints >= 1)
            {
                if (!transition3)
                {
                    transition3 = true;
                    SpawnLeavesTransition3();
                    tree.SetBool("tree-transition-100-50", true);
                }
                else
                {
                    tree.SetBool("tree-damage-50", true);
                }
            }

            hurtDur -= Time.fixedDeltaTime;
            if(hurtDur <= 0)
            {
                hurtDur = restoreHurtDur;
                isHurt = false;
            }
        }
        if (!isHurt)
        {
            tree.SetBool("tree-damage-300", false);
            tree.SetBool("tree-damage-200", false);
            tree.SetBool("tree-damage-100", false);
            tree.SetBool("tree-damage-50", false);
        }
        if (isHit)
        {
            redTextDur -= Time.fixedDeltaTime;
            hpTxt.color = new Color(255, 0, 0);
            if(redTextDur <= 0)
            {
                hpTxt.color = new Color(255, 255, 255);
                redTextDur = restoreRedtextDur;
                isHit = false;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("EnemyAttackCollider"))
        {
            healthPoints -= 2;
            if (!isHurt)
            {
                if (render.isVisible)
                {
                    source.PlayOneShot(hurt);
                }
                isHurt = true;
                isHit = true;
                treeLight.PlayHurtLight();
            }
        }
    }
    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (collision.gameObject.CompareTag("EnemyAttackCollider"))
    //    {
    //        isHurt = true;
    //    }
    //}
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyAttackCollider"))
        {
            tree.SetBool("tree-transition-300-200", false);
            tree.SetBool("tree-transition-200-100", false);
            tree.SetBool("tree-transition-100-50", false);
            tree.SetBool("tree-dead", false);
        }
    }
    void SpawnLeaves()
    {
        int leaves = Random.Range(1, 2);
        for(int i = 0; i <= leaves; i++)
        {
            int leaveType = Random.Range(1, 5);
            if (leaveType == 1)
            {
                Instantiate(leaf1, new Vector3(transform.position.x + Random.Range(-3.5f, 3.5f), transform.position.y + Random.Range(-1f, 0.5f)), transform.rotation);
            }
            else if (leaveType == 2)
            {
                Instantiate(leaf2, new Vector3(transform.position.x + Random.Range(-3.5f, 3.5f), transform.position.y + Random.Range(-1f, 0.5f)), transform.rotation);
            }
            else if (leaveType == 3)
            {
                Instantiate(leaf3, new Vector3(transform.position.x + Random.Range(-3.5f, 3.5f), transform.position.y + Random.Range(-1f, 0.5f)), transform.rotation);
            }
            else if (leaveType == 4)
            {
                Instantiate(leaf4, new Vector3(transform.position.x + Random.Range(-3.5f, 3.5f), transform.position.y + Random.Range(-1f, 0.5f)), transform.rotation);
            }
        }
    }
    void SpawnLeavesTransition()
    {
        int leaves = Random.Range(2, 4);
        for (int i = 0; i <= leaves; i++)
        {
            int leaveType = Random.Range(1, 5);
            if (leaveType == 1)
            {
                Instantiate(leaf1, new Vector3(transform.position.x + Random.Range(-3.5f, -1f), transform.position.y + Random.Range(-2.2f, 0.9f)), transform.rotation);
            }
            else if (leaveType == 2)
            {
                Instantiate(leaf2, new Vector3(transform.position.x + Random.Range(-3.5f, -1f), transform.position.y + Random.Range(-2.2f, 0.9f)), transform.rotation);
            }
            else if (leaveType == 3)
            {
                Instantiate(leaf3, new Vector3(transform.position.x + Random.Range(-3.5f, -1f), transform.position.y + Random.Range(-2.2f, 0.9f)), transform.rotation);
            }
            else if (leaveType == 4)
            {
                Instantiate(leaf4, new Vector3(transform.position.x + Random.Range(-3.5f, -1f), transform.position.y + Random.Range(-2.2f, 0.9f)), transform.rotation);
            }
        }
    }
    void SpawnLeaves2()
    {
        int leaves = Random.Range(1, 2);
        for (int i = 0; i <= leaves; i++)
        {
            int leaveType = Random.Range(1, 5);
            if (leaveType == 1)
            {
                Instantiate(leaf1, new Vector3(transform.position.x + Random.Range(0, 3.5f), transform.position.y + Random.Range(-2.2f, 0.9f)), transform.rotation);
            }
            else if (leaveType == 2)
            {
                Instantiate(leaf2, new Vector3(transform.position.x + Random.Range(0, 3.5f), transform.position.y + Random.Range(-2.2f, 0.9f)), transform.rotation);
            }
            else if (leaveType == 3)
            {
                Instantiate(leaf3, new Vector3(transform.position.x + Random.Range(0, 3.5f), transform.position.y + Random.Range(-2.2f, 0.9f)), transform.rotation);
            }
            else if (leaveType == 4)
            {
                Instantiate(leaf4, new Vector3(transform.position.x + Random.Range(0, 3.5f), transform.position.y + Random.Range(-2.2f, 0.9f)), transform.rotation);
            }
        }
    }
    void SpawnLeavesTransition2()
    {
        int leaves = Random.Range(2, 4);
        for (int i = 0; i <= leaves; i++)
        {
            int leaveType = Random.Range(1, 5);
            if (leaveType == 1)
            {
                Instantiate(leaf1, new Vector3(transform.position.x + Random.Range(0, 3.5f), transform.position.y + Random.Range(-2.2f, 0.9f)), transform.rotation);
            }
            else if (leaveType == 2)
            {
                Instantiate(leaf2, new Vector3(transform.position.x + Random.Range(0, 3.5f), transform.position.y + Random.Range(-2.2f, 0.9f)), transform.rotation);
            }
            else if (leaveType == 3)
            {
                Instantiate(leaf3, new Vector3(transform.position.x + Random.Range(0, 3.5f), transform.position.y + Random.Range(-2.2f, 0.9f)), transform.rotation);
            }
            else if (leaveType == 4)
            {
                Instantiate(leaf4, new Vector3(transform.position.x + Random.Range(0, 3.5f), transform.position.y + Random.Range(-2.2f, 0.9f)), transform.rotation);
            }
        }
    }
    void SpawnLeaves3()
    {
        int leaves = Random.Range(1, 2);
        for (int i = 0; i <= leaves; i++)
        {
            int leaveType = Random.Range(1, 5);
            if (leaveType == 1)
            {
                Instantiate(leaf1, new Vector3(transform.position.x + Random.Range(-1.5f, 1.5f), transform.position.y + Random.Range(0.5f, 3.5f)), transform.rotation);
            }
            else if (leaveType == 2)
            {
                Instantiate(leaf2, new Vector3(transform.position.x + Random.Range(-1.5f, 1.5f), transform.position.y + Random.Range(0.5f, 3.5f)), transform.rotation);
            }
            else if (leaveType == 3)
            {
                Instantiate(leaf3, new Vector3(transform.position.x + Random.Range(-1.5f, 1.5f), transform.position.y + Random.Range(0.5f, 3.5f)), transform.rotation);
            }
            else if (leaveType == 4)
            {
                Instantiate(leaf4, new Vector3(transform.position.x + Random.Range(-1.5f, 1.5f), transform.position.y + Random.Range(0.5f, 3.5f)), transform.rotation);
            }
        }
    }
    void SpawnLeavesTransition3()
    {
        int leaves = Random.Range(2, 4);
        for (int i = 0; i <= leaves; i++)
        {
            int leaveType = Random.Range(1, 5);
            if (leaveType == 1)
            {
                Instantiate(leaf1, new Vector3(transform.position.x + Random.Range(-1, 1), transform.position.y + Random.Range(0.5f, 3.5f)), transform.rotation);
            }
            else if (leaveType == 2)
            {
                Instantiate(leaf2, new Vector3(transform.position.x + Random.Range(-1, 1), transform.position.y + Random.Range(0.5f, 3.5f)), transform.rotation);
            }
            else if (leaveType == 3)
            {
                Instantiate(leaf3, new Vector3(transform.position.x + Random.Range(-1, 1), transform.position.y + Random.Range(0.5f, 3.5f)), transform.rotation);
            }
            else if (leaveType == 4)
            {
                Instantiate(leaf4, new Vector3(transform.position.x + Random.Range(-1, 1), transform.position.y + Random.Range(0.5f, 3.5f)), transform.rotation);
            }
        }
    }
}
