using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySliderUI : MonoBehaviour
{
    public Image background;
    public Image fill;

    private float transparency;

    private void OnEnable()
    {
        background.color = new Color(background.color.r, background.color.g, background.color.b, 1);
        fill.color = new Color(fill.color.r, fill.color.g, fill.color.b, 1);

        transparency = 0f;
        StartCoroutine(Fade());
    }
    private IEnumerator Fade()
    {
        yield return new WaitForSeconds(2);
        while (background.color.a > 0)
        {
            transparency += 0.01f;

            background.color = new Color(background.color.r, background.color.g, background.color.b, background.color.a - transparency);
            fill.color = new Color(fill.color.r, fill.color.g, fill.color.b, fill.color.a - transparency);
            yield return new WaitForSeconds(0.01f);
        }
        gameObject.SetActive(false);
    }
}
