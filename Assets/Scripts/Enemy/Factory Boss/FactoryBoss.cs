using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryBoss : MonoBehaviour
{
    public GameObject coalMissileManager;
    public GameObject earthquake;
    public GameObject cannon;

    private float cooldown, restoreCooldown;

    private void Start()
    {
        coalMissileManager.SetActive(false);
        cannon.SetActive(false);
        earthquake.SetActive(false);

        cooldown = 5;
        restoreCooldown = cooldown;
    }
    private void FixedUpdate()
    {
        Timer();
    }
    private void Timer()
    {
        cooldown -= Time.fixedDeltaTime;
        if(cooldown <= 0)
        {
            cooldown = restoreCooldown;
            StartCoroutine(AttackGenerator());
        }
    }
    private IEnumerator AttackGenerator()
    {
        yield return new WaitForSeconds(1);

        int genAttack = Random.Range(1, 100);
        if(genAttack < 35)
        {
            LaunchMissile();
        }
        if (genAttack >= 35 && genAttack <= 70)
        {
            FireCannon();
        }
        if(genAttack > 70)
        {
            EarthQuake();
        }
    }
    private void LaunchMissile()
    {
        coalMissileManager.SetActive(true);
    }
    private void FireCannon()
    {
        cannon.SetActive(true);
    }
    private void EarthQuake()
    {
        earthquake.SetActive(true);
    }
}
