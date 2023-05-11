using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class TimeManipulator : MonoBehaviour
{
    public float slowDownScale = 0.05f;
    public float cooldown = 5f;
    public float radius = 15f;

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
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            slowDownObjects(getSphereOfEffect(radius));
        }
        else if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            slowDownObjects(getSphereOfEffect(radius));
        }
    }

    ITimeControllable[] getSphereOfEffect(float radius)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        List<ITimeControllable> affectedObjects = new List<ITimeControllable>();

        foreach (Collider collider in colliders)
        {
            ITimeControllable timeControllableGameObject = collider.GetComponent<ITimeControllable>();
            if (timeControllableGameObject != null)
            {
                affectedObjects.Add(timeControllableGameObject);
            }
        }

        return affectedObjects.ToArray();
    }

    void slowDownObjects(ITimeControllable[] affectedObjects)
    {
        foreach (ITimeControllable timeControllable in affectedObjects)
        {
            timeControllable.SetTimeScale(slowDownScale);
        }
    }
}
