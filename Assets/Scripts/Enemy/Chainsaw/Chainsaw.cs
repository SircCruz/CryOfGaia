using System.Collections;
using UnityEngine;

public class Chainsaw : MonoBehaviour
{
    EnemyHealthUI ui;
    CharacterAttacks character;
    GenerateEnemy chainsawSpawnLimiter;
    ChainsawSound sound;
    Rigidbody2D rgbd;
    
    public GameObject deathLight;

    public Animator chainsawAnim;
    public CharacterBulletScript characterbullet;
    public Melee characterMelee;
    public Skill1 skill1;
    public EnemyAttackGenerator enemyAttackGen;
    public float hitPoints = 20;
    public int attackDamage = 1;
    private int killCounter;

    private float knockbackCooldown = 0.8f;
    private float restoreKnockbackCooldown;
    private bool isHit = false;

    private float stunDuration = 4f;
    private float restoreStunDuration;
    private bool isStunned = false;

    SceneLoader app;

    public GameObject damageText;
    public string damage;
    private void OnApplicationQuit()
    {
        app.isQuitting = true;
    }
    private void OnDestroy()
    {
        if (!app.isQuitting)
        {
            PlayerPrefs.SetInt("Enemies Killed", ++killCounter);
            chainsawSpawnLimiter.ReduceLimit();
        }
    }
    void Start()
    {
        ui = GetComponentInChildren<EnemyHealthUI>();
        chainsawSpawnLimiter = GameObject.FindGameObjectWithTag("Game Master").GetComponent<GenerateEnemy>();

        sound = gameObject.GetComponentInChildren<ChainsawSound>();
        rgbd = gameObject.GetComponent<Rigidbody2D>();

        deathLight.SetActive(false);

        app = chainsawSpawnLimiter.GetComponent<SceneLoader>();

        character = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterAttacks>();
        restoreKnockbackCooldown = knockbackCooldown;
        killCounter = PlayerPrefs.GetInt("Enemies Killed", 0);
    }
    void Update()
    {
        if (hitPoints <= 0)
        {
            gameObject.layer = LayerMask.NameToLayer("Task Object");
            enemyAttackGen.enabled = false;
            deathLight.SetActive(true);
            chainsawAnim.SetBool("death", true);
            sound.Dead();
            Destroy(gameObject, 3);
        }
    }
    private void FixedUpdate()
    {
        if (isHit)
        {
            knockbackCooldown -= Time.fixedDeltaTime;
            enemyAttackGen.enabled = false;
            if(knockbackCooldown <= 0)
            {
                if(hitPoints <= 0)
                {
                    enemyAttackGen.enabled = false;
                }
                else
                {
                    enemyAttackGen.enabled = true;
                    chainsawAnim.SetBool("knock", false);
                    isHit = false;
                    knockbackCooldown = restoreKnockbackCooldown;
                }
            }
        }
        if (isStunned)
        {
            stunDuration -= Time.fixedDeltaTime;
            enemyAttackGen.enabled = false;
            if(stunDuration <= 0)
            {
                if (hitPoints <= 0)
                {
                    enemyAttackGen.enabled = false;
                }
                else
                {
                    stunDuration = restoreStunDuration;
                    enemyAttackGen.enabled = true;
                    chainsawAnim.SetBool("stun", false);
                    isStunned = false;
                }
                
            }
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            hitPoints -= characterbullet.damage;
            damage = "-" + characterbullet.damage.ToString("#.#");
            Instantiate(damageText, transform.position, transform.rotation, ui.transform);
            ui.HPBarUpdate();
        }
        if(collision.gameObject.tag == "Melee")
        {
            hitPoints -= characterMelee.damage;
            damage = "-" + characterMelee.damage.ToString("#.#");
            Instantiate(damageText, transform.position, transform.rotation, ui.transform);
            ui.HPBarUpdate();
        }
        if (collision.gameObject.tag == "Skill1")
        {
            hitPoints -= skill1.damage;
            damage = "-" + skill1.damage.ToString("#.#");
            Instantiate(damageText, transform.position, transform.rotation, ui.transform);
            isHit = true;
            chainsawAnim.SetBool("knock", true);
            ui.HPBarUpdate();
        }
        if(collision.gameObject.tag == "Skill2")
        {
            isStunned = true;
            hitPoints -= character.skill2AttackDamage;
            damage = "STUNNED!";
            Instantiate(damageText, transform.position, transform.rotation, ui.transform);
            chainsawAnim.SetBool("stun", true);
            ui.HPBarUpdate();
        }
        if(collision.gameObject.tag == "KillEnemies")
        {
            gameObject.layer = LayerMask.NameToLayer("Task Object");
            enemyAttackGen.enabled = false;
            chainsawAnim.SetBool("death", true);
            rgbd.constraints = RigidbodyConstraints2D.FreezePositionX;
            Destroy(gameObject, 3);
        }
    }
}
