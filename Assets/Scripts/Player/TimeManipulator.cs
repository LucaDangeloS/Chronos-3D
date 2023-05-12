using LunarCatsStudio.SuperRewinder;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TimeManipulator : MonoBehaviour
{
    public float slowDownScale = 0.05f;
    public float cooldown = 2f;
    protected float cooldownTimer = 0f;
    public float radius = 15f;
    public Camera playerCamera;

    private List<IRewindable> rewindableObjects;
    private List<RewindObject> rewindableObjects2;

    private void OnEnable()
    {
        rewindableObjects = new List<IRewindable>(FindObjectsOfType<MonoBehaviour>().OfType<IRewindable>());
        rewindableObjects2 = new List<RewindObject>(FindObjectsOfType<MonoBehaviour>().OfType<RewindObject>());
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
            // First check if we are looking at an object that can be slowed down
            ITimeControllable target = getRayCastCollide(radius * 2f);
            if (target != null)
            {
                slowDownObject(target);
            }
            // If not, slow down all objects in a sphere of effect
            else
            {
                slowDownObjects(getSphereOfEffect(radius));
            }
            cooldownTimer = cooldown;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            rewindableObjects.ForEach(obj => { obj.Rewind(); });
            rewindableObjects2.ForEach(obj => { obj.StartRewind(); });
        }
        if (Input.GetKeyUp(KeyCode.R))
        {
            rewindableObjects.ForEach(obj => { obj.StopRewind(); });
            rewindableObjects2.ForEach(obj => { obj.StopRewind(); });
        }
    }

    ITimeControllable getRayCastCollide(float range)
    {
        int ignoreRaycastLayer = LayerMask.NameToLayer("Ignore Raycast");
        int layerMask = ~(1 << ignoreRaycastLayer);
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out RaycastHit hit, range, layerMask))
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
            timeControllable.SetTimeScale(slowDownScale);
        }
    }

    void slowDownObject(ITimeControllable affectedObject)
    {
        affectedObject.SetTimeScale(slowDownScale);
    }
}
