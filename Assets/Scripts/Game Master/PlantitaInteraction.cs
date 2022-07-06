using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.Notifications.Android;
using System.Collections;

public class PlantitaInteraction : MonoBehaviour
{
    public PlantData plantData;
    public PlantsAvailableRandomizer plantRandom;

    public TextMeshProUGUI coinsTxt;
    private int coins;

    public GameObject selectPlantsCanvas;
    public GameObject PlantsCanvas;
    public GameObject timerObj;
    public GameObject upgradeStats;

    public GameObject message;
    public TextMeshProUGUI messageTxt;
    public TextMeshProUGUI title;

    public TextMeshProUGUI waterProgress;
    public TextMeshProUGUI timeLeft;
    public GameObject waterCanFull;

    public GameObject[] closePressed;

    public float timer, restoreTimer;
    public int addWater;

    DateTime currentDate;
    private TimeSpan interval;
    AndroidNotificationChannel channel;
    AndroidNotification notification, newNotification;
    int identifier;

    private bool isWaterFull;

    public string[] plantName;
    public GameObject[] plant;

    public GameObject[] addButton;
    bool[] isAcquired;
    int unlocked;

    bool isReloadingAcquiredPlants;

    bool isTimerUpdated;

    int sunflowerLevel, jasmineLevel, santanLevel, dandelionLevel, roseLevel, tulipsLevel, portulacaLevel;

    public bool allowAcquiring;
    public bool isCoinsEnough;

    public Sprite closePressedButton, closeButton;
    public Image close, closeStats;
    
    public Image openStats;
    public Sprite openStatsButton, openStatsPressed;

    public Image back;
    public Sprite backButton, backButtonPressed;
    private void OnApplicationPause(bool status)
    {
        isTimerUpdated = status;
        UpdateTimer();
    }
    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            PlayerPrefs.SetInt("Acquired Plants", unlocked);

            PlayerPrefs.SetString("Quit Time", DateTime.Now.ToBinary().ToString());
            PlayerPrefs.SetInt("Stored Water", addWater);
            PlayerPrefs.SetFloat("Timer", timer);

            PlayerPrefs.Save();

