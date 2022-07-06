using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    WorldPhysics freeze;
    public bool isQuitting;

    private void Start()
    {
        freeze = gameObject.GetComponent<WorldPhysics>();
    }
    public void MainMenu(bool startAtMissionSelect)
    {
        if (startAtMissionSelect)
        {
            PlayerPrefs.SetInt("Back To Mission", 1);
        }
        SceneManager.LoadScene("MainMenuScene");
        freeze.stopPhysics = false;
        isQuitting = true;
    }
    public void Campaign()
    {
        SceneManager.LoadScene("CampaignSelect");
    }
    public void Campaign1()
    {
        SceneManager.LoadScene("MissionSelect");
    }
    public void ReloadScene()
    {
        isQuitting = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
