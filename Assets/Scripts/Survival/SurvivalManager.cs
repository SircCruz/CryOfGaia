using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class SurvivalManager : MonoBehaviour
{
    public GameObject splashText;

    public GameObject[] survivalEnemyType;

    public Player player;

    public GenerateEnemy genEnemy;

    public GameObject killremainingenemies;

    public Slider timer;
    public TextMeshProUGUI timerTxt;
    public Text waveTxt, scoreText, bestText;

    float currentWave;
    float bestWave;
    public float timePerWave;

    bool isDelayed;
    bool isSpawnModified = false;

    int counter;

    public TextMeshProUGUI coinTxt, rewardTxt, earnedTxt;
    float pendingreward = 0;
    float basereward = 100;
    int totalcoins;
    void Start()
    {
        currentWave = PlayerPrefs.GetFloat("Current Wave", 1);

        timePerWave = 181;
        timer.maxValue = timePerWave;
        timer.value = timePerWave;

        waveTxt.text = "Wave " + currentWave;

        isDelayed = false;

        SpawnModifier();

        counter = PlayerPrefs.GetInt("Survival Wave Counter", 1);

        pendingreward = PlayerPrefs.GetFloat("Pending Coins", 0);
        rewardTxt.text = PlayerPrefs.GetFloat("Pending Coins").ToString();
        earnedTxt.text = "+" + rewardTxt.text;

        totalcoins = PlayerPrefs.GetInt("Coins");
    }

    private void FixedUpdate()
    {
        coinTxt.text = "+" + (basereward + (basereward * (currentWave * 0.1f))).ToString();
       
        if(player.HUD.value > 0) UpdateTimer();

        bestWave = PlayerPrefs.GetFloat("Best Wave", 1);

        if(player.HUD.value <= 0)
        {
            scoreText.text = "Score: " + currentWave;
            bestText.text = "Best: " + bestWave;

            totalcoins = Convert.ToInt32(PlayerPrefs.GetInt("Coins") + PlayerPrefs.GetFloat("Pending Coins"));

            PlayerPrefs.SetInt("Coins", totalcoins);
            PlayerPrefs.SetFloat("Current Wave", 1);
            PlayerPrefs.SetFloat("Pending Coins", 0);
        }
    }
    void UpdateTimer()
    {
        if (!isDelayed) timer.value -= Time.fixedDeltaTime;

        TimeSpan interval = TimeSpan.FromSeconds(timer.value);
        timerTxt.text = interval.ToString(@"mm\:ss");

        if(timer.value <= 0 && !isDelayed)
        {
            Reward();
            rewardTxt.text = PlayerPrefs.GetFloat("Pending Coins").ToString();
            earnedTxt.text = "+" + rewardTxt.text;

            if (!isDelayed) PlayerPrefs.SetFloat("Current Wave", ++currentWave);

            genEnemy.enabled = false;
            killremainingenemies.SetActive(true);

            BestWaveUpdate();
            StartCoroutine(Delay());

            isSpawnModified = false;
            SpawnModifier();

            splashText.SetActive(true);

            isDelayed = true;
        }
    }
    void Reward()
    {
        pendingreward = PlayerPrefs.GetFloat("Pending Coins") + (basereward + (basereward * (currentWave * 0.1f)));
        PlayerPrefs.SetFloat("Pending Coins", pendingreward);
    }
    void BestWaveUpdate()
    {
        if(currentWave > bestWave)
        {
            PlayerPrefs.SetFloat("Best Wave", currentWave);
        }
    }
    private void NewWave()
    {
        waveTxt.text = "Wave " + currentWave;
        timer.value = timePerWave;

        player.HUD.value = player.HUD.maxValue;
        player.fill.color = player.HUDgradient.Evaluate(1f);

        splashText.SetActive(false);
    }
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(5);
        NewWave();

        isDelayed = false;
        genEnemy.enabled = true;
    }
    void SpawnModifier()
    {
        if (!isSpawnModified)
        {
            genEnemy.limit = currentWave + 4;

            genEnemy.cooldownMax = 5.05f - (currentWave * 0.05f);
            genEnemy.cooldownMin = 5.05f - (currentWave * 0.05f);

            if (genEnemy.cooldownMax <= 0 || genEnemy.cooldownMin <= 0)
            {
                genEnemy.cooldownMax = 0.2f;
                genEnemy.cooldownMin = 0.2f;
            }

            isSpawnModified = true;
        }
        if(currentWave == 3)
        {
            for(int i = 49; i <= genEnemy.enemies.Length - 1; i++) genEnemy.enemies[i] = survivalEnemyType[1];
        }
        if(currentWave == 11)
        {
            for (int i = 39; i <= 79; i++) genEnemy.enemies[i] = survivalEnemyType[1];
            for (int i = 80; i <= 89; i++) genEnemy.enemies[i] = survivalEnemyType[2];
            for (int i = 90; i <= 99; i++) genEnemy.enemies[i] = survivalEnemyType[3];
        }
        if(currentWave == 12)
        {
            for (int i = 0; i <= 24; i++) genEnemy.enemies[i] = survivalEnemyType[0];
            for (int i = 25; i <= 49; i++) genEnemy.enemies[i] = survivalEnemyType[1];
            for (int i = 50; i <= 74; i++) genEnemy.enemies[i] = survivalEnemyType[2];
            for (int i = 75; i <= 99; i++) genEnemy.enemies[i] = survivalEnemyType[3];
        }
        if(currentWave == 13)
        {
            for (int i = 0; i <= 49; i++) genEnemy.enemies[i] = survivalEnemyType[2];
            for (int i = 50; i <= 99; i++) genEnemy.enemies[i] = survivalEnemyType[3];
            genEnemy.cooldownMax = 5.02f - (currentWave * 0.02f) + 1;
            genEnemy.cooldownMin = 5.02f - (currentWave * 0.02f) + 1;
        }
        if(currentWave == 14)
        {
            for (int i = 0; i <= 9; i++) genEnemy.enemies[i] = survivalEnemyType[0];
            for (int i = 10; i <= 19; i++) genEnemy.enemies[i] = survivalEnemyType[1];
            for (int i = 20; i <= 59; i++) genEnemy.enemies[i] = survivalEnemyType[2];
            for (int i = 60; i <= 99; i++) genEnemy.enemies[i] = survivalEnemyType[3];
        }
        if (currentWave == 15)
        {
            for (int i = 0; i <= 49; i++) genEnemy.enemies[i] = survivalEnemyType[2];
            for (int i = 50; i <= 99; i++) genEnemy.enemies[i] = survivalEnemyType[3];
        }
        if(currentWave == 16)
        {
            for (int i = 0; i <= 4; i++) genEnemy.enemies[i] = survivalEnemyType[0];
            for (int i = 5; i <= 9; i++) genEnemy.enemies[i] = survivalEnemyType[1];
            for (int i = 10; i <= 44; i++) genEnemy.enemies[i] = survivalEnemyType[2];
            for (int i = 45; i <= 79; i++) genEnemy.enemies[i] = survivalEnemyType[3];
            for (int i = 80; i <= 89; i++) genEnemy.enemies[i] = survivalEnemyType[4];
            for (int i = 90; i <= 99; i++) genEnemy.enemies[i] = survivalEnemyType[5];
        }
        if (currentWave == 17)
        {
            for (int i = 0; i <= 4; i++) genEnemy.enemies[i] = survivalEnemyType[0];
            for (int i = 5; i <= 9; i++) genEnemy.enemies[i] = survivalEnemyType[1];
            for (int i = 10; i <= 29; i++) genEnemy.enemies[i] = survivalEnemyType[2];
            for (int i = 30; i <= 49; i++) genEnemy.enemies[i] = survivalEnemyType[3];
            for (int i = 50; i <= 74; i++) genEnemy.enemies[i] = survivalEnemyType[4];
            for (int i = 75; i <= 99; i++) genEnemy.enemies[i] = survivalEnemyType[5];
        }
        if (currentWave == 18)
        {
            for (int i = 0; i <= 49; i++) genEnemy.enemies[i] = survivalEnemyType[4];
            for (int i = 50; i <= 99; i++) genEnemy.enemies[i] = survivalEnemyType[5];
            genEnemy.cooldownMax = 5.02f - (currentWave * 0.02f) + 0.75f;
            genEnemy.cooldownMin = 5.02f - (currentWave * 0.02f) + 0.75f;
        }
        if (currentWave == 19)
        {
            for (int i = 0; i <= 24; i++) genEnemy.enemies[i] = survivalEnemyType[2];
            for (int i = 25; i <= 49; i++) genEnemy.enemies[i] = survivalEnemyType[3];
            for (int i = 50; i <= 74; i++) genEnemy.enemies[i] = survivalEnemyType[4];
            for (int i = 75; i <= 99; i++) genEnemy.enemies[i] = survivalEnemyType[5];
        }
        if (currentWave == 20)
        {
            for (int i = 0; i <= 49; i++) genEnemy.enemies[i] = survivalEnemyType[4];
            for (int i = 50; i <= 99; i++) genEnemy.enemies[i] = survivalEnemyType[5];
        }
        if(currentWave == 21)
        {
            for (int i = 0; i <= 4; i++) genEnemy.enemies[i] = survivalEnemyType[2];
            for (int i = 5; i <= 9; i++) genEnemy.enemies[i] = survivalEnemyType[3];
            for (int i = 10; i <= 44; i++) genEnemy.enemies[i] = survivalEnemyType[4];
            for (int i = 45; i <= 89; i++) genEnemy.enemies[i] = survivalEnemyType[5];
            for (int i = 90; i <= 94; i++) genEnemy.enemies[i] = survivalEnemyType[6];
            for (int i = 95; i <= 99; i++) genEnemy.enemies[i] = survivalEnemyType[7];
        }
        if(currentWave == 23)
        {
            for (int i = 0; i <= 49; i++) genEnemy.enemies[i] = survivalEnemyType[6];
            for (int i = 50; i <= 99; i++) genEnemy.enemies[i] = survivalEnemyType[7];
            genEnemy.cooldownMax = 5.02f - (currentWave * 0.02f) + 3f;
            genEnemy.cooldownMin = 5.02f - (currentWave * 0.02f) + 3f;
        }
        if(currentWave == 24)
        {
            for (int i = 0; i <= 4; i++) genEnemy.enemies[i] = survivalEnemyType[2];
            for (int i = 5; i <= 9; i++) genEnemy.enemies[i] = survivalEnemyType[3];
            for (int i = 10; i <= 44; i++) genEnemy.enemies[i] = survivalEnemyType[4];
            for (int i = 45; i <= 89; i++) genEnemy.enemies[i] = survivalEnemyType[5];
            for (int i = 90; i <= 94; i++) genEnemy.enemies[i] = survivalEnemyType[6];
            for (int i = 95; i <= 99; i++) genEnemy.enemies[i] = survivalEnemyType[7];
        }
        if (currentWave == 25)
        {
            for (int i = 0; i <= 49; i++) genEnemy.enemies[i] = survivalEnemyType[6];
            for (int i = 50; i <= 99; i++) genEnemy.enemies[i] = survivalEnemyType[7];
            genEnemy.cooldownMax = 5.02f - (currentWave * 0.02f) + 2f;
            genEnemy.cooldownMin = 5.02f - (currentWave * 0.02f) + 2f;
        }
        if (currentWave == 26)
        {
            for (int i = 0; i <= 44; i++) genEnemy.enemies[i] = survivalEnemyType[4];
            for (int i = 45; i <= 89; i++) genEnemy.enemies[i] = survivalEnemyType[5];
            for (int i = 90; i <= 92; i++) genEnemy.enemies[i] = survivalEnemyType[6];
            for (int i = 93; i <= 94; i++) genEnemy.enemies[i] = survivalEnemyType[7];
            for (int i = 95; i <= 97; i++) genEnemy.enemies[i] = survivalEnemyType[8];
            for (int i = 98; i <= 99; i++) genEnemy.enemies[i] = survivalEnemyType[9];
        }
        if (currentWave == 28)
        {
            for (int i = 0; i <= 24; i++) genEnemy.enemies[i] = survivalEnemyType[6];
            for (int i = 25; i <= 49; i++) genEnemy.enemies[i] = survivalEnemyType[7];
            for (int i = 50; i <= 74; i++) genEnemy.enemies[i] = survivalEnemyType[8];
            for (int i = 75; i <= 99; i++) genEnemy.enemies[i] = survivalEnemyType[9];
            genEnemy.cooldownMax = 5.02f - (currentWave * 0.02f) + 3f;
            genEnemy.cooldownMin = 5.02f - (currentWave * 0.02f) + 3f;
        }
        if (currentWave == 29)
        {
            for (int i = 0; i <= 44; i++) genEnemy.enemies[i] = survivalEnemyType[4];
            for (int i = 45; i <= 89; i++) genEnemy.enemies[i] = survivalEnemyType[5];
            for (int i = 90; i <= 94; i++) genEnemy.enemies[i] = survivalEnemyType[8];
            for (int i = 95; i <= 99; i++) genEnemy.enemies[i] = survivalEnemyType[9];
        }
        if (currentWave == 30)
        {
            for (int i = 0; i <= 49; i++) genEnemy.enemies[i] = survivalEnemyType[8];
            for (int i = 50; i <= 99; i++) genEnemy.enemies[i] = survivalEnemyType[9];
            genEnemy.cooldownMax = 5.02f - (currentWave * 0.02f) + 2f;
            genEnemy.cooldownMin = 5.02f - (currentWave * 0.02f) + 2f;
        }
        if(currentWave == 31)
        {
            for (int i = 0; i <= 44; i++) genEnemy.enemies[i] = survivalEnemyType[4];
            for (int i = 45; i <= 89; i++) genEnemy.enemies[i] = survivalEnemyType[5];
            for (int i = 90; i <= 92; i++) genEnemy.enemies[i] = survivalEnemyType[8];
            for (int i = 93; i <= 94; i++) genEnemy.enemies[i] = survivalEnemyType[9];
            for (int i = 95; i <= 97; i++) genEnemy.enemies[i] = survivalEnemyType[10];
            for (int i = 98; i <= 99; i++) genEnemy.enemies[i] = survivalEnemyType[11];
        }
        if (currentWave == 33)
        {
            for (int i = 0; i <= 24; i++) genEnemy.enemies[i] = survivalEnemyType[8];
            for (int i = 25; i <= 49; i++) genEnemy.enemies[i] = survivalEnemyType[9];
            for (int i = 50; i <= 74; i++) genEnemy.enemies[i] = survivalEnemyType[10];
            for (int i = 75; i <= 99; i++) genEnemy.enemies[i] = survivalEnemyType[11];
            genEnemy.cooldownMax = 5.02f - (currentWave * 0.02f) + 3f;
            genEnemy.cooldownMin = 5.02f - (currentWave * 0.02f) + 3f;
        }
        if (currentWave == 34)
        {
            for (int i = 0; i <= 44; i++) genEnemy.enemies[i] = survivalEnemyType[4];
            for (int i = 45; i <= 89; i++) genEnemy.enemies[i] = survivalEnemyType[5];
            for (int i = 90; i <= 94; i++) genEnemy.enemies[i] = survivalEnemyType[10];
            for (int i = 95; i <= 99; i++) genEnemy.enemies[i] = survivalEnemyType[11];
        }
        if (currentWave == 35)
        {
            for (int i = 0; i <= 49; i++) genEnemy.enemies[i] = survivalEnemyType[10];
            for (int i = 50; i <= 99; i++) genEnemy.enemies[i] = survivalEnemyType[11];
            genEnemy.cooldownMax = 5.02f - (currentWave * 0.02f) + 2f;
            genEnemy.cooldownMin = 5.02f - (currentWave * 0.02f) + 2f;
        }
        if(currentWave >= 36)
        {
            if (counter == 3)
            {
                for (int i = 0; i <= 49; i++) genEnemy.enemies[i] = survivalEnemyType[10];
                for (int i = 50; i <= 99; i++) genEnemy.enemies[i] = survivalEnemyType[11];
                genEnemy.cooldownMax = 5.02f - (currentWave * 0.02f) + 2f;
                genEnemy.cooldownMin = 5.02f - (currentWave * 0.02f) + 2f;

                counter = 1;
            }
            else
            {
                for (int i = 0; i <= 44; i++) genEnemy.enemies[i] = survivalEnemyType[4];
                for (int i = 45; i <= 89; i++) genEnemy.enemies[i] = survivalEnemyType[5];
                for (int i = 90; i <= 94; i++) genEnemy.enemies[i] = survivalEnemyType[10];
                for (int i = 95; i <= 99; i++) genEnemy.enemies[i] = survivalEnemyType[11];

                ++counter;
            }
            PlayerPrefs.SetInt("Survival Wave Counter", counter);
        }
    }
}
