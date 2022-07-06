using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaf : MonoBehaviour
{
    private float timer;
    public Animator leaf;
    private void Start()
    {
        timer = Random.Range(5f, 8f);
    }
    private void FixedUpdate()
    {
        timer -= Time.fixedDeltaTime;
        if(timer <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            leaf.SetBool("Idle", true);
        }
    }
}
