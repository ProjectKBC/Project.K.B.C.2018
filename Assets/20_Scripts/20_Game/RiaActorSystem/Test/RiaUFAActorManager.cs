using UnityEngine;

public sealed class RiaUFAActorManager : RiaActorManager
{
    [SerializeField]
    private PlayerNumber playerNumber = PlayerNumber.player1;

    [SerializeField]
    private UFA1Script ufa1Script = null;
    [SerializeField]
    private UFA2Script ufa2Script = null;

    private UFA1Status UFA1Status(GameObject _go) { return new UFA1Status(_go, this.playerNumber); }
    private UFA2Status UFA2Status(GameObject _go) { return new UFA2Status(_go, this.playerNumber); }

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

            var pos = (this.playerNumber == PlayerNumber.player1) ?
                      new Vector3(-42.5f, 70) :
                      new Vector3(42.5f, 70);

            if (countUFA == 0)
            {
                var status = this.UFA1Status(actor.gameObject);
                var script = this.ufa1Script;

                actor.WakeUp(status, script, pos);
            }
            else
            {
                var status = this.UFA2Status(actor.gameObject);
                var script = this.ufa2Script;

                actor.WakeUp(status, script, pos);
            }
            
            this.createUFATime = this.PlayElapsedTime;
            countUFA = (countUFA + 1) % 2;
        }
    }
}