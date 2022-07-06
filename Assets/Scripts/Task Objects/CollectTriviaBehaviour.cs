using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectTriviaBehaviour : MonoBehaviour
{
    TriviaText trivia;
    bool isColliding;
    
    private void Start()
    {
        trivia = GameObject.Find("Game Master").GetComponent<TriviaText>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isColliding)
        {
            trivia.numCounter++;
            Destroy(gameObject);
            isColliding = true;
        }
        if (collision.gameObject.CompareTag("Platform"))
        {
            gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
        }
    }
}
