using UnityEngine;

public abstract class RiaActorManager : MonoBehaviour// : SingletonMonoBehaviour<RiaActorManager>
{
    [SerializeField]
    private GameObject[] objects = new GameObject[0];

    public float PlayElapsedTime { get; protected set; }
    protected GameObject[] Objects { get { return this.objects; } set { this.objects = value; } }
    protected RiaActor[] Actors { get; set; }

    private bool isInit;

    /// <summary>
    /// Awake
    /// </summary>
    protected void Awake()
    {
        this.isInit = false;
    }

    public void Init()
    {
        Actors = new RiaActor[objects.Length];
        for (int i = 0; i < objects.Length; ++i)
        {
            Actors[i] = objects[i].GetComponent<RiaActor>();
            if (Actors[i] == null)
            {
                Debug.LogError(
                    "<color=#f00>" + objects[i].name + "has not " + typeof(RiaActor) + ".</color>",
                    objects[i]);
            }

            Actors[i].Init();
        }

        this.PlayElapsedTime = 0f;

        this.OnInitialize();

        this.isInit = true;
    }

    public void Play()
    {
        if (!this.isInit) { Debug.LogError("Initializeされていません。", this.gameObject); return; }

        this.PlayElapsedTime += Time.deltaTime;

        for (int i = 0; i < Actors.Length; ++i)
        {
            Actors[i].Play();
        }

        this.OnUpdate();
    }

    protected RiaActor GetFreeActor()
    {
        if (!this.isInit) { Debug.LogError("Initializeされていません。", this.gameObject); return null; }

        for (int i = 0; i < Actors.Length; ++i)
        {
            if (!Actors[i].IsActive)
            {
                return Actors[i];
            }
        }

        Debug.LogWarning("キャパシティーを超えました", this.gameObject);
        return null;
    }

    protected abstract void OnInitialize();
    protected abstract void OnUpdate();
}

