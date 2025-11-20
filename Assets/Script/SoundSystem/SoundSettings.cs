using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[System.Serializable]
public struct AudioSaveData
{
    public float masterVolume;
    public float musicVolume;
    public float soundFXVolume;

}

public class SoundSettings : MonoBehaviour
{
    public static SoundSettings instance;
    public AudioMixer audioMixer;

    public Slider masterSlider;
    public Slider musicSlider;
    public Slider SoundfxSlider;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        CallLoadAudioData();
    }

    public void UpdateMusicVolume(float volume)
    {
        audioMixer.SetFloat("VolumeMusic", volume);
    }

    public void UpdateSoundFXVolume(float volume)
    {
        audioMixer.SetFloat("VolumeSoundFX", volume);
    }

    public void UpdateMasterVolume(float volume)
    {
        audioMixer.SetFloat("VolumeMaster", volume);
    }

    public void CallSaveAudioData()
    {
        SaveSystem.Save();
    }

    public void CallLoadAudioData()
    {
        SaveSystem.Load();
    }

    public void Save(ref AudioSaveData data)
    {
        // Get Master Volume
        audioMixer.GetFloat("VolumeMaster", out float masterVol);
        data.masterVolume = masterVol;

        // Get Music Volume
        audioMixer.GetFloat("VolumeMusic", out float musicVol);
        data.musicVolume = musicVol;

        // Get Sound FX Volume
        audioMixer.GetFloat("VolumeSoundFX", out float soundVol);
        data.soundFXVolume = soundVol;
    }

    public void Load(AudioSaveData data)
    {
        musicSlider.value = data.musicVolume;
        SoundfxSlider.value = data.soundFXVolume;
        masterSlider.value = data.masterVolume;
    }
}
