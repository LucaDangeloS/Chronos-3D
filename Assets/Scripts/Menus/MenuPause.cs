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
    public TimeManipulator timeManipulator;

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
        timeManipulator.SetTime(0f);
        playerController.setSyncDeltaTime(true);
        menuPause.SetActive(true);
    }

    public void Resume() {
        inputController.setGamePaused(false);
        timeManipulator.SetTime(1f);
        playerController.setSyncDeltaTime(false);
        menuPause.SetActive(false);
        menuOptions.SetActive(false);
    }


    public void Retry() {
        timeManipulator.SetTime(0f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        // Application.LoadLevel(Application.loadedLevel);
        //  SceneManager.LoadScene("Level2");
    }

}
