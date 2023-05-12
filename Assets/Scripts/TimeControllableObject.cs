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
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }

        if (rb == null)
        {
            return;
        }

        if (timeScale != 1f)
        {
            // Reescale valocity
            velocityDelta = rb.velocity - prevVelocity;
            rb.velocity = prevVelocity + (velocityDelta * timeScale);
            if (hasGravity)
            {
                rb.AddForce(gravity * timeScale, ForceMode.Acceleration);
            }

            // Reescale rotation speed
            rb.angularVelocity *= timeScale;

            // Gradually return timeScale to 1f
            if (Mathf.Abs(timeScale - 1f) < timeStep)
                SetTimeScale(1f, false);
            else if (timeScale < 1f)
                SetTimeScale(timeScale + (timeStep / duration), false);
            else if (timeScale > 1f)
                SetTimeScale(timeScale - (timeStep / duration), false);
        }
        prevVelocity = rb.velocity;
    }

    public virtual void SetTimeScale(float newTime, bool firstSet = true)
    {
        if (rb == null || (firstSet && cooldownTimer > 0))
        {
            return;
        }

        if (newTime != 1f)
        {
            rb.useGravity = false;
            if (firstSet)
            {
                initialSlowDown = newTime;
                rb.velocity *= newTime;
                prevVelocity *= newTime;
            } else
            {
                rb.velocity *= (1 + (timeScale - newTime) / initialSlowDown);
                prevVelocity *= (1 + (timeScale - newTime) / initialSlowDown);
            }
            timeScale = newTime;
            if (outline != null)
            {
                HighlightObject();
            }
            cooldownTimer = cooldown;
        }
        else
        {
            rb.useGravity = hasGravity;
            rb.velocity = rb.velocity / timeScale;
            prevVelocity = prevVelocity / timeScale;
            timeScale = 1f;
            if (outline != null)
                HighlightObject();
        }
    }

    protected virtual void HighlightObject()
    {
        float lerpAmount = Mathf.Clamp01((1f - timeScale) / 0.5f);
        Color newColor = Color.Lerp(new Color(1f, 0.92f, 0.016f, 0), Color.red, lerpAmount);
        outline.ChangeColor(newColor);
    }
}
