using System.Collections;
using UnityEngine;

public class Coalman : MonoBehaviour
{
    CharacterAttacks character;
    GenerateEnemy chainsawSpawnLimiter;
    //ChainsawSound sound;
    Rigidbody2D rgbd;

    //public GameObject deathLight;

    //public Animator enemyAnim;
    public EnemyAttackGenerator enemyAttackGenerator;
    public CharacterBulletScript characterbullet;
    public Melee characterMelee;
    public Skill1 skill1;
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
        chainsawSpawnLimiter = GameObject.FindGameObjectWithTag("Game Master").GetComponent<GenerateEnemy>();

        //sound = gameObject.GetComponentInChildren<ChainsawSound>();
        rgbd = gameObject.GetComponent<Rigidbody2D>();

        //deathLight.SetActive(false);

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
            enemyAttackGenerator.enabled = false;
            //deathLight.SetActive(true);
            //enemyAnim.SetBool("death", true);
            //sound.Dead();
            Destroy(gameObject);
        }
    }
    private void FixedUpdate()
    {
        if (isHit)
        {
            knockbackCooldown -= Time.fixedDeltaTime;
            enemyAttackGenerator.enabled = false;
            if (knockbackCooldown <= 0)
            {
                if (hitPoints <= 0)
                {
                    enemyAttackGenerator.enabled = false;
                }
                else
                {
                    enemyAttackGenerator.enabled = true;
                    //enemyAnim.SetBool("knock", false);
                    isHit = false;
                    knockbackCooldown = restoreKnockbackCooldown;
                }
            }
        }
        if (isStunned)
        {
            stunDuration -= Time.fixedDeltaTime;
            enemyAttackGenerator.enabled = false;
            if (stunDuration <= 0)
            {
                if (hitPoints <= 0)
                {
                    enemyAttackGenerator.enabled = false;
                }
                else
                {
                    stunDuration = restoreStunDuration;
                    enemyAttackGenerator.enabled = true;
                    //enemyAnim.SetBool("stun", false);
                    isStunned = false;
                }

            }
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            hitPoints -= characterbullet.damage;
        }
        if (collision.gameObject.tag == "Melee")
        {
            hitPoints -= characterMelee.damage;
        }
        if (collision.gameObject.tag == "Skill1")
        {
            hitPoints -= skill1.damage;
            isHit = true;
            //enemyAnim.SetBool("knock", true);
        }
        if (collision.gameObject.tag == "Skill2")
        {
            isStunned = true;
            hitPoints -= character.skill2AttackDamage;
            //enemyAnim.SetBool("stun", true);
        }
        if (collision.gameObject.tag == "KillEnemies")
        {
            gameObject.layer = LayerMask.NameToLayer("Task Object");
            enemyAttackGenerator.enabled = false;
            //enemyAnim.SetBool("death", true);
            rgbd.constraints = RigidbodyConstraints2D.FreezePositionX;
            Destroy(gameObject, 3);
        }
    }
}
