using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundHelper : MonoBehaviour
{
    public float speed = 1000;
    float pos = 0;
    private RawImage image;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<RawImage>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        pos += speed * Time.fixedDeltaTime;

        if (pos > 0.5F)

            pos = 0;

        image.uvRect = new Rect(pos, 0, 1, 1);
    }
    
}
