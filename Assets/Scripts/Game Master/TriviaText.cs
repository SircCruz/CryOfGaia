using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TriviaText : MonoBehaviour
{
    WorldPhysics world;

    public GameObject trivia;
    public TextMeshProUGUI triviaNumber;
    public TextMeshProUGUI triviaDescription;
    public TextMeshProUGUI counter;
    public Image counterImage;

    public int numCounter;
    int totalTriviaCounter;
    private void Start()
    {
        counter = GameObject.Find("Counter").GetComponent<TextMeshProUGUI>();
        counterImage = GameObject.Find("CounterImage").GetComponent<Image>();
        world = GameObject.Find("Game Master").GetComponent<WorldPhysics>();

        counterImage.enabled = false;

        totalTriviaCounter = PlayerPrefs.GetInt("Total Trivia", 1);

        trivia.SetActive(false);
    }
    private void Update()
    {
        counter.text = numCounter.ToString("#");
        if(numCounter == 0)
        {
            counterImage.enabled = false;
        }
        else
        {
            counterImage.enabled = true;
        }
    }
    public void ShowTrivia()
    {
        if(numCounter != 0)
        {
            world.stopPhysics = true;
            trivia.SetActive(true);
            triviaNumber.text = "Trivia";
            if (totalTriviaCounter == 1)
            {
                triviaNumber.text = "Did you know?";
                triviaDescription.text = "The flower that Aspen summons on one of his skills is called \"Angel's Trumpet\". However, don't get fooled by their large, fragrant flowers! All parts of angel's trumpets are considered poisonous and ingestion of this flower can cause disturbing hallucinations, paralysis, tachycardia, memory loss and can sometimes be fatal!";
                totalTriviaCounter++;
            }
            else if(totalTriviaCounter == 2)
            {
                triviaDescription.text = "\n\nPlanting trees improves forest health. By planting the right species, reforestation helps makes our forests more resilient to future challenges like climate change and wildfire.";
                totalTriviaCounter++;
            }
            else if (totalTriviaCounter == 3)
            {
                triviaDescription.text = "\n\nAs trees grow and consume air, they remove harmful pollutants from the air. Planting trees help re-establish forest cover and improve our \"natural air filter.\"";
                totalTriviaCounter++;
            }
            else if (totalTriviaCounter == 4)
            {
                triviaDescription.text = "\n\nPlanting trees help sustain and increase the carbon sequestration potential of our forests, mitigating the effects of global climate change.";
                totalTriviaCounter++;
            }
            else if (totalTriviaCounter == 5)
            {
                triviaDescription.text = "\n\nForests are vital!\n\nForests cover 30% of the earth’s land.";
                totalTriviaCounter++;
            }
            else if (totalTriviaCounter == 6)
            {
                triviaDescription.text = "\n\nTrees are essential constituents of the ecosystem that absorb carbon.";
                totalTriviaCounter++;
            }
            else if (totalTriviaCounter == 7)
            {
                triviaDescription.text = "";
                totalTriviaCounter++;
            }
            else if (totalTriviaCounter == 8)
            {
                triviaDescription.text = "";
                totalTriviaCounter++;
            }
            else if (totalTriviaCounter == 9)
            {
                triviaDescription.text = "";
                totalTriviaCounter++;
            }
            else if (totalTriviaCounter == 10)
            {
                triviaDescription.text = "";
                totalTriviaCounter++;
            }
            numCounter--;
            PlayerPrefs.SetInt("Total Trivia", totalTriviaCounter);
        }
    }
    public void CloseTrivia()
    {
        world.stopPhysics = false;
        trivia.SetActive(false);
    }
}
