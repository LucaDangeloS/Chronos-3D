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

    //Ajustamos el valor máximo del slider al valor del tiempo del nivel.
    public void SetMaxTime(int time) {
        slider.maxValue = time;
        slider.value = time;
    }

    //Actualizamos el valor de la barra de tiempo.
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


    // Cada segundo vamos actualizando el valor del slider.
    private IEnumerator UpdateTime() {
        while (remainingDuration >= 0 ) {
            remainingDuration--;
            slider.value = remainingDuration;
            yield return new WaitForSeconds(1f);

        }
        onEnd();
    }


    //Al finalizar el timer matamos al jugador y activamos la pantalla de muerte.
    private void onEnd() {
        inputController.setGamePaused(true);
        Time.timeScale = 0f;
        gameOverScreen.SetActive(true);
    }

}
