/* Author: flanny7
 * 
*/ 

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeComponent : MonoBehaviour
{
    private Image image;
    private FadeState state;

    private Color FadeColor { get { return this.image.color; } set { this.image.color = value; } }

    private float FadeAlpha
    {
        get { return this.FadeColor.a; }
        set
        {
            Color color = this.FadeColor;
            color.a = value;
            this.FadeColor = color;
        }
    }

    private void Start()
    {
        this.image = this.GetComponent<Image>();
        this.state = FadeState.Free;
    }
    
    public IEnumerator FadeIn(float _time)
    {
        if (this.state != FadeState.Free) { yield break; }

        if (_time == 0.0f)
        {
            FadeAlpha = 0.0f;
            yield break;
        }

        this.state = FadeState.FadeIn;

        float startAlpha = 1.0f;
        float targetAlpha = 0.0f;

        this.FadeAlpha = startAlpha;

        float startTime = Time.time;
        while (targetAlpha < FadeAlpha)
        {
            float timeStep = (Time.time - startTime) / _time;
            timeStep = Mathf.Clamp01(timeStep);
            this.FadeAlpha = Mathf.Lerp(startAlpha, targetAlpha, timeStep);
            yield return null;
        }

        this.state = FadeState.Free;
    }

    public IEnumerator FadeOut(float _time)
    {
        if (this.state != FadeState.Free) { yield break; }

        // ReSharper disable once CompareOfFloatsByEqualityOperator
        if (_time == 0.0f)
        {
            FadeAlpha = 1.0f;
            yield break;
        }

        this.state = FadeState.FadeOut;

        float startAlpha = 0.0f;
        float targetAlpha = 1.0f;

        this.FadeAlpha = startAlpha;

        float startTime = Time.time;
        while (FadeAlpha < targetAlpha)
        {
            float timeStep = (Time.time - startTime) / _time;
            timeStep = Mathf.Clamp01(timeStep);
            this.FadeAlpha = Mathf.Lerp(startAlpha, targetAlpha, timeStep);
            yield return null;
        }

        this.state = FadeState.Free;
    }
}