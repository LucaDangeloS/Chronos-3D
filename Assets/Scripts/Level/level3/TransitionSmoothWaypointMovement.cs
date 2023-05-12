using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionSmoothWaypointMovement : ObjSmoothWaypointMovement
{
    public void ForceMove(Vector3 point)
    {
        transform.DOMove(point, speed)
            .SetEase(Ease.InOutCubic)
            .SetRecyclable(true);
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        Destroy(rb);
        rotationTween.Pause();
    }
}