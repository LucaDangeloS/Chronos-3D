using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using DG.Tweening;

public class TimeControllableEnemy : TimeControllableObject, ITimeControllable
{
    private Enemies enemy;

    // Still buggy with object forces
    // Still need to stop the object rotation
    void Start()
    {
        enemy = GetComponent<Enemies>();
    }

    protected new void UpdateTimeScale()
    {
        base.UpdateTimeScale();
        enemy.timeScale = timeScale;
    }
}
