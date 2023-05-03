using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPause : MonoBehaviour
{
    [SerializeField] private GameObject menuPause;

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
        Time.timeScale = 0f;
        menuPause.SetActive(true);
    }

    public void Resume() {
        Time.timeScale = 1f;
        menuPause.SetActive(false);
    }

}
