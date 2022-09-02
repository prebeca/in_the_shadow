using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider volumeSlider;

    Resolution[] resolutions;
    public TMP_Dropdown resolutionDropdown;

    List<FullScreenMode> modes = new List<FullScreenMode>();
    public TMP_Dropdown modeDropdown;

    // Start is called before the first frame update
    void Start()
    {
        // volume
        float currentVolume = PlayerPrefs.GetFloat("volume", 0);
        volumeSlider.value = currentVolume;
        audioMixer.SetFloat("volume", currentVolume);
        PlayerPrefs.SetFloat("volume", currentVolume);

        // fullscreen mode
        modeDropdown.ClearOptions();

        modes.Add(FullScreenMode.ExclusiveFullScreen);
        modes.Add(FullScreenMode.FullScreenWindow);
        modes.Add(FullScreenMode.MaximizedWindow);
        modes.Add(FullScreenMode.Windowed);

        List<string> modesStr = new List<string>();
        int modeIndex = 0;
        for (int i = 0; i < modes.Count; ++i)
        {
            if (modes[i] == Screen.fullScreenMode)
                modeIndex = i;
            modesStr.Add(modes[i].ToString());
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

        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResIndex = 0;

        for (int i = 0; i < resolutions.Length; ++i)
        {
            string option = resolutions[i].width + "*" + resolutions[i].height + " " + resolutions[i].refreshRate + "Hz";
            options.Add(option);

            if (resolutions[i].width == savedResolution.width &&
                resolutions[i].height == savedResolution.height &&
                resolutions[i].refreshRate == savedResolution.refreshRate)
                currentResIndex = i;
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void setResolution(int resIndex)
    {
        Resolution resolution = resolutions[resIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreenMode, resolution.refreshRate);
        PlayerPrefs.SetInt("resWidth", resolution.width);
        PlayerPrefs.SetInt("resHeight", resolution.height);
        PlayerPrefs.SetInt("refreshRate", resolution.refreshRate);
    }

    public void setFullScreenMode(int modeIndex)
    {
        Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, modes[modeIndex], Screen.currentResolution.refreshRate);
    }

    public void setVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
        PlayerPrefs.SetFloat("volume", volume);
    }
}
