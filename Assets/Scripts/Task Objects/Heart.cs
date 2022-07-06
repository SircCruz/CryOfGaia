using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    Player playerHP;
    Rigidbody2D rgbd;
    private void Start()
    {
        playerHP = GameObject.Find("Character").GetComponent<Player>();
        rgbd = gameObject.GetComponent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            playerHP.HUD.value += Random.Range(3f, 5f);
            playerHP.fill.color = playerHP.HUDgradient.Evaluate(playerHP.HUD.normalizedValue);
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Platform"))
        {
            rgbd.isKinematic = true;
            rgbd.constraints = RigidbodyConstraints2D.FreezePositionY;
        }
    }
}
