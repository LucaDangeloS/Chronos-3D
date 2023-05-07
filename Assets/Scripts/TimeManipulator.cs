using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TimeManipulator : MonoBehaviour
{
    public float timeScale = 1f;
    public GameObject player;

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
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl)) {
            Debug.Log("Slowi");
            SlowTime(0.5f);
        }
        Time.timeScale = timeScale;
    }

    public void SlowTime(float slowFactor)
    {
        timeScale = slowFactor;
    }
}
