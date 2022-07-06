using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill2 : MonoBehaviour
{
    [HideInInspector] public bool tutorialMode;
    public GameObject trivia;

    WorldPhysics cam;
    public Rigidbody2D rgbd;
    bool isCamShake;

    public Animator skill2Anim;
    public GameObject trigger;
    bool isAtGround;
    float castTime = 2.1f;
    float triggerActive = 0.255f;

    private void Start()
    {
        trigger.SetActive(false);
        cam = GameObject.Find("Game Master").GetComponent<WorldPhysics>();

        GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("flowerskill");
    }
    private void FixedUpdate()
    {
        if (isAtGround)
        {
            castTime -= Time.fixedDeltaTime;
            if (castTime <= 0)
            {
                trigger.SetActive(true);
                if (!tutorialMode)
                {
                    if (PlayerPrefs.GetInt("Trivia" + 5, 0) == 0)
                    {
                        Instantiate(trivia, transform.position, transform.rotation);
                        PlayerPrefs.SetInt("Trivia" + 5, 1);
                    }
                }
                triggerActive -= Time.fixedDeltaTime;
                if (triggerActive <= 0)
                {
                    trigger.SetActive(false);
                    Destroy(gameObject, 1.3f);
                }
                ShakeCam();
            }
        }
    }
    void ShakeCam()
    {
        if (!isCamShake)
        {
            cam.CameraShake(0.5f, 2);
            isCamShake = true;
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform") || collision.gameObject.CompareTag("Platform2"))
        {
            rgbd.freezeRotation = true;
            skill2Anim.SetTrigger("explode");
            isAtGround = true;
        }
    }
}
