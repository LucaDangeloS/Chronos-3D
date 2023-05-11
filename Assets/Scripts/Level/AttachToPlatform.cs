using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachToPlatform : MonoBehaviour
{
    private GameObject entity;
    private Vector3 lastPosition;

    void Start()
    {
        lastPosition = transform.position;
    }

    void FixedUpdate()
    {
        if (entity != null)
        {
            Vector3 positionDelta = transform.position - lastPosition;
            if (Mathf.Abs(positionDelta.magnitude) > 0.0001f && entity != null)
            {
                entity.transform.position += positionDelta;
            }
        }
        lastPosition = transform.position;
    }

    void OnTriggerEnter(Collider collision)
    {
        Debug.Log("asdasd");
        if (collision.gameObject.CompareTag("Player"))
        {
            entity = collision.gameObject;
            lastPosition = transform.position;
        }
    }

    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            entity = null;
        }
    }
}
