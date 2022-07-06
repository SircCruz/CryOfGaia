using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateEnemy : MonoBehaviour
{
    public GameObject[] enemies;

    [HideInInspector] public GameObject enemy;
    private SpriteRenderer layer;
    public int spawnLimiter;
    public float limit;
    public int genEnemyType;

    public float leftSpawnPoint = 49.5f;
    public float rightSpawnPoint = -53.4f;
    private Vector2 spawnLocation1;
    private Vector2 spawnLocation2;

    public int killCounter;
    int location;

    float cooldown;
    public float cooldownMin = 1f;
    public float cooldownMax = 5f;

    bool generateCooldown = true;

    public BadgeShow badge;
    void Start()
    {
        enemy = EnemyType();

        spawnLocation1 = new Vector2(rightSpawnPoint, 0);
        spawnLocation2 = new Vector2(leftSpawnPoint, 0);
        layer = enemy.GetComponent<SpriteRenderer>();

        killCounter = PlayerPrefs.GetInt("Enemies Killed");
    }
    void FixedUpdate()
    {
        enemy = EnemyType();
        if (spawnLimiter <= limit)
        {
            if (generateCooldown)
            {
                cooldown = Random.Range(cooldownMin, cooldownMax);
                generateCooldown = false;
            }
            if (!generateCooldown)
            {
                location = Random.Range(1, 3);

                cooldown -= Time.fixedDeltaTime;
                if (cooldown <= 0)
                {
                    if (location == 1)
                    {
                        Instantiate(enemy, spawnLocation1, enemy.transform.rotation);
                    }
                    else if (location == 2)
                    {
                        Instantiate(enemy, spawnLocation2, enemy.transform.rotation);
                    }
                    
                    generateCooldown = true;
                    spawnLimiter++;
                    layer.sortingOrder = spawnLimiter * -1;
                }
            }
        }
        if(badge != null)
        {
            if (killCounter >= 50000)
            {
                badge.ShowBadge("Guardian");
            }
            else if (killCounter >= 1000)
            {
                badge.ShowBadge("Savior");
            }
        }
    }
    public void ReduceLimit()
    {
        PlayerPrefs.SetInt("Enemies Killed", ++killCounter);
        spawnLimiter -= 1;
    }
    private GameObject EnemyType()
    {
        genEnemyType = Random.Range(0, enemies.Length);
        for(int i = 0; i < enemies.Length; i++)
        {
            if (genEnemyType == i)
            {
                return enemies[i];
            }
        }
        return null;
    }
}
