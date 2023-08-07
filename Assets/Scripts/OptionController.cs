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
        AnalyticsService.Instance.CustomData("pantallaAccedida", new Dictionary<string, object>()
        {
            { "Id_scene", "main_scene" },
            { "Id__button", "option" },

        });

        if (PlayerPrefs.HasKey("musicVolume"))
        {
            LoadVolume();
        } else
        {
            SetMusicVolume();
            SetSfxVolume();

        }
    }

    // Update is called once per frame
    void Update()
    {

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
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        SetMusicVolume();
        sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume");
        SetSfxVolume();
    }

    public void ResetOptions()
    {
        //PlayerPrefs.SetFloat("MyExposedParamMusic", Mathf.Log10(.5f) * 20);
        //PlayerPrefs.SetFloat("MyExposedParamSfx", Mathf.Log10(.5f) * 20);
        PlayerPrefs.SetInt("NewGamePlus", 0);
        PlayerPrefs.SetInt("Credits", 0);
        newGamePlusButton.SetActive(false);
    }

}
