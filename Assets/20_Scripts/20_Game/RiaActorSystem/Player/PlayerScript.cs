using UnityEngine;

namespace Game.Player
{
	public abstract class PlayerScript : RiaCharacterScript
	{
		[SerializeField]
		protected Sprite sprite = null;
		[SerializeField]
		protected float maxHitPoint = 1;
		[SerializeField]
		protected float moveSpeed = 1;
		[SerializeField]
		protected float hitDamagePoint = 1;

		protected override void OnInit(RiaCharacterStatus _status)
		{
			var status = _status as PlayerStatus;

			status.SpriteRenderer.sprite = this.sprite;
			status.HitPoint = this.maxHitPoint;

			this.OnInit(status);
		}

		protected override void OnPlay(RiaCharacterStatus _status)
		{
			var status = _status as PlayerStatus;

			this.Move(status);

			this.OnPlay(status);
		}

		protected override void OnEnd(RiaCharacterStatus _status)
		{
			var status = _status as PlayerStatus;

			this.OnEnd(status);
		}

		// 移動
		protected virtual void Move(PlayerStatus _status)
		{
			// Down
			if (RiaInput.Instance.GetKey(RiaInput.KeyType.Up, _status.PlayerNumber))
			{
				_status.trans_.position += Vector3.up * _status.MoveSpeedRate * this.moveSpeed * Time.deltaTime;
			}
			// Up
			if (RiaInput.Instance.GetKey(RiaInput.KeyType.Down, _status.PlayerNumber))
			{
				_status.trans_.position += Vector3.down * _status.MoveSpeedRate * this.moveSpeed * Time.deltaTime;
			}
			// Right
			if (RiaInput.Instance.GetKey(RiaInput.KeyType.Right, _status.PlayerNumber))
			{
				_status.trans_.position += Vector3.right * _status.MoveSpeedRate * this.moveSpeed * Time.deltaTime;
			}
			// Left
			if (RiaInput.Instance.GetKey(RiaInput.KeyType.Left, _status.PlayerNumber))
			{
				_status.trans_.position += Vector3.left * _status.MoveSpeedRate * this.moveSpeed * Time.deltaTime;
			}

			// 画面外処理
			var pos = _status.trans_.position;
			var leftLine = (_status.PlayerNumber == PlayerNumber.player1) ?
				PlayableArea.pl1AreaLeftLine : PlayableArea.pl2AreaLeftLine;
			var rightLine = (_status.PlayerNumber == PlayerNumber.player1) ?
				PlayableArea.pl1AreaRightLine : PlayableArea.pl2AreaRightLine;

			_status.trans_.position =
			new Vector3(
				Mathf.Clamp(pos.x, leftLine, rightLine),
				Mathf.Clamp(pos.y, PlayableArea.playAreaBottomLine, PlayableArea.playAreaTopLine),
				pos.z);
		}
	}

}