using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using DG.Tweening;
using UnityEngine.UI;

public class TimeControllableEnemy : TimeControllableObject
{
    private Enemies enemy;

    // Still buggy with object forces
    // Still need to stop the object rotation
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        outline = GetComponent<Outline>();
        enemy = GetComponent<Enemies>();
        hasGravity = rb.useGravity;
    }

    void FixedUpdate()
    {
        UpdateTimeScale();
    }

    protected new void UpdateTimeScale()
    {
        base.UpdateTimeScale();
        enemy.timeScale = timeScale;
    }
}
