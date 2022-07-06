using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttacksProto : MonoBehaviour
{
    //Attach this script if the enemy targets the objects
    public bool attackImmediately;

    public Transform target;
    public Transform enemy;
    Vector2 targetPos;

    public GameObject enemyAttackObject;
    bool isSpawn;

    //public Animator enemyAnim;

    [SerializeField] private float runSpeed = 4f;

    [SerializeField] private float attackRange = 1f;
    [SerializeField] private float attackingTime = 1.4f;
    [SerializeField] private float damageEffectTime = 0.1f;
    [SerializeField] private float cooldown = 2f;
    float restoreAttackingTime;
    float restoreCooldown;

    public bool startAttack = false;
    private bool stopCountingAttackTime = false;
    private bool stopCountingEffectTime = false;
    private bool startMoving = true;

    private float restoreDamageEffectTime;
    void OnEnable()
    {
        if (attackingTime == restoreAttackingTime)
        {
            //enemyAnim.SetBool("run", true);
            if (attackingTime < restoreAttackingTime)
            {
                //enemyAnim.SetBool("idle", true);
            }
        }
    }
    void OnDisable()
    {
        //enemyAnim.SetBool("run", false);
        //enemyAnim.SetBool("idle", false);
        //enemyAnim.SetBool("attack", false);
    }
    // Start is called before the first frame update
    void Start()
    {
        restoreAttackingTime = attackingTime;
        restoreDamageEffectTime = damageEffectTime;
        restoreCooldown = cooldown;
    }
    void FixedUpdate()
    {
        if (startMoving)
        {
            //enemyAnim.SetBool("idle", false);
            //enemyAnim.SetBool("run", true);
            followObject();
        }
        if (!attackImmediately)
        {
            if (Mathf.Abs(target.transform.position.x - transform.position.x) <= attackRange + 0.1f)
            {
                startMoving = false;
                startAttack = true;
            }
        }
        else
        {
            startAttack = true;
        }
        if (startAttack)
        {
            //enemyAnim.SetBool("attack", true);
            //enemyAnim.SetBool("run", false);
            //enemyAnim.SetBool("idle", false);
            Attack();
        }
    }
    private void Attack()
    {
        if (!stopCountingAttackTime)
        {
            attackingTime -= Time.deltaTime;
        }
        if (attackingTime <= 0)
        {
            //enemyAnim.SetBool("attack", false);
            attackingTime = 0;
            stopCountingAttackTime = true;
            SpawnAttackObject();
            if (!stopCountingEffectTime)
            {
                damageEffectTime -= Time.deltaTime;
            }
            if (damageEffectTime <= 0)
            {
                //enemyAnim.SetBool("idle", true);
                damageEffectTime = 0;
                stopCountingEffectTime = true;
                cooldown -= Time.deltaTime;
                if (cooldown <= 0)
                {
                    startMoving = true;
                    startAttack = false;
                    stopCountingAttackTime = false;
                    stopCountingEffectTime = false;
                    isSpawn = false;

                    cooldown = restoreCooldown;
                    damageEffectTime = restoreDamageEffectTime;
                    attackingTime = restoreAttackingTime;
                }
            }
        }
    }
    private void SpawnAttackObject()
    {
        if (!isSpawn)
        {
            Instantiate(enemyAttackObject, transform.position, transform.rotation, gameObject.transform);
            isSpawn = true;
        }
    }
    private void followObject()
    {

        if (target.transform.position.x < transform.position.x)
        {
            targetPos = new Vector2(target.transform.position.x + attackRange, enemy.transform.position.y);
        }
        else if (target.transform.position.x > transform.position.x)
        {
            targetPos = new Vector2(target.transform.position.x - attackRange, enemy.transform.position.y);
        }
        enemy.transform.position = Vector2.MoveTowards(enemy.transform.position, targetPos, runSpeed * Time.fixedDeltaTime);

        if (target.transform.position.x < enemy.transform.position.x)
        {
            Vector3 enemyScale = enemy.transform.localScale;
            enemyScale.z = 1;
            enemyScale.x = -1;
            enemy.transform.localScale = enemyScale;
        }
        else if (target.transform.position.x > enemy.transform.position.x)
        {
            Vector3 enemyScale = enemy.transform.localScale;
            enemyScale.z = 1;
            enemyScale.x = 1;
            enemy.transform.localScale = enemyScale;
        }
    }
}
