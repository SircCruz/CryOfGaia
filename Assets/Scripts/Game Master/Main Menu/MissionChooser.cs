using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MissionChooser : MonoBehaviour
{
    public GameObject mainMenuCanvas;
    public Slider loadingSlider;
    public InteractionManager interactionManager;
    public GameObject parallaxBG;
    public GameObject missionSelect;
    public Image container;
    public TextMeshProUGUI description;
    public GameObject coins;

    public Image enemyContainer1, enemyContainer2, enemyContainer3;
    public Sprite[] enemies;

    public Button btnStart;
    public Image imgStart;
    public TextMeshProUGUI txtStart;
    public TextMeshProUGUI txtLoadingValue;
    AsyncOperation operation;

    int mission = 0;
    // Start is called before the first frame update
    void Start()
    {
        container.enabled = false;
        description.enabled = false;
        btnStart.enabled = false;
        imgStart.enabled = false;
        txtStart.enabled = false;
        coins.SetActive(false);
        enemyContainer1.enabled = false;
        enemyContainer2.enabled = false;

        mission = 0;
    }
    public void C1Mission1()
    {
        container.enabled = true;
        description.enabled = true;
        btnStart.enabled = true;
        imgStart.enabled = true;
        txtStart.enabled = true;
        coins.SetActive(true);

        if(interactionManager.selectedCampaign == 1)
        {
            mission = 1;
            description.text = "Reward:   500\nEnemy: \n\nTask: Plant and grow three (3) trees.";
            enemyContainer1.enabled = true;
            enemyContainer2.enabled = false;
            enemyContainer3.enabled = false;
            enemyContainer1.sprite = enemies[0];
        }
        else if (interactionManager.selectedCampaign == 2)
        {
            mission = 4;
            description.text = "Reward:   500\n\nTask: Reach the ozone layer thickness level to 100%.";
        }
    }
    public void C1Mission2()
    {
        container.enabled = true;
        description.enabled = true;
        btnStart.enabled = true;
        imgStart.enabled = true;
        txtStart.enabled = true;
        coins.SetActive(true);

        if(interactionManager.selectedCampaign == 1)
        {
            mission = 2;
            description.text = "Reward:   500\nEnemies:\n\nTask: Protect the trees against the enemies for five minutes!";
            enemyContainer1.enabled = true;
            enemyContainer2.enabled = true;
            enemyContainer3.enabled = false;
            enemyContainer1.sprite = enemies[0];
            enemyContainer2.sprite = enemies[1];
        }
        else if (interactionManager.selectedCampaign == 2)
        {
            mission = 5;
            description.text = "Reward:   500\n\nTask: Collect and Put 200 trashes into the trash bin.";
        }
    }
    public void C1Mission3()
    {
        container.enabled = true;
        description.enabled = true;
        btnStart.enabled = true;
        imgStart.enabled = true;
        txtStart.enabled = true;
        coins.SetActive(true);

        if (interactionManager.selectedCampaign == 1)
        {
            mission = 3;
            description.text = "Reward: 1000\nEnemy:\n\nTask: Defeat the Chainsaw Boss.";
            enemyContainer1.enabled = false;
            enemyContainer2.enabled = false;
            enemyContainer3.enabled = true;
            enemyContainer1.sprite = enemies[2];
        }
        else if (interactionManager.selectedCampaign == 2)
        {
            mission = 6;
            description.text = "Reward: 1000\n\nTask: Defeat the Factory Boss.";
        }
    }
    public void StartMission()
    {
        missionSelect.SetActive(false);
        parallaxBG.SetActive(false);
        interactionManager.loadingCanvas.SetActive(true);
        StartCoroutine(LoadAsync(mission));
    }
    public void Plantita()
    {
        mainMenuCanvas.SetActive(false);
        parallaxBG.SetActive(false);
        interactionManager.loadingCanvas.SetActive(true);
        StartCoroutine(LoadPlantita());
    }
    public void Survival()
    {
        mainMenuCanvas.SetActive(false);
        parallaxBG.SetActive(false);
        interactionManager.loadingCanvas.SetActive(true);
        StartCoroutine(LoadSurvival());
    }
    public void Credits()
    {
        StartCoroutine(LoadCredits());
    }
    IEnumerator LoadPlantita()
    {
        operation = SceneManager.LoadSceneAsync("PlantitaScene");
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            loadingSlider.value = progress;
            if (operation.progress >= 0.9f)
            {
                operation.allowSceneActivation = true;
            }
            float text = progress * 100;
            txtLoadingValue.text = text.ToString("f0") + "%";

            yield return null;
        }
    }
    IEnumerator LoadSurvival()
    {
        operation = SceneManager.LoadSceneAsync("SurvivalScene");
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            loadingSlider.value = progress;
            if (operation.progress >= 0.9f)
            {
                operation.allowSceneActivation = true;
            }
            float text = progress * 100;
            txtLoadingValue.text = text.ToString("f0") + "%";

            yield return null;
        }
    }
    IEnumerator LoadCredits()
    {
        operation = SceneManager.LoadSceneAsync("CreditsScene_Yesterday");
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            loadingSlider.value = progress;
            if (operation.progress >= 0.9f)
            {
                operation.allowSceneActivation = true;
            }
            float text = progress * 100;
            txtLoadingValue.text = text.ToString("f0") + "%";

            yield return null;
        }
    }
    public IEnumerator LoadAsync(int mission)
    {
        if (mission == 0)
        {
            mainMenuCanvas.SetActive(false);
            parallaxBG.SetActive(false);
            operation = SceneManager.LoadSceneAsync("TutorialScene");
        }
        else if(mission == 1)
        {
            operation = SceneManager.LoadSceneAsync("C1Mission1Scene");
        }
        else if (mission == 2)
        {
            operation = SceneManager.LoadSceneAsync("C1Mission2Scene");
        }
        else if (mission == 3)
        {
            operation = SceneManager.LoadSceneAsync("C1BossMission");
        }
        else if (mission == 4)
        {
            operation = SceneManager.LoadSceneAsync("C2Mission1Scene");
        }
        else if (mission == 5)
        {
            operation = SceneManager.LoadSceneAsync("C2Mission2Scene");
        }
        else if (mission == 6)
        {
            operation = SceneManager.LoadSceneAsync("C2BossMission");
        }
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            loadingSlider.value = progress;
            if (operation.progress >= 0.9f)
            {
                operation.allowSceneActivation = true;
            }
            float text = progress * 100;
            txtLoadingValue.text = text.ToString("f0") + "%";

            yield return null;
        }
    }
}
