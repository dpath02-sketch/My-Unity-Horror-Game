using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour, IConfigs
{
    public string StartScene = "SampleScene";
    public Slider sensitivitySlider;
    public TextMeshProUGUI sensitivityText;
    public Slider fovSlider;
    public TextMeshProUGUI fovText;
    public Toggle sprintToggle;
    public List<Image> settingsImages;
    public List<TextMeshProUGUI> settingsTexts;
    public List<Slider> settingsSliders;
    public List<Toggle> settingsToggles;
    private bool showSettings = true;
    // Start is called before the first frame update
    void Start()
    {
        ShowSettinngs();
        Cursor.lockState = CursorLockMode.None;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewGame()
    {
        //TODO Wipe data, save configs
        ConfigManager.instance.SaveConfigs();
        SceneManager.LoadScene("SampleScene");
    }

    public void LoadGame()
    {
        //TODO save configs, data should be saved and loaded by other scene. no point loading now.
        ConfigManager.instance.SaveConfigs();
        SceneManager.LoadScene("SampleScene");
    }

    public void ShowSettinngs()
    {
        if (showSettings)
        {
            showSettings = false;
        }
        else
        {
            showSettings = true;
        }
        if (showSettings)
        {
            // List o. images. includes slider components
            foreach (Image image in settingsImages)
            {
                image.color = new Color(0.9F, 0.9F, 0.675F, 1);
            }
            // List of texts (menu could have more than 1)
            foreach (TextMeshProUGUI text in settingsTexts)
            {
                text.color = new Color(0.9F, 0.9F, 0.675F, 1);
            }
            // Slider visuals are covered by the images list. just dissable interactions
            foreach (Slider slider in settingsSliders)
            {
                slider.interactable = true;
            }
            foreach (Toggle toggle in settingsToggles)
            {
                toggle.interactable = true;
            }
        }
        else
        {
             foreach (Image image in settingsImages)
            {
                image.color = new Color(0, 0, 0, 0);
            }
            foreach (TextMeshProUGUI text in settingsTexts)
            {
                text.color = new Color(0, 0, 0, 0);
            }
            foreach (Slider slider in settingsSliders)
            {
                slider.interactable = false;
            }
            foreach (Toggle toggle in settingsToggles)
            {
                toggle.interactable = false;
            }
        }
    }

    public void QuitGame()
    {
        ConfigManager.instance.SaveConfigs();
        Application.Quit();
    }

    public void SensitivityUpdate()
    {
        sensitivityText.text = (sensitivitySlider.value / 10).ToString("0.0") + " degrees/pixel";
    }

    public void FovUpdate()
    {
        fovText.text = fovSlider.value.ToString() + " degrees";
    }

    //gotta get em configs
    public void LoadConfigs(Configs configs)
    {
        sensitivitySlider.value = configs.turnRateConfig;
        SensitivityUpdate();
        fovSlider.value = configs.fovConfig;
        FovUpdate();
        sprintToggle.isOn = configs.sprintToggle;
    }

    public void SaveConfigs(ref Configs configs)
    {
        configs.turnRateConfig = sensitivitySlider.value;
        configs.fovConfig = fovSlider.value;
        configs.sprintToggle = sprintToggle.isOn;
    }
}
