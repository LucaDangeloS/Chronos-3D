using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class TimeManipulator : MonoBehaviour
{
    public float slowDownScale = 0.05f;
    public float cooldown = 3f;
    public float cooldownTimer = 0f;
    public float radius = 15f;
    public Camera playerCamera;

    private void OnEnable()
    {
        playerCamera = GetComponentInChildren<Camera>();
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
            //Debug.Log(getRayCastCollide(40f));
            slowDownObjects(getSphereOfEffect(radius));
            cooldownTimer = cooldown;
        }
    }

    ITimeControllable getRayCastCollide(float range)
    {
        // Get the raycast hit object from the center of the player camera
        // from where the player is looking
        RaycastHit hit;
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0, 0, range));
        // Debug draw ray
        Debug.DrawRay(ray.origin, ray.direction * range, Color.yellow);
        if (Physics.Raycast(ray, out hit, range))
        {
            ITimeControllable timeControllableGameObject = hit.collider.GetComponent<ITimeControllable>();
            if (timeControllableGameObject != null)
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
}
