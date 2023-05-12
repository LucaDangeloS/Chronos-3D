using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachToPlatform : MonoBehaviour
{
    private List<GameObject> entities = new List<GameObject>();
    private Vector3 lastPosition;

    void Start()
    {
        lastPosition = transform.position;
    }

    void FixedUpdate()
    {
        if (entities != null && entities.Count != 0)
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
