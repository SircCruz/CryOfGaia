using System.Collections;
using System.Collections.Generic;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    SceneLoader app;
    Rigidbody2D rb;
    Transform player;
    public GameObject rockGround;
    float yForce = 17f;
    float xForce;
    float collideCounter = 0;

    private void OnApplicationQuit()
    {
        app.isQuitting = true;
    }
    private void OnDestroy()
    {
        if (!app.isQuitting)
        {
            Instantiate(rockGround, new Vector3(transform.position.x, transform.position.y - 0.3f), Quaternion.identity);
        }
    }
    void Start()
    {
        app = GameObject.FindGameObjectWithTag("Game Master").GetComponent<SceneLoader>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        xForce = player.transform.position.x;
        rb = GetComponent<Rigidbody2D>();

        FindObjectOfType<AudioManager>().Play("rockfire");
    }
    void FixedUpdate()
    {
        rb.AddForce(new Vector2(xForce - 1f, yForce),ForceMode2D.Force);
        yForce -= 0.3f;
        if (yForce <= 0)
        {
            rb.gravityScale = 1f;
        }
        transform.Rotate(new Vector3(0,0, 360 * Time.fixedDeltaTime));
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            if(collideCounter != 1)
            {
                collideCounter = 1;
            }
        }
        if(collision.gameObject.tag == "Platform")
        {
            if (collideCounter == 1)
            {
                Destroy(gameObject);
            }
        }
    }
}
