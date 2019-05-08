using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public AudioMixer menuVolume;
    Resolution[] resolutions;

    public Dropdown resolutionDropdown;
    public Toggle fullScreenToggle;
    public Dropdown qualityDropdown;
    public Slider volumeSlider;

    private void Start()
    {
        addResOptions();
        fullScreenToggle.isOn = Screen.fullScreen;

        qualityDropdown.value = QualitySettings.GetQualityLevel();
    }

    public void setVolume(float volume)
    {
        menuVolume.SetFloat("Volume", volume);
    }

    public void adjustGraphicQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void setFullScreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen; 
    }

    private void addResOptions()
    {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if(resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void setRes(int resIndex)
    {
        Resolution resolution = resolutions[resIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}