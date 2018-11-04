using UnityEngine;
using RiaActorSystem;

namespace Game.Bullet.Player
{
	public sealed class VeronicaNormalBullet : RiaPlayerBullet
	{
		// CharacterScriptの上書き
		private new VeronicaNormalBulletScript Script;

		// キャッシュ
		private SpriteRenderer spRender;

		public VeronicaNormalBullet(GameObject _go, RiaCharacterScript _script, PlayerNumber _playerNumber) : base(_go, _script, _playerNumber)
		{
			// CharacterScriptの上書き
			this.Script = _script as VeronicaNormalBulletScript;

			this.spRender = this.Actor.GetComponent<SpriteRenderer>();
			this.spRender.sprite = this.Script.Sprite;
		}

		#region Override Function

		protected override void OnInit()
		{
		}

		protected override void OnWait()
		{
		}

		protected override void OnPlay()
		{
		}

		protected override void OnEnd()
		{
		}

		public override void Division()
		{
		}

		public override void Move()
		{
			this.Trans.position += Vector3.up * this.MoveSpeed * Time.deltaTime * 60.0f;
		}

		public override void Animation()
		{
		}

		protected override void OnCollision()
		{
		}

		#endregion
	}
}