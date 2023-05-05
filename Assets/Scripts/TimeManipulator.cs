using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TimeManipulator : MonoBehaviour
{
    private InputAction fKeyAction;
    private Vector3 direction = Vector3.forward;
    public int range = 55;

    private void OnEnable()
    {
        // Get a reference to the "F" key action
        fKeyAction = new InputAction("F Key", InputActionType.Button, "<Keyboard>/f");
        fKeyAction.Enable();
    }

    private void OnDisable()
    {
        // Disable the action when the script is disabled
        fKeyAction.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new(transform.position, transform.TransformDirection(direction * range));
        Debug.DrawRay(transform.position, transform.TransformDirection(direction * range));
        //Debug.Log("Raycast hit: " + transform.position + transform.TransformDirection(direction * range));
        // If the player presses F key, it will slow down the time of the object tha tthe plays is pointing towards
        // Read input from the input system package from player
        if (fKeyAction.ReadValue<float>() > 0)
        {
            
        }
    }
}
