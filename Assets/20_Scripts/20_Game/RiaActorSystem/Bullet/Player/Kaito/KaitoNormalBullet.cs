using UnityEngine;
using RiaActorSystem;

namespace Game.Bullet.Player
{
	public sealed class KaitoNormalBullet : RiaPlayerBullet
	{
		// CharacterScriptの上書き
		private new KaitoNormalBulletScript Script;

		// キャッシュ
		private SpriteRenderer spRender;

		public KaitoNormalBullet(GameObject _go, RiaCharacterScript _script, PlayerNumber _playerNumber) : base(_go, _script, _playerNumber)
		{
			// CharacterScriptの上書き
			this.Script = _script as KaitoNormalBulletScript;

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
			float angleDir = this.Trans.eulerAngles.z * (Mathf.PI / 180.0f);
			Vector3 dir = new Vector3(-Mathf.Sin(angleDir), Mathf.Cos(angleDir), 0);
			this.Trans.position += dir * this.MoveSpeed * Time.deltaTime * 60.0f;
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