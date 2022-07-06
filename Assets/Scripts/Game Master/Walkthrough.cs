using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class Walkthrough : MonoBehaviour
{
    public bool repeatable;
    public string mapName;

    public GameObject[] controls;
    public TriviaText text;
    public GenerateEnemy genEnemy;
    public CharacterAttacks charaAttacks;

    public Image imageGuide;
    public Sprite[] images;
    public int[] imageQueue;
    [Space]
    public Image aspenGuide;
    public Sprite[] aspen;
    public int[] aspenQueue;
    public GameObject aspenName;

    public string[] dialogues;
    private char[] letters;
    

    public GameObject next;
    public TextMeshProUGUI dialogue;

    private int behaviorType = 0;
    private int counter = 0;

    private bool isLastDialogue;

    private int firstTimer;

    private void OnEnable()
    {
        GameMode();
    }
    private void FixedUpdate()
    {
        if(counter == dialogues.Length - 1)
        {
            isLastDialogue = true;
        }
        
    }
    private void Start()
    {
        firstTimer = PlayerPrefs.GetInt(mapName, 0);
        if (firstTimer == 1)
        {
            if (!repeatable)
            {
                gameObject.SetActive(false);
            }
        }
        ClearText();

        letters = dialogues[counter].ToCharArray();
        StartCoroutine(DisplayText());
    }
    IEnumerator DisplayText()
    {
        for(int i = 0; i < letters.Length; i++)
        {
            dialogue.text += letters[i];
            yield return new WaitForFixedUpdate();
        }
        next.SetActive(true);
        behaviorType = 1;
    }
    void ImmediateDisplayText()
    {
        dialogue.text = dialogues[counter];
        next.SetActive(true);
        behaviorType = 1;
    }
    void NextDialogue()
    {
        if (!isLastDialogue)
        {
            behaviorType = 0;

            ClearText();

            counter += 1;
            letters = dialogues[counter].ToCharArray();
            StartCoroutine(DisplayText());
        }
        else
        {
            PlayerPrefs.SetInt("First Timer", 1);
            gameObject.SetActive(false);
        }
    }
    void ClearText()
    {
        next.SetActive(false);
        dialogue.text = "";
    }
    public void OnDialogueClick()
    {
        if(behaviorType == 0)
        {
            //ImmediateDisplayText();
        }
        if(behaviorType == 1)
        {
            NextDialogue();
            ImageGuide();
            AspenGuide();
        }
    }
    void ImageGuide()
    {
        try
        {
            imageGuide.color = new Color(255, 255, 255, 255);
            imageGuide.sprite = images[imageQueue[counter]];
        }
        catch(System.IndexOutOfRangeException e)
        {
            imageGuide.color = new Color(255, 255, 255, 0);
        }  
    }
    void AspenGuide()
    {
        try
        {
            aspenGuide.color = new Color(255, 255, 255, 255);
            aspenGuide.sprite = aspen[aspenQueue[counter]];
            aspenName.SetActive(true);
        }
        catch (System.IndexOutOfRangeException e)
        {
            aspenGuide.color = new Color(255, 255, 255, 0);
            aspenName.SetActive(false);
        }
    }
    public void GameMode()
    {
        for (int i = 0; i < controls.Length; i++)
        {
            controls[i].SetActive(!charaAttacks.enabled);
        }

        if(text != null)
        {
            text.enabled = !text.enabled;
        }
        if(genEnemy != null)
        {
            genEnemy.enabled = !genEnemy.enabled;
        }
        charaAttacks.enabled = !charaAttacks.enabled;
    }
    private void OnDisable()
    {
        GameMode();
    }
}
