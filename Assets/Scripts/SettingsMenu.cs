using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public GameObject optionsPanel;

    public Dropdown resolutionDropdown;
    Resolution[] resolutions;

    private void Start()
    {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }


    //ABRIR Y CERRAR
    public void ShowOptions()
    {
        optionsPanel.SetActive(true);
    }
    public void CloseOptions()
    {
        optionsPanel.SetActive(false);
    }


    //AUDIO
    public void SetGeneralVolume(float volume)
    {
        audioMixer.SetFloat("myvolume", volume);
    }
    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("mymusicvolume", volume);
    }
    public void SetSoundsVolume(float volume)
    {
        audioMixer.SetFloat("mysoundsvolume", volume);
    }

    //CALIDAD
    public void SetQuality (int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    //PANTALLA COMPLETA

    public void SetFullScreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    //RESOLUCION
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
   
}
