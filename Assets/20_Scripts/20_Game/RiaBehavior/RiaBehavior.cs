using UnityEngine;

public abstract class RiaBehavior : MonoBehaviour
{
    #region STATIC READONLY

    protected static readonly Vector3 VECTOR_ZERO = Vector3.zero;
    protected static readonly Vector3 VECTOR_ONE = Vector3.one;
    protected static readonly Quaternion ROTATE_NONE = Quaternion.identity;

    #endregion STATIC READONLY

    #region PUBLIC MEMBER

    public bool Alive { get; protected set; }

    #endregion PUBLIC MEMBER

    #region SERIALIZE PRIVATE MEMBER

    [SerializeField]
    private bool isDebug = false;

    #endregion SERIALIZE PRIVATE MEMBER

    #region PROTECTED MEMBER

    protected GameObject go_ = null;
    protected Transform trans_ = null;
    protected float elapsedTime = 0;

    #endregion PROTECTED MEMBER

    #region PRIVATE MEMBER

    private Vector3 firstPos;
    private Vector3 firstScale;
    private Quaternion firstRotate;

    #endregion PRIVATE MEMBER

    /// <summary>
    /// RiaManagerのUpdate関数内で呼び出す関数
    /// </summary>
    public void Run()
    {
        if (!Alive) { return; }

        elapsedTime += Time.deltaTime;

        if (isDebug) { Debug.Log("Run() in " + this.name); }

        OnRun();
    }

    /// <summary>
    /// RiaManagerのAwake関数内で呼び出す関数
    /// </summary>
    public void Init()
    {
        if (isDebug) { Debug.Log("Init() in " + this.name); }

        this.go_ = this.gameObject;
        this.trans_ = this.transform;
        this.elapsedTime = 0;

        this.firstPos = this.trans_.position;
        this.firstRotate = this.trans_.localRotation;
        this.firstScale = this.trans_.localScale;

        this.go_.SetActive(false);
        this.Alive = false;

        OnInit();
    }

    /// <summary>
    /// SetActive(false)のようなもの
    /// </summary>
    public void Sleep()
    {
        if (isDebug) { Debug.Log("Sleep() in " + this.name); }

        this.elapsedTime = 0;

        this.go_.SetActive(false);
        this.Alive = false;

        OnSleep();
    }

    /// <summary>
    /// SetActive(true)のようなもの
    /// </summary>
    public void WakeUp()
    {
        if (isDebug) { Debug.Log("WakeUp() in " + this.name); }

        this.go_.SetActive(true);
        this.Alive = true;

        OnWakeUp(this.firstPos, this.firstRotate);
    }

    /// <summary>
    /// SetActive(true)のようなもの
    /// </summary>
    /// <param name="_position"></param>
    /// <param name="_rotation"></param>
    public void WakeUp(Vector3 _position, Quaternion _rotation)
    {
        if (isDebug) { Debug.Log("WakeUp() in " + this.name); }

        this.trans_.localPosition = _position;
        this.trans_.localRotation = _rotation;
        this.trans_.localScale = this.firstScale;
        this.go_.SetActive(true);
        this.Alive = true;

        OnWakeUp(_position, _rotation);
    }

    protected abstract void OnRun();

    protected abstract void OnInit();

    protected abstract void OnSleep();

    protected abstract void OnWakeUp(Vector3 _position, Quaternion _rotation);
}