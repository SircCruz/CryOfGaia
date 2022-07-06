using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Security.Permissions;
using TMPro;

public class CharacterAttacks : MonoBehaviour
{
    Player playerScript;
    CharacterController2D character;
    public Animator playerAnim;
    public GameObject player;
    public GameObject melee;
    public GameObject rangeNormal, rangePressed;
    public GameObject meleeNormal, meleePressed;
    public GameObject skill1CooldownImg, skill1Normal, skill1Pressed;
    public GameObject skill2CooldownImg, skill2Normal, skill2Pressed;

    public bool isAttacking = false;
    private float attackTime = 0.2f;
    private float restoreAttackTime;

    public Button btnSkill1;
    public Button btnSkill2;

    private float skill1Cooldown;
    private float restoreSkill1Cooldown;
    private bool skill1isUsed = false;
    
    private float skill2Cooldown;
    private float restoreSkill2Cooldown;
    private bool skill2isUsed = false;

    bool isRanged, isMelee, isSkill1;

    //for ranged attack
    public GameObject bullet;
    public GameObject skill1;

    //Skill1
    Vector3 spawnPos = Vector3.right;
    Vector3 playerPos;
    Vector3 skill1Offset;
    Quaternion playerRotation;
    float spawnDistance = 1f;
    float skill1spawnDistance = 1.8f;
    public Slider skill1Slider;
    public TextMeshProUGUI skill1CooldownText;

    //skill2
    public float skill2AttackDamage;
    public Player stopPlayer;
    private float preAttackTime = 2f;
    private float restorePreAttackTime;
    private bool isUsedSkill2 = false;
    public GameObject skill2;
    Vector3 skill2Offset;
    float skill2spawnDistance = 0.8f;
    public Slider skill2Slider;
    public TextMeshProUGUI skill2CooldownText;
    private bool disable;

    // Start is called before the first frame update
    void Start()
    {
        skill1Cooldown = UpgradeCheck.Skill1Cooldown();

        skill2Cooldown = UpgradeCheck.Skill2Cooldown();
        skill2AttackDamage = UpgradeCheck.Skill2Damage();

        restoreAttackTime = attackTime;

        restorePreAttackTime = preAttackTime;

        restoreSkill1Cooldown = skill1Cooldown;
        restoreSkill2Cooldown = skill2Cooldown;

        character = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController2D>();
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        playerAnim = character.GetComponent<Animator>();

        btnSkill1.interactable = false;
        btnSkill2.interactable = false;

        skill1Slider.enabled = true;
        skill1Slider.maxValue = skill1Cooldown;
        skill1Slider.value = skill1Cooldown;
        skill1CooldownText.text = skill1Cooldown.ToString("##");

        skill2Slider.enabled = true;
        skill2Slider.maxValue = skill2Cooldown;
        skill2Slider.value = skill2Cooldown;
        skill2CooldownText.text = skill2Cooldown.ToString("##");

    }

