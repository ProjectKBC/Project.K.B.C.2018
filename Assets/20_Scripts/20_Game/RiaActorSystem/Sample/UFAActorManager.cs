using UnityEngine;

public sealed class UFAActorManager : RiaActorManager
{
    [SerializeField]
    private PlayerNumber playerNumber = PlayerNumber.player1;

    [SerializeField]
    private UFAActorFactory factory = null;

    [SerializeField]
    private float createUFASpan = 1.0f;
    private float createUFATime;
    private float createUFAElapsedTime { get { return this.PlayElapsedTime - this.createUFATime; } }
    private int countUFA;

    protected override void OnInitialize()
    {
        this.createUFATime = this.PlayElapsedTime;
        this.countUFA = 0;
    }

    protected override void OnUpdate()
    {
        if (this.createUFASpan < this.createUFAElapsedTime)
        {
            var actor = this.GetFreeActor();

            if (!actor) { return; }

            // 画面端とかの構造体あったらいいなぁ by flanny
            var pos = (this.playerNumber == PlayerNumber.player1) ?
                      new Vector3(-42.5f, 70) :
                      new Vector3(42.5f, 70);

            if (countUFA == 0)
            {
                this.factory.CreateUFA(
                    UFAActorFactory.UFACategory.UFA1,
                    this.playerNumber,
                    actor,
                    pos);
            }
            else
            {
                this.factory.CreateUFA(
                    UFAActorFactory.UFACategory.UFA2,
                    this.playerNumber,
                    actor,
                    pos);
            }
            
            this.createUFATime = this.PlayElapsedTime;
            countUFA = (countUFA + 1) % 2;
        }
    }
}