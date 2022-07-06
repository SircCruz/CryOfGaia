using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Options : MonoBehaviour
{
    public AudioManager audioManager;
    AudioSource sound;
    public AudioSource music;
    public Slider soundSlider;
    public Slider musicSlider;
    public TextMeshProUGUI soundsTxt;
    public TextMeshProUGUI musicTxt;

    public Toggle notify1;
    public Toggle notify2;

    public Button applyBtn;

    public TMP_Dropdown renderScale;
    public TMP_Dropdown renderQuality;
    public TMP_Dropdown antiAlias;
    
    public Toggle enableHDR;
    int _enableHDR;

    void Start()
    {
        renderScale.value = PlayerPrefs.GetInt("Render Scale", 1);

        renderQuality.value = PlayerPrefs.GetInt("Render Quality", 2);
        RenderQualitySettings();

        antiAlias.value = PlayerPrefs.GetInt("Anti Alias", 0);

        CheckHDRSettings();

        sound = FindObjectOfType<AudioManager>().GetComponent<AudioSource>();
        StartAudioSettings();

        Apply();
    }

    void Update()
    {
        if (enableHDR.isOn)
        {
            _enableHDR = 1;
        }
        else
        {
            _enableHDR = 0;
        }

        UpdateAudioSettings();
        UpdateNotificationSettings();
    }
    void StartAudioSettings()
    {
        soundSlider.value = PlayerPrefs.GetFloat("Sound Volume", 0.1f);
        musicSlider.value = PlayerPrefs.GetFloat("Music Volume", 0.1f);
    }
    void UpdateAudioSettings()
    {
        for (int i = 0; i < audioManager.sounds.Length; i++)
        {
            PlayerPrefs.SetFloat("Sound Volume", soundSlider.value);
            audioManager.sounds[i].volume = soundSlider.value;
        }
        PlayerPrefs.SetFloat("Music Volume", musicSlider.value);
        music.volume = musicSlider.value;

        sound.volume = soundSlider.value;

        if (soundSlider.value <= 0)
        {
            soundsTxt.text = "0";
        }
        else
        {
            soundsTxt.text = (soundSlider.value * 100).ToString("##.#");
        }
        if (musicSlider.value <= 0)
        {
            musicTxt.text = "0";
        }
        else
        {
            musicTxt.text = (musicSlider.value * 100).ToString("##.#");
        }
    }
    void UpdateNotificationSettings()
    {
        if (notify1.isOn)
        {
            PlayerPrefs.SetInt("Notify1", 1);
        }
        else
        {
            PlayerPrefs.SetInt("Notify1", 0);
        }
        if (notify2.isOn)
        {
            PlayerPrefs.SetInt("Notify2", 1);
        }
        else
        {
            PlayerPrefs.SetInt("Notify2", 0);
        }
    }
    public void Apply()
    {
        RenderScaleSettings();
        RenderQualitySettings();
        HDRSettings();
        AntiAliasingSettings();
        SaveSettings();

        applyBtn.interactable = false;
    }
    void RenderScaleSettings()
    {
        var rpAsset = GraphicsSettings.renderPipelineAsset;
        var urpAsset = (UniversalRenderPipelineAsset)rpAsset;

        if (renderScale.value == 0)
        {
            urpAsset.renderScale = 0.5f;
        }
        else
        {
            urpAsset.renderScale = renderScale.value;
        }
    }
    void RenderQualitySettings()
    {
        QualitySettings.SetQualityLevel(renderQuality.value);
    }
    void CheckHDRSettings()
    {
        _enableHDR = PlayerPrefs.GetInt("HDR", 0);
        if (_enableHDR == 1)
        {
            enableHDR.isOn = true;
        }
        else
        {
            enableHDR.isOn = false;
        }
    }
    void HDRSettings()
    {
        var rpAsset = GraphicsSettings.renderPipelineAsset;
        var urpAsset = (UniversalRenderPipelineAsset)rpAsset;

        if (_enableHDR == 1)
        {
            urpAsset.supportsHDR = true;
        }
        else
        {
            urpAsset.supportsHDR = false;
        }
    }
    void AntiAliasingSettings()
    {
        var rpAsset = GraphicsSettings.renderPipelineAsset;
        var urpAsset = (UniversalRenderPipelineAsset)rpAsset;

        if(antiAlias.value == 0)
        {
            urpAsset.msaaSampleCount = 1;
        }
        if (antiAlias.value == 1)
        {
            urpAsset.msaaSampleCount = 2;
        }
        if (antiAlias.value == 2)
        {
            urpAsset.msaaSampleCount = 4;
        }
        if (antiAlias.value == 3)
        {
            urpAsset.msaaSampleCount = 8;
        }
    }
    public void SaveSettings()
    {
        PlayerPrefs.SetInt("Anti Alias", antiAlias.value);
        PlayerPrefs.SetInt("HDR", _enableHDR);
        PlayerPrefs.SetInt("Render Scale", renderScale.value);
        PlayerPrefs.SetInt("Render Quality", renderQuality.value);
    }
    public void EnableApply()
    {
        applyBtn.interactable = true;
    }
}
