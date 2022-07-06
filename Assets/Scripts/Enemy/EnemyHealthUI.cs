using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthUI : MonoBehaviour
{
    private Canvas canvas;

    private Slider slider;
    public GameObject sliderObj;

    Chainsaw chainsaw;
    void Start()
    {
        canvas = gameObject.GetComponent<Canvas>();
        canvas.worldCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        slider = sliderObj.GetComponent<Slider>();
        chainsaw = GetComponentInParent<Chainsaw>();

        slider.maxValue = chainsaw.hitPoints;
        slider.value = chainsaw.hitPoints;

        float yPos = UnityEngine.Random.Range(-0.2f, 0.2f);
        transform.position = new Vector2(transform.position.x, transform.position.y + yPos);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        slider.value = chainsaw.hitPoints;
    }
    public void HPBarUpdate()
    {
        sliderObj.SetActive(true);
    }
}
