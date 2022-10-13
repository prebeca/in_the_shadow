using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public Player player = null;
    public BetterMenu menu = null;

    // Sound
    public AudioMixer audioMixer;
    public Slider volumeSlider;

    // Resolution
    List<Resolution> resolutions;
    public TMP_Dropdown resolutionDropdown;

    // Screen resolution
    List<FullScreenMode> modes = new List<FullScreenMode>();
    public TMP_Dropdown modeDropdown;

    // Quality
    public TMP_Dropdown qualityDropdown;
    public TMP_Dropdown vSyncDropdown;

    void Start()
    {
        // quality
        qualityDropdown.value = QualitySettings.GetQualityLevel();
        qualityDropdown.RefreshShownValue();

        // vsync
        setVSync(PlayerPrefs.GetInt("vsync", QualitySettings.vSyncCount));
        vSyncDropdown.value = QualitySettings.vSyncCount;
        vSyncDropdown.RefreshShownValue();

        // volume
        float currentVolume = PlayerPrefs.GetFloat("volume", 0);
        volumeSlider.value = currentVolume;
        audioMixer.SetFloat("volume", currentVolume);
        PlayerPrefs.SetFloat("volume", currentVolume);

        // fullscreen mode
        modeDropdown.ClearOptions();

        // modes.Add(FullScreenMode.ExclusiveFullScreen);
        modes.Add(FullScreenMode.FullScreenWindow);
        // modes.Add(FullScreenMode.MaximizedWindow);
        modes.Add(FullScreenMode.Windowed);

        List<string> modesStr = new List<string>();
        modesStr.Add("Full Screen");
        modesStr.Add("Windowed");

        int modeIndex = 0;
        for (int i = 0; i < modes.Count; ++i)
        {
            if (modes[i] == Screen.fullScreenMode)
                modeIndex = i;
        }

        modeDropdown.AddOptions(modesStr);
        modeDropdown.value = modeIndex;
        modeDropdown.RefreshShownValue();

        // resolution
        Resolution savedResolution = new Resolution();
        savedResolution.width = PlayerPrefs.GetInt("resWidth", Screen.currentResolution.width);
        savedResolution.height = PlayerPrefs.GetInt("resHeight", Screen.currentResolution.height);
        savedResolution.refreshRate = PlayerPrefs.GetInt("refreshRate", Screen.currentResolution.refreshRate);

        Screen.SetResolution(savedResolution.width, savedResolution.height, Screen.fullScreenMode, savedResolution.refreshRate);

        resolutions = new List<Resolution>(Screen.resolutions);
        resolutions.Reverse();

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResIndex = 0;

        for (int i = 0, index = 0; i < resolutions.Count; ++i)
        {
            string option = resolutions[i].width + "*" + resolutions[i].height;
            if (options.Contains(option))
            {
                resolutions.RemoveAt(i--);
                continue;
            }
            options.Add(option);

            if (resolutions[i].width == savedResolution.width &&
                resolutions[i].height == savedResolution.height)
                currentResIndex = index;
            ++index;
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void setResolution(int resIndex)
    {
        Resolution resolution = resolutions[resIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreenMode);
        PlayerPrefs.SetInt("resWidth", resolution.width);
        PlayerPrefs.SetInt("resHeight", resolution.height);
        PlayerPrefs.SetInt("refreshRate", resolution.refreshRate);
    }

    public void setFullScreenMode(int modeIndex)
    {
        Screen.fullScreenMode = modes[modeIndex];

        // int currentResIndex = 0;

        // for (int i = 0; i < resolutions.Count; ++i)
        // {
        //     if (resolutions[i].width == Screen.currentResolution.width &&
        //         resolutions[i].height == Screen.currentResolution.height)
        //         currentResIndex = i;
        // }
        // resolutionDropdown.value = currentResIndex;
        // resolutionDropdown.RefreshShownValue();
    }

    public void setVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
        PlayerPrefs.SetFloat("volume", volume);
    }

    public void setQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex, false);
    }

    public void setVSync(int vSyncIndex)
    {
        QualitySettings.vSyncCount = vSyncIndex;
        PlayerPrefs.SetInt("vsync", QualitySettings.vSyncCount);
    }

    public void toMenu()
    {
        player.moveTo(menu.transform.position);
    }
}
