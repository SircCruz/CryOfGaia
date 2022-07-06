using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Confetti : MonoBehaviour
{
    //number of confetti will generate
    public GameObject blue, red, yellow, orange, green;
    void Start()
    {
        int confettiAmount = Random.Range(160, 170);

        for(int i = 1; i <= confettiAmount; i++)
        {
            int color = Random.Range(1, 6);
            float posX = Random.Range(-0.2f, 0.2f);
            float posY = Random.Range(-0.5f, 0.5f);
            if(color == 1)
            {
                Instantiate(blue, new Vector2(transform.position.x + posX, transform.position.y + posY), transform.rotation);
            }
            else if(color == 2)
            {
                Instantiate(red, new Vector2(transform.position.x + posX, transform.position.y + posY), transform.rotation);
            }
            else if (color == 3)
            {
                Instantiate(yellow, new Vector2(transform.position.x + posX, transform.position.y + posY), transform.rotation);
            }
            else if (color == 4)
            {
                Instantiate(orange, new Vector2(transform.position.x + posX, transform.position.y + posY), transform.rotation);
            }
            else if (color == 5)
            {
                Instantiate(green, new Vector2(transform.position.x + posX, transform.position.y + posY), transform.rotation);
            }
        }
        Destroy(gameObject);
    }
}
