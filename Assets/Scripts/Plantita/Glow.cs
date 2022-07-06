using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Glow : MonoBehaviour
{
    Light2D light;

    float speed, maxLight = 0.4f, minLight = 0f;
    bool lightenUp, genSpeed;
    private void Start()
    {
        light = gameObject.GetComponentInChildren<Light2D>();
    }
    private void FixedUpdate()
    {
        if(!genSpeed)
        {
            speed = Random.Range(0.15f, 0.25f);
            genSpeed = true;
        }
        else
        {
            if (!lightenUp)
            {
                light.intensity += Time.fixedDeltaTime * speed;
                if(light.intensity >= maxLight)
                {
                    lightenUp = true;
                }
            }
            if (lightenUp)
            {
                light.intensity -= Time.fixedDeltaTime * speed;
                if (light.intensity <= minLight)
                {
                    lightenUp = false;
                    genSpeed = false;
                }
            }
        }
    }
}
