using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Seed : MonoBehaviour
{
    public GameObject seed;
    public Animator seedAnim;

    private float timeActive = 10f;
    C1Mission1 c1Mission1;
    private void Start()
    {
        c1Mission1 = GameObject.FindGameObjectWithTag("Task Monitor").GetComponent<C1Mission1>();
    }
    private void Update()
    {
        if (c1Mission1.isHoldingSeed)
        {
            seed.layer = LayerMask.NameToLayer("Task Object");
        }
        else if (!c1Mission1.isHoldingSeed)
        {
            seed.layer = LayerMask.NameToLayer("Task Item");
        }
        timeActive -= Time.fixedDeltaTime;
        if(timeActive <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && !c1Mission1.isHoldingSeed)
        {
            c1Mission1.isHoldingSeed = true;
            Destroy(gameObject);
        }
        if(collision.gameObject.tag == "Platform")
        {
            seedAnim.SetBool("ground", true);
        }
    }
}
