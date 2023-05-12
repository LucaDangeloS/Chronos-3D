using System.Collections;
using UnityEngine;
using LunarCatsStudio.SuperRewinder;
using System.Linq;
using System.Collections.Generic;

public class TimeControllableObject : MonoBehaviour, ITimeControllable
{
    public float timeScale = 1f;
    public float duration = 10f;
    public float cooldown = 3f;
    protected float cooldownTimer = 0f;
    private float timeStep = 0.05f;

    protected Rigidbody rb;
    protected Outline outline;
    protected bool hasGravity = false;

    private Vector3 gravity = Physics.gravity;
    private Vector3 prevVelocity = Vector3.zero;
    private Vector3 velocityDelta = Vector3.zero;

    private float initialSlowDown;

    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        outline = GetComponent<Outline>();
        hasGravity = rb.useGravity;
    }


    void FixedUpdate()
    {
        UpdateTimeScale();
    }

    protected virtual void UpdateTimeScale()
    {
        // If the object is on cooldown, reduce the cooldown timer
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }
        
        if (rb == null)
        {
            return;
        }

        // If speed is not normal
        if (timeScale != 1f)
        {
            // Scale the changes in velocity of the object using the timeScale
            // calculate the delta valocity
            velocityDelta = rb.velocity - prevVelocity;
            // Apply the delta velocity scaled by the timeScale
            rb.velocity = prevVelocity + (velocityDelta * timeScale);

            // If the object has gravity, simulate it reescalating the gravity force
            if (hasGravity)
            {
                rb.AddForce(gravity * timeScale, ForceMode.Acceleration);
            }

            // Don't forget to reescale angular valocity too
            rb.angularVelocity *= timeScale;

            // Gradually return timeScale to 1f doing checks to avoid floating point errors
            if (Mathf.Abs(timeScale - 1f) < timeStep)
                SetTimeScale(1f, false);
            else if (timeScale < 1f)
                SetTimeScale(timeScale + (timeStep / duration), false);
            else if (timeScale > 1f)
                SetTimeScale(timeScale - (timeStep / duration), false);
        }

        // Set the previous velocity of the object
        prevVelocity = rb.velocity;
    }

    public virtual void SetTimeScale(float newTime, bool firstSet = true)
    {
        // If there is no rigidbody or the object is on cooldown
        if (rb == null || (firstSet && cooldownTimer > 0))
        {
            return;
        }
        // If the speed is set to one that is not the default
        if (newTime != 1f)
        {
            rb.useGravity = false;
            // If it's the first slow down, save the initial velocity
            if (firstSet)
            {
                initialSlowDown = newTime;
                rb.velocity *= newTime;
                prevVelocity *= newTime;
            } 
            // The rest are speed ups to make the speed of the object return to normal gradually
            else
            {
                // Gradually return the velocity to normal interpolating the timeScale
                rb.velocity *= (1 + (timeScale - newTime) / initialSlowDown);
                prevVelocity *= (1 + (timeScale - newTime) / initialSlowDown);
            }
            timeScale = newTime;
            if (outline != null)
            {
                HighlightObject();
            }
            // Set the cooldown timer
            cooldownTimer = cooldown;
        }
        // When the speed is set back to 1
        else
        {
            // return the gravity to the object (if it had)
            rb.useGravity = hasGravity;
            // Scale the current velocity of the object back to normal
            rb.velocity = rb.velocity / timeScale;
            prevVelocity = prevVelocity / timeScale;
            timeScale = 1f;
            if (outline != null)
                HighlightObject();
        }
    }

    protected virtual void HighlightObject()
    {
        // The highlith color is red when the object is slowed down
        float lerpAmount = Mathf.Clamp01((1f - timeScale) / 0.5f);
        // And it's interpolated between 0 alpha and 1 alpha depending on the timeScale to make the outline disappear
        Color newColor = Color.Lerp(new Color(1f, 0.92f, 0.016f, 0), Color.red, lerpAmount);
        // Changes the color of the outline (edited external script from a package)
        outline.ChangeColor(newColor);
    }
}
