/* Author : flanny7
 * Update : 2018/10/22
*/

using UnityEngine;
using RiaActorSystem;

namespace Game.Player
{
	public abstract class RiaPlayer : RiaCharacter
	{
		public float HitPoint { get; private set; }
		
		protected new RiaPlayerScript Script;
		protected SpriteRenderer spRender;

		public RiaPlayer(GameObject _go, RiaCharacterScript _script, PlayerNumber _playerNumber) : base(_go, _script, _playerNumber)
		{
			this.Script = _script as RiaPlayerScript;
			this.HitPoint = this.Script.MaxHitPoint;

			this.spRender = this.Go.GetComponent<SpriteRenderer>();
			this.spRender.sprite = this.Script.Sprite;
		}

		protected virtual void Move()
		{
			// Down
			if (RiaInput.Instance.GetPush(RiaInput.KeyType.Up, this.PlayerNumber))
			{
				this.Trans.position += Vector3.up * this.Script.MoveSpeed * Time.deltaTime * 60;
			}
			// Up
			if (RiaInput.Instance.GetPush(RiaInput.KeyType.Down, this.PlayerNumber))
			{
				this.Trans.position += Vector3.down * this.Script.MoveSpeed * Time.deltaTime * 60;
			}
			// Right
			if (RiaInput.Instance.GetPush(RiaInput.KeyType.Right, this.PlayerNumber))
			{
				this.Trans.position += Vector3.right * this.Script.MoveSpeed * Time.deltaTime * 60;
			}
			// Left
			if (RiaInput.Instance.GetPush(RiaInput.KeyType.Left, this.PlayerNumber))
			{
				this.Trans.position += Vector3.left * this.Script.MoveSpeed * Time.deltaTime * 60;
			}

			// 画面外処理
			var pos = this.Trans.position;
			var leftLine = (this.PlayerNumber == PlayerNumber.player1) ?
				PlayableArea.pl1AreaLeftLine : PlayableArea.pl2AreaLeftLine;

			var rightLine = (this.PlayerNumber == PlayerNumber.player1) ?
				PlayableArea.pl1AreaRightLine : PlayableArea.pl2AreaRightLine;

			this.Trans.position =
			new Vector3(
				Mathf.Clamp(pos.x, leftLine, rightLine),
				Mathf.Clamp(pos.y, PlayableArea.playAreaBottomLine, PlayableArea.playAreaTopLine),
				pos.z);
		}
	}
}