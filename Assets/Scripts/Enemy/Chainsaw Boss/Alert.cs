using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Alert : MonoBehaviour
{
    Light2D alert;
    float intensity = 1f;
    float limit = 2f;
    bool lightUp = true;

    private void Start()
    {
        alert = gameObject.GetComponentInChildren<Light2D>();
    }
    void FixedUpdate()
    {
        if (alert != null)
        {
            if (lightUp)
            {
                intensity += Time.fixedDeltaTime * 2.8f;
                alert.intensity = intensity;
                if (intensity >= limit)
                {
                    lightUp = false;
                }
            }
            if (!lightUp)
            {
                intensity += Time.fixedDeltaTime * -2.8f;
                alert.intensity = intensity;
                if (intensity <= 0.5f)
                {
                    lightUp = true;
                }
            }
        }
    }
}
