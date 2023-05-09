using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoulderCamera : MonoBehaviour
{
    private Quaternion initialRotation;
    private Transform parent;

    private void Start()
    {
        initialRotation = transform.rotation;
        parent = transform.parent;
    }

    private void LateUpdate()
    {
        transform.rotation = initialRotation * Quaternion.Inverse(parent.rotation) * transform.parent.rotation;
    }
}
