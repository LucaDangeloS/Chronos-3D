using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class RotateRockSet : MonoBehaviour, ITimeControllable
{
    public float rotationSpeed = 3f;
    public float timeScale = 1f;
    public float duration = 16f;
    public Vector3 rotationComponent = new Vector3(360, 0, 0);
    private Tween rotationTween;
    private float timeStep = 0.05f;

    // Start is called before the first frame update
    void Start()
    {
        Rotate();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (timeScale != 1f)
        {
            // Gradually return timeScale to 1f
            if (Mathf.Abs(timeScale - 1f) < timeStep)
                SetTimeScale(1f, false);
            else if (timeScale < 1f)
                SetTimeScale(timeScale + (timeStep / duration), false);
            else if (timeScale > 1f)
                SetTimeScale(timeScale - (timeStep / duration), false);
        }
    }

    private void Rotate()
    {
        // slow rotate rock sets
        rotationTween = transform.DOLocalRotate(rotationComponent, 25f / rotationSpeed, RotateMode.LocalAxisAdd)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Incremental)
            .SetRecyclable(true);
    }

    public void SetTimeScale(float newTime, bool firstSet = true)
    {
        timeScale = newTime;
        rotationTween.timeScale = timeScale;
        // Get all outline components from children
        Outline[] outlines = GetComponentsInChildren<Outline>();
        foreach (Outline outline in outlines)
        {
            float lerpAmount = Mathf.Clamp01((1f - timeScale) / 0.5f);
            Color newColor = Color.Lerp(new Color(1f, 0.92f, 0.016f, 0), Color.red, lerpAmount);
            outline.ChangeColor(newColor);
        }
    }
}