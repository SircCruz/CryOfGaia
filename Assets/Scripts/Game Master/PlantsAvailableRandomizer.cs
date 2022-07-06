using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Unity.Notifications.Android;

public class PlantsAvailableRandomizer : MonoBehaviour
{
    public TextMeshProUGUI timerTxt;
    public PlantitaInteraction interaction;

    private DateTime midNightDate;
    private TimeSpan getCurrentTime;

    double currentTime_d;
    DateTime currentDate;
    DateTime newDay;

    AndroidNotificationChannel channel;
    AndroidNotification notification, newNotification;
    int identifier;

    private int isDayPassed;
    //items
    private int[] items;
    public Image[] plantType;
    public Sprite sunflower, jasmine, santan, dandelion, rose, tulips, portulaca, sold;
    public TextMeshProUGUI[] levelTxt;
    public Text[] description;
    public TextMeshProUGUI[] cost;
    public GameObject[] buyBtn;

    int sunflowerLevel, jasmineLevel, santanLevel, dandelionLevel, roseLevel, tulipsLevel, portulacaLevel;
    int isPurchased1, isPurchased2, isPurchased3, isPurchased4;
    private void Awake()
    {
        items = new int[7];
        if(PlayerPrefs.GetInt("First Timer") == 0)
        {
            isDayPassed = 1;
            PlayerPrefs.SetInt("First Timer", 1);
        }
        else
        {
            for (int i = 0; i < plantType.Length; i++)
            {
                items[i] = PlayerPrefs.GetInt("Items" + i);

                levelTxt[i].text = PlayerPrefs.GetString("Level" + i);

                description[i].text = PlayerPrefs.GetString("Description" + i);

                cost[i].text = PlayerPrefs.GetString("Cost" + i);
            }
        }
    }
    private void Start()
    {
        //PlayerPrefs.DeleteAll();

        long temp = long.Parse(PlayerPrefs.GetString("Midnight Time", DateTime.Now.ToBinary().ToString()));
        midNightDate = DateTime.FromBinary(temp);

        sunflowerLevel = PlayerPrefs.GetInt("Sunflower Level", 1);
        jasmineLevel = PlayerPrefs.GetInt("Jasmine Level", 1);
        santanLevel = PlayerPrefs.GetInt("Santan Level", 1);
        dandelionLevel = PlayerPrefs.GetInt("Dandelion Level", 1);
        roseLevel = PlayerPrefs.GetInt("Rose Level", 1);
        tulipsLevel = PlayerPrefs.GetInt("Tulips Level", 1);
        portulacaLevel = PlayerPrefs.GetInt("Portulaca Level", 1);

        isPurchased1 = PlayerPrefs.GetInt("Purchased1", 0);
        isPurchased2 = PlayerPrefs.GetInt("Purchased2", 0);
        isPurchased3 = PlayerPrefs.GetInt("Purchased3", 0);
        isPurchased4 = PlayerPrefs.GetInt("Purchased4", 0);

        ShowItems();
        ItemsCheck();

        channel = new AndroidNotificationChannel()
        {
            Id = "channel_1",
            Name = "Plantita Channel",
            Importance = Importance.High,
            Description = "Notify if new set of items are available"
        };
        AndroidNotificationCenter.RegisterNotificationChannel(channel);

        ScheduleNotificationByNextDay();
    }
    private void Update()
    {
        if (isDayPassed == 0)
        {
            currentDate = DateTime.Now;
            getCurrentTime = midNightDate.Subtract(currentDate);
            timerTxt.text = "New set of flower buds will be available in: " + getCurrentTime.ToString(@"hh\:mm\:ss");
            currentTime_d = getCurrentTime.TotalSeconds;
            if (currentTime_d <= 0)
            {
                isDayPassed = 1;
            }
        }
        if (isDayPassed == 1)
        {
            DateTime now = DateTime.Now.AddDays(1);
            newDay = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0);
            PlayerPrefs.SetString("Midnight Time", newDay.ToBinary().ToString());

            long temp = long.Parse(PlayerPrefs.GetString("Midnight Time"));
            midNightDate = DateTime.FromBinary(temp);

            isPurchased1 = 0;
            isPurchased2 = 0;
            isPurchased3 = 0;
            isPurchased4 = 0;
            PlayerPrefs.SetInt("Purchased1", isPurchased1);
            PlayerPrefs.SetInt("Purchased2", isPurchased2);
            PlayerPrefs.SetInt("Purchased3", isPurchased3);
            PlayerPrefs.SetInt("Purchased4", isPurchased4);

            ItemsCheck();

            Reroll();

            isDayPassed = 0;
        }

