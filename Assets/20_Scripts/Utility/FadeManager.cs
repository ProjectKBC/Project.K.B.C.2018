using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum FadeState
{
    Free,
    FadeIn,
    FadeOut,
}

public class FadeManager : SingletonMonoBehaviour<FadeManager>
{
    public delegate void BetweenCrossFadeFunc();
    public delegate bool IsBreakFunc();

    [SerializeField]
    private int screenWidth = 1920;
    [SerializeField]
    private int screenHeight = 1080;
    [Space(8.0f)]
    [SerializeField]
    private bool isStartFade = true;
    [SerializeField]
    private float startFadeTime = 0.25f;

    public int ScreenWidth { get { return this.screenWidth; } }
    public int ScreenHeight { get { return this.screenHeight; } }

    private GameObject go;
    private Image image;
    private Canvas canvas;
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

    public void LoadScene(float _time, string _sceneName)
    {
        StartCoroutine(CrossFade(_time, () => { SceneManager.LoadScene(_sceneName); }));
    }

    protected override void OnInit()
    {
        this.go = gameObject;
        DontDestroyOnLoad(this.gameObject);

        var tmpCanvas = this.go.AddComponent<Canvas>();
        this.canvas = tmpCanvas;
        this.canvas.renderMode = RenderMode.ScreenSpaceCamera;
        this.canvas.sortingOrder = 999;

        var scale = this.go.AddComponent<CanvasScaler>();
        scale.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scale.referenceResolution = Vector2.one;

        var obj = new GameObject("FadeImage");
        obj.transform.SetParent(this.transform, false);
        var tmpImage = obj.AddComponent<Image>();
        this.image = tmpImage;

        this.FadeColor = Color.black;
        this.FadeAlpha = 0;

        if (this.isStartFade)
        {
            this.FadeAlpha = 1;
            this.StartCoroutine(this.FadeIn(this.startFadeTime));
        }

        if (Application.platform == RuntimePlatform.WindowsPlayer ||
            Application.platform == RuntimePlatform.OSXPlayer ||
            Application.platform == RuntimePlatform.LinuxPlayer)
        {
            Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;   
            Screen.SetResolution(this.screenWidth, this.screenHeight, false);
        }

        this.state = FadeState.Free;
    }

    public IEnumerator FadeIn(float _time)
    {
        if (this.state != FadeState.Free) { yield break; }

        // ReSharper disable once CompareOfFloatsByEqualityOperator
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

    public IEnumerator CrossFade(float _time, BetweenCrossFadeFunc _betweenCrossFadeFunc = null)
    {
        yield return this.FadeOut(_time / 2);

        if (_betweenCrossFadeFunc != null) _betweenCrossFadeFunc();

        yield return this.FadeIn(_time / 2);
    }

    public IEnumerator Wait(float _time, IsBreakFunc _isBreak = null)
    {
        // _time == 0.0fの時は_isBreakにしたがって永遠と待つ
        // ReSharper disable once CompareOfFloatsByEqualityOperator
        if (_time == 0.0f && _isBreak != null)
        {
            while (true)
            {
                if (_isBreak()) { break; }

                yield return null;
            }

            yield break;
        }

        float startTime = Time.time;
        while (Time.time - startTime <= _time)
        {
            if (_isBreak != null && _isBreak()) { break; }

            yield return null;
        }
    }
}
