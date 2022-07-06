using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirePit : MonoBehaviour
{
    [SerializeField] private Slider indicator;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("FireCoal"))
        {
            indicator.value -= 0.01f;
        }
    }
}
