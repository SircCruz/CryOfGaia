using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InteractionManager : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject campaignSelect;
    public GameObject missionSelect;
    public GameObject loadingCanvas;
    public GameObject achievementCanvas;
    public GameObject triviaCanvas;
    public GameObject optionsPanel;
    public GameObject exitPanel;
    public GameObject playButtonPressed;

    public GameObject c1, c2, c3, c4;
    public int selectedCampaign = 1;

    //fade animation
    public RectTransform campaignCanvas;
    private float moveDuration = 0.3f;
    private float fadeEffect = 0.3f;
    private float restoreMoveDuration;
    private bool allowNexting = true;
    private bool fadeIn = false;
    private bool fadeOut = false;
    private bool left = false;
    private bool right = false;
    public Button play;
    public Image playIcon;
    public Image playIcon2;
    public Image playIconPressed;

    public Image campaignPanel;
    public Sprite campaign1;
    public Sprite campaign2;
    public Sprite comingSoon;

    public Image achievements;
    public Sprite achievementButton;
    public Sprite achievementPressed;

    public Image trivia;
    public Sprite triviaButton;
    public Sprite triviaPressed;

    public Image options;
    public Sprite optionsButton;
    public Sprite optionPressed;
    void Start()
    {
        if(PlayerPrefs.GetInt("Back To Mission", 0) == 0)
        {
            loadingCanvas.SetActive(false);
            mainMenuPanel.SetActive(true);
            campaignSelect.SetActive(false);
            missionSelect.SetActive(false);
            achievementCanvas.SetActive(false);
            optionsPanel.SetActive(false);
            playButtonPressed.SetActive(false);
            c1.SetActive(false);
            c2.SetActive(false);
            c3.SetActive(false);
            c4.SetActive(false);
        }
        else
        {
            loadingCanvas.SetActive(false);
            mainMenuPanel.SetActive(false);
            campaignSelect.SetActive(false);
            missionSelect.SetActive(true);
            achievementCanvas.SetActive(false);
            optionsPanel.SetActive(false);
            playButtonPressed.SetActive(false);
            c1.SetActive(false);
            c2.SetActive(false);
            c3.SetActive(false);
            c4.SetActive(false);
            PlayerPrefs.SetInt("Back To Mission", 0);
        }

        restoreMoveDuration = moveDuration;
    }
    void FixedUpdate()
    {
        if (!allowNexting)
        {
            fadeOut = true;
            moveDuration -= Time.fixedDeltaTime;
            if (right)
            {
                campaignCanvas.position = new Vector3(campaignCanvas.position.x - 0.1f, campaignCanvas.position.y);
            }
            if (left)
            {
                campaignCanvas.position = new Vector3(campaignCanvas.position.x + 0.1f, campaignCanvas.position.y);
            }
            if (moveDuration <= 0)
            {
                allowNexting = true;
                moveDuration = restoreMoveDuration;
                campaignCanvas.position = new Vector3(0, campaignCanvas.position.y);
                left = false;
                right = false;
            }
        }
        if (right)
        {
            if (campaignCanvas.position.x <= -0.7f)
            {
                campaignCanvas.position = new Vector3(0.7f, campaignCanvas.position.y);
                UpdateText();
                UpdateTitle();
            }
        }
        if (left)
        {
            if (campaignCanvas.position.x >= 0.7f)
            {
                campaignCanvas.position = new Vector3(-0.7f, campaignCanvas.position.y);
                UpdateText();
                UpdateTitle();
            }
        }
        if (fadeOut)
        {
            if (!allowNexting)
            {
                fadeEffect -= 0.11f;
                campaignPanel.color = new Color(255, 255, 255, fadeEffect);
                playIcon.color = new Color(255, 255, 255, fadeEffect);
                playIcon2.color = new Color(255, 255, 255, fadeEffect);
                playIconPressed.color = new Color(255, 255, 255, fadeEffect);
                if (fadeEffect <= 0)
                {
                    fadeOut = false;
                    fadeIn = true;
                }
            }
        }
        if (fadeIn)
        {
            fadeEffect += 0.08f;
            campaignPanel.color = new Color(255, 255, 255, fadeEffect);
            playIcon.color = new Color(255, 255, 255, fadeEffect);
            playIcon2.color = new Color(255, 255, 255, fadeEffect);
            playIconPressed.color = new Color(255, 255, 255, fadeEffect);
            if (fadeEffect >= 0.3f)
            {
                campaignPanel.color = new Color(255, 255, 255, 255);
                playIcon.color = new Color(255, 255, 255, 255);
                playIcon2.color = new Color(255, 255, 255, 255);
                playIconPressed.color = new Color(255, 255, 255, 255);
                fadeIn = false;
            }
        }
    }
    public void gotoMain()
    {
        Start();
    }
    public void gotoCampaign()
    {
        if(PlayerPrefs.GetInt("Tutorial", 0) == 1)
        {
            mainMenuPanel.SetActive(false);
            campaignSelect.SetActive(true);
            missionSelect.SetActive(false);
            gotoSelectedCampaign(false);
            UpdateText();
        }
    }
    public void gotoMission()
    {
        mainMenuPanel.SetActive(false);
        campaignSelect.SetActive(false);
        missionSelect.SetActive(true);

    }
    public void StartMission()
    {

    }
    public void NextPage()
    {
        if (allowNexting)
        {
            allowNexting = false;
            right = true;
            if (selectedCampaign == 4)
            {
                selectedCampaign = 1;
            }
            else
            {
                selectedCampaign += 1;
            }
            gotoSelectedCampaign(false);
        }
    }
    public void PreviousPage()
    {
        if (allowNexting)
        {
            allowNexting = false;
            left = true;
            if (selectedCampaign == 1)
            {
                selectedCampaign = 4;
            }
            else
            {
                selectedCampaign -= 1;
            }
            gotoSelectedCampaign(true);
        }
    }
    public void gotoSelectedCampaign(bool isTapped)
    {
        if (isTapped)
        {
            GetImageOnButtonClick.SetImage(true, playButtonPressed);
        }
        else if(!isTapped)
        {
            GetImageOnButtonClick.SetImage(false, playButtonPressed);
        }
        if(selectedCampaign == 1)
        {
            c1.SetActive(true);
            c2.SetActive(false);
            c3.SetActive(false);
            c4.SetActive(false);
        }
        else if (selectedCampaign == 2)
        {
            c1.SetActive(false);
            c2.SetActive(true);
            c3.SetActive(false);
            c4.SetActive(false);
        }
        else if (selectedCampaign == 3)
        {
            c1.SetActive(false);
            c2.SetActive(false);
            c3.SetActive(true);
            c4.SetActive(false);
        }
        else if (selectedCampaign == 4)
        {
            c1.SetActive(false);
            c2.SetActive(false);
            c3.SetActive(false);
            c4.SetActive(true);
        }
    }
    private void UpdateTitle()
    {
        if (selectedCampaign == 1)
        {
            campaignPanel.sprite = campaign1;
        }
        else if (selectedCampaign == 2)
        {
            campaignPanel.sprite = campaign2;
        }
        else if (selectedCampaign == 3)
        {
            campaignPanel.sprite = comingSoon;
        }
        else if (selectedCampaign == 4)
        {
            campaignPanel.sprite = comingSoon;
        }
    }
    private void UpdateText()
    {
        if (selectedCampaign == 1)
        {
            play.enabled = true;
            playIcon.enabled = true;
            playIcon2.enabled = true;
            playIconPressed.enabled = true;
            playButtonPressed.SetActive(false);
        }
        else if (selectedCampaign == 2)
        {
            play.enabled = false;
            playIcon.enabled = false;
            playIcon2.enabled = false;
            playIconPressed.enabled = false;
            playButtonPressed.SetActive(false);
        }
        else if (selectedCampaign == 3)
        {
            play.enabled = false;
            playIcon.enabled = false;
            playIcon2.enabled = false;
            playIconPressed.enabled = false;
        }
        else if (selectedCampaign == 4)
        {
            play.enabled = false;
            playIcon.enabled = false;
            playIcon2.enabled = false;
            playIconPressed.enabled = false;
        }
    }
    public void OpenAchievement()
    {
        achievementCanvas.SetActive(true);
    }
    public void OpenAchievementDown()
    {
        achievements.sprite = achievementPressed;
    }
    public void OpenAchievementExit()
    {
        achievements.sprite = achievementButton;
    }
    public void OpenTrivia()
    {
        triviaCanvas.SetActive(true);
    }
    public void OpenTriviaDown()
    {
        trivia.sprite = triviaPressed;
    }
    public void OpenTriviExit()
    {
        trivia.sprite = triviaButton;
    }
    public void OpenOptions()
    {
        optionsPanel.SetActive(true);
    }
    public void OpenOptionsDown()
    {
        options.sprite = optionPressed;
    }
    public void OpenOptionsExit()
    {
        options.sprite = optionsButton;
    }
    public void CloseOptions()
    {
        optionsPanel.SetActive(false);
    }
    public void CloseTrivia()
    {
        triviaCanvas.SetActive(false);
    }
    public void CloseAchievements()
    {
        achievementCanvas.SetActive(false);
    }
    public void OpenExitPanel()
    {
        exitPanel.SetActive(true);
    }
    public void CloseExitPanel()
    {
        exitPanel.SetActive(false);
    }
}
