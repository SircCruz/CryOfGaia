using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackProto : MonoBehaviour
{
    //Attach this script if the target of the enemy is the character

    Transform target;
    Vector2 targetPos;

    [HideInInspector] public Vector3 enemyScale;

    public Transform enemy;

    public GameObject enemyAttackObject;
    bool isSpawn;

    private float runSpeed = 4f;

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

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        restoreAttackingTime = attackingTime;
        restoreDamageEffectTime = damageEffectTime;
        restoreCooldown = cooldown;
    }
    void FixedUpdate()
    {
        if (startMoving)
        {
            followPlayer();
        }
        if (Mathf.Abs(target.transform.position.x - transform.position.x) <= attackRange + 0.1f)
        {
            startMoving = false;
            startAttack = true;
        }
        if (startAttack)
        {
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
            attackingTime = 0;
            stopCountingAttackTime = true;
            SpawnAttackObject();
            if (!stopCountingEffectTime)
            {
                damageEffectTime -= Time.deltaTime;
            }
            if (damageEffectTime <= 0)
            {
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
            Instantiate(enemyAttackObject, transform.position, Quaternion.identity, gameObject.transform);
            isSpawn = true;
        }
    }
    private void followPlayer()
    {
        if (target != null)
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
                enemyScale = enemy.transform.localScale;
                enemyScale.z = 1;
                enemyScale.x = -1;
                enemy.transform.localScale = enemyScale;
            }
            else if (target.transform.position.x > enemy.transform.position.x)
            {
                enemyScale = enemy.transform.localScale;
                enemyScale.z = 1;
                enemyScale.x = 1;
                enemy.transform.localScale = enemyScale;
            }
        }
    }
}
