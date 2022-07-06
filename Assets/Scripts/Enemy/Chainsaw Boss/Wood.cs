using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wood : MonoBehaviour
{
    public Animator woodAnim;
    BoxCollider2D box;
    float speed = 10f;
    private void Start()
    {
        box = gameObject.GetComponent<BoxCollider2D>();

        int sound = Random.Range(1, 4);
        FindObjectOfType<AudioManager>().Play("wood" + sound);
    }
    void FixedUpdate()
    {
        transform.position = new Vector2(transform.position.x - speed * Time.fixedDeltaTime, transform.position.y);
        if(transform.position.x <= -30f)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            woodAnim.SetBool("Hit", true);
            Destroy(gameObject, 0.4f);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            box.enabled = false;
        }
    }
    private void OnDestroy()
    {
        woodAnim.SetBool("Exit", true);
    }
}
