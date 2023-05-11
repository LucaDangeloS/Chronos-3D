using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachToPlatform : MonoBehaviour
{
    private List<GameObject> entities;
    private Vector3 lastPosition;

    void Start()
    {
        lastPosition = transform.position;
    }

    void FixedUpdate()
    {
        if (entities != null)
        {
            Vector3 positionDelta = transform.position - lastPosition;
            if (Mathf.Abs(positionDelta.magnitude) > 0.0001f && entities != null)
            {
                entities.ForEach(e => { e.transform.position += positionDelta; });
            }
        }
        lastPosition = transform.position;
    }

    void OnTriggerEnter(Collider collision)
    {
        Debug.Log("asdasd");
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemies"))
        {
            entities.Add(collision.gameObject);
            lastPosition = transform.position;
        }
    }

    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemies"))
        {
            entities.Remove(collision.gameObject);
        }
    }
}
