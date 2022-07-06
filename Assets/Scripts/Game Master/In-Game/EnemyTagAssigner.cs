using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTagAssigner : MonoBehaviour
{
    GenerateEnemy enemies;
    EnemyAttackProto[] enemyLists;

    public float enemyScale;

    private void Start()
    {
        enemies = GameObject.Find("Game Master").GetComponent<GenerateEnemy>();
    }
    public void Refresh()
    {
        enemyLists = new EnemyAttackProto[enemies.spawnLimiter];

        for(int i = 0; i <enemyLists.Length; i++)
        {
            enemyScale = enemyLists[i].enemyScale.x;
        }
    }
}
