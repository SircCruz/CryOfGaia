using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SplashWaveComplete : MonoBehaviour
{
    bool startShrinking = false;
    float shrinkSpeed = 2f;

    public TextMeshProUGUI waveCompleteColor;
    private void OnEnable()
    {
        transform.localScale = new Vector3(2, 2);
        waveCompleteColor.color = new Color(waveCompleteColor.color.r, waveCompleteColor.color.g, waveCompleteColor.color.b, 0);
        startShrinking = true;
    }
    private void OnDisable()
    {
        transform.localScale = new Vector3(1, 1);
    }
    private void FixedUpdate()
    {
        if (startShrinking)
        {
            transform.localScale = new Vector3(transform.localScale.x - shrinkSpeed * Time.fixedDeltaTime, transform.localScale.y - shrinkSpeed * Time.fixedDeltaTime);
            waveCompleteColor.color = new Color(waveCompleteColor.color.r, waveCompleteColor.color.g, waveCompleteColor.color.b, waveCompleteColor.color.a + shrinkSpeed * Time.fixedDeltaTime);
            if (transform.localScale.x <= 1)
            {
                transform.localScale = new Vector3(1, 1);
                startShrinking = false;
            }
        }
    }
}
