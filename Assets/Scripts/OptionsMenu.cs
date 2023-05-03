using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{

    public AudioMixer audioMixer;
    public Slider slider;

    public float sliderValue;


    void Start() {
        slider.value = PlayerPrefs.GetFloat("menuMusic", 0.5f);
        AudioListener.volume = slider.value;
        checkMute();
    }

    public void ChangeSlider(float value) {
        sliderValue = value;
        PlayerPrefs.SetFloat("menuMusic", sliderValue);
        AudioListener.volume = slider.value;
        checkMute();
    }

    public void checkMute() {
        if (sliderValue == 0) {
            //image true
        } else {
            // image false
        }
    }

    // public Dropdown resolutionDropdown;

    // Resolution[] resolutions;

    // void Start() {
    //     resolutions = Screen.resolutions;

    //     resolutionDropdown.ClearOptions();

    //     List<string> options = new List<string>();

    //     for (int i = 0; i < resolutions.Length; i++) {
    //         string option = resolutions[i].width + " x " + resolutions[i].height;
    //         options.Add(option);
    //     }

    //     resolutionDropdown.AddOptions(options);
    // }


    public void SetFullscreen(bool isFullScreen) {
        Screen.fullScreen = isFullScreen;
    }

}
