using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquashAndStretch : MonoBehaviour
{
    float stretchY = 30f;
    float stretchX = 100f;
    float position = 0;

    
    bool allowStretching = true;

    private void FixedUpdate()
    {
        if (allowStretching)
        {
            position = transform.position.y + (0.045f * Time.fixedDeltaTime);
            stretchY += 1.9f * Time.fixedDeltaTime;
            stretchX += 0.7f * Time.fixedDeltaTime;

            float stretchLimit = Random.Range(34f, 37f);
            if (transform.localScale.y >= stretchLimit)
            {
                allowStretching = false;
            }
        }
        else
        {
            position = transform.position.y - (0.045f * Time.fixedDeltaTime);
            stretchY -= 1.9f * Time.fixedDeltaTime;
            stretchX -= 0.7f * Time.fixedDeltaTime;
            if (transform.localScale.y <= 30f)
            {
                allowStretching = true;
            }
        }
        transform.position = new Vector2(transform.position.x, position); 
        transform.localScale = new Vector2(stretchX, stretchY);
    }
}
