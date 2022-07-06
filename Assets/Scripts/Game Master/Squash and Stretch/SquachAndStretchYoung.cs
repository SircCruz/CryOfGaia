using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquachAndStretchYoung : MonoBehaviour
{
    float stretchY = 15f;
    float stretchX = 50f;
    float position = 0;


    bool allowStretching = true;

    private void FixedUpdate()
    {
        if (allowStretching)
        {
            position = transform.position.y + (0.03f * Time.fixedDeltaTime);
            stretchY += 0.9f * Time.fixedDeltaTime;
            stretchX += 0.07f * Time.fixedDeltaTime;

            float stretchLimit = Random.Range(18f, 20f);
            if (transform.localScale.y >= stretchLimit)
            {
                allowStretching = false;
            }
        }
        else
        {
            position = transform.position.y - (0.03f * Time.fixedDeltaTime);
            stretchY -= 0.9f * Time.fixedDeltaTime;
            stretchX -= 0.07f * Time.fixedDeltaTime;
            if (transform.localScale.y <= 15f)
            {
                allowStretching = true;
            }
        }
        transform.position = new Vector2(transform.position.x, position);
        transform.localScale = new Vector2(stretchX, stretchY);
    }
}
