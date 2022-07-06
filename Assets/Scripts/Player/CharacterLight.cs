using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class CharacterLight : MonoBehaviour
{
    Light2D lanternLight;
    public float maxLimit, minLimit;
    bool lightUp;

    private void Start()
    {
        lanternLight = gameObject.GetComponent<Light2D>();
    }
    void FixedUpdate()
    {
        if (!lightUp)
        {
            lanternLight.intensity += Time.fixedDeltaTime;
            if (lanternLight.intensity >= maxLimit)
            {
                lightUp = true;
            }
        }
        if (lightUp)
        {
            lanternLight.intensity -= Time.fixedDeltaTime;
            if(lanternLight.intensity <= minLimit)
            {
                lightUp = false;
            }
        }
    }
}
