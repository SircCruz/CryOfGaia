using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Achievements : MonoBehaviour
{
    public GameObject descriptionBox;

    public TextMeshProUGUI achievementTitle;
    public TextMeshProUGUI description;
    public TextMeshProUGUI progressText;

    public Slider progress;

    int campaign1Complete, campaign1Max;

    int killCounter, killCounterMax1, killCounterMax2;
    int chainsawBossKillCount, chainsawBossMax;

    int treasurer;
    int treasurer1Max, treasurer2Max;

    float sunFlowerProgress, sunFlowerMax;
    float jasmineProgress, jasminMax;
    float santanProgress, santanMax;
    float dandelionProgress, dandelionMax;
    float roseProgress, roseMax;
    float tulipsProgess, tulipsMax;
    float portulacaProgress, portulacaMax;

    public Transform[] badge;
    public Image[] badgeImg;
    public Sprite spriteUnlocked;

    bool[] isActive;
    private void Start()
    {
        descriptionBox.SetActive(false);
        isActive = new bool[15];

        treasurer = PlayerPrefs.GetInt("Collected Coins", 0);
        treasurer1Max = 10000;
        treasurer2Max = 100000;
        if(treasurer >= treasurer1Max)
        {
            badgeImg[2].sprite = spriteUnlocked;
        }
        if(treasurer >= treasurer2Max)
        {
            badgeImg[3].sprite = spriteUnlocked;
        }

        killCounter = PlayerPrefs.GetInt("Enemies Killed", 0);
        killCounterMax1 = 1000;
        if(killCounter >= killCounterMax1)
        {
            badgeImg[0].sprite = spriteUnlocked;
        }
        killCounterMax2 = 50000;
        if(killCounter >= killCounterMax2)
        {
            badgeImg[1].sprite = spriteUnlocked;
        }

        chainsawBossKillCount = PlayerPrefs.GetInt("Chainsaw Boss Kill Count", 0);
        chainsawBossMax = 100;
        if(chainsawBossKillCount >= chainsawBossMax)
        {
            badgeImg[4].sprite = spriteUnlocked;
        }

        campaign1Complete = PlayerPrefs.GetInt("Campaign 1 Complete", 0);
        campaign1Max = 1;
        if (campaign1Complete == campaign1Max)
        {
            badgeImg[6].sprite = spriteUnlocked;
        }

        sunFlowerProgress = PlayerPrefs.GetFloat("Sunflower Gained" + 4);
        sunFlowerMax = 5000;
        if (sunFlowerProgress == sunFlowerMax)
        {
            badgeImg[8].sprite = spriteUnlocked;
        }
        jasmineProgress = PlayerPrefs.GetFloat("Jasmine Gained" + 3);
        jasminMax = 5000;
        if(jasmineProgress == jasminMax)
        {
            badgeImg[9].sprite = spriteUnlocked;
        }
        santanProgress = PlayerPrefs.GetFloat("Santan Gained" + 3);
        santanMax = 6000;
        if(santanProgress == santanMax)
        {
            badgeImg[10].sprite = spriteUnlocked;
        }
        dandelionProgress = PlayerPrefs.GetFloat("Dandelion Gained" + 2);
        dandelionMax = 6000;
        if(dandelionProgress == dandelionMax)
        {
            badgeImg[11].sprite = spriteUnlocked;
        }
        roseProgress = PlayerPrefs.GetFloat("Rose Gained" + 2);
        roseMax = 6000;
        if(roseProgress == roseMax)
        {
            badgeImg[12].sprite = spriteUnlocked;
        }
        tulipsProgess = PlayerPrefs.GetFloat("Tulips Gained" + 2);
        tulipsMax = 7000;
        if(tulipsProgess == tulipsMax)
        {
            badgeImg[13].sprite = spriteUnlocked;
        }
        portulacaProgress = PlayerPrefs.GetFloat("Portulaca Gained" + 2);
        portulacaMax = 7000;
        if(portulacaProgress == portulacaMax)
        {
            badgeImg[14].sprite = spriteUnlocked;
        }
    }
    public void ShowDescription(int index)
    {
        descriptionBox.SetActive(true);
        progress.maxValue = 0;
        progress.value = 0;
        if (!isActive[index])
        {
            for (int i = 0; i < badge.Length; i++)
            {
                badge[i].localScale = new Vector2(1, 1);
                isActive[i] = false;
            }
            if (index == 0)
            {
                achievementTitle.text = "Savior";
                description.text = "Defeated 1000 enemies.";
                badge[0].localScale = new Vector2(1.2f, 1.2f);
                progress.maxValue = killCounterMax1;
                progress.value = killCounter;
            }
            else if (index == 1)
            {
                achievementTitle.text = "Guardian";
                description.text = "Defeated 50000 enemies.";
                badge[1].localScale = new Vector2(1.2f, 1.2f);
                progress.maxValue = killCounterMax2;
                progress.value = killCounter;
            }
            else if (index == 2)
            {
                achievementTitle.text = "Gatherer";
                description.text = "Collected 10000 coins.";
                badge[2].localScale = new Vector2(1.2f, 1.2f);
                progress.maxValue = treasurer1Max;
                progress.value = treasurer;
            }
            else if (index == 3)
            {
                achievementTitle.text = "Grand Collector";
                description.text = "Collected 100000 coins.";
                badge[3].localScale = new Vector2(1.2f, 1.2f);
                progress.maxValue = treasurer2Max;
                progress.value = treasurer;
            }
            else if (index == 4)
            {
                achievementTitle.text = "Rusty Blades";
                description.text = "Defeated the Chainsaw Boss 100 times.";
                badge[4].localScale = new Vector2(1.2f, 1.2f);
                progress.maxValue = chainsawBossMax;
                progress.value = chainsawBossKillCount;
            }
            else if (index == 5)
            {
                achievementTitle.text = "Production Downtime";
                description.text = "Defeated the Factory Boss 100 times.";
                badge[5].localScale = new Vector2(1.2f, 1.2f);
            }
            else if (index == 6)
            {
                achievementTitle.text = "New Beginnings";
                description.text = "Completed the \"Ruined Forest\" campaign.";
                badge[6].localScale = new Vector2(1.2f, 1.2f);
                progress.maxValue = campaign1Max;
                progress.value = campaign1Complete;
            }
            else if (index == 7)
            {
                achievementTitle.text = "Promise of the Future";
                description.text = "Completed the \"Polluted City\" campaign.";
                badge[7].localScale = new Vector2(1.2f, 1.2f);
            }
            else if (index == 8)
            {
                achievementTitle.text = "Nature's Sunshine";
                description.text = "Grow Sunflower to level 4.";
                badge[8].localScale = new Vector2(1.2f, 1.2f);
                progress.maxValue = sunFlowerMax;
                progress.value = sunFlowerProgress;

            }
            else if (index == 9)
            {
                achievementTitle.text = "Sweet Fragrance";
                description.text = "Grow Jasmine to level 3.";
                badge[9].localScale = new Vector2(1.2f, 1.2f);
                progress.maxValue = jasminMax;
                progress.value = jasmineProgress;
            }
            else if (index == 10)
            {
                achievementTitle.text = "Crimson Bloom";
                description.text = "Grow Santan to level 3.";
                badge[10].localScale = new Vector2(1.2f, 1.2f);
                progress.maxValue = santanMax;
                progress.value = santanProgress;
            }
            else if (index == 11)
            {
                achievementTitle.text = "Parachuting Wish";
                description.text = "Grow Dandelion to level 2.";
                badge[11].localScale = new Vector2(1.2f, 1.2f);
                progress.maxValue = dandelionMax;
                progress.value = dandelionProgress;
            }
            else if (index == 12)
            {
                achievementTitle.text = "Thorned Beauty";
                description.text = "Grow Rose to level 2.";
                badge[12].localScale = new Vector2(1.2f, 1.2f);
                progress.maxValue = roseMax;
                progress.value = roseProgress;
            }
            else if (index == 13)
            {
                achievementTitle.text = "Delicate and Bold";
                description.text = "Grow Tulips to level 2";
                badge[13].localScale = new Vector2(1.2f, 1.2f);
                progress.maxValue = tulipsMax;
                progress.value = tulipsProgess;
            }
            else if (index == 14)
            {
                achievementTitle.text = "Vibrant Blossoms";
                description.text = "Grow Portulaca to level 2.";
                badge[14].localScale = new Vector2(1.2f, 1.2f);
                progress.maxValue = portulacaMax;
                progress.value = portulacaProgress;
            }
            progressText.text = progress.value.ToString() + "/" + progress.maxValue.ToString();
            isActive[index] = true;
        }
        else
        {
            for(int i = 0; i < badge.Length; i++)
            {
                badge[i].localScale = new Vector2(1, 1);
            }
            isActive[index] = false;
            descriptionBox.SetActive(false);
        }
    }
}
