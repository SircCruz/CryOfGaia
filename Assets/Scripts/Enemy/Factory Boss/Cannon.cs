using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public GameObject bullet;

    private float cooldown = 0.2f;
    private void OnEnable()
    {
        int count = Random.Range(1, 7);
        StartCoroutine(LaunchBullet(count));
    }
    private IEnumerator LaunchBullet(int bulletCount)
    {
        for(int i = 1; i <= bulletCount; i++)
        {
            Instantiate(bullet, transform.position, bullet.transform.rotation);
            yield return new WaitForSeconds(cooldown);
        }
        gameObject.SetActive(false);
    }
}
