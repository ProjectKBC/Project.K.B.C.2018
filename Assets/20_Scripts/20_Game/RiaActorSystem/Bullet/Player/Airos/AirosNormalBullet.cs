using UnityEngine;
using RiaActorSystem;

namespace Game.Bullet.Player
{
	public sealed class AirosNormalBullet : RiaPlayerBullet
	{
		// CharacterScriptの上書き
		private new AirosNormalBulletScript Script;

		public AirosNormalBullet(GameObject _go, RiaCharacterScript _script, PlayerNumber _playerNumber) : base(_go, _script, _playerNumber)
		{
			// CharacterScriptの上書き
			this.Script = _script as AirosNormalBulletScript;
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