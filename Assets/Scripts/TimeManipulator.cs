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
            ITimeControllable target = getRayCastCollide(radius * 2f);
            if (target != null)
            {
                slowDownObject(target);
            }
            else
            {
                slowDownObjects(getSphereOfEffect(radius));
            }
            cooldownTimer = cooldown;
        }
    }

    ITimeControllable getRayCastCollide(float range)
    {
        int ignoreRaycastLayer = LayerMask.NameToLayer("Ignore Raycast");
        int layerMask = ~(1 << ignoreRaycastLayer);
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out RaycastHit hit, range, layerMask))
        {
            Debug.Log(hit.collider);
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
            timeControllable.SetTimeScale(slowDownScale);
        }
    }

    void slowDownObject(ITimeControllable affectedObject)
    {
        affectedObject.SetTimeScale(slowDownScale);
    }
}
