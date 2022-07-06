using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Confetti1 : MonoBehaviour
{
    //the behavior of confetti
    float forceDur = 0.5f, forceDur2 = 0.5f;
    bool force2;
    float activeDur;

    Rigidbody2D rgbd;
    float xForce, yForce;
    void Start()
    {
        rgbd = gameObject.GetComponent<Rigidbody2D>();

        activeDur = Random.Range(2f, 3.5f);

        xForce = Random.Range(-0.002f, 0.002f);
        yForce = Random.Range(0.005f,0.009f);

        rgbd.AddForce(new Vector2(xForce, yForce), ForceMode2D.Impulse);
    }
    private void FixedUpdate()
    {
        forceDur -= Time.fixedDeltaTime;
        if(forceDur >= 0 && !force2)
        {
            rgbd.mass = 0.01f;
            rgbd.gravityScale = 1f;
            force2 = true;
        }
        if (force2)
        {
            forceDur2 -= Time.fixedDeltaTime;
            if (forceDur2 <= 0)
            {
                rgbd.gravityScale = 0.2f;
            }
        }
        activeDur -= Time.fixedDeltaTime;
        if(activeDur <= 0)
        {
            Destroy(gameObject);
        }
    }
}
