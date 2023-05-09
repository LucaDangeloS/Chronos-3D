using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightObj : MonoBehaviour
{
    public Material highlightMaterial;
    public float highlightAlpha = 0.5f;

    private Material originalMaterial;
    private Renderer renderer;

    private void Start()
    {
        renderer = GetComponent<Renderer>();
        originalMaterial = renderer.material;
        Highlight(true);
    }

    public void Highlight(bool on)
    {
        if (on)
        {
            renderer.material = highlightMaterial;
            Color color = highlightMaterial.color;
            color.a = highlightAlpha;
            highlightMaterial.color = color;
        }
        else
        {
            renderer.material = originalMaterial;
        }
    }
}

