using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using StarterAssets;



public class TimeBar : MonoBehaviour
{

    public int levelSeconds;
    public Slider slider;
    private int remainingDuration;
    public GameObject gameOverScreen;
    public StarterAssetsInputs inputController;

    public void SetMaxTime(int time) {
        slider.maxValue = time;
        slider.value = time;
    }

    public void SetTime(int Time) {
        slider.value = Time;
    }
    // Start is called before the first frame update
    void Start()
    {
        remainingDuration = levelSeconds;
        SetMaxTime(levelSeconds);
        StartCoroutine(UpdateTime());
    }


    private IEnumerator UpdateTime() {
        while (remainingDuration >= 0 ) {
            remainingDuration--;
            slider.value = remainingDuration;
            yield return new WaitForSeconds(1f);

        }
        onEnd();
    }


    private void onEnd() {
        inputController.setGamePaused(true);
        Time.timeScale = 0f;
        gameOverScreen.SetActive(true);
    }

}