        for(int i = 0; i < plantType.Length; i++)
        {
            if (plantType[i].sprite == sold)
            {
                levelTxt[i].text = "";
                description[i].text = "";
                buyBtn[i].SetActive(false);
            }
            else
            {
                buyBtn[i].SetActive(true);
            }
        }
    }
    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            for (int i = 0; i < 4; i++)
            {
                PlayerPrefs.SetString("Level" + i, levelTxt[i].text);
                PlayerPrefs.SetString("Description" + i, description[i].text);
                PlayerPrefs.SetString("Cost" + i, cost[i].text);
            }

            PlayerPrefs.SetInt("Purchased1", isPurchased1);
            PlayerPrefs.SetInt("Purchased2", isPurchased2);
            PlayerPrefs.SetInt("Purchased3", isPurchased3);
            PlayerPrefs.SetInt("Purchased4", isPurchased4);

            PlayerPrefs.Save();
        }
    }
    private void Reroll()
    {
        System.Random random = new System.Random();
        for (int i = 0; i < items.Length; i++)
        {
            items[i] = i;
        }
        for (int i = items.Length - 1; i >= 0; i--)
        {
            int randomIndex = random.Next(0, i + 1);

            int temp = items[i];
            items[i] = items[randomIndex];
            items[randomIndex] = temp;
            PlayerPrefs.SetInt("Items" + i, items[i]);
        }
        ShowItems();
    }
    private void ShowItems()
    {
        for (int i = 0; i < items.Length - 3; i++)
        {
            if (items[i] == 0)
            {
                plantType[i].sprite = sunflower;
                interaction.plantName[i] = "Sunflower";
                if(sunflowerLevel == 1)
                {
                    levelTxt[i].text = "Lvl 1";
                    description[i].text = "Increases the health points by 10%";
                    cost[i].text = "1000";
                }
                else if (sunflowerLevel == 2)
                {
                    levelTxt[i].text = "Lvl 2";
                    description[i].text = "Increases the health points by 20%";
                    cost[i].text = "2000";
                }
                else if (sunflowerLevel == 3)
                {
                    levelTxt[i].text = "Lvl 3";
                    description[i].text = "Increases the health points by 50%";
                    cost[i].text = "5000";
                }
                else if (sunflowerLevel == 4)
                {
                    levelTxt[i].text = "Lvl 4";
                    description[i].text = "Increases the health points by 100%";
                    cost[i].text = "10000";
                }
                else if (sunflowerLevel >= 5)
                {
                    plantType[i].sprite = sold;
                }
            }
            if (items[i] == 1)
            {
                plantType[i].sprite = jasmine;
                interaction.plantName[i] = "Jasmine";
                if (jasmineLevel == 1)
                {
                    levelTxt[i].text = "Lvl 1";
                    description[i].text = "Increases the ranged attack damage by 10%";
                    cost[i].text = "2000";
                }
                else if (jasmineLevel == 2)
                {
                    levelTxt[i].text = "Lvl 2";
                    description[i].text = "Increases the ranged attack damage by 50%";
                    cost[i].text = "5000";
                }
                else if (jasmineLevel == 3)
                {
                    levelTxt[i].text = "Lvl 3";
                    description[i].text = "Increases the ranged attack damage by 100%";
                    cost[i].text = "10000";
                }
                else if (jasmineLevel >= 4)
                {
                    plantType[i].sprite = sold;
                }
            }
            if (items[i] == 2)
            {
                plantType[i].sprite = santan;
                interaction.plantName[i] = "Santan";
                if (santanLevel == 1)
                {
                    levelTxt[i].text = "Lvl 1";
                    description[i].text = "Increases the melee attack damage by 10%";
                    cost[i].text = "3000";
                }
                else if(santanLevel == 2)
                {
                    levelTxt[i].text = "Lvl 2";
                    description[i].text = "Increases the melee attack damage by 50%";
                    cost[i].text = "6000";
                }
                else if(santanLevel == 3)
                {
                    levelTxt[i].text = "Lvl 3";
                    description[i].text = "Increases the melee attack damage by 100%";
                    cost[i].text = "10000";
                }
                else if (santanLevel >= 4)
                {
                    plantType[i].sprite = sold;
                }
            }
            if (items[i] == 3)
            {
                plantType[i].sprite = dandelion;
                interaction.plantName[i] = "Dandelion";
                if (dandelionLevel == 1)
                {
                    levelTxt[i].text = "Lvl 1";
                    description[i].text = "Increases the Giant Fireball damage by 50%";
                    cost[i].text = "10000";
                }
                else if (dandelionLevel == 2)
                {
                    levelTxt[i].text = "Lvl 2";
                    description[i].text = "Increases the Giant Fireball damage by 100%";
                    cost[i].text = "20000";
                }
                else if (dandelionLevel >= 3)
                {
                    plantType[i].sprite = sold;
                }
            }
            if (items[i] == 4)
            {
                plantType[i].sprite = rose;
                interaction.plantName[i] = "Rose";
                if (roseLevel == 1)
                {
                    levelTxt[i].text = "Lvl 1";
                    description[i].text = "Reduces the Giant Fireball cooldown to 15 seconds.";
                    cost[i].text = "10000";
                }
                else if(roseLevel == 2)
                {
                    levelTxt[i].text = "Lvl 2";
                    description[i].text = "Reduces the Giant Fireball cooldown to 5 seconds.";
                    cost[i].text = "20000";
                }
                else if (roseLevel >= 3)
                {
                    plantType[i].sprite = sold;
                }
            }
            if (items[i] == 5)
            {
                plantType[i].sprite = tulips;
                interaction.plantName[i] = "Tulips";
                if (tulipsLevel == 1)
                {
                    levelTxt[i].text = "Lvl 1";
                    description[i].text = "Increases the Angel's Trumpet damage by 50%.";
                    cost[i].text = "20000";
                }
                else if(tulipsLevel == 2)
                {
                    levelTxt[i].text = "Lvl 2";
                    description[i].text = "Increases the Angel's Trumpet damage by 100%.";
                    cost[i].text = "40000";
                }
                else if (tulipsLevel >= 3)
                {
                    plantType[i].sprite = sold;
                }
            }
            if (items[i] == 6)
            {
                plantType[i].sprite = portulaca;
                interaction.plantName[i] = "Portulaca";
                if (portulacaLevel == 1)
                {
                    levelTxt[i].text = "Lvl 1";
                    description[i].text = "Reduces the Angel's Trumpet cooldown to 30 seconds.";
                    cost[i].text = "20000";
                }
                else if(portulacaLevel == 2)
                {
                    levelTxt[i].text = "Lvl 2";
                    description[i].text = "Reduces the Angel's Trumpet cooldown to 15 seconds.";
                    cost[i].text = "40000";
                }
                else if (portulacaLevel >= 3)
                {
                    plantType[i].sprite = sold;
                }
            }
        }
    }
    public void SaveBeforeExit()
    {
        for (int i = 0; i < plantType.Length; i++)
        {
            PlayerPrefs.SetString("Level" + i, levelTxt[i].text);
            PlayerPrefs.SetString("Description" + i, description[i].text);
            PlayerPrefs.SetString("Cost" + i, cost[i].text);

            PlayerPrefs.SetInt("Purchased1", isPurchased1);
            PlayerPrefs.SetInt("Purchased2", isPurchased2);
            PlayerPrefs.SetInt("Purchased3", isPurchased3);
            PlayerPrefs.SetInt("Purchased4", isPurchased4);
        }
    }
    public void ItemsLock(int itemNum)
    {
        if (interaction.allowAcquiring && interaction.isCoinsEnough)
        {
            if (itemNum == 1)
            {
                isPurchased1 = 1;
            }
            else if (itemNum == 2)
            {
                isPurchased2 = 1;
            }
            else if (itemNum == 3)
            {
                isPurchased3 = 1;
            }
            else if (itemNum == 4)
            {
                isPurchased4 = 1;
            }
            ItemsCheck();
        }
    }
    private void ItemsCheck()
    {
        if (isPurchased1 == 1)
        {
            plantType[0].sprite = sold;
        }
        if (isPurchased2 == 1)
        {
            plantType[1].sprite = sold;
        }
        if (isPurchased3 == 1)
        {
            plantType[2].sprite = sold;
        }
        if (isPurchased4 == 1)
        {
            plantType[3].sprite = sold;
        }
    }
    void ScheduleNotificationByNextDay()
    {
        if(PlayerPrefs.GetInt("Notify2") == 1)
        {
            AndroidNotificationCenter.CancelNotification(identifier);

            DateTime now = DateTime.Now.AddDays(1);
            newDay = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0);
            PlayerPrefs.SetString("Midnight Time", newDay.ToBinary().ToString());

            notification = new AndroidNotification();
            notification.Title = "Cry of Gaia";
            notification.Text = "New set of items are now available!";
            notification.FireTime = newDay;
            notification.SmallIcon = "icon_0";
            notification.LargeIcon = "icon_1";

            identifier = AndroidNotificationCenter.SendNotification(notification, channel.Id);
            ScheduleNotification();
        }
    }
    void ScheduleNewNotificationByNextDay()
    {
        AndroidNotificationCenter.CancelNotification(identifier);

        DateTime now = DateTime.Now.AddDays(1);
        newDay = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0);
        PlayerPrefs.SetString("Midnight Time", newDay.ToBinary().ToString());

        newNotification = new AndroidNotification();
        newNotification.Title = "Cry of Gaia";
        newNotification.Text = "New set of items are now available!";
        newNotification.FireTime = newDay;
        newNotification.SmallIcon = "icon_0";
        newNotification.LargeIcon = "icon_1";
    }
    void ScheduleNotification()
    {
        if (AndroidNotificationCenter.CheckScheduledNotificationStatus(identifier) == NotificationStatus.Scheduled)
        {
            // Replace the currently scheduled notification with a new notification.
            ScheduleNewNotificationByNextDay();
            AndroidNotificationCenter.UpdateScheduledNotification(identifier, newNotification, channel.Id);
        }
        else if (AndroidNotificationCenter.CheckScheduledNotificationStatus(identifier) == NotificationStatus.Delivered)
        {
            //Remove the notification from the status bar
            AndroidNotificationCenter.CancelNotification(identifier);
        }
        else if (AndroidNotificationCenter.CheckScheduledNotificationStatus(identifier) == NotificationStatus.Unknown)
        {
            ScheduleNotificationByNextDay();
        }
    }
}
