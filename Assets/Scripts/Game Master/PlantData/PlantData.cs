using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class PlantData : MonoBehaviour
{
    public Transform message;
    bool showMessageUp, showMessageDown;
    float messageUp, messageDown;

    int waterAvailable;
    public TextMeshProUGUI waterAvailableTxt;
    public TextMeshProUGUI waterAvailableTxt2;
    float delay, restoreDelay;
    bool noWaterAnim;

    public PlantitaInteraction interaction;
    public Plant[] plant;
    public TextMeshProUGUI[] progress;

    public string[] plants;
    public GameObject[] plantSlot, plantSlotText;
    bool[] isActive;
    public int[] levelAssigner;

    public GameObject watering;
    public Image wateringCan, wateringCanFull;
    bool isWatering;

    int sunFlowerCounter, jasmineCounter, santanCounter, dandelionCounter, roseCounter, tulipsCounter, portulacaCounter;
    int acqSunflowerCounter, acqJasmineCounter, acqSantanCounter, acqDandelionCounter, acqRoseCounter, acqTulipsCounter, acqPortulacaCounter;
    void Start()
    {
        //PlayerPrefs.DeleteAll();
        isActive = new bool[18];

        showMessageUp = false;
        showMessageDown = false;
        messageDown = message.transform.position.y;
        message.position = new Vector2(message.transform.position.x, message.transform.position.y + 8f);
        messageUp = message.transform.position.y;

        noWaterAnim = false;
        delay = 0.2f;
        restoreDelay = delay;

        sunFlowerCounter = 0;
        acqSunflowerCounter = 0;

        jasmineCounter = 0;
        acqJasmineCounter = 0;

        santanCounter = 0;
        acqSantanCounter = 0;

        dandelionCounter = 0;
        acqDandelionCounter = 0;

        roseCounter = 0;
        acqRoseCounter = 0;

        tulipsCounter = 0;
        acqTulipsCounter = 0;
        for (int i = 0; i < plants.Length; i++)
        {
            LoadPlants(i);
        }
    }
    void LoadPlants(int i)
    {
        plants[i] = PlayerPrefs.GetString("Plant Type" + i, "");
        if (plants[i].Equals("Sunflower"))
        {
            sunFlowerCounter++;
            levelAssigner[i] = sunFlowerCounter;
        }
        if (plants[i].Equals("Jasmine"))
        {
            jasmineCounter++;
            levelAssigner[i] = jasmineCounter;
        }
        if (plants[i].Equals("Santan"))
        {
            santanCounter++;
            levelAssigner[i] = santanCounter;
        }
        if (plants[i].Equals("Dandelion"))
        {
            dandelionCounter++;
            levelAssigner[i] = dandelionCounter;
        }
        if (plants[i].Equals("Rose"))
        {
            roseCounter++;
            levelAssigner[i] = roseCounter;
        }
        if (plants[i].Equals("Tulips"))
        {
            tulipsCounter++;
            levelAssigner[i] = tulipsCounter;
        }
        if (plants[i].Equals("Portulaca"))
        {
            portulacaCounter++;
            levelAssigner[i] = portulacaCounter;
        }
    }
    private void FixedUpdate()
    {
        for(int i = 0; i < plant.Length; i++)
        {
            progress[i].text = plant[i].waterGained.value + "/" + plant[i].waterGained.maxValue;
        }

        waterAvailable = interaction.addWater;

        if (showMessageDown)
        {
            message.position = new Vector2(message.transform.position.x, message.transform.position.y + (Time.fixedDeltaTime * -40f));
            if (message.transform.position.y <= messageDown)
            {
                showMessageDown = false;
            }
        }
        if (showMessageUp)
        {
            message.position = new Vector2(message.transform.position.x, message.transform.position.y + (Time.fixedDeltaTime * 40f));
            if (message.transform.position.y >= messageUp)
            {
                showMessageUp = false;
            }
        }
        if (noWaterAnim)
        {
            waterAvailableTxt2.color = new Color(255, 0, 0);
            delay -= Time.deltaTime;
            if(delay <= 0)
            {
                waterAvailableTxt2.color = new Color(255, 255, 255);
                delay = restoreDelay;
                noWaterAnim = false;
            }
        }
    }
    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            for (int i = 0; i < plants.Length; i++)
            {
                PlayerPrefs.SetString("Plant Type" + i, plants[i]);
            }
        }
    }
    public void ShowItems(int slot)
    {
        if (!isActive[slot])
        {
            for (int i = 0; i < plantSlot.Length; i++)
            {
                plantSlot[i].SetActive(false);
                plantSlotText[i].SetActive(false);
                isActive[i] = false;
            }
            plantSlotText[slot].SetActive(true);
            plantSlot[slot].SetActive(true);
            isActive[slot] = true;
        }
        else
        {
            plantSlotText[slot].SetActive(false);
            plantSlot[slot].SetActive(false);
            isActive[slot] = false;
        }
    }
    public void AcquirePlant()
    {
        if (interaction.allowAcquiring)
        {
            acqSunflowerCounter = 0;
            acqJasmineCounter = 0;
            acqSantanCounter = 0;
            acqDandelionCounter = 0;
            acqRoseCounter = 0;
            acqTulipsCounter = 0;
            acqPortulacaCounter = 0;
            for (int i = 0; i < plants.Length; i++)
            {
                if (plants[i].Equals("Sunflower"))
                {
                    acqSunflowerCounter++;
                    levelAssigner[i] = acqSunflowerCounter;
                }
                if (plants[i].Equals("Jasmine"))
                {
                    acqJasmineCounter++;
                    levelAssigner[i] = acqJasmineCounter;
                }
                if (plants[i].Equals("Santan"))
                {
                    acqSantanCounter++;
                    levelAssigner[i] = acqSantanCounter;
                }
                if (plants[i].Equals("Dandelion"))
                {
                    acqDandelionCounter++;
                    levelAssigner[i] = acqDandelionCounter;
                }
                if (plants[i].Equals("Rose"))
                {
                    acqRoseCounter++;
                    levelAssigner[i] = acqRoseCounter;
                }
                if (plants[i].Equals("Tulips"))
                {
                    acqTulipsCounter++;
                    levelAssigner[i] = acqTulipsCounter;
                }
                if (plants[i].Equals("Portulaca"))
                {
                    acqPortulacaCounter++;
                    levelAssigner[i] = acqPortulacaCounter;
                }
            }
        }
    }
    public void Watering()
    {
        if(waterAvailable != 0)
        {
            if (!isWatering)
            {
                showMessageDown = true;
                showMessageUp = false;
                for(int i = 0; i < plant.Length; i++)
                {
                    plantSlot[i].SetActive(false);
                    if (plant[i].waterGained.value != plant[i].waterGained.maxValue)
                    {
                        plantSlotText[i].SetActive(true);
                        isActive[i] = true;
                    }
                    else
                    {
                        plantSlotText[i].SetActive(false);
                        isActive[i] = false;
                    }
                }
                isWatering = true;
            }
            else
            {
                showMessageUp = true;
                showMessageDown = false;
                for (int i = 0; i < plant.Length; i++)
                {
                    plantSlotText[i].SetActive(false);
                    isActive[i] = false;
                }
                isWatering = false;
            }
        }
        else
        {
            noWaterAnim = true;
        }
    }
    public void WaterPlant(int position)
    {
        if (isWatering)
        {
            if (waterAvailable != 0)
            {
                wateringCan.enabled = false;
                wateringCanFull.enabled = false;
                showMessageUp = true;
                for (int i = 0; i < plant.Length; i++)
                {
                    if(position == i)
                    {
                        plant[i].waterGained.value += waterAvailable;
                        plantSlot[i].SetActive(true);
                        plantSlotText[i].SetActive(true);
                        isActive[i] = true;
                        Instantiate(watering, new Vector2(plantSlot[i].transform.position.x - 0.6f, plantSlot[i].transform.position.y + 2.2f), transform.rotation);
                    }
                    else
                    {
                        plantSlot[i].SetActive(false);
                        plantSlotText[i].SetActive(false);
                        isActive[i] = false;
                    }
                }
                waterAvailable = 0;
                PlayerPrefs.SetInt("Stored Water", waterAvailable);
                interaction.Water();
            }
            isWatering = false;
        }
    }
}
