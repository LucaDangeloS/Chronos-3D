using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;


public class heightLimit : MonoBehaviour
{

    public GameObject gameOverScreen;
    public StarterAssetsInputs inputController;


    public void OnTriggerEnter(Collider collider) {

        if (collider.gameObject.CompareTag("Player") ) {
            inputController.setGamePaused(true);
            Time.timeScale = 0f;
            gameOverScreen.SetActive(true);
        } else {
            Destroy(collider.gameObject,1.5f);
        }
    }

}
