using UnityEngine;

[CreateAssetMenu(menuName = "RiaCharacterScript/UFA2Script", fileName = "UFA2Script")]
public sealed class UFA2Script : RiaCharacterScript
{
	public float HitDamagePoint { get { return this.hitDamagePoint; } private set { this.hitDamagePoint = value; } }

    [SerializeField]
    private Sprite sprite = null;
    [SerializeField]
    private float maxHitPoint = 1;
    [SerializeField]
    private float moveSpeed = 1;
    [SerializeField]
    private float hitDamagePoint = 1;
    [SerializeField]
    private float survivalTime = 5f;

    protected override void OnInit(RiaCharacterStatus _status)
    {
        var status = _status as UFA2Status;

        status.SpriteRenderer.sprite = this.sprite;
        status.HitPoint = this.maxHitPoint;
    }

    protected override void OnPlay(RiaCharacterStatus _status)
    {
        var status = _status as UFA2Status;

        Move(status);

        if (this.survivalTime < status.playElapsedTime)
        {
            status.actor_.Sleep();
        }
    }

    private void Move(UFA2Status status)
    {
        var offset = (status.PlayerNumber == PlayerNumber.player1) ? -42.5f : 42.5f;

        var pos = status.trans_.position;
        pos.y -= Time.deltaTime * 60 * this.moveSpeed;
        pos.x = Mathf.Sin(Time.time) * 15 + offset;

        status.trans_.position = pos;
    }

    protected override void OnEnd(RiaCharacterStatus _status)
    {
        //var status = _status as UFA2Status;
    }
}