using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using UnityEngine.PlayerLoop;

public class SettingsTab : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;
    [SerializeField] private Slider mouseSensitivitySlider;
    [SerializeField] private RotatableCamera rotatableCamera;
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private TMP_Dropdown graphicsDropdown;
    [SerializeField] private TMP_Dropdown antiAliasingDropdown;
    
    private Resolution[] resolutions;
    private readonly int[] antiAliasing = { 0, 2, 4, 8 };
    
    void Start()
    {
        //musicVolumeSlider.value = SoundManager.instance.audioSource.volume;
        SetupResolution();
        SetupAntiAliasing();
        graphicsDropdown.value = QualitySettings.GetQualityLevel();
        graphicsDropdown.RefreshShownValue();
    }

    private void SetupResolution()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue(); 
    }

    private void SetupAntiAliasing()
    {
        int currentAA = QualitySettings.GetQualityLevel();
        int index = System.Array.IndexOf(antiAliasing, currentAA);
        antiAliasingDropdown.value = index;
        antiAliasingDropdown.RefreshShownValue();
    }
    
    public void MasterVolumeChanged()
    {
        float volume = masterVolumeSlider.value;
        audioMixer.SetFloat("Master", Mathf.Log(volume)*20);
    }

    public void MusicVolumeChanged()
    {
        float volume = musicVolumeSlider.value;
        audioMixer.SetFloat("Music", Mathf.Log(volume)*20);
    }

    public void SfxVolumeChanged()
    {
        float volume = sfxVolumeSlider.value;
        audioMixer.SetFloat("SFX", Mathf.Log10(volume)*20);
    }

    public void MouseSensitivityChanged()
    {
        if (rotatableCamera == null) return;
        float mouseSensitivity = mouseSensitivitySlider.value;
        rotatableCamera.mouseSensitivity = mouseSensitivity;
    }
    
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetVsync(bool isVsync)
    {
        QualitySettings.vSyncCount = isVsync ? 1 : 0;
    }

    public void SetAliasing(int index)
    {
        QualitySettings.antiAliasing = antiAliasing[index];
    }


    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    
}
