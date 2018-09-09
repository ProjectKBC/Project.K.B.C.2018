using UnityEngine;

public class PlayerTest : MonoBehaviour
{
    public enum EditMode
    {
        EditPositionMove,
        EditRotate,
        EditScale,
        EditSpeed,
    }

    public enum AxisMode
    {
        X,
        Y,
        Z,
    }

    public float Speed { get { return this.speed; } private set { this.speed = value; } }

    [SerializeField]
    private float speed = 1.0f;

    private EditMode editMode;
    private AxisMode axisMode;

    // transformのキャッシュ
    private Transform trans;
    // TestUIManagerのキャッシュ
    private TestUIManager testUiManager;

    private void Awake()
    {
        // キャッシュ類
        this.trans = transform;
        this.testUiManager = GameObject.Find("Canvas_TestUI").GetComponent<TestUIManager>();

        // パラメータの初期化
        this.editMode = EditMode.EditPositionMove;
        this.axisMode = AxisMode.X;
    }

    private void Update()
    {
        ChangeEditMode();

        switch(this.editMode)
        {
            case EditMode.EditPositionMove:
                Move();
                break;

            case EditMode.EditRotate:
                EditRotate();
                break;

            case EditMode.EditScale:
                EditScale();
                break;

            case EditMode.EditSpeed:
                EditSpeed();
                break;

            default: Debug.LogError("Impossible EditMode"); break;
        }

        SendParam();
    }

    private void ChangeEditMode()
    {
        if (Input.GetKey(KeyCode.P) || Input.GetKey(KeyCode.M))
        {
            this.editMode = EditMode.EditPositionMove;
        }

        if (Input.GetKey(KeyCode.R))
        {
            this.editMode = EditMode.EditRotate;
        }

        // Sのみ
        if (Input.GetKey(KeyCode.S) && !(Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt)))
        {
            this.editMode = EditMode.EditScale;
        }

        // S + Alt
        if (Input.GetKey(KeyCode.S) && (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt)))
        {
            this.editMode = EditMode.EditSpeed;
        }
    }

    private void Move()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            this.trans.localPosition += Vector3.up * this.speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            this.trans.localPosition += Vector3.down * this.speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            this.trans.localPosition += Vector3.right * this.speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            this.trans.localPosition += Vector3.left * this.speed * Time.deltaTime;
        }
    }

    private void EditRotate()
    {
        ChangeAxisMode();

        switch(this.axisMode)
        {
            case AxisMode.X:
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    this.trans.Rotate(Vector3.right, 100.0f * Time.deltaTime, Space.World);
                }

                if (Input.GetKey(KeyCode.DownArrow))
                {
                    this.trans.Rotate(Vector3.right, 100.0f * Time.deltaTime, Space.World);
                }

                if (Input.GetKey(KeyCode.RightArrow))
                {
                    this.trans.Rotate(Vector3.right, 10.0f * Time.deltaTime, Space.World);
                }

                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    this.trans.Rotate(Vector3.right, 10.0f * Time.deltaTime, Space.World);
                }

                break;

            case AxisMode.Y:
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    this.trans.Rotate(Vector3.up, 100.0f * Time.deltaTime, Space.World);
                }

                if (Input.GetKey(KeyCode.DownArrow))
                {
                    this.trans.Rotate(Vector3.down, 100.0f * Time.deltaTime, Space.World);
                }

                if (Input.GetKey(KeyCode.RightArrow))
                {
                    this.trans.Rotate(Vector3.up, 10.0f * Time.deltaTime, Space.World);
                }

                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    this.trans.Rotate(Vector3.down, 10.0f * Time.deltaTime, Space.World);
                }

                break;

            case AxisMode.Z:
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    this.trans.Rotate(Vector3.forward, 100.0f * Time.deltaTime, Space.World);
                }

                if (Input.GetKey(KeyCode.DownArrow))
                {
                    this.trans.Rotate(Vector3.back, 100.0f * Time.deltaTime, Space.World);
                }

                if (Input.GetKey(KeyCode.RightArrow))
                {
                    this.trans.Rotate(Vector3.forward, 10.0f * Time.deltaTime, Space.World);
                }

                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    this.trans.Rotate(Vector3.back, 10.0f * Time.deltaTime, Space.World);
                }

                break;

            default: Debug.LogError("Impossible AxisMode"); break;
        }
    }

    private void EditScale()
    {
        ChangeAxisMode();
        
        switch(this.axisMode)
        {
            case AxisMode.X:
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    this.trans.localScale += Vector3.right * Time.deltaTime * 100.0f;
                }

                if (Input.GetKey(KeyCode.DownArrow))
                {
                    this.trans.localScale += Vector3.left * Time.deltaTime * 100.0f;
                }

                if (Input.GetKey(KeyCode.RightArrow))
                {
                    this.trans.localScale += Vector3.right * Time.deltaTime * 10.0f;
                }

                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    this.trans.localScale += Vector3.left * Time.deltaTime * 10.0f;
                }

                break;

            case AxisMode.Y:
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    this.trans.localScale += Vector3.up * Time.deltaTime * 100.0f;
                }

                if (Input.GetKey(KeyCode.DownArrow))
                {
                    this.trans.localScale += Vector3.down * Time.deltaTime * 100.0f;
                }

                if (Input.GetKey(KeyCode.RightArrow))
                {
                    this.trans.localScale += Vector3.up * Time.deltaTime * 10.0f;
                }

                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    this.trans.localScale += Vector3.down * Time.deltaTime * 10.0f;
                }

                break;

            case AxisMode.Z:
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    this.trans.localScale += Vector3.forward * Time.deltaTime * 100.0f;
                }

                if (Input.GetKey(KeyCode.DownArrow))
                {
                    this.trans.localScale += Vector3.back * Time.deltaTime * 100.0f;
                }

                if (Input.GetKey(KeyCode.RightArrow))
                {
                    this.trans.localScale += Vector3.forward * Time.deltaTime * 10.0f;
                }

                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    this.trans.localScale += Vector3.back * Time.deltaTime * 10.0f;
                }

                break;

            default: Debug.LogError("Impossible AxisMode"); break;
        }
    }

    private void EditSpeed()
    {
        if (Input.GetKey(KeyCode.UpArrow)) { this.speed += 1; }
        if (Input.GetKey(KeyCode.DownArrow)) { this.speed += -1; }
        if (Input.GetKey(KeyCode.RightArrow)) { this.speed += 0.1f; }
        if (Input.GetKey(KeyCode.LeftArrow)) { this.speed += -0.1f; }

        SendMode();
    }

    private void ChangeAxisMode()
    {
        if (Input.GetKey(KeyCode.X)) { this.axisMode = AxisMode.X; }
        if (Input.GetKey(KeyCode.Y)) { this.axisMode = AxisMode.Y; }
        if (Input.GetKey(KeyCode.Z)) { this.axisMode = AxisMode.Z; }

        SendMode();
    }

    /// <summary>
    /// TestUIManagerにModeの状態を送る
    /// </summary>
    private void SendMode()
    {
        this.testUiManager.SetEditMode(this.editMode.ToString());
        this.testUiManager.SetAxisMode(this.axisMode.ToString());
    }

    /// <summary>
    /// TestUIManagerにパラメータの値を送る
    /// </summary>
    private void SendParam()
    {
        this.testUiManager.SetParam(this.trans, speed);
    }
}
