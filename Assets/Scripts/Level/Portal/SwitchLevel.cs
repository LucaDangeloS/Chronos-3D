using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SwitchLevel : MonoBehaviour
{
    public float transitionDelayTime = 5f;
    public Collider portalCollider;
    public ParticleSystem playerParticles;
    private bool isInsidePortal = false;

    public GameObject chargeWallpaper;
    public Slider Slider;

    public void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            isInsidePortal = true;
            playerParticles.Play();
            StartCoroutine(DelayLoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerParticles.Stop();
            isInsidePortal = false;
        }
    }

    public void LoadLevel(int index)
    {
        // If made async the player inputs have to be disabled
        SceneManager.LoadScene(index);
    }

    IEnumerator DelayLoadLevel(int index)
    {
        yield return new WaitForSeconds(transitionDelayTime);
        if (!isInsidePortal) yield break;

        chargeWallpaper.SetActive(true);
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(CargarAsync());

    }

    public IEnumerator CargarAsync()
    {
        chargeWallpaper.SetActive(true);
        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        while (!operation.isDone)
        {
            Debug.Log("cargando??");
            float progress = Mathf.Clamp01(operation.progress / .9f);
            Slider.value = progress;

            yield return null;
        }
    }


}
