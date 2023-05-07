using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class SwitchLevel : MonoBehaviour
{
    public float transitionDelayTime = 5f;
    public Collider portalCollider;
    public ParticleSystem playerParticles;
    private bool isInsidePortal = false;

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
        SceneManager.LoadScene(index);
    }

    IEnumerator DelayLoadLevel(int index)
    {
        Debug.Log("Delaying load level");
        yield return new WaitForSeconds(transitionDelayTime);
        if (!isInsidePortal) yield break;
        Debug.Log("Portal active");
        LoadLevel(index);
    }
}
