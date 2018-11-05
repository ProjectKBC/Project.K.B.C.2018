using UnityEngine;
using RiaActorSystem;
using RiaSpriteAnimationSystem;

namespace Game.Bullet.Player
{
	public sealed class AirosSpecialBullet : RiaPlayerBullet
	{
		// CharacterScriptの上書き
		private new AirosSpecialBulletScript Script;

		public AirosSpecialBullet(GameObject _go, RiaCharacterScript _script, PlayerNumber _playerNumber) : base(_go, _script, _playerNumber)
		{
			// CharacterScriptの上書き
			this.Script = _script as AirosSpecialBulletScript;

			// RiaSpriteAnimation[] animations = { this.Script.Animation };
			// this.animator.SetAnimations(animations, this.Script.Animation.KeyName);
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
			this.animator.Run();
		}

		protected override void OnCollision()
		{
		}

		#endregion
	}
}