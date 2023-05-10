using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {


    public GameObject chargeWallpaper;
    public Slider Slider;

    public void PlayGame() {
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        StartCoroutine(CargarAsync());
    }

    public IEnumerator CargarAsync() {
        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);

        chargeWallpaper.SetActive(true);

        while(!operation.isDone) {

            float progress = Mathf.Clamp01(operation.progress / .9f);
            Slider.value = progress;

            yield return null;
        }
    }


    public void QuitGame() {
        Debug.Log("Quit game");
        Application.Quit();
    }
}
