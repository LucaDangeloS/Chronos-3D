using Cinemachine;
using StarterAssets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPause : MonoBehaviour
{
    [SerializeField] private GameObject menuPause;
    [SerializeField] private GameObject menuOptions;
    public CinemachineVirtualCamera cameraController;
    public StarterAssetsInputs inputController;
    public ThirdPersonController playerController;

    private void Start() {
        Time.timeScale = 1f;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (menuPause.activeSelf || menuOptions.activeSelf) {
                Resume();
            } else {
                Pause();
            }
        }
    }

    // Start is called before the first frame update
    public void Pause() {
        inputController.setGamePaused(true);
        Time.timeScale = 0f;
        menuPause.SetActive(true);
    }

    public void Resume() {
        inputController.setGamePaused(false);
        Time.timeScale = 1f;
        menuPause.SetActive(false);
        menuOptions.SetActive(false);
    }


    public void Retry() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void goToMenu() {
        SceneManager.LoadScene("mainMenu");
    }

}
