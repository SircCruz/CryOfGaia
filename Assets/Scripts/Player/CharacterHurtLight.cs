using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class CharacterHurtLight : MonoBehaviour
{
    private Light2D light;
    private float max, speed;

    // Start is called before the first frame update
    private void OnEnable()
    {
        light = gameObject.GetComponent<Light2D>();

        speed = 0.01f;

        max = 2;
        light.intensity = 0;

        StartCoroutine(LightUp());
    }
    private IEnumerator LightUp()
    {
        while(light.intensity < max)
        {
            light.intensity += speed * 32;

            yield return new WaitForFixedUpdate();
        }
        StartCoroutine(LightDown());
    }
    private IEnumerator LightDown()
    {
        while(light.intensity > 0)
        {
            light.intensity -= speed * 32;

            yield return new WaitForFixedUpdate();
        }
        gameObject.SetActive(false);
    }
}
