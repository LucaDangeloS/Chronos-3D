using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class TimeControllableObject : MonoBehaviour
{
    public float timeScale = 1f;
    private Rigidbody rb;
    private bool hasGravity = false;
    private Vector3 gravity = Physics.gravity;
    private Vector3 prevVelocity = Vector3.zero;
    private Vector3 velocityDelta = Vector3.zero;
    // Still buggy with object forces
    // Still need to stop the object rotation
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        hasGravity = rb.useGravity;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            SetTimeScale(0.2f);
        }
    }
    void FixedUpdate()
    {
        if (rb == null)
        {
            return;
        }
        
        if (timeScale != 1f)
        {
            velocityDelta = rb.velocity - prevVelocity;
            rb.velocity = prevVelocity + (velocityDelta * timeScale);
            if (hasGravity)
            {
                rb.AddForce(gravity * timeScale, ForceMode.Acceleration);
            }
        }
        prevVelocity = rb.velocity;
    }

    public void SetTimeScale(float newTime)
    {
        if (rb == null)
        {
            return;
        }

        if (timeScale == 1f)
        {
            rb.useGravity = false;
            timeScale = newTime;
            rb.velocity = rb.velocity * timeScale;
            prevVelocity = prevVelocity * timeScale;
        }
        else
        {
            rb.useGravity = hasGravity ? true : false;
            rb.velocity = rb.velocity / timeScale;
            prevVelocity = prevVelocity / timeScale;
            timeScale = 1f;
        }
    }
}
