using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //isDebugging True: keyboard controls False: touch controls
    bool isDebugging = false;

    public GameObject hurtLight;
    public Gradient HUDgradient;
    public Slider HUD;
    public Image fill;
    public GameObject youDiedPanel;
    public CharacterController2D controller;
    public Rigidbody2D playerRgbd;
    public WorldPhysics gameMaster;
    public Animator playerAnim;
    public GameObject moveLeft, moveRight, jumpBtn, skill1, skill2, rangeAtk, meleeAtk;

    public Button activeImage;
    public GameObject jumpNormal, jumpPressed;
    public GameObject moveLeftNormal, moveLeftPressed;
    public GameObject moveRightNormal, moveRightPressed;

    public float runSpeed = 40f;
    float horizontalMove = 0f;
    bool directionMove = false;
    public bool stopMoving = true;

    bool jump = false;
    bool isJumping = false;

    public bool isHurt = false;
    public float recoveryTime = 0.0001f;
    private float restoreRecoveryTime;

    bool isDead = false;
    float deathDur = 4.2f;

    private Canvas ui;
    public GameObject damageText;
    public string damage;
    void Dead()
    {
        if (!isDead)
        {
            int voice = Random.Range(1, 3);
            FindObjectOfType<AudioManager>().Play("charadeath" + voice);

            playerAnim.SetTrigger("dead");
            moveRight.SetActive(false);
            moveLeft.SetActive(false);
            jumpBtn.SetActive(false);
            rangeAtk.SetActive(false);
            meleeAtk.SetActive(false);
            skill1.SetActive(false);
            skill2.SetActive(false);
            playerRgbd.constraints = RigidbodyConstraints2D.FreezePositionX;
            playerRgbd.freezeRotation = true;
            gameObject.layer = LayerMask.NameToLayer("Task Item");
            isDead = true;
        }
    }
    void Start()
    {
        fill.color = HUDgradient.Evaluate(1f);
        restoreRecoveryTime = recoveryTime;
        youDiedPanel.SetActive(false);

        HUD.maxValue = UpgradeCheck.CharacterHealthPoints();
        HUD.value = UpgradeCheck.CharacterHealthPoints();

        GetImageOnButtonClick.SetImage(false, moveLeftNormal, moveLeftPressed);
        GetImageOnButtonClick.SetImage(false, moveRightNormal, moveRightPressed);

        ui = GetComponentInChildren<Canvas>();
        ui.worldCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }
    void Update()
    {
        if (!isDead)
        {
            if (!isHurt)
            {
                // Keyboard
                if (isDebugging)
                {
                    horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
                    if (Input.GetButtonUp("Horizontal"))
                    {
                        horizontalMove = 0;
                        playerAnim.SetBool("jump", false);
                    }
                    if (Input.GetButtonDown("Horizontal"))
                    {
                        if (directionMove && !stopMoving)
                        {
                            MoveRight(true);
                        }
                        if (!directionMove && !stopMoving)
                        {
                            MoveLeft(true);
                        }
                        playerAnim.SetTrigger("run");
                    }
                    if (Input.GetButtonDown("Jump"))
                    {
                        playerAnim.SetTrigger("jump");
                        isJumping = true;
                        jump = true;
                    }
                }
                else if (!isDebugging)
                {
                    //Touch Screen
                    if (!directionMove && !stopMoving)
                    {
                        horizontalMove = -1 * runSpeed;
                    }
                    if (directionMove && !stopMoving)
                    {
                        horizontalMove = 1 * runSpeed;
                    }
                    if (stopMoving && !isHurt)
                    {
                        horizontalMove = 0;
                    }
                }
            }
        }
        playerAnim.SetFloat("run", Mathf.Abs(horizontalMove));
    }
    private void FixedUpdate()
    {
        if (HUD.value <= 0f)
        {
            Dead();
            deathDur -= Time.fixedDeltaTime;
            if (deathDur <= 0)
            {
                youDiedPanel.SetActive(true);
                gameMaster.stopPhysics = true;
            }
        }
        if (!isHurt)
        {
            controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
            jump = false;
            
        }
        if (isHurt)
        {
            playerRgbd.velocity = Vector3.zero;
            playerRgbd.angularVelocity = 0;
            recoveryTime -= Time.fixedDeltaTime;
            if (recoveryTime <= 0)
            {
                isHurt = false;
                recoveryTime = restoreRecoveryTime;
            }
        }
    }
    public void Hurt()
    {
        FindObjectOfType<AudioManager>().Play("enemyattackhit");
        int voice = Random.Range(1, 5);
        FindObjectOfType<AudioManager>().Play("charahurt" + voice);

        playerAnim.SetTrigger("hurt");
        HUD.value -= 1f;
        fill.color = HUDgradient.Evaluate(HUD.normalizedValue);
        isHurt = true;

        hurtLight.SetActive(true);

        damage = "-1";
        Instantiate(damageText, transform.position, transform.rotation, ui.transform);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "EnemyAttackCollider" && !isHurt)
        {
            Hurt();   
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform" || collision.gameObject.tag == "Platform2")
        {
            isJumping = false;
            playerAnim.SetBool("jump", isJumping);
        }
    }
    public void MoveLeft(bool isTapped)
    {
        if (isTapped)
        {
            GetImageOnButtonClick.SetImage(true, moveLeftNormal, moveLeftPressed);
            if (!isHurt)
            {
                stopMoving = false;
                directionMove = false;
            }
        }
        else
        {
            GetImageOnButtonClick.SetImage(false, moveLeftNormal, moveLeftPressed);
        }
    }
    public void MoveRight(bool isTapped)
    {
        if (isTapped)
        {
            GetImageOnButtonClick.SetImage(true, moveRightNormal, moveRightPressed);
            if (!isHurt)
            {
                stopMoving = false;
                directionMove = true;
            }
        }
        else
        {
            GetImageOnButtonClick.SetImage(false, moveRightNormal, moveRightPressed);
        }
    }
    public void MoveStop()
    {
        stopMoving = true;
    }
    public void Jump(bool isTapped)
    {
        if (isTapped)
        {
            GetImageOnButtonClick.SetImage(true, jumpNormal, jumpPressed);
            if (!isHurt)
            {
                if (!isJumping)
                {
                    GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("charajump");
                }
                isJumping = true;
                playerAnim.SetBool("jump", isJumping);
                jump = true;
            }
        }
        else
        {
            GetImageOnButtonClick.SetImage(false, jumpNormal, jumpPressed);
        }
    }
}
static class GetImageOnButtonClick
{
    public static void SetImage(bool isButtonClicked, GameObject activeImage, GameObject pressedImage)
    {
        if (isButtonClicked)
        {
            activeImage.SetActive(false);
            pressedImage.SetActive(true);
        }
        else
        {
            activeImage.SetActive(true);
            pressedImage.SetActive(false);
        }
    }
    public static void SetImage(bool isButtonClicked, GameObject pressedImage)
    {
        if (isButtonClicked)
        {
            pressedImage.SetActive(true);
        }
        else
        {
            pressedImage.SetActive(false);
        }
    }
}
static class GetImageOnCooldown
{
    public static void SetImage(float cooldownDuration, GameObject cooldownImage, GameObject normalImage, GameObject pressedImage)
    {
        if(cooldownDuration > 0)
        {
            cooldownImage.SetActive(true);
            normalImage.SetActive(false);
            pressedImage.SetActive(false);
        }
        else
        {
            cooldownImage.SetActive(false);
            normalImage.SetActive(true);
        }
    }
}
