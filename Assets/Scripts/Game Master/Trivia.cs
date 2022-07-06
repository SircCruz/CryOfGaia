using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Trivia : MonoBehaviour
{
    public GameObject descriptionBox;

    public TextMeshProUGUI triviaTitle;
    public TextMeshProUGUI description;

    public Transform[] trivia;
    public Image[] triviaImg;

    bool[] isActive;

    int[] gotText;
    bool[] isUnlocked;

    public Sprite open;
    public Sprite close;

    private void Start()
    {
        isUnlocked = new bool[10];

        descriptionBox.SetActive(false);
        isActive = new bool[10];
        gotText = new int[10];
        for (int i = 0; i < gotText.Length; i++)
        {
            gotText[i] = PlayerPrefs.GetInt("Trivia" +  i, 0);
            if(gotText[i] == 1)
            {
                isUnlocked[i] = true;
                triviaImg[i].sprite = close;
            }
        }
           
    }
    public void ShowDescription(int index)
    {
        descriptionBox.SetActive(true);
        if (!isActive[index])
        {
            description.text = "Play Missions to find trivia texts!";
            for (int i = 0; i < trivia.Length; i++)
            {
                trivia[i].localScale = new Vector2(1, 1);
                isActive[i] = false;

                if (isUnlocked[i])
                {
                    triviaImg[i].sprite = close;
                }
            }
            if (index == 0)
            {
                triviaTitle.text = "Trivia #1";
                if (gotText[0] == 1)
                {
                    description.text = "\n\nPlanting trees improves forest health. By planting the right species, reforestation helps makes our forests more resilient to future challenges like climate change and wildfire.";
                }

                if (isUnlocked[0])
                {
                    triviaImg[0].sprite = open;
                }
                trivia[0].localScale = new Vector2(1.2f, 1.2f);
            }
            else if (index == 1)
            {
                triviaTitle.text = "Trivia #2";
                if (gotText[1] == 1)
                {
                    description.text = "\n\nAs trees grow and consume air, they remove harmful pollutants from the air. Planting trees help re-establish forest cover and improve our \"natural air filter.\"";
                }

                if (isUnlocked[1])
                {
                    triviaImg[1].sprite = open;
                }
                trivia[1].localScale = new Vector2(1.2f, 1.2f);
            }
            else if (index == 2)
            {
                triviaTitle.text = "Trivia #3";
                if (gotText[2] == 1)
                {
                    description.text = "\n\nPlanting trees help sustain and increase the carbon sequestration potential of our forests, mitigating the effects of global climate change.";
                }

                if (isUnlocked[2])
                {
                    triviaImg[2].sprite = open;
                }
                trivia[2].localScale = new Vector2(1.2f, 1.2f);
            }
            else if (index == 3)
            {
                triviaTitle.text = "Trivia #4";
                if (gotText[3] == 1)
                {
                    description.text = "\n\nForests are vital!\n\nForests cover 30% of the earth’s land.";
                }

                if (isUnlocked[3])
                {
                    triviaImg[3].sprite = open;
                }
                trivia[3].localScale = new Vector2(1.2f, 1.2f);
            }
            else if (index == 4)
            {
                triviaTitle.text = "Trivia #5";
                if (gotText[4] == 1)
                {
                    description.text = "\n\nTrees are essential constituents of the ecosystem that absorb carbon.";
                }

                if (isUnlocked[4])
                {
                    triviaImg[4].sprite = open;
                }
                trivia[4].localScale = new Vector2(1.2f, 1.2f);
            }
            else if (index == 5)
            {
                triviaTitle.text = "Trivia #6";
                if (gotText[5] == 1)
                {
                    description.text = "\n\nDid you know?\n\nThe flower that Aspen summons on one of his skills is called \"Angel's Trumpet\". However, don't get fooled by their large, fragrant flowers! All parts of angel's trumpets are considered poisonous and ingestion of this flower can cause disturbing hallucinations, paralysis, tachycardia, memory loss and can sometimes be fatal!";
                }

                if (isUnlocked[5])
                {
                    triviaImg[5].sprite = open;
                }
                trivia[5].localScale = new Vector2(1.2f, 1.2f);
            }
            else if (index == 6)
            {
                triviaTitle.text = "Trivia #7";
                if (gotText[6] == 1)
                {
                    description.text = "";
                }

                if (isUnlocked[6])
                {
                    triviaImg[6].sprite = open;
                }
                trivia[6].localScale = new Vector2(1.2f, 1.2f);
            }
            else if (index == 7)
            {
                triviaTitle.text = "Trivia #8";
                if (gotText[7] == 1)
                {
                    description.text = "";
                }

                if (isUnlocked[7])
                {
                    triviaImg[7].sprite = open;
                }
                trivia[7].localScale = new Vector2(1.2f, 1.2f);
            }
            else if (index == 8)
            {
                triviaTitle.text = "Trivia #9";
                if (gotText[8] == 1)
                {
                    description.text = "";
                }

                if (isUnlocked[8])
                {
                    triviaImg[8].sprite = open;
                }
                trivia[8].localScale = new Vector2(1.2f, 1.2f);

            }
            else if (index == 9)
            {
                triviaTitle.text = "Trivia #10";
                if (gotText[9] == 1)
                {
                    description.text = "";
                }

                if (isUnlocked[9])
                {
                    triviaImg[9].sprite = open;
                }
                trivia[9].localScale = new Vector2(1.2f, 1.2f);
            }
            isActive[index] = true;
        }
        else
        {
            for (int i = 0; i < trivia.Length; i++)
            {
                trivia[i].localScale = new Vector2(1, 1);
                if (isUnlocked[i])
                {
                    triviaImg[i].sprite = close;
                }
            }
            isActive[index] = false;
            descriptionBox.SetActive(false);
        }
    }
}
