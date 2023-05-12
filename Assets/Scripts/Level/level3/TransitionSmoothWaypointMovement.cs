using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionSmoothWaypointMovement : ObjSmoothWaypointMovement
{
    // Force the object to move to a specific point and stop all other movement to keep it stable
    public void ForceMove(Vector3 point)
    {
        transform.DOMove(point, speed)
            .SetEase(Ease.InOutCubic)
            .SetRecyclable(true);
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        // Remove it's rigidbody to avoid physics
        Destroy(rb);
        rotationTween.Pause();
    }

    public override void Rewind()
    {
    }

    public override void StopRewind()
    {
    }
}