using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjSmoothWaypointMovement : ObjWaypointMovement
{
    [Header("Duration does not work here, set speed instead")]
    public float speed = 10f;

    // Change the movement of the Tween to make a Path movement between it's waypoints
    // following a Catmull-Rom spline
    public override void MoveToNextWaypoint()
    {
        if (waypoints.Count == 0)
        {
            return;
        }

        if (currentWaypoint >= waypoints.Count)
        {
            // create empty tween
            movementTween = transform.DOMove(transform.position, duration)
                .SetEase(Ease.InOutQuad)
                .SetRecyclable(true)
                .OnComplete(MoveToNextWaypoint);
            return;
        }

        Vector3[] waypointsPos = waypoints.ConvertAll(waypoint => waypoint.transform.position).ToArray();

        movementTween = transform.DOPath(waypointsPos, speed, PathType.CatmullRom)
            .SetSpeedBased(true)
            .SetOptions(true)
            .SetEase(Ease.Linear)
            .SetRecyclable(true)
            .OnComplete(MoveToNextWaypoint);
        
        movementTween.timeScale = objTimeScale;

        currentWaypoint++;
        currentWaypoint %= waypoints.Count;
    }
}
