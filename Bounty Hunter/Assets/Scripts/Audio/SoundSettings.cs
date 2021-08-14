using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSettings : MonoBehaviour
{
    AudioManager audioManager;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider SFXSlider;
    [SerializeField] Slider masterSlider;
    [SerializeField] VolumeSettingsSO volumeSO;

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
        SetSliderValues();
    }

    void SetSliderValues()
    {
        musicSlider.value = volumeSO.musicVolume;
        SetMasterVolume(musicSlider.value);

        SFXSlider.value = volumeSO.SFXVolume;
        SetMasterVolume(SFXSlider.value);

        masterSlider.value = volumeSO.masterVolume;
        SetMasterVolume(masterSlider.value);
    }

    public void SetMasterVolume(float volume)
    {
        if(audioManager != null)
        {
            audioManager.SetAudioParameter("Volume_Master", volume);
            volumeSO.masterVolume = volume;

        }
    }
    public void SetMusicVolume(float volume)
    {
        if (audioManager != null)
        {
            audioManager.SetAudioParameter("Volume_Music", volume);
            volumeSO.musicVolume = volume;
        }
    }
    public void SetSFXVolume(float volume)
    {
        if (audioManager != null)
        {
            audioManager.SetAudioParameter("Volume_SFX", volume);
            volumeSO.SFXVolume = volume;

        }
    }
}
