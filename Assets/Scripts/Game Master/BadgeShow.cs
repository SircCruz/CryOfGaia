using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class BadgeShow : MonoBehaviour
{
    public TextMeshProUGUI description;

    public float messageDown,messageDown2, messageUp, messageDuration, messageDuration2;
    public float restoreMessageDuration, restoreMessageDuration2;
    public bool isDown, goDown, goUp;

    string achieved;
    int treasurerIShowed, treasurerIIShowed;
    int executionerIShowed, executionerIIShowed;
    int annihilatorIShowed;
    int campaign1Showed;
    int sunflowerShowed, jasmineShowed, santanShowed, dandelionShowed, roseShowed, tulipsShowed, portulacaShowed;
    private void Start()
    {

        transform.position = new Vector2(transform.position.x, transform.position.y + 6);

        messageDown = transform.position.y - 6;

        messageDown2 = transform.position.y - 6.5f;

        messageUp = transform.position.y;
        isDown = true;

        messageDuration = 6f;
        restoreMessageDuration = messageDuration;

        messageDuration2 = 6.1f;
        restoreMessageDuration2 = messageDuration2;

        executionerIShowed = PlayerPrefs.GetInt("Executioner I Showed", 0);
        executionerIIShowed = PlayerPrefs.GetInt("Executioner II Showed", 0); 

        treasurerIShowed = PlayerPrefs.GetInt("Treasurer I Showed", 0);
        treasurerIIShowed = PlayerPrefs.GetInt("Treasurer II Showed", 0);

        campaign1Showed = PlayerPrefs.GetInt("Campaign 1 Showed", 0);

        annihilatorIShowed = PlayerPrefs.GetInt("Annihilator I Showed", 0);

        sunflowerShowed = PlayerPrefs.GetInt("Sunflower Showed", 0);
        jasmineShowed = PlayerPrefs.GetInt("Jasmine Showed", 0);
        santanShowed = PlayerPrefs.GetInt("Santan Showed", 0);
        dandelionShowed = PlayerPrefs.GetInt("Dandelion Showed", 0);
        roseShowed = PlayerPrefs.GetInt("Rose Showed", 0);
        tulipsShowed = PlayerPrefs.GetInt("Tulips Showed", 0);
        portulacaShowed = PlayerPrefs.GetInt("Portulaca Showed", 0);

        gameObject.SetActive(false);
    }
    private void FixedUpdate()
    {
        if (isDown)
        {
            gameObject.SetActive(true);
            transform.position = new Vector2(transform.position.x, transform.position.y + (Time.fixedDeltaTime * -16));
            if(transform.position.y <= messageDown)
            {
                isDown = false;
            }
        }
        if (!isDown)
        {
            messageDuration -= Time.fixedDeltaTime;
            if(messageDuration <= 0)
            {
                messageDuration = restoreMessageDuration;
                goDown = true;
            }
        }
        if (goDown)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + (Time.fixedDeltaTime * -8));
            if (transform.position.y <= messageDown2)
            {
                goDown = false;
            }
        }
        if(!goDown && !isDown)
        {
            messageDuration2 -= Time.fixedDeltaTime;
            if(messageDuration2 <= 0)
            {
                messageDuration2 = restoreMessageDuration2;
                goUp = true;
            }
        }
        if (goUp)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + (Time.fixedDeltaTime * 20));
            if(transform.position.y >= messageUp)
            {
                isDown = true;
                goUp = false;

                if(achieved.Equals("Executioner I"))
                {
                    executionerIShowed = 1;
                    PlayerPrefs.SetInt("Executioner I Showed", executionerIShowed);
                }
                if (achieved.Equals("Executioner II"))
                {
                    executionerIIShowed = 1;
                    PlayerPrefs.SetInt("Executioner II Showed", executionerIIShowed);
                }
                if (achieved.Equals("Treasurer I"))
                {
                    treasurerIShowed = 1;
                    PlayerPrefs.SetInt("Treasurer I Showed", treasurerIShowed);
                }
                if (achieved.Equals("Treasurer II"))
                {
                    treasurerIIShowed = 1;
                    PlayerPrefs.SetInt("Treasurer II Showed", treasurerIIShowed);
                }

                if (achieved.Equals("Campaign 1"))
                {
                    campaign1Showed = 1;
                    PlayerPrefs.SetInt("Campaign 1 Showed", campaign1Showed);
                }
                if(achieved.Equals("Annihilator I"))
                {
                    annihilatorIShowed = 1;
                    PlayerPrefs.SetInt("Annihilator I Showed", annihilatorIShowed);
                }

                if (achieved.Equals("Sunflower"))
                {
                    sunflowerShowed = 1;
                    PlayerPrefs.SetInt("Sunflower Showed", sunflowerShowed);
                }
                if (achieved.Equals("Jasmine"))
                {
                    jasmineShowed = 1;
                    PlayerPrefs.SetInt("Jasmine Showed", jasmineShowed);
                }
                if (achieved.Equals("Santan"))
                {
                    santanShowed = 1;
                    PlayerPrefs.SetInt("Santan Showed", santanShowed);
                }
                if (achieved.Equals("Dandelion"))
                {
                    dandelionShowed = 1;
                    PlayerPrefs.SetInt("Dandelion Showed", dandelionShowed);
                }
                if (achieved.Equals("Rose"))
                {
                    roseShowed = 1;
                    PlayerPrefs.SetInt("Rose Showed", roseShowed);
                }
                if (achieved.Equals("Tulips"))
                {
                    tulipsShowed = 1;
                    PlayerPrefs.SetInt("Tulips Showed", tulipsShowed);
                }
                if (achieved.Equals("Portulaca"))
                {
                    portulacaShowed = 1;
                    PlayerPrefs.SetInt("Portulaca Showed", portulacaShowed);
                }
                gameObject.SetActive(false);
            }
        }
    }
    public void ShowBadge(string achieved)
    {
        if(executionerIShowed == 0 && achieved.Equals("Executioner I"))
        {
            description.text = "Savior";
            gameObject.SetActive(true);
            this.achieved = achieved;
        }
        if (executionerIIShowed == 0 && achieved.Equals("Executioner II"))
        {
            description.text = "Guardian";
            gameObject.SetActive(true);
            this.achieved = achieved;
        }
        if (treasurerIShowed == 0 && achieved.Equals("Treasurer I"))
        {
            description.text = "Gatherer";
            gameObject.SetActive(true);
            this.achieved = achieved;
        }
        if (treasurerIIShowed == 0 && achieved.Equals("Treasurer II"))
        {
            description.text = "Grand Collector";
            gameObject.SetActive(true);
            this.achieved = achieved;
        }
        if (campaign1Showed == 0 && achieved.Equals("Campaign 1"))
        {
            description.text = "New Beginnings";
            gameObject.SetActive(true);
            this.achieved = achieved;
        }
        if(annihilatorIShowed == 0 && achieved.Equals("Annihilator I"))
        {
            description.text = "Rusty Blades";
            gameObject.SetActive(true);
            this.achieved = achieved;
        }
        if(sunflowerShowed == 0 && achieved.Equals("Sunflower"))
        {
            description.text = "Nature's Sunshine";
            gameObject.SetActive(true);
            this.achieved = achieved;
        }
        if (jasmineShowed == 0 && achieved.Equals("Jasmine"))
        {
            description.text = "Sweet Fragrance";
            gameObject.SetActive(true);
            this.achieved = achieved;
        }
        if (santanShowed == 0 && achieved.Equals("Santan"))
        {
            description.text = "Crimson Bloom";
            gameObject.SetActive(true);
            this.achieved = achieved;
        }
        if (dandelionShowed == 0 && achieved.Equals("Dandelion"))
        {
            description.text = "Parachuting Wish";
            gameObject.SetActive(true);
            this.achieved = achieved;
        }
        if (roseShowed == 0 && achieved.Equals("Rose"))
        {
            description.text = "Thorned Beauty";
            gameObject.SetActive(true);
            this.achieved = achieved;
        }
        if (tulipsShowed == 0 && achieved.Equals("Tulips"))
        {
            description.text = "Delicate and Bold";
            gameObject.SetActive(true);
            this.achieved = achieved;
        }
        if (portulacaShowed == 0 && achieved.Equals("Portulaca"))
        {
            description.text = "Vibrant Blossoms";
            gameObject.SetActive(true);
            this.achieved = achieved;
        }
    }
}
