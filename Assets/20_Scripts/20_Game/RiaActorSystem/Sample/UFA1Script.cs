using UnityEngine;

[CreateAssetMenu(menuName = "RiaCharacterScript/UFA1Script", fileName = "UFA1Script")]
public sealed class UFA1Script : RiaCharacterScript
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
        var status = _status as UFA1Status;

        status.SpriteRenderer.sprite = this.sprite;
        status.HitPoint = this.maxHitPoint;
    }

    protected override void OnPlay(RiaCharacterStatus _status)
    {
        var status = _status as UFA1Status;

        Move(status);

        if (this.survivalTime < status.playElapsedTime)
        {
            status.actor_.Sleep();
        }
    }

    private void Move(UFA1Status status)
    {
        status.trans_.position += Vector3.down * Time.deltaTime * 60 * this.moveSpeed;
    }

    protected override void OnEnd(RiaCharacterStatus _status)
    {
        //var status = _status as UFA1Status;
    }
}