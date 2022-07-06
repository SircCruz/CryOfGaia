using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class AchievementCounter : MonoBehaviour
{
    Player player;
    public CinemachineVirtualCamera vcam;
    public CinemachineVirtualCamera vcam2;
    public Transform chainsawBoss;
    public BadgeShow badge;
    public GameObject taskUI;
    public int killCount;
    int coins;
    int collectedcoins;
    int campaign1Complete;

    public bool startCounting = false;
    float duration = 1.5f;
    public GameObject missionComplete, buttons; 
    private void Start()
    {
        coins = PlayerPrefs.GetInt("Coins", 0);
        collectedcoins = PlayerPrefs.GetInt("Collected Coins", 0);
        killCount = PlayerPrefs.GetInt("Chainsaw Boss Kill Count", 0);

        player = GameObject.Find("Character").GetComponent<Player>();
        missionComplete.SetActive(false);
    }
    private void FixedUpdate()
    {
        if (badge != null)
        {
            if (campaign1Complete == 1)
            {
                badge.ShowBadge("Campaign 1");
            }
            if (killCount >= 100)
            {
                badge.ShowBadge("Annihilator I");
            }
            if (collectedcoins >= 100000)
            {
                badge.ShowBadge("Treasurer II");
            }
            else if (collectedcoins >= 10000)
            {
                badge.ShowBadge("Treasurer I");
            }
        }
        if (startCounting)
        {
            duration -= Time.fixedDeltaTime;
            if(duration <= 0)
            {
                vcam2.enabled = false;
                missionComplete.SetActive(true);
            }
        }
    }
    public void AddCount()
    {
        coins += 1000;
        collectedcoins += 1000;
        campaign1Complete = 1;
        PlayerPrefs.SetInt("Campaign 1 Complete", campaign1Complete);
        PlayerPrefs.SetInt("Coins", coins);
        PlayerPrefs.SetInt("Collected Coins", collectedcoins);
        PlayerPrefs.SetInt("Chainsaw Boss Kill Count", ++killCount);
    }
    public void lookAtChainsawBoss()
    {
        taskUI.SetActive(false);
        vcam.enabled = false;
        vcam2.enabled = true;
        buttons.SetActive(false);
        player.MoveStop();
        vcam2.m_LookAt = chainsawBoss.transform;
        vcam2.m_Follow = chainsawBoss.transform;
    }
}
