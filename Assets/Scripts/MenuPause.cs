using Cinemachine;
using StarterAssets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPause : MonoBehaviour
{
    [SerializeField] private GameObject menuPause;
    public CinemachineVirtualCamera cameraController;
    public StarterAssetsInputs inputController;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (menuPause.activeSelf) {
                Resume();
            } else {
                Pause();
            }
        }
    }

    // Start is called before the first frame update
    public void Pause() {
        //cameraController.Follow = null;
        inputController.setGamePaused(true);
        Time.timeScale = 0f;
        menuPause.SetActive(true);
    }

    public void Resume() {
        //cameraController.Follow = playerCameraRoot.transform;
        inputController.setGamePaused(false);
        Time.timeScale = 1f;
        menuPause.SetActive(false);
    }

}