    // Update is called once per frame
    void Update()
    {
        //for ranged attack
        if (!character.m_FacingRight)
        {
            playerPos = new Vector2(player.transform.position.x - spawnDistance, player.transform.position.y - 0.5f);
            skill1Offset = new Vector2(player.transform.position.x - skill2spawnDistance, player.transform.position.y);
            skill2Offset = new Vector2(player.transform.position.x - skill2spawnDistance, player.transform.position.y + 1.7f);
        }
        else
        {
            playerPos = new Vector2(player.transform.position.x + spawnDistance, player.transform.position.y - 0.5f);
            skill1Offset = new Vector2(player.transform.position.x + skill2spawnDistance, player.transform.position.y);
            skill2Offset = new Vector2(player.transform.position.x + skill2spawnDistance, player.transform.position.y + 1.7f);
        }
        Vector3 playerDirection = player.transform.forward;
        playerRotation = player.transform.rotation;
        if (Input.GetKeyDown(KeyCode.U))
        {
            Melee(true);
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            Attack(true);
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            Skill1(true);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            Skill2(true);
        }
    }
    private void FixedUpdate()
    {
        //attack
        if (isAttacking)
        {
            attackTime -= Time.fixedDeltaTime;
            if(attackTime <= 0)
            {
                if (isMelee)
                {
                    Vector3 playerDirection = player.transform.forward;
                    spawnPos = playerPos + playerDirection * spawnDistance;
                    Instantiate(melee, spawnPos, playerRotation);
                    isMelee = false;
                }
                if (isRanged)
                {
                    Vector3 playerDirection = player.transform.forward;
                    spawnPos = playerPos + playerDirection * spawnDistance;
                    Instantiate(bullet, spawnPos, playerRotation);
                    isRanged = false;
                }
                if (isSkill1)
                {
                    Vector3 playerDirection = player.transform.forward;
                    spawnPos = skill1Offset + playerDirection * skill1spawnDistance;
                    Instantiate(skill1, skill1Offset, playerRotation);
                    btnSkill1.interactable = false;
                    isSkill1 = false;
                }
                attackTime = restoreAttackTime;
                isAttacking = false;
            }
        }
        //Skill2
        if (btnSkill2.interactable == false)
        {
            skill2isUsed = true;
        }
        if (skill2isUsed)
        {
            GetImageOnCooldown.SetImage(skill2Cooldown, skill2CooldownImg, skill2Normal, skill2Pressed);
            skill2Slider.enabled = true;
            skill2Slider.value = skill2Cooldown;
            skill2CooldownText.text = skill2Cooldown.ToString("##");
            skill2Cooldown -= Time.fixedDeltaTime;
            if (skill2Cooldown <= 0)
            {
                GetImageOnCooldown.SetImage(skill2Cooldown, skill2CooldownImg, skill2Normal, skill2Pressed);
                skill2Slider.enabled = false;
                skill2CooldownText.text = "";
                skill2Cooldown = restoreSkill2Cooldown;
                btnSkill2.interactable = true;
                skill2isUsed = false;
            }
        }
        if (isUsedSkill2)
        {
            playerScript.playerRgbd.velocity = Vector3.zero;
            playerScript.playerRgbd.angularVelocity = 0;
            playerScript.runSpeed = 20;
            preAttackTime -= Time.fixedDeltaTime;
            gameObject.layer = LayerMask.NameToLayer("Task Item");
            if (preAttackTime <= 0)
            {
                Instantiate(skill2, skill2Offset, transform.rotation);
                playerScript.runSpeed = 80;
                gameObject.layer = LayerMask.NameToLayer("Player");
                isUsedSkill2 = false;
                preAttackTime = restorePreAttackTime;
            }
        }
        //skill1
        if (btnSkill1.interactable == false)
        {
            skill1isUsed = true;
        }
        if (skill1isUsed)
        {
            GetImageOnCooldown.SetImage(skill1Cooldown, skill1CooldownImg, skill1Normal, skill1Pressed);
            btnSkill1.interactable = false;
            skill1Slider.enabled = true;
            skill1Slider.value = skill1Cooldown;
            skill1CooldownText.text = skill1Cooldown.ToString("##");
            skill1Cooldown -= Time.fixedDeltaTime;
            if (skill1Cooldown <= 0f)
            {
                GetImageOnCooldown.SetImage(skill1Cooldown, skill1CooldownImg, skill1Normal, skill1Pressed);
                skill1Slider.enabled = false;
                skill1CooldownText.text = "";
                btnSkill1.interactable = true;
                skill1Cooldown = restoreSkill1Cooldown;
                skill1isUsed = false;
            }
        }
        
    }
    public void Melee(bool isTapped)
    {
        if (isTapped && !disable)
        {
            GetImageOnButtonClick.SetImage(true, meleeNormal, meleePressed);
            if (!isAttacking)
            {
                int genAnim = Random.Range(1, 3);
                if (genAnim == 1)
                {
                    playerAnim.SetTrigger("melee 1");
                }
                else if (genAnim == 2)
                {
                    playerAnim.SetTrigger("melee 2");
                }
                isMelee = true;
                isAttacking = true;
            }
        }
        else
        {
            GetImageOnButtonClick.SetImage(false, meleeNormal, meleePressed);
        }
    }
    public void Attack(bool isTapped)
    {
        if (isTapped && !disable)
        {
            GetImageOnButtonClick.SetImage(true, rangeNormal, rangePressed);
            if (!isAttacking)
            {
                int genAnim = Random.Range(1, 3);
                if (genAnim == 1)
                {
                    playerAnim.SetTrigger("range 1");
                }
                else if (genAnim == 2)
                {
                    playerAnim.SetTrigger("range 2");
                }
                isRanged = true;
                isAttacking = true;
            }
        }
        else
        {
            GetImageOnButtonClick.SetImage(false, rangeNormal, rangePressed);
        }
    }
    public void Skill1(bool isTapped)
    {
        if (!skill1isUsed && !disable)
        {
            if (isTapped)
            {
                GetImageOnButtonClick.SetImage(true, skill1Normal, skill1Pressed);
                playerAnim.SetTrigger("skill 1");
                isSkill1 = true;
                isAttacking = true;
            }
            else
            {
                GetImageOnButtonClick.SetImage(false, skill1Normal, skill1Pressed);
            }
        }
    }
    public void Skill2(bool isTapped)
    {
        if (!skill2isUsed)
        {
            if (isTapped)
            {
                GetImageOnButtonClick.SetImage(true, skill2Normal, skill2Pressed);
                FindObjectOfType<AudioManager>().Play("flowerskillvoice");
                playerAnim.SetTrigger("skill 2");
                isUsedSkill2 = true;
                StartCoroutine(DisableControls());
                btnSkill2.interactable = false;
            }
            else
            {
                GetImageOnButtonClick.SetImage(false, skill2Normal, skill2Pressed);
            }
        }
    }
    IEnumerator DisableControls()
    {
        disable = true;
        yield return new WaitForSeconds(2.5f);
        disable = false;
    }
}
static class UpgradeCheck
{
    public static float CharacterHealthPoints()
    {
        if (PlayerPrefs.GetInt("Sunflower Upgrade" + 4) == 1)
        {
            return (30 * 2) + (30 * 0.1f) + (30 * 0.2f) + (30 * 0.5f);
        }
        else if (PlayerPrefs.GetInt("Sunflower Upgrade" + 3) == 1)
        {
            return 30 + (30 * 0.1f) + (30 * 0.2f) + (30 * 0.5f);
        }
        else if (PlayerPrefs.GetInt("Sunflower Upgrade" + 2) == 1)
        {
            return 30 + (30 * 0.1f) + (30 * 0.2f);
        }
        else if (PlayerPrefs.GetInt("Sunflower Upgrade" + 1) == 1)
        {
            return 30 + (30 * 0.1f);
        }
        else
        {
            return 30;
        }
    }
    public static float RangedDamage()
    {
        if (PlayerPrefs.GetInt("Jasmine Upgrade" + 3) == 1)
        {
            return (2 * 2) + (2 * 0.1f) + (2 * 0.5f);
        }
        else if (PlayerPrefs.GetInt("Jasmine Upgrade" + 2) == 1)
        {
            return 2 + (2 * 0.1f) + (2 * 0.5f);
        }
        else if (PlayerPrefs.GetInt("Jasmine Upgrade" + 1) == 1)
        {
            return 2 + (2 * 0.1f);
        }
        else
        {
            return 2;
        }
    }
    public static Color RangedVisualUpgrade()
    {
        if (PlayerPrefs.GetInt("Jasmine Upgrade" + 3) == 1)
        {
            return new Color(0, 255, 0);
        }
        else if (PlayerPrefs.GetInt("Jasmine Upgrade" + 2) == 1)
        {
            return new Color(255, 0, 155);
        }
        else if (PlayerPrefs.GetInt("Jasmine Upgrade" + 1) == 1)
        {
            return new Color(255, 125, 0);
        }
        else
        {
            return new Color(255, 255, 255);
        }
    }
    public static float MeleeDamage()
    {
        if (PlayerPrefs.GetInt("Santan Upgrade" + 3) == 1)
        {
            return (1.5f * 2) + (1.5f * 0.1f) + (1.5f * 0.5f);
        }
        else if (PlayerPrefs.GetInt("Santan Upgrade" + 2) == 1)
        {
            return 1.5f + (1.5f * 0.1f) + (1.5f * 0.5f);
        }
        else if (PlayerPrefs.GetInt("Santan Upgrade" + 1) == 1)
        {
            return 1.5f + (1.5f * 0.1f);
        }
        else
        {
            return 1.5f;
        }
    }
    public static float Skill1Damage()
    {
        if (PlayerPrefs.GetInt("Dandelion Upgrade" + 2) == 1)
        {
            return 5 * 2 + (5 * 0.5f);
        }
        else if (PlayerPrefs.GetInt("Dandelion Upgrade" + 1) == 1)
        {
            return 5 + (5 * 0.5f);
        }
        else
        {
            return 5;
        }
    }
    public static Color Skill1VisualUpgrade()
    {
        if (PlayerPrefs.GetInt("Dandelion Upgrade" + 2) == 1)
        {
            return new Color(0, 255, 0);
        }
        else if (PlayerPrefs.GetInt("Dandelion Upgrade" + 1) == 1)
        {
            return new Color(255, 0, 155);
        }
        else
        {
            return new Color(255, 255, 255);
        }
    }
    public static float Skill1Cooldown()
    {
        if(PlayerPrefs.GetInt("Rose Upgrade" + 2) == 1)
        {
            return 5;
        }
        else if (PlayerPrefs.GetInt("Rose Upgrade" + 1) == 1)
        {
            return 15;
        }
        else
        {
            return 25;
        }
    }
    public static float Skill2Damage()
    {
        if (PlayerPrefs.GetInt("Tulips Upgrade" + 2) == 1)
        {
            return 7 * 2 + (7 * 0.5f);
        }
        else if (PlayerPrefs.GetInt("Tulips Upgrade" + 1) == 1)
        {
            return 7 + (7 * 0.5f);
        }
        else
        {
            return 7;
        }
    }
    public static float Skill2Cooldown()
    {
        if (PlayerPrefs.GetInt("Portulaca Upgrade" + 2) == 1)
        {
            return 15;
        }
        else if (PlayerPrefs.GetInt("Portulaca Upgrade" + 1) == 1)
        {
            return 30;
        }
        else
        {
            return 45;
        }
    }
}
