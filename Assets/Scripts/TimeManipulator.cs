using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TimeManipulator : MonoBehaviour
{
    public float timeScale = 1f;

    private void OnEnable()
    {
        // Get a reference to the "F" key action

    }

    private void OnDisable()
    {
        // Disable the action when the script is disabled
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl)) {
            SetTime(0.2f);
        } else if (Input.GetKeyDown(KeyCode.LeftAlt)) {
            SetTime(1f);
        }
        Time.timeScale = timeScale;
    }

    public void SetTime(float slowFactor)
    {
        timeScale = slowFactor;
    }
}
