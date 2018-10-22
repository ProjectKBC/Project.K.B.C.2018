using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class RiaActorManager : MonoBehaviour
{
    [SerializeField]
    private RiaActor[] actors = new RiaActor[0];

    public float PlayElapsedTime { get; protected set; }
    protected RiaActor[] Actors { get { return this.actors; } set { this.actors = value; } }

    private bool isInit = false;

    public void Init()
    {
        for (int i = 0; i < actors.Length; ++i)
        {
			actors[i].Init();
        }
		
		this.isInit = true;

		this.PlayElapsedTime = 0f;

        this.OnInitialize();
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

	public RiaActor[] GetActiveActors()
	{
		return this.Actors.Where(x => x.IsActive == true).ToArray<RiaActor>();
	}

    protected abstract void OnInitialize();
    protected abstract void OnUpdate();
}