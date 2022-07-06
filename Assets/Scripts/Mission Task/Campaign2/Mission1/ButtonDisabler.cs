using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonDisabler : MonoBehaviour
{
    [SerializeField] private GameObject buttons;

    public void HideButtons()
    {
        buttons.SetActive(false);
    }
    public void ShowButtons()
    {
        buttons.SetActive(true);
    }
}
