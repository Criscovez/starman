using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using Unity.Services.Analytics;
using Unity.Services.Core;
public class OptionController : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    public GameObject newGamePlusButton;

    void Start()
    {
        AnalyticsTracker.TrackScreenSeen("main_scene", "option");

        LoadVolume();
    }

    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        audioMixer.SetFloat("MyExposedParamMusic", Mathf.Log10(volume)*20);
        //Debug.Log(volume.ToString());
        PlayerPrefs.SetFloat("musicVolume", volume);
    }

    public void SetSfxVolume()
    {
        float volume = sfxSlider.value;
        audioMixer.SetFloat("MyExposedParamSfx", Mathf.Log10(volume) * 20);
        //Debug.Log(volume.ToString());
        PlayerPrefs.SetFloat("sfxVolume", volume);
    }

    private void LoadVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume", 1);
        SetMusicVolume();
        sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume",  1);
        SetSfxVolume();
    }

    public void ResetOptions()
    {        
        PlayerPrefs.DeleteAll();
        LoadVolume();

        newGamePlusButton.SetActive(false);
    }

}
