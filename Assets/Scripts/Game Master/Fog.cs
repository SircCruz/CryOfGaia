using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Fog : MonoBehaviour
{
    private SpriteRenderer fog;
    private bool isTransparent;

    private byte color = 67;
    private byte speed = 1;
    private void Start()
    {
        fog = gameObject.GetComponent<SpriteRenderer>();
    }
    private void FixedUpdate()
    {
        if (!isTransparent)
        {
            color -= speed;
            if(color < 50)
            {
                isTransparent = true;
            }
        }
        else
        {
            color += speed;
            if (color > 80)
            {
                isTransparent = false;
            }
        }
        fog.color = new Color32(138, 101, 171, color);
    }
}
