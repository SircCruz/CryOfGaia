using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    Rigidbody2D rgbd;

    float xSpeed;
    float ySpeed;

    bool atGround;

    C2Mission2 mission;
    private void Start()
    {
        mission = GameObject.Find("Task Monitor").GetComponent<C2Mission2>();
        rgbd = gameObject.GetComponent<Rigidbody2D>();

        xSpeed = Random.Range(-3, 3);
        ySpeed = Random.Range(3, 5);
    }
    private void FixedUpdate()
    {
        if (!atGround)
        {
            ySpeed -= 0.1f;
            transform.position = new Vector2(transform.position.x + (xSpeed * Time.fixedDeltaTime), transform.position.y + (ySpeed * Time.fixedDeltaTime));
        }

        if (mission.isHoldingTrash)
        {
            gameObject.layer = LayerMask.NameToLayer("Task Object");
        }
        else
        {
            gameObject.layer = LayerMask.NameToLayer("Task Item");
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            atGround = true;
            rgbd.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!mission.isHoldingTrash)
            {
                Destroy(gameObject);
            }
        }
    }
    private void OnDestroy()
    {
        mission.isHoldingTrash = true;
    }
}
