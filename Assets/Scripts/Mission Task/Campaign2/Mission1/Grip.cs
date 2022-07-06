using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Grip : MonoBehaviour
{
    [SerializeField] private GameObject gripMessage;
    public Slider indicator;
    public Transform gripMessageTransform;

    [HideInInspector] public bool isActive;

    private bool isHoldingGrip;

    private void Start()
    {
        indicator.maxValue = 10;
        indicator.value = 10;
    }
    private void FixedUpdate()
    {
        if(indicator.value == indicator.maxValue)
        {
            isActive = false;
        }
        if(indicator.value == 0)
        {
            isActive = true;
        }

        if (isHoldingGrip)
        {
            indicator.value += 0.02f;
            gripMessageTransform.localScale = new Vector2(1.05f, 1.05f);
        }
        else
        {
            gripMessageTransform.localScale = new Vector2(1f, 1f);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gripMessage.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gripMessage.SetActive(false);
        }
    }
    public void RotateGrip()
    {
        isHoldingGrip = true;
    }
    public void RotateGripExit()
    {
        isHoldingGrip = false;
    }
}
