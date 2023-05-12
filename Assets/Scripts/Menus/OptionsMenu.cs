using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

using TMPro;

public class OptionsMenu : MonoBehaviour
{

    public AudioMixer audioMixer;
    public Slider slider;

    public float sliderValue;


    public TMP_Dropdown dropdownResolutions;
    Resolution[] resolutions;



    void Start() {
        slider.value = PlayerPrefs.GetFloat("menuMusic", 1f);
        AudioListener.volume = slider.value;

        checkResolution();
    }


    void update() {

    }

    public void ChangeSlider(float value) {
        sliderValue = value;
        PlayerPrefs.SetFloat("menuMusic", sliderValue);
        AudioListener.volume = slider.value;
    }


    public void checkResolution() {
        resolutions = Screen.resolutions;
        dropdownResolutions.ClearOptions();
        List<string> options = new List<string>();
        int currentResolution = 0;

        for (int i = 0; i < resolutions.Length; i++) {
            if (resolutions[i].width < 600) {
                continue;
            }
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (Screen.fullScreen && resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height) {
                    currentResolution = i;
                }

        }

        dropdownResolutions.AddOptions(options);
        dropdownResolutions.value = currentResolution;
        dropdownResolutions.RefreshShownValue();
    }



    public void changeResolution(int resolutionIndex) {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }


    public void SetFullscreen(bool isFullScreen) {
        dropdownResolutions.interactable = !isFullScreen;
        Screen.fullScreen = isFullScreen;
    }

}
