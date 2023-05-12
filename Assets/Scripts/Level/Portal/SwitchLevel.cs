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

    //Detecta el jugador en el portal para pasar el siguiente nivel.
    public void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            isInsidePortal = true;
            playerParticles.Play();
            StartCoroutine(DelayLoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
        }
    }

    //Detecta salir al jugador en el portal parar el contador para pasar el siguiente nivel.

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
        SceneManager.LoadScene(index);
    }

    IEnumerator DelayLoadLevel(int index)
    {
        yield return new WaitForSeconds(transitionDelayTime);

        //si el jugador sale del portal, la cuenta se para, evitando que pase al siguiente nivel
        if (!isInsidePortal) yield break;

        chargeWallpaper.SetActive(true);
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(CargarAsync());

    }

    //Función para cargar el siguiente nivel de forma asíncrona.
    public IEnumerator CargarAsync()
    {
        chargeWallpaper.SetActive(true);
        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);

        //Mientras la carga del nivel no esté completa, el porcentaje de carga se va aplicando al slider (barra de carga).
        while (!operation.isDone)
        {
            Debug.Log("cargando??");
            float progress = Mathf.Clamp01(operation.progress / .9f);
            Slider.value = progress;

            yield return null;
        }
    }


}
