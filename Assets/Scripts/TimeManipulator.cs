using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.InputSystem;

public class TimeManipulator : MonoBehaviour
{
    public float slowDownScale = 0.05f;
    public float cooldown = 2f;
    protected float cooldownTimer = 0f;
    public float radius = 15f;
    public Camera playerCamera;

    private void OnEnable()
    {
    }

    private void OnDisable()
    {
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
            return;
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            //ITimeControllable target = getRayCastCollide(radius * 1.5f);
            //if (target != null)
            //{
            //    Debug.Log("Raycast hit: " + target);
            //    slowDownObject(target);
            //} else
            //{
                Debug.Log("Sphere of effect");
                slowDownObjects(getSphereOfEffect(radius));
            //}
            cooldownTimer = cooldown;
        }
    }

    ITimeControllable getRayCastCollide(float range)
    {
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out RaycastHit hit, range))
        {
            if (hit.collider.TryGetComponent<ITimeControllable>(out var timeControllableGameObject))
            {
                return timeControllableGameObject;
            }
        }
        return null;
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
            Debug.Log(timeControllable);
            timeControllable.SetTimeScale(slowDownScale);
        }
    }

    void slowDownObject(ITimeControllable affectedObject)
    {
        affectedObject.SetTimeScale(slowDownScale);
    }
}
