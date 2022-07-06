using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.SceneManagement;

public class Logo : MonoBehaviour
{
    public TextMeshProUGUI jmcee;
    public TextMeshProUGUI underline;
    public TextMeshProUGUI inc;
    public Light2D directLight;

    float dilateTransDur = 1f;
    bool stopDilate = false;

    int lineCounter = 1, lineLimit = 6;
    float linePerTime = 0.01f;
    float restoreLineTime;

    float displayTime = 1f;
    bool isLighting;

    float transparency = 255;
    byte trans;
    bool isDoneTrans = false;
    private void Start()
    {
        restoreLineTime = linePerTime;
    }
    void FixedUpdate()
    {
        if (!stopDilate)
        {
            jmcee.fontMaterial.SetFloat(ShaderUtilities.ID_OutlineSoftness, dilateTransDur);
            dilateTransDur -= 1.2f * Time.fixedDeltaTime;
            if (dilateTransDur <= 0)
            {
                stopDilate = true;
            }
        }
        if (stopDilate)
        {
            if (lineCounter <= lineLimit)
            {
                linePerTime -= Time.fixedDeltaTime;
                if (linePerTime <= 0)
                {
                    underline.text += "_";
                    linePerTime = restoreLineTime;
                    lineCounter += 1;
                }
            }
        }
        if(lineCounter >= 6)
        {
            inc.text = "inc.";
            if (!isLighting)
            {
                directLight.intensity += 0.025f;
                if (directLight.intensity >= 0.25f || isLighting)
                {
                    isLighting = true;
                }
            }
            if (isLighting)
            {
                directLight.intensity -= 0.015f;
            }
            displayTime -= Time.fixedDeltaTime;
            if (displayTime <= 0)
            {
                try
                {
                    transparency -= 400 * Time.fixedDeltaTime;
                    trans = Convert.ToByte(transparency);
                    jmcee.color = new Color32(255, 255, 255, trans);
                    underline.color = new Color32(255, 255, 255, trans);
                    inc.color = new Color32(255, 255, 255, trans);
                }
                catch(OverflowException e)
                {
                    jmcee.color = new Color32(255, 255, 255, 0);
                    underline.color = new Color32(255, 255, 255, 0);
                    inc.color = new Color32(255, 255, 255, 0);
                    SceneManager.LoadScene("MainMenuScene");
                }
            }
        }
    }
}
