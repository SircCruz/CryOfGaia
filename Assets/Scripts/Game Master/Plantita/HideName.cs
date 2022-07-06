using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HideName : MonoBehaviour
{
    public GameObject[] buttons;
    public TextMeshProUGUI hideTxt;

    bool open;
    public void PhotoMode()
    {
        if (!open)
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].SetActive(false);
            }
            open = true;
            hideTxt.text = "Show";
        }
        else
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].SetActive(true);
            }
            open = false;
            hideTxt.text = "Hide";
        }
    }
}
