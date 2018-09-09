using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class TestUIManager : MonoBehaviour
{
    private Text textEditModeValue;
    private Text textAxisModeValue;
    private Text textPositionXValue;
    private Text textPositionYValue;
    private Text textPositionZValue;
    private Text textRotateXValue;
    private Text textRotateYValue;
    private Text textRotateZValue;
    private Text textScaleXValue;
    private Text textScaleYValue;
    private Text textScaleZValue;
    private Text textSpeedValue;

    private bool ready;

    private TestUIManager()
    {
        this.ready = false;
    }

    private void Awake()
    {
        // todo: Findを使わずに改善せよ
        this.textEditModeValue  = GameObject.Find("Canvas_TestUI/Text_EditModeValue").GetComponent<Text>();
        this.textAxisModeValue  = GameObject.Find("Canvas_TestUI/Text_AxisModeValue").GetComponent<Text>();
        this.textPositionXValue = GameObject.Find("Canvas_TestUI/Position/Text_PositionXValue").GetComponent<Text>();
        this.textPositionYValue = GameObject.Find("Canvas_TestUI/Position/Text_PositionYValue").GetComponent<Text>();
        this.textPositionZValue = GameObject.Find("Canvas_TestUI/Position/Text_PositionZValue").GetComponent<Text>();
        this.textRotateXValue   = GameObject.Find("Canvas_TestUI/Rotate/Text_RotateXValue").GetComponent<Text>();
        this.textRotateYValue   = GameObject.Find("Canvas_TestUI/Rotate/Text_RotateYValue").GetComponent<Text>();
        this.textRotateZValue   = GameObject.Find("Canvas_TestUI/Rotate/Text_RotateZValue").GetComponent<Text>();
        this.textScaleXValue    = GameObject.Find("Canvas_TestUI/Scale/Text_ScaleXValue").GetComponent<Text>();
        this.textScaleYValue    = GameObject.Find("Canvas_TestUI/Scale/Text_ScaleYValue").GetComponent<Text>();
        this.textScaleZValue    = GameObject.Find("Canvas_TestUI/Scale/Text_ScaleZValue").GetComponent<Text>();
        this.textSpeedValue     = GameObject.Find("Canvas_TestUI/Speed/Text_SpeedValue").GetComponent<Text>();

        ready = true;
    }

    public void SetEditMode(string _str)
    {
        if (!ready) { return; }

        this.textEditModeValue.text = _str;
    }   

    public void SetAxisMode(string _str)
    {
        if (!ready) { return; }
   
        this.textAxisModeValue.text = _str;
    }

    public void SetParam(Transform _trans, float _speed)
    {
        if (!ready) { return; }

        Vector3 pos = _trans.position;
        Vector3 rotate = _trans.eulerAngles;
        Vector3 scale = _trans.localScale;

        this.textPositionXValue.text = pos.x.ToString(CultureInfo.CurrentCulture);
        this.textPositionYValue.text = pos.y.ToString(CultureInfo.CurrentCulture);
        this.textPositionZValue.text = pos.z.ToString(CultureInfo.CurrentCulture);

        this.textRotateXValue.text = rotate.x.ToString(CultureInfo.CurrentCulture);
        this.textRotateYValue.text = rotate.y.ToString(CultureInfo.CurrentCulture);
        this.textRotateZValue.text = rotate.z.ToString(CultureInfo.CurrentCulture);

        this.textScaleXValue.text = scale.x.ToString(CultureInfo.CurrentCulture);
        this.textScaleYValue.text = scale.y.ToString(CultureInfo.CurrentCulture);
        this.textScaleZValue.text = scale.z.ToString(CultureInfo.CurrentCulture);

        this.textSpeedValue.text = _speed.ToString(CultureInfo.CurrentCulture);
    }
}
