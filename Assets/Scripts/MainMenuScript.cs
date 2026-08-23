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
    public List<Image> settingsImages;
    public List<TextMeshProUGUI> settingsTexts;
    public List<Slider> settingsSliders;
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
        //TODO wipe data & configs
        ConfigManager.instance.SaveConfigs();
        SceneManager.LoadScene("SampleScene");
    }

    public void LoadGame()
    {
        //TODO load data & configs
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
            //list o. images. includes slider components
            foreach (Image image in settingsImages)
            {
                image.color = new Color(0.9F, 0.9F, 0.675F, 1);
            }
            //list of texts (menu could have more than 1)
            foreach (TextMeshProUGUI text in settingsTexts)
            {
                text.color = new Color(0.9F, 0.9F, 0.675F, 1);
            }
            //slider visuals are covered by the images list. just dissable interactions
            foreach (Slider slider in settingsSliders)
            {
                slider.interactable = true;
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
        //TODO have a config object w. sensitivity to save 4 later
    }

    //gotta get em configs
    public void LoadConfigs(Configs configs)
    {
        sensitivitySlider.value = configs.turnRateConfig;
        SensitivityUpdate();
    }

    public void SaveConfigs(ref Configs configs)
    {
        configs.turnRateConfig = sensitivitySlider.value;
    }
}
