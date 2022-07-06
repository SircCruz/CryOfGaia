using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chainsaw2 : MonoBehaviour
{
    CharacterAttacks character;
    GenerateEnemy chainsawSpawnLimiter;
    ChainsawSound sound;
    Rigidbody2D rgbd;

    public GameObject enemyLight;
    public Animator chainsawAnim;
    public CharacterBulletScript characterbullet;
    public Melee characterMelee;
    public Skill1 skill1;

    public EnemyAttack enemyAttack;
    public EnemyAttacks target1;
    public EnemyAttacks target2;
    public EnemyAttacks target3;

    public EnemyAttackGenerator enemyAttackGen;

    public float hitPoints = 25;
    public int attackDamage = 1;

    private float knockbackCooldown = 0.8f;
    private float restoreKnockbackCooldown;
    private bool isHit = false;

    private float stunDuration = 4f;
    private float restoreStunDuration;
    private bool isStunned = false;
    private void OnDestroy()
    {
        chainsawSpawnLimiter.ReduceLimit();
    }
    void Start()
    {
        chainsawSpawnLimiter = GameObject.FindGameObjectWithTag("Game Master").GetComponent<GenerateEnemy>();

        sound = gameObject.GetComponentInChildren<ChainsawSound>();
        rgbd = gameObject.GetComponent<Rigidbody2D>();

        character = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterAttacks>();

        restoreKnockbackCooldown = knockbackCooldown;
    }
    void Update()
    {
        if (hitPoints <= 0)
        {
            gameObject.layer = LayerMask.NameToLayer("Task Object");
            enemyAttackGen.enabled = false;
            enemyLight.SetActive(true);
            chainsawAnim.SetBool("death", true);
            //sound.Dead();
            Destroy(gameObject, 3);
        }
    }
    private void FixedUpdate()
    {
        if (isHit)
        {
            knockbackCooldown -= Time.fixedDeltaTime;
            enemyAttackGen.enabled = false;
            if (knockbackCooldown <= 0)
            {
                if (hitPoints <= 0)
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
            if (stunDuration <= 0)
            {
                if (hitPoints <= 0)
                {
                    enemyAttackGen.enabled = false;
                }
                else
                {
                    enemyAttackGen.enabled = true;
                    stunDuration = restoreStunDuration;
                    chainsawAnim.SetBool("stun", false);
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
            chainsawAnim.SetBool("knock", true);
            isHit = true;
        }
        if (collision.gameObject.tag == "Skill2")
        {
            chainsawAnim.SetBool("stun", true);
            isStunned = true;
            hitPoints -= character.skill2AttackDamage;
        }
        if (collision.gameObject.tag == "KillEnemies")
        {
            gameObject.layer = LayerMask.NameToLayer("Task Object");
            enemyAttack.enabled = false;
            chainsawAnim.SetBool("death", true);
            rgbd.constraints = RigidbodyConstraints2D.FreezePositionX;
            Destroy(gameObject, 3);
        }
    }
}
