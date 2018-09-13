using System.Collections;
using UnityEngine;

public sealed class ButtonControllerOnClick : ButtonController
{
    // [SerializeField]
    // private Camera menuCamera = null;
    // [SerializeField]
    // private Vector3 menuCameraPosMultiScreen = new Vector3(-50f, 1f, 0f);
    [SerializeField]
    private RectTransform screenPivotTrans = null;

    [SerializeField]
    private Vector2 soloScreenPos = Vector2.down * -1920;
    [SerializeField]
    private Vector2 multiScreenPos = Vector2.right * -1920;

    [SerializeField]
    private GameObject menuScreenGameObject = null;
    [SerializeField]
    private GameObject multiScreenGameObject = null;

    [SerializeField]
    private FadeComponent menuPanelFade = null;
    [SerializeField]
    private FadeComponent multiPanelFade = null;

    private void Start()
    {
        // this.menuCamera = Camera.main.gameObject.GetComponent<Camera>();
        // this.menuScreenGameObject = GameObject.FindWithTag("menuScreenGameObject");
        // this.multiScreenGameObject = GameObject.FindWithTag("multiScreenGameObject");

        // this.multiScreenGameObject.SetActive(false);
    }

    protected override void OnClick(string _objectName)
    {
        switch (_objectName)
        {
            case "solo":
                this.SoloButtonClick();
                break;

            case "multi":
                this.MultiButtonClick();
                break;

            case "option":
                this.OptionButtonClick();
                break;

            default:
                var e = new System.Exception("Not implemented!!");
                Debug.LogError(e);
                throw e;
        }
    }

    public void SoloButtonClick()
    {
        // Debug.Log(MethodBase.GetCurrentMethod().Name);
        Debug.Log("SoloButton Click");
    }

    public void MultiButtonClick()
    {
        // Debug.Log(MethodBase.GetCurrentMethod().Name);
        Debug.Log("MultiButton Click");

        // this.menuCamera.transform.position = menuCameraPosMultiScreen;
        // this.screenPivotTrans.anchoredPosition = this.multiScreenPos;
        StartCoroutine(
            MoveScreenPivot(
                multiScreenPos,
                multiScreenGameObject,
                menuScreenGameObject,
                0.5f,
                this.multiPanelFade,
                1.5f));
    }

    public void OptionButtonClick()
    {
        // Debug.Log(MethodBase.GetCurrentMethod().Name);
        Debug.Log("OptionButton Click");
    }

    private IEnumerator MoveScreenPivot(
        Vector2 _targetPos, 
        GameObject _startScreen = null,
        GameObject _targetScreen = null,
        float _moveTime = .5f,
        FadeComponent _fadeComponent = null,
        float _faidInTime = 1.5f)
    {
        if (!_targetScreen) { _targetScreen.SetActive(true); }

        // yield return FadeManager.Instance.FadeOut(0.1f);

        if (_fadeComponent)
        {
            yield return _fadeComponent.FadeOut(0);
        }

        var startPos = this.screenPivotTrans.anchoredPosition;

        float startTime = Time.time;
        while (Time.time - startTime <= _moveTime ||
               this.screenPivotTrans.anchoredPosition != _targetPos)
        {
            float timeStep = (Time.time - startTime) / _moveTime;
            timeStep = Mathf.Clamp01(timeStep);

            var tmp = this.screenPivotTrans.anchoredPosition;
            tmp.x = Mathf.Lerp(startPos.x, _targetPos.x, timeStep);
            tmp.y = Mathf.Lerp(startPos.y, _targetPos.y, timeStep);

            this.screenPivotTrans.anchoredPosition = tmp;

            yield return null;
        }

        if (_fadeComponent)
        {
            yield return _fadeComponent.FadeIn(_faidInTime);
        }

        // yield return FadeManager.Instance.FadeIn(0.1f);

        if (!_startScreen) { _startScreen.SetActive(false); }
    }
}
