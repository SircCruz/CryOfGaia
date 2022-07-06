using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Plant : MonoBehaviour
{
    public PlantData data;
    public Slider waterGained;

    public int index;
    public TextMeshProUGUI plantName;
    GameObject plantSlot;

    public GameObject plantType;
    public GameObject bud, sunflower, jasmine, santan, dandelion, rose, tulips, portulaca;
    public GameObject sunflowerYoung, jasmineYoung, santanYoung, dandelionYoung, roseYoung, tulipsYoung, portulacaYoung;
    public GameObject smoke, sparkle, glow, confetti;
    bool isSpawn, isSpawn2, isSpawn3, isSpawn4;
    Bud budScript, youngScript;

    public BadgeShow badge;

    int level;
    int sunflowerIsDone1, sunflowerIsDone2, sunflowerIsDone3, sunflowerIsDone4;
    int jasmineIsDone1, jasmineIsDone2, jasmineIsDone3;
    int santanIsDone1, santanIsDone2, santanIsDone3;
    int dandelionIsDone1, dandelionIsDone2;
    int roseIsDone1, roseIsDone2;
    int tulipsIsDone1, tulipsIsDone2;
    int portulacaIsDone1, portulacaIsDone2;
    float flowerPosY;
    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            SaveData();
        }
    }
    private void Start()
    {
        //PlayerPrefs.DeleteAll();
        plantName.text = data.plants[index];
        level = data.levelAssigner[index];
       
        if (plantName.text.Equals("Sunflower"))
        {
            sunflowerIsDone1 = PlayerPrefs.GetInt("Sunflower Upgrade" + 1, 0);
            sunflowerIsDone2 = PlayerPrefs.GetInt("Sunflower Upgrade" + 2, 0);
            sunflowerIsDone3 = PlayerPrefs.GetInt("Sunflower Upgrade" + 3, 0);
            sunflowerIsDone4 = PlayerPrefs.GetInt("Sunflower Upgrade" + 4, 0);
            if (level == 1)
            {
                waterGained.maxValue = 250;
            }
            if (level == 2)
            {
                waterGained.maxValue = 1000;
            }
            if (level == 3)
            {
                waterGained.maxValue = 2000;
            }
            if (level == 4)
            {
                waterGained.maxValue = 5000;
            }
            waterGained.value = PlayerPrefs.GetFloat("Sunflower Gained" + level, 0);
        }
        if (plantName.text.Equals("Jasmine"))
        {
            jasmineIsDone1 = PlayerPrefs.GetInt("Jasmine Upgrade" + 1, 0);
            jasmineIsDone2 = PlayerPrefs.GetInt("Jasmine Upgrade" + 2, 0);
            jasmineIsDone3 = PlayerPrefs.GetInt("Jasmine Upgrade" + 3, 0);
            if (level == 1)
            {
                waterGained.maxValue = 500;
            }
            if(level == 2)
            {
                waterGained.maxValue = 2000;
            }
            if(level == 3)
            {
                waterGained.maxValue = 5000;
            }
            waterGained.value = PlayerPrefs.GetFloat("Jasmine Gained" + level, 0);
        }
        if (plantName.text.Equals("Santan"))
        {
            santanIsDone1 = PlayerPrefs.GetInt("Santan Upgrade" + 1, 0);
            santanIsDone2 = PlayerPrefs.GetInt("Santan Upgrade" + 2, 0);
            santanIsDone3 = PlayerPrefs.GetInt("Santan Upgrade" + 3, 0);
            if (level == 1)
            {
                waterGained.maxValue = 600;
            }
            if (level == 2)
            {
                waterGained.maxValue = 3000;
            }
            if (level == 3)
            {
                waterGained.maxValue = 6000;
            }
            waterGained.value = PlayerPrefs.GetFloat("Santan Gained" + level, 0);
        }
        if (plantName.text.Equals("Dandelion"))
        {
            dandelionIsDone1 = PlayerPrefs.GetInt("Dandelion Upgrade" + 1, 0);
            dandelionIsDone2 = PlayerPrefs.GetInt("Dandelion Upgrade" + 2, 0);
            if (level == 1)
            {
                waterGained.maxValue = 3000;
            }
            if(level == 2)
            {
                waterGained.maxValue = 6000;
            }
            waterGained.value = PlayerPrefs.GetFloat("Dandelion Gained" + level, 0);
        }
        if (plantName.text.Equals("Rose"))
        {
            roseIsDone1 = PlayerPrefs.GetInt("Rose Upgrade" + 1, 0);
            roseIsDone2 = PlayerPrefs.GetInt("Rose Upgrade" + 2, 0);
            if (level == 1)
            {
                waterGained.maxValue = 3000;
            }
            if (level == 2)
            {
                waterGained.maxValue = 6000;
            }
            waterGained.value = PlayerPrefs.GetFloat("Rose Gained" + level, 0);
        }
        if (plantName.text.Equals("Tulips"))
        {
            tulipsIsDone1 = PlayerPrefs.GetInt("Tulips Upgrade" + 1, 0);
            tulipsIsDone2 = PlayerPrefs.GetInt("Tulips Upgrade" + 2, 0);
            if (level == 1)
            {
                waterGained.maxValue = 4000;
            }
            if (level == 2)
            {
                waterGained.maxValue = 7000;
            }
            waterGained.value = PlayerPrefs.GetFloat("Tulips Gained" + level, 0);
        }
        if (plantName.text.Equals("Portulaca"))
        {
            portulacaIsDone1 = PlayerPrefs.GetInt("Portulaca Upgrade" + 1, 0);
            portulacaIsDone2 = PlayerPrefs.GetInt("Portulaca Upgrade" + 2, 0);
            if (level == 1)
            {
                waterGained.maxValue = 4000;
            }
            if (level == 2)
            {
                waterGained.maxValue = 7000;
            }
            waterGained.value = PlayerPrefs.GetFloat("Portulaca Gained" + level, 0);
        }
        plantSlot = data.plantSlotText[index];
        plantSlot.SetActive(false);
    }
    private void Update()
    {
        PlantUpdate();

        level = data.levelAssigner[index];

        if (plantName.text.Equals("Sunflower"))
        {
            PlayerPrefs.SetFloat("Sunflower Gained" + level, waterGained.value);
            PlayerPrefs.SetFloat("Sunflower Max" + level, waterGained.maxValue);
            if (waterGained.value >= 250)
            {
                sunflowerIsDone1 = 1;
                PlayerPrefs.SetInt("Sunflower Upgrade" + 1, sunflowerIsDone1);
            }
            if (waterGained.value >= 1000)
            {
                sunflowerIsDone2 = 1;
                PlayerPrefs.SetInt("Sunflower Upgrade" + 2, sunflowerIsDone2);
            }
            if (waterGained.value >= 2000)
            {
                sunflowerIsDone3 = 1;
                PlayerPrefs.SetInt("Sunflower Upgrade" + 3, sunflowerIsDone3);
            }
            if (waterGained.value >= 5000)
            {
                sunflowerIsDone4 = 1;
                PlayerPrefs.SetInt("Sunflower Upgrade" + 4, sunflowerIsDone4);
                badge.ShowBadge("Sunflower");
            }
        }
        if (plantName.text.Equals("Jasmine"))
        {
            PlayerPrefs.SetFloat("Jasmine Gained" + level, waterGained.value);
            PlayerPrefs.SetFloat("Jasmine Max" + level, waterGained.maxValue);
            
            if (waterGained.value >= 500)
            {
                jasmineIsDone1 = 1;
                PlayerPrefs.SetInt("Jasmine Upgrade" + 1, jasmineIsDone1);
            }
            if (waterGained.value >= 2000)
            {
                jasmineIsDone2 = 1;
                PlayerPrefs.SetInt("Jasmine Upgrade" + 2, jasmineIsDone2);
            }
            if (waterGained.value >= 5000)
            {
                jasmineIsDone3 = 1;
                PlayerPrefs.SetInt("Jasmine Upgrade" + 3, jasmineIsDone3);
                badge.ShowBadge("Jasmine");
            }
        }
        if (plantName.text.Equals("Santan"))
        {
            PlayerPrefs.SetFloat("Santan Gained" + level, waterGained.value);
            PlayerPrefs.SetFloat("Santan Max" + level, waterGained.maxValue);
            if (waterGained.value >= 600)
            {
                santanIsDone1 = 1;
                PlayerPrefs.SetInt("Santan Upgrade" + 1, santanIsDone1);
            }
            if (waterGained.value >= 3000)
            {
                santanIsDone2 = 1;
                PlayerPrefs.SetInt("Santan Upgrade" + 2, santanIsDone2);
            }
            if (waterGained.value >= 6000)
            {
                santanIsDone3 = 1;
                PlayerPrefs.SetInt("Santan Upgrade" + 3, santanIsDone3);
                badge.ShowBadge("Santan");
            }
        }
        else if (plantName.text.Equals("Dandelion"))
        {
            PlayerPrefs.SetFloat("Dandelion Gained" + level, waterGained.value);
            PlayerPrefs.SetFloat("Dandelion Max" + level, waterGained.maxValue);
            if(waterGained.value >= 3000)
            {
                dandelionIsDone1 = 1;
                PlayerPrefs.SetInt("Dandelion Upgrade" + 1, dandelionIsDone1);
            }
            if(waterGained.value >= 6000)
            {
                dandelionIsDone2 = 1;
                PlayerPrefs.SetInt("Dandelion Upgrade" + 2, dandelionIsDone2);
                badge.ShowBadge("Dandelion");
            }
        }
        else if (plantName.text.Equals("Rose"))
        {
            PlayerPrefs.SetFloat("Rose Gained" + level, waterGained.value);
            PlayerPrefs.SetFloat("Rose Max" + level, waterGained.maxValue);
            if (waterGained.value >= 3000)
            {
                roseIsDone1 = 1;
                PlayerPrefs.SetInt("Rose Upgrade" + 1, roseIsDone1);
            }
            if (waterGained.value >= 6000)
            {
                roseIsDone2 = 1;
                PlayerPrefs.SetInt("Rose Upgrade" + 2, roseIsDone2);
                badge.ShowBadge("Rose");
            }
        }
        else if (plantName.text.Equals("Tulips"))
        {
            PlayerPrefs.SetFloat("Tulips Gained" + level, waterGained.value);
            PlayerPrefs.SetFloat("Tulips Max" + level, waterGained.maxValue);
            if (waterGained.value >= 4000)
            {
                tulipsIsDone1 = 1;
                PlayerPrefs.SetInt("Tulips Upgrade" + 1, tulipsIsDone1);
            }
            if (waterGained.value >= 7000)
            {
                tulipsIsDone2 = 1;
                PlayerPrefs.SetInt("Tulips Upgrade" + 2, tulipsIsDone2);
                badge.ShowBadge("Tulips");
            }
        }
        else if (plantName.text.Equals("Portulaca"))
        {
            PlayerPrefs.SetFloat("Portulaca Gained" + level, waterGained.value);
            PlayerPrefs.SetFloat("Portulaca Max" + level, waterGained.maxValue);
            if (waterGained.value >= 4000)
            {
                portulacaIsDone1 = 1;
                PlayerPrefs.SetInt("Portulaca Upgrade" + 1, portulacaIsDone1);
            }
            if (waterGained.value >= 7000)
            {
                portulacaIsDone2 = 1;
                PlayerPrefs.SetInt("Portulaca Upgrade" + 2, portulacaIsDone2);
                badge.ShowBadge("Portulaca");
            }
        }
    }
    public void SaveData()
    {
        if (plantName.text.Equals("Sunflower"))
        {
            PlayerPrefs.SetInt("Sunflower Upgrade" + 1, sunflowerIsDone1);
            PlayerPrefs.SetInt("Sunflower Upgrade" + 2, sunflowerIsDone2);
            PlayerPrefs.SetInt("Sunflower Upgrade" + 3, sunflowerIsDone3);
            PlayerPrefs.SetInt("Sunflower Upgrade" + 4, sunflowerIsDone4);
        }
        if (plantName.text.Equals("Jasmine"))
        {
            PlayerPrefs.SetInt("Jasmine Upgrade" + 1, jasmineIsDone1);
            PlayerPrefs.SetInt("Jasmine Upgrade" + 2, jasmineIsDone2);
            PlayerPrefs.SetInt("Jasmine Upgrade" + 3, jasmineIsDone3);
        }
        if (plantName.text.Equals("Santan"))
        {
            PlayerPrefs.SetInt("Santan Upgrade" + 1, santanIsDone1);
            PlayerPrefs.SetInt("Santan Upgrade" + 2, santanIsDone2);
            PlayerPrefs.SetInt("Santan Upgrade" + 3, santanIsDone3);
        }
        if (plantName.text.Equals("Dandelion"))
        {
            PlayerPrefs.SetInt("Dandelion Upgrade" + 1, dandelionIsDone1);
            PlayerPrefs.SetInt("Dandelion Upgrade" + 2, dandelionIsDone2);
        }
        if (plantName.text.Equals("Rose"))
        {
            PlayerPrefs.SetInt("Rose Upgrade" + 1, roseIsDone1);
            PlayerPrefs.SetInt("Rose Upgrade" + 2, roseIsDone2);
        }
        if (plantName.text.Equals("Tulips"))
        {
            PlayerPrefs.SetInt("Tulips Upgrade" + 1, tulipsIsDone1);
            PlayerPrefs.SetInt("Tulips Upgrade" + 2, tulipsIsDone2);
        }
        if (plantName.text.Equals("Portulaca"))
        {
            PlayerPrefs.SetInt("Portulaca Upgrade" + 1, portulacaIsDone1);
            PlayerPrefs.SetInt("Portulaca Upgrade" + 2, portulacaIsDone2);
        }
    }

    void PlantUpdate()
    {
        if (waterGained.value == waterGained.maxValue)
        {
            SpawnEffects();
        }
        if (waterGained.value >= waterGained.maxValue * 0.6f)
        {
            SpawnFull();
        }
        else if(waterGained.value >= waterGained.maxValue * 0.3f)
        {
            SpawnYoung();
        }
        else
        {
            SpawnBud();
        }
    }
    public void SpawnBud()
    {
        if (!isSpawn)
        {
            flowerPosY = 0.95f;
            var name = Instantiate(bud, new Vector2(transform.position.x, transform.position.y + flowerPosY), transform.rotation, plantType.transform);
            name.name = "Bud" + index;
            isSpawn = true;
        }
    }
    void SpawnYoung()
    {
        if (!isSpawn2)
        {
            flowerPosY = 0.6f;
            if (isSpawn)
            {
                budScript = GameObject.Find("Bud" + index).GetComponent<Bud>();
                budScript.hide = true;
                Instantiate(smoke, new Vector2(transform.position.x, transform.position.y + flowerPosY), transform.rotation);
                Instantiate(confetti, new Vector2(transform.position.x, transform.position.y + flowerPosY), transform.rotation, plantType.transform);
            }
            if (plantName.text.Equals("Sunflower"))
            {
                var name = Instantiate(sunflowerYoung, new Vector2(transform.position.x, transform.position.y + flowerPosY), transform.rotation, plantType.transform);
                name.name = "SunflowerYoung" + index;
            }
            else if (plantName.text.Equals("Jasmine"))
            {
                var name = Instantiate(jasmineYoung, new Vector2(transform.position.x, transform.position.y + flowerPosY), transform.rotation, plantType.transform);
                name.name = "JasmineYoung" + index;
            }
            else if (plantName.text.Equals("Santan"))
            {
                var name = Instantiate(santanYoung, new Vector2(transform.position.x, transform.position.y + flowerPosY), transform.rotation, plantType.transform);
                name.name = "SantanYoung" + index;
            }
            else if (plantName.text.Equals("Dandelion") && waterGained.value >= 100)
            {
                var name = Instantiate(dandelionYoung, new Vector2(transform.position.x, transform.position.y + flowerPosY), transform.rotation, plantType.transform);
                name.name = "DandelionYoung" + index;
            }
            else if (plantName.text.Equals("Rose"))
            {
                var name = Instantiate(roseYoung, new Vector2(transform.position.x, transform.position.y + flowerPosY), transform.rotation, plantType.transform);
                name.name = "RoseYoung" + index;
            }
            else if (plantName.text.Equals("Tulips"))
            {
                var name = Instantiate(tulipsYoung, new Vector2(transform.position.x, transform.position.y + flowerPosY), transform.rotation, plantType.transform);
                name.name = "TulipsYoung" + index;
            }
            else if (plantName.text.Equals("Portulaca"))
            {
                var name = Instantiate(portulacaYoung, new Vector2(transform.position.x, transform.position.y + flowerPosY), transform.rotation, plantType.transform);
                name.name = "PortulacaYoung" + index;
            }
            isSpawn2 = true;
        }
    }
    void SpawnFull()
    {
        if (!isSpawn3)
        {
            flowerPosY = 0.95f;
            if (isSpawn2)
            {
                Instantiate(smoke, new Vector2(transform.position.x, transform.position.y + flowerPosY), transform.rotation);
                Instantiate(confetti, new Vector2(transform.position.x, transform.position.y + flowerPosY), transform.rotation, plantType.transform);
            }
            if (plantName.text.Equals("Sunflower"))
            {
                if (isSpawn2)
                {
                    youngScript = GameObject.Find("SunflowerYoung" + index).GetComponent<Bud>();
                    youngScript.hide = true;
                }
                var name = Instantiate(sunflower, new Vector2(transform.position.x, transform.position.y + flowerPosY), transform.rotation, plantType.transform);
                name.name = index.ToString();
            }
            else if (plantName.text.Equals("Jasmine"))
            {
                if (isSpawn2)
                {
                    youngScript = GameObject.Find("JasmineYoung" + index).GetComponent<Bud>();
                    youngScript.hide = true;
                }
                var name = Instantiate(jasmine, new Vector2(transform.position.x, transform.position.y + flowerPosY), transform.rotation, plantType.transform);
                name.name = index.ToString();
            }
            else if (plantName.text.Equals("Santan"))
            {
                if (isSpawn2)
                {
                    youngScript = GameObject.Find("SantanYoung" + index).GetComponent<Bud>();
                    youngScript.hide = true;
                }
                var name = Instantiate(santan, new Vector2(transform.position.x, transform.position.y + flowerPosY), transform.rotation, plantType.transform);
                name.name = index.ToString();
            }
            else if (plantName.text.Equals("Dandelion"))
            {
                if (isSpawn2)
                {
                    youngScript = GameObject.Find("DandelionYoung" + index).GetComponent<Bud>();
                    youngScript.hide = true;
                }
                var name = Instantiate(dandelion, new Vector2(transform.position.x, transform.position.y + flowerPosY), transform.rotation, plantType.transform);
                name.name = index.ToString();
            }
            else if (plantName.text.Equals("Rose"))
            {
                if (isSpawn2)
                {
                    youngScript = GameObject.Find("RoseYoung" + index).GetComponent<Bud>();
                    youngScript.hide = true;
                }
                var name = Instantiate(rose, new Vector2(transform.position.x, transform.position.y + flowerPosY), transform.rotation, plantType.transform);
                name.name = index.ToString();
            }
            else if (plantName.text.Equals("Tulips"))
            {
                if (isSpawn2)
                {
                    youngScript = GameObject.Find("TulipsYoung" + index).GetComponent<Bud>();
                    youngScript.hide = true;
                }
                var name = Instantiate(tulips, new Vector2(transform.position.x, transform.position.y + flowerPosY), transform.rotation, plantType.transform);
                name.name = index.ToString();
            }
            else if (plantName.text.Equals("Portulaca"))
            {
                if (isSpawn2)
                {
                    youngScript = GameObject.Find("PortulacaYoung" + index).GetComponent<Bud>();
                    youngScript.hide = true;
                }
                var name = Instantiate(portulaca, new Vector2(transform.position.x, transform.position.y + flowerPosY), transform.rotation, plantType.transform);
                name.name = index.ToString();
            }
            isSpawn3 = true;
        }
    }
    void SpawnEffects()
    {
        if (!isSpawn4)
        {
            if (isSpawn3)
            {
                Instantiate(smoke, new Vector2(transform.position.x, transform.position.y + flowerPosY), transform.rotation);
                Instantiate(confetti, new Vector2(transform.position.x, transform.position.y + flowerPosY), transform.rotation, plantType.transform);
            }
            flowerPosY = 1.3f;
            Instantiate(glow, new Vector2(transform.position.x, transform.position.y + 0.3f), transform.rotation, plantType.transform);
            Instantiate(sparkle, new Vector2(transform.position.x, transform.position.y + flowerPosY), transform.rotation, plantType.transform);
            isSpawn4 = true;
        }
    }
}
