using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILoadingPlayNow : MonoBehaviour
{
    public GameObject buttons;
    public GameObject loadingScreen;

    private bool startNow;
    void Start()
    {
        buttons.SetActive(false);
        loadingScreen.SetActive(true);
        startNow = false;
    }
    public void showScene()
    {
        buttons.SetActive(true);
        loadingScreen.SetActive(false);
        startNow = true;
    }
    private void Update()
    {
        if (startNow)
        {
            Time.timeScale = 1;
        }
        if (!startNow)
        {
            Time.timeScale = 0;
        }
    }
}
