using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class RotateRockSet : MonoBehaviour, ITimeControllable, IRewindable
{
    public float rotationSpeed = 3f;
    public float timeScale = 1f;
    public float duration = 16f;
    public Vector3 rotationComponent = new Vector3(360, 0, 0);
    private Tween rotationTween;
    private float timeStep = 0.05f;

    public bool isRewinding { get; set; } = false;
    public float maxRecordTime { get; set; } = 5f;
    public float recordedTime { get; set; } = 0f;


    // Start is called before the first frame update
    void Start()
    {
        Rotate();
    }

    void Update()
    {
        UpdateCooldown();
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
        HighlightObject();
    }

    protected void HighlightObject()
    {
        Outline[] outlines = GetComponentsInChildren<Outline>();
        foreach (Outline outline in outlines)
        {
            float lerpAmount = Mathf.Clamp01((1f - timeScale) / 0.5f);
            Color newColor = Color.Lerp(new Color(1f, 0.92f, 0.016f, 0), Color.red, lerpAmount);
            outline.ChangeColor(newColor);
        }
    }

    public void Rewind()
    {
        isRewinding = true;
        rotationTween.PlayBackwards();
    }

    public void StopRewind()
    {
        rotationTween.PlayForward();
    }

    public void UpdateCooldown()
    {
        if (isRewinding && recordedTime > 0f)
        {
            recordedTime -= Time.deltaTime;
        }
        else if (isRewinding)
        {
            isRewinding = false;
            StopRewind();
        }
        else if (recordedTime < maxRecordTime)
        {
            recordedTime += Time.deltaTime;
        }
    }
}
