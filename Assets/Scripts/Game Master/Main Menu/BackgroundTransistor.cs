using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundTransistor : MonoBehaviour
{
    public RawImage cityBG;
    float transparency;
    float transitionSpeed = 0.1f;

    private void Start()
    {
        StartCoroutine(CityBG());
    }
    private void FixedUpdate()
    {
        
    }
    IEnumerator CityBG()
    {
        yield return new WaitForSeconds(15);
        while (cityBG.color.a < 1)
        {
            yield return new WaitForFixedUpdate();
            transparency += 0.05f;
            cityBG.color = new Color(cityBG.color.r, cityBG.color.g, cityBG.color.b, transparency);
        }
        StartCoroutine(ForestBG());
    }
    IEnumerator ForestBG()
    {
        yield return new WaitForSeconds(15);
        while (cityBG.color.a > 0)
        {
            yield return new WaitForFixedUpdate();
            transparency -= 0.05f;
            cityBG.color = new Color(cityBG.color.r, cityBG.color.g, cityBG.color.b, transparency);
        }
        StartCoroutine(CityBG());
    }
}