            ScheduleNotificationByWaterCount();
        }
        isTimerUpdated = !focus;
        UpdateTimer();
    }
    private void Awake() 
    {
        //PlayerPrefs.DeleteAll();
        isTimerUpdated = false;

        restoreTimer = 301f;

        UpdateTimer();

        unlocked = PlayerPrefs.GetInt("Acquired Plants", 0);

        selectPlantsCanvas.SetActive(false);
        message.SetActive(false);
        isAcquired = new bool[18];

        for(int i = 1; i < addButton.Length; i++)
        {
            addButton[i].SetActive(false);
        }

        LoadPlants();

        upgradeStats.SetActive(false);

        coins = PlayerPrefs.GetInt("Coins", 0);
        coinsTxt.text = coins.ToString();

        channel = new AndroidNotificationChannel()
        {
            Id = "channel_1",
            Name = "Plantita Channel",
            Importance = Importance.High,
            Description = "Notify if water is full"
        };
        AndroidNotificationCenter.RegisterNotificationChannel(channel);
    }
    void LoadPlants()
    {
        allowAcquiring = true;
        isCoinsEnough = true;
        isReloadingAcquiredPlants = true;
        for (int i = unlocked; i > 0; i--)
        {
            plant[i - 1].SetActive(true);
            PlantAcquired(0);
        }
        isReloadingAcquiredPlants = false;
        isCoinsEnough = false;
        allowAcquiring = false;
    }
    void FixedUpdate()
    {
        Timer();
    }
    public void Water()
    {
        addWater = PlayerPrefs.GetInt("Stored Water", 50);
    }
    void Timer()
    {
        if (addWater >= 50)
        {
            waterCanFull.SetActive(true);
            waterProgress.text = "50";
            addWater = 50;
            isWaterFull = true;
        }
        if (addWater < 50)
        {
            waterCanFull.SetActive(false);
            isWaterFull = false;
        }
        if (!isWaterFull)
        {
            timerObj.SetActive(true);
            timer -= Time.fixedDeltaTime;
            interval = TimeSpan.FromSeconds(timer);
            timeLeft.text = interval.ToString(@"mm\:ss");
            waterProgress.text = addWater.ToString() + "/50";
            if (timer <= 0f)
            {
                addWater++;
                timer = restoreTimer;
            }
        }
        if (isWaterFull)
        {
            timerObj.SetActive(false);
            timer = restoreTimer;
        }
    }
    private void UpdateTimer()
    {
        if (!isTimerUpdated)
        {
            currentDate = DateTime.Now;
            long temp = long.Parse(PlayerPrefs.GetString("Quit Time", DateTime.Now.ToBinary().ToString()));
            DateTime oldDate = DateTime.FromBinary(temp);
            TimeSpan difference = currentDate.Subtract(oldDate);

            timer = PlayerPrefs.GetFloat("Timer", restoreTimer);
            addWater = PlayerPrefs.GetInt("Stored Water", 50);

            float elapsedTime = (float)difference.TotalSeconds;
            for (; ; )
            {
                if (elapsedTime >= restoreTimer)
                {
                    elapsedTime -= restoreTimer;
                    PlayerPrefs.SetInt("Stored Water", ++addWater);
                }
                else
                {
                    break;
                }
            }
            timer -= elapsedTime;

            isTimerUpdated = true;
        }
        
    }
    public void OpenPlantsCanvas()
    {
        plantRandom.enabled = true;

        sunflowerLevel = PlayerPrefs.GetInt("Sunflower Level", 1);
        jasmineLevel = PlayerPrefs.GetInt("Jasmine Level", 1);
        santanLevel = PlayerPrefs.GetInt("Santan Level", 1);
        dandelionLevel = PlayerPrefs.GetInt("Dandelion Level", 1);
        roseLevel = PlayerPrefs.GetInt("Rose Level", 1);
        tulipsLevel = PlayerPrefs.GetInt("Tulips Level", 1);
        portulacaLevel = PlayerPrefs.GetInt("Portulaca Level", 1);

        selectPlantsCanvas.SetActive(true);
    }
    public void ClosePlantsCanvas()
    {
        selectPlantsCanvas.SetActive(false);
    }
    public void ClosePlantsCanvasEnter()
    {
        close.sprite = closePressedButton;
    }
    public void ClosePlantsCanvasUp()
    {
        close.sprite = closeButton;
    }
    public void PlantAcquired(int item)
    {
        if (!isReloadingAcquiredPlants)
        {
            if ((plantName[0].Equals("Sunflower") && item == 1) ||
                (plantName[1].Equals("Sunflower") && item == 2) ||
                (plantName[2].Equals("Sunflower") && item == 3) || 
                (plantName[3].Equals("Sunflower") && item == 4))
            {
                if (sunflowerLevel == 1)
                {
                    allowAcquiring = true;
                    if (coins >= 1000)
                    {
                        isCoinsEnough = true;
                    }
                    if (allowAcquiring && isCoinsEnough)
                    {
                        coins -= 1000;
                        coinsTxt.text = coins.ToString();
                        PlayerPrefs.SetInt("Coins", coins);
                    }
                }
                else if(sunflowerLevel == 2)
                {
                    if (PlayerPrefs.GetInt("Sunflower Upgrade" + 1) == 1)
                    {
                        allowAcquiring = true;
                    }
                    if (coins >= 2000)
                    {
                        isCoinsEnough = true;
                    }
                    if (allowAcquiring && isCoinsEnough)
                    {
                        coins -= 2000;
                        coinsTxt.text = coins.ToString();
                        PlayerPrefs.SetInt("Coins", coins);
                    }
                }
                else if(sunflowerLevel == 3)
                {
                    if (PlayerPrefs.GetInt("Sunflower Upgrade" + 2) == 1)
                    {
                        allowAcquiring = true;
                    }
                    if (coins >= 5000)
                    {
                        isCoinsEnough = true;
                    }
                    if (allowAcquiring && isCoinsEnough)
                    {
                        coins -= 5000;
                        coinsTxt.text = coins.ToString();
                        PlayerPrefs.SetInt("Coins", coins);
                    }
                }
                else if (sunflowerLevel == 4)
                {
                    if (PlayerPrefs.GetInt("Sunflower Upgrade" + 3) == 1)
                    {
                        allowAcquiring = true;
                    }
                    if (coins >= 10000)
                    {
                        isCoinsEnough = true;
                    }
                    if (allowAcquiring && isCoinsEnough)
                    {
                        coins -= 10000;
                        coinsTxt.text = coins.ToString();
                        PlayerPrefs.SetInt("Coins", coins);
                    }
                }
            }
            if ((plantName[0].Equals("Jasmine") && item == 1) ||
                (plantName[1].Equals("Jasmine") && item == 2) ||
                (plantName[2].Equals("Jasmine") && item == 3) ||
                (plantName[3].Equals("Jasmine") && item == 4))
            {
                if(jasmineLevel == 1)
                {
                    allowAcquiring = true;
                    if (coins >= 2000)
                    {
                        isCoinsEnough = true;
                    }
                    if (allowAcquiring && isCoinsEnough)
                    {
                        coins -= 2000;
                        coinsTxt.text = coins.ToString();
                        PlayerPrefs.SetInt("Coins", coins);
                    }
                }
                else if(jasmineLevel == 2)
                {
                    if(PlayerPrefs.GetInt("Jasmine Upgrade" + 1) == 1)
                    {
                        allowAcquiring = true;
                    }
                    if (coins >= 5000)
                    {
                        isCoinsEnough = true;
                    }
                    if (allowAcquiring && isCoinsEnough)
                    {
                        coins -= 5000;
                        coinsTxt.text = coins.ToString();
                        PlayerPrefs.SetInt("Coins", coins);
                    }
                }
                else if(jasmineLevel == 3)
                {
                    if (PlayerPrefs.GetInt("Jasmine Upgrade" + 2) == 1)
                    {
                        allowAcquiring = true;
                    }
                    if (coins >= 10000)
                    {
                        isCoinsEnough = true;
                    }
                    if(allowAcquiring && isCoinsEnough)
                    {
                        coins -= 10000;
                        coinsTxt.text = coins.ToString();
                        PlayerPrefs.SetInt("Coins", coins);
                    }
                }
            }
            if ((plantName[0].Equals("Santan") && item == 1) ||
                (plantName[1].Equals("Santan") && item == 2) ||
                (plantName[2].Equals("Santan") && item == 3) ||
                (plantName[3].Equals("Santan") && item == 4))
            {
                if (santanLevel == 1)
                {
                    allowAcquiring = true;
                    if(coins >= 3000)
                    {
                        isCoinsEnough = true;
                    }
                    if(allowAcquiring && isCoinsEnough)
                    {
                        coins -= 3000;
                        coinsTxt.text = coins.ToString();
                        PlayerPrefs.SetInt("Coins", coins);
                    }
                }
                else if (santanLevel == 2)
                {
                    if (PlayerPrefs.GetInt("Santan Upgrade" + 1) == 1)
                    {
                        allowAcquiring = true;
                    }
                    if (coins >= 6000)
                    {
                        isCoinsEnough = true;
                    }
                    if (allowAcquiring && isCoinsEnough)
                    {
                        coins -= 6000;
                        coinsTxt.text = coins.ToString();
                        PlayerPrefs.SetInt("Coins", coins);
                    }
                }
                else if (santanLevel == 3)
                {
                    if (PlayerPrefs.GetInt("Santan Upgrade" + 2) == 1)
                    {
                        allowAcquiring = true;
                    }
                    if (coins >= 10000)
                    {
                        isCoinsEnough = true;
                    }
                    if (allowAcquiring && isCoinsEnough)
                    {
                        coins -= 10000;
                        coinsTxt.text = coins.ToString();
                        PlayerPrefs.SetInt("Coins", coins);
                    }
                }
            }
            if ((plantName[0].Equals("Dandelion") && item == 1) ||
                (plantName[1].Equals("Dandelion") && item == 2) ||
                (plantName[2].Equals("Dandelion") && item == 3) ||
                (plantName[3].Equals("Dandelion") && item == 4))
            {
                if(dandelionLevel == 1)
                {
                    allowAcquiring = true;
                    if (coins >= 10000)
                    {
                        isCoinsEnough = true;
                    }
                    if (allowAcquiring && isCoinsEnough)
                    {
                        coins -= 10000;
                        coinsTxt.text = coins.ToString();
                        PlayerPrefs.SetInt("Coins", coins);
                    }
                }
                else if(dandelionLevel == 2)
                {
                    if(PlayerPrefs.GetInt("Dandelion Upgrade" + 1) == 1)
                    {
                        allowAcquiring = true;
                    }
                    if (coins >= 20000)
                    {
                        isCoinsEnough = true;
                    }
                    if (allowAcquiring && isCoinsEnough)
                    {
                        coins -= 20000;
                        coinsTxt.text = coins.ToString();
                        PlayerPrefs.SetInt("Coins", coins);
                    }
                }
            }
            if ((plantName[0].Equals("Rose") && item == 1) ||
                (plantName[1].Equals("Rose") && item == 2) ||
                (plantName[2].Equals("Rose") && item == 3) ||
                (plantName[3].Equals("Rose") && item == 4))
            {
                if (roseLevel == 1)
                {
                    allowAcquiring = true;
                    if (coins >= 10000)
                    {
                        isCoinsEnough = true;
                    }
                    if (allowAcquiring && isCoinsEnough)
                    {
                        coins -= 10000;
                        coinsTxt.text = coins.ToString();
                        PlayerPrefs.SetInt("Coins", coins);
                    }
                }
                else if (roseLevel == 2)
                {
                    if (PlayerPrefs.GetInt("Rose Upgrade" + 1) == 1)
                    {
                        allowAcquiring = true;
                    }
                    if (coins >= 20000)
                    {
                        isCoinsEnough = true;
                    }
                    if (allowAcquiring && isCoinsEnough)
                    {
                        coins -= 20000;
                        coinsTxt.text = coins.ToString();
                        PlayerPrefs.SetInt("Coins", coins);
                    }
                }
            }
            if ((plantName[0].Equals("Tulips") && item == 1) ||
                (plantName[1].Equals("Tulips") && item == 2) ||
                (plantName[2].Equals("Tulips") && item == 3) ||
                (plantName[3].Equals("Tulips") && item == 4))
            {
                if (tulipsLevel == 1)
                {
                    allowAcquiring = true;
                    if (coins >= 20000)
                    {
                        isCoinsEnough = true;
                    }
                    if (allowAcquiring && isCoinsEnough)
                    {
                        coins -= 20000;
                        coinsTxt.text = coins.ToString();
                        PlayerPrefs.SetInt("Coins", coins);
                    }
                }
                else if (tulipsLevel == 2)
                {
                    if (PlayerPrefs.GetInt("Tulips Upgrade" + 1) == 1)
                    {
                        allowAcquiring = true;
                    }
                    if (coins >= 40000)
                    {
                        isCoinsEnough = true;
                    }
                    if (allowAcquiring && isCoinsEnough)
                    {
                        coins -= 40000;
                        coinsTxt.text = coins.ToString();
                        PlayerPrefs.SetInt("Coins", coins);
                    }
                }
            }
            if ((plantName[0].Equals("Portulaca") && item == 1) ||
                (plantName[1].Equals("Portulaca") && item == 2) ||
                (plantName[2].Equals("Portulaca") && item == 3) ||
                (plantName[3].Equals("Portulaca") && item == 4))
            {
                if (portulacaLevel == 1)
                {
                    allowAcquiring = true;
                    if (coins >= 20000)
                    {
                        isCoinsEnough = true;
                    }
                    if (allowAcquiring && isCoinsEnough)
                    {
                        coins -= 20000;
                        coinsTxt.text = coins.ToString();
                        PlayerPrefs.SetInt("Coins", coins);
                    }
                }
                else if (portulacaLevel == 2)
                {
                    if (PlayerPrefs.GetInt("Portulaca Upgrade" + 1) == 1)
                    {
                        allowAcquiring = true;
                    }
                    if (coins >= 40000)
                    {
                        isCoinsEnough = true;
                    }
                    if (allowAcquiring && isCoinsEnough)
                    {
                        coins -= 40000;
                        coinsTxt.text = coins.ToString();
                        PlayerPrefs.SetInt("Coins", coins);
                    }
                }
            }
        }
        if (allowAcquiring && isCoinsEnough)
        {
            try
            {
                for (int i = 0; i < isAcquired.Length; i++)
                {
                    isAcquired[i] = false;
                }
                if (!isReloadingAcquiredPlants)
                {
                    if (item == 1)
                    {
                        PlayerPrefs.SetString("Plant Type" + unlocked, plantName[0]);
                    }
                    else if (item == 2)
                    {
                        PlayerPrefs.SetString("Plant Type" + unlocked, plantName[1]);
                    }
                    else if (item == 3)
                    {
                        PlayerPrefs.SetString("Plant Type" + unlocked, plantName[2]);
                    }
                    else if (item == 4)
                    {
                        PlayerPrefs.SetString("Plant Type" + unlocked, plantName[3]);
                    }
                    plantData.plants[unlocked] = PlayerPrefs.GetString("Plant Type" + unlocked);
                    plant[unlocked].SetActive(true);
                    unlocked++;
                }
                isAcquired[unlocked] = true;
                Progress();
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (IndexOutOfRangeException e)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                plant[5].SetActive(true);
                for (int i = 0; i < addButton.Length; i++)
                {
                    addButton[i].SetActive(false);
                }
            }
            if (!isReloadingAcquiredPlants)
            {
                if (item == 1)
                {
                    if (plantName[0].Equals("Sunflower"))
                    {
                        sunflowerLevel++;
                        PlayerPrefs.SetInt("Sunflower Level", sunflowerLevel);
                    }
                    else if (plantName[0].Equals("Jasmine"))
                    {
                        jasmineLevel++;
                        PlayerPrefs.SetInt("Jasmine Level", jasmineLevel);
                    }
                    else if (plantName[0].Equals("Santan"))
                    {
                        santanLevel++;
                        PlayerPrefs.SetInt("Santan Level", santanLevel);
                    }
                    else if (plantName[0].Equals("Dandelion"))
                    {
                        dandelionLevel++;
                        PlayerPrefs.SetInt("Dandelion Level", dandelionLevel);
                    }
                    else if (plantName[0].Equals("Rose"))
                    {
                        roseLevel++;
                        PlayerPrefs.SetInt("Rose Level", roseLevel);
                    }
                    else if (plantName[0].Equals("Tulips"))
                    {
                        tulipsLevel++;
                        PlayerPrefs.SetInt("Tulips Level", tulipsLevel);
                    }
                    else if (plantName[0].Equals("Portulaca"))
                    {
                        portulacaLevel++;
                        PlayerPrefs.SetInt("Portulaca Level", portulacaLevel);
                    }
                }
                else if (item == 2)
                {
                    if (plantName[1].Equals("Sunflower"))
                    {
                        sunflowerLevel++;
                        PlayerPrefs.SetInt("Sunflower Level", sunflowerLevel);
                    }
                    else if (plantName[1].Equals("Jasmine"))
                    {
                        jasmineLevel++;
                        PlayerPrefs.SetInt("Jasmine Level", jasmineLevel);
                    }
                    else if (plantName[1].Equals("Santan"))
                    {
                        santanLevel++;
                        PlayerPrefs.SetInt("Santan Level", santanLevel);
                    }
                    else if (plantName[1].Equals("Dandelion"))
                    {
                        dandelionLevel++;
                        PlayerPrefs.SetInt("Dandelion Level", dandelionLevel);
                    }
                    else if (plantName[1].Equals("Rose"))
                    {
                        roseLevel++;
                        PlayerPrefs.SetInt("Rose Level", roseLevel);
                    }
                    else if (plantName[1].Equals("Tulips"))
                    {
                        tulipsLevel++;
                        PlayerPrefs.SetInt("Tulips Level", tulipsLevel);
                    }
                    else if (plantName[1].Equals("Portulaca"))
                    {
                        portulacaLevel++;
                        PlayerPrefs.SetInt("Portulaca Level", portulacaLevel);
                    }
                }
                else if (item == 3)
                {
                    if (plantName[2].Equals("Sunflower"))
                    {
                        sunflowerLevel++;
                        PlayerPrefs.SetInt("Sunflower Level", sunflowerLevel);
                    }
                    else if (plantName[2].Equals("Jasmine"))
                    {
                        jasmineLevel++;
                        PlayerPrefs.SetInt("Jasmine Level", jasmineLevel);
                    }
                    else if (plantName[2].Equals("Santan"))
                    {
                        santanLevel++;
                        PlayerPrefs.SetInt("Santan Level", santanLevel);
                    }
                    else if (plantName[2].Equals("Dandelion"))
                    {
                        dandelionLevel++;
                        PlayerPrefs.SetInt("Dandelion Level", dandelionLevel);
                    }
                    else if (plantName[2].Equals("Rose"))
                    {
                        roseLevel++;
                        PlayerPrefs.SetInt("Rose Level", roseLevel);
                    }
                    else if (plantName[2].Equals("Tulips"))
                    {
                        tulipsLevel++;
                        PlayerPrefs.SetInt("Tulips Level", tulipsLevel);
                    }
                    else if (plantName[2].Equals("Portulaca"))
                    {
                        portulacaLevel++;
                        PlayerPrefs.SetInt("Portulaca Level", portulacaLevel);
                    }
                }
                else if (item == 4)
                {
                    if (plantName[3].Equals("Sunflower"))
                    {
                        sunflowerLevel++;
                        PlayerPrefs.SetInt("Sunflower Level", sunflowerLevel);
                    }
                    else if (plantName[3].Equals("Jasmine"))
                    {
                        jasmineLevel++;
                        PlayerPrefs.SetInt("Jasmine Level", jasmineLevel);
                    }
                    else if (plantName[3].Equals("Santan"))
                    {
                        santanLevel++;
                        PlayerPrefs.SetInt("Santan Level", santanLevel);
                    }
                    else if (plantName[3].Equals("Dandelion"))
                    {
                        dandelionLevel++;
                        PlayerPrefs.SetInt("Dandelion Level", dandelionLevel);
                    }
                    else if (plantName[3].Equals("Rose"))
                    {
                        roseLevel++;
                        PlayerPrefs.SetInt("Rose Level", roseLevel);
                    }
                    else if (plantName[3].Equals("Tulips"))
                    {
                        tulipsLevel++;
                        PlayerPrefs.SetInt("Tulips Level", tulipsLevel);
                    }
                    else if (plantName[3].Equals("Portulaca"))
                    {
                        portulacaLevel++;
                        PlayerPrefs.SetInt("Portulaca Level", portulacaLevel);
                    }
                }
                plantRandom.ItemsLock(item);
                plantData.AcquirePlant();
                allowAcquiring = false;
                isCoinsEnough = false;
            }
        }
        else
        {
            if ((plantName[0].Equals("Sunflower") && item == 1) ||
                (plantName[1].Equals("Sunflower") && item == 2) ||
                (plantName[2].Equals("Sunflower") && item == 3) ||
                (plantName[3].Equals("Sunflower") && item == 4))
            {
                message.SetActive(true);
                if (!allowAcquiring)
                {
                    title.text = "Oops..";
                    messageTxt.text = "Looks like the level " + (sunflowerLevel - 1) + " Sunflower is not fully grown yet.";
                }
                else if (!isCoinsEnough)
                {
                    title.text = "Not enough coins!";
                    messageTxt.text = "You don't have enough coins. Play missions to earn more!";
                }
            }
            if ((plantName[0].Equals("Jasmine") && item == 1) ||
                (plantName[1].Equals("Jasmine") && item == 2) ||
                (plantName[2].Equals("Jasmine") && item == 3) ||
                (plantName[3].Equals("Jasmine") && item == 4))
            {
                message.SetActive(true);
                if (!allowAcquiring)
                {
                    title.text = "Oops..";
                    messageTxt.text = "Looks like the level " + (jasmineLevel - 1) + " Jasmine is not fully grown yet.";
                }
                else if (!isCoinsEnough)
                {
                    title.text = "Not enough coins!";
                    messageTxt.text = "You don't have enough coins. Play missions to earn more!";
                }
            }
            if ((plantName[0].Equals("Santan") && item == 1) ||
                (plantName[1].Equals("Santan") && item == 2) ||
                (plantName[2].Equals("Santan") && item == 3) ||
                (plantName[3].Equals("Santan") && item == 4))
            {
                message.SetActive(true);
                if (!allowAcquiring)
                {
                    title.text = "Oops..";
                    messageTxt.text = "Looks like the level " + (santanLevel - 1) + " Santan is not fully grown yet.";
                }
                else if (!isCoinsEnough)
                {
                    title.text = "Not enough coins!";
                    messageTxt.text = "You don't have enough coins. Play missions to earn more!";
                }
            }
            if ((plantName[0].Equals("Dandelion") && item == 1) ||
                (plantName[1].Equals("Dandelion") && item == 2) ||
                (plantName[2].Equals("Dandelion") && item == 3) ||
                (plantName[3].Equals("Dandelion") && item == 4))
            {
                message.SetActive(true);
                if (!allowAcquiring)
                {
                    title.text = "Oops..";
                    messageTxt.text = "Looks like the level " + (dandelionLevel - 1) + " Dandelion is not fully grown yet.";
                }
                else if (!isCoinsEnough)
                {
                    title.text = "Not enough coins!";
                    messageTxt.text = "You don't have enough coins. Play missions to earn more!";
                }
            }
            if ((plantName[0].Equals("Rose") && item == 1) ||
                (plantName[1].Equals("Rose") && item == 2) ||
                (plantName[2].Equals("Rose") && item == 3) ||
                (plantName[3].Equals("Rose") && item == 4))
            {
                message.SetActive(true);
                if (!allowAcquiring)
                {
                    title.text = "Oops..";
                    messageTxt.text = "Looks like the level " + (roseLevel - 1) + " Rose is not fully grown yet.";
                }
                else if (!isCoinsEnough)
                {
                    title.text = "Not enough coins!";
                    messageTxt.text = "You don't have enough coins. Play missions to earn more!";
                }
            }
            if ((plantName[0].Equals("Tulips") && item == 1) ||
                (plantName[1].Equals("Tulips") && item == 2) ||
                (plantName[2].Equals("Tulips") && item == 3) ||
                (plantName[3].Equals("Tulips") && item == 4))
            {
                message.SetActive(true);
                if (!allowAcquiring)
                {
                    title.text = "Oops..";
                    messageTxt.text = "Looks like the level " + (tulipsLevel - 1) + " Tulips is not fully grown yet.";
                }
                else if (!isCoinsEnough)
                {
                    title.text = "Not enough coins!";
                    messageTxt.text = "You don't have enough coins. Play missions to earn more!";
                }
            }
            if ((plantName[0].Equals("Portulaca") && item == 1) ||
                (plantName[1].Equals("Portulaca") && item == 2) ||
                (plantName[2].Equals("Portulaca") && item == 3) ||
                (plantName[3].Equals("Portulaca") && item == 4))
            {
                message.SetActive(true);
                if (!allowAcquiring)
                {
                    title.text = "Oops..";
                    messageTxt.text = "Looks like the level " + (portulacaLevel - 1) + " Portulaca is not fully grown yet.";
                }
                else if (!isCoinsEnough)
                {
                    title.text = "Not enough coins!";
                    messageTxt.text = "You don't have enough coins. Play missions to earn more!";
                }
            }
            allowAcquiring = false;
            isCoinsEnough = false;
        }
        ClosePlantsCanvas();
    }
    private void Progress()
    {
        for (int i = 0; i < addButton.Length; i++)
        {
            addButton[i].SetActive(false);
        }
        for (int i = 0; i < addButton.Length; i++)
        {
            if (isAcquired[i] == true)
            {
                addButton[i].SetActive(true);
            }
        }
    }
    
    public void SaveBeforeExit()
    {
        PlayerPrefs.SetInt("Acquired Plants", unlocked);

        PlayerPrefs.SetString("Quit Time", DateTime.Now.ToBinary().ToString());
        PlayerPrefs.SetInt("Stored Water", addWater);
        PlayerPrefs.SetFloat("Timer", timer);

        PlayerPrefs.Save();

        ScheduleNotificationByWaterCount();

        SceneManager.LoadScene("MainMenuScene");
    }
    public void OpenUpgradeStats()
    {
        upgradeStats.SetActive(true);
    }
    public void OpenStatsCanvasEnter()
    {
        openStats.sprite = openStatsPressed;
    }
    public void OpenStatsCanvasUp()
    {
        openStats.sprite = openStatsButton;
    }
    public void CloseUpgradeStats()
    {
        upgradeStats.SetActive(false);
    }
    public void CloseUpgradeStatsEnter()
    {
        closeStats.sprite = closePressedButton;
    }
    public void CloseUpgradeStatsUp()
    {
        closeStats.sprite = closeButton;
    }
    public void CloseMessage()
    {
        message.SetActive(false);
    }
    public void SaveBeforeExitEnter()
    {
        back.sprite = backButtonPressed;
    }
    public void SaveBeforeExitUp()
    {
        back.sprite = backButton;
    }
    void ScheduleNotificationByWaterCount()
    {
        if(PlayerPrefs.GetInt("Notify1") == 1)
        {
            int totalSeconds = 9000;
            for (int i = addWater; i >= 0; i--)
            {
                totalSeconds -= 180;
            }
            if (totalSeconds >= 60)
            {
                notification = new AndroidNotification
                {
                    Title = "Cry of Gaia",
                    Text = "Watering can is full, time to water your plants!",
                    FireTime = DateTime.Now.AddSeconds(totalSeconds),
                    SmallIcon = "icon_0",
                    LargeIcon = "icon_1"
                };
                identifier = AndroidNotificationCenter.SendNotification(notification, channel.Id);
                ScheduleNotification();
            }
        }
    }
    private void ScheduleNotification()
    {
        if (AndroidNotificationCenter.CheckScheduledNotificationStatus(identifier) == NotificationStatus.Scheduled)
        {
            // Replace the currently scheduled notification with a new notification.
            ScheduleNewNotificationByWaterCount();
            AndroidNotificationCenter.UpdateScheduledNotification(identifier, newNotification, channel.Id);
        }
        else if (AndroidNotificationCenter.CheckScheduledNotificationStatus(identifier) == NotificationStatus.Delivered)
        {
            //Remove the notification from the status bar
            AndroidNotificationCenter.CancelNotification(identifier);
        }
        else if (AndroidNotificationCenter.CheckScheduledNotificationStatus(identifier) == NotificationStatus.Unknown)
        {
            ScheduleNotificationByWaterCount();
        }
    }
    void ScheduleNewNotificationByWaterCount()
    {
        int totalSeconds = 9000;
        for (int i = addWater; i >= 0; i--)
        {
            totalSeconds -= 180;
        }
        if (totalSeconds >= 60)
        {
            newNotification = new AndroidNotification
            {
                Title = "Cry of Gaia",
                Text = "Watering can is full, time to water your plants!",
                FireTime = DateTime.Now.AddSeconds(totalSeconds),
                SmallIcon = "icon_0",
                LargeIcon = "icon_1"
            };
        }
    }
}