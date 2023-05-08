using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class ObjWaypointMovement : MonoBehaviour
{

    [SerializeField] protected List<GameObject> waypoints;
    protected int currentWaypoint = 0;

    [Header("Duration of each movement step")]
    public float duration = 3f;
    [Header("Time scale of the object, usually controlled by the TimeControllableObject script")]
    public float objTimeScale = 1f;

    private bool isTimeControllable = false;
    private TimeControllableObject timeControllableObject = null;

    [Header("Rotations for the object")]
    public float rotateXcomponent = 0f;
    public float rotateYcomponent = 0f;
    public float rotateZcomponent = 0f;
    public float rotateSpeed = 1f;

    protected Tween movementTween;
    protected Tween rotationTween;

    private void Start()
    {
        timeControllableObject = GetComponent<TimeControllableObject>();
        isTimeControllable = timeControllableObject != null;
        MoveToNextWaypoint();
        Rotate();
    }

    private void Update()
    {
        if ((isTimeControllable && objTimeScale != timeControllableObject.timeScale))
        {
            objTimeScale = timeControllableObject.timeScale;
            movementTween.timeScale = objTimeScale;
            rotationTween.timeScale = objTimeScale;
        }
    }

    public virtual void MoveToNextWaypoint()
    {
        if (currentWaypoint >= waypoints.Count)
        {
            // create empty tween
            movementTween = transform.DOMove(transform.position, duration)
                .SetEase(Ease.InOutQuad)
                .SetRecyclable(true)
                .OnComplete(MoveToNextWaypoint);
            return;
        }

        movementTween = transform.DOMove(waypoints[currentWaypoint].transform.position, duration)
            .SetEase(Ease.InOutQuad)
            .SetRecyclable(true)
            .OnComplete(MoveToNextWaypoint);
        movementTween.timeScale = objTimeScale;

        currentWaypoint++;
        currentWaypoint %= waypoints.Count;
    }

    public virtual void Rotate()
    {
        rotationTween = transform.DORotate(new Vector3(rotateZcomponent, rotateXcomponent, rotateYcomponent), 10f / rotateSpeed, RotateMode.LocalAxisAdd)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Incremental)
            .SetRecyclable(true);
        rotationTween.timeScale = objTimeScale;
    }
}
