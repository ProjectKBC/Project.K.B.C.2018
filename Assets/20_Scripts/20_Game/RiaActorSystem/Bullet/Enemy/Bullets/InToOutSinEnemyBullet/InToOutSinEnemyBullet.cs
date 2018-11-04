using UnityEngine;
using RiaActorSystem;
using RiaSpriteAnimationSystem;

namespace Game.Bullet.Enemy
{
	public sealed class InToOutSinEnemyBullet : RiaEnemyBullet
	{
		// CharacterScriptの上書き
		private new InToOutSinEnemyBulletScript Script;
		private Vector3 myAppearPos;
		private Vector3 playerPos;

		public InToOutSinEnemyBullet(GameObject _go, RiaCharacterScript _script, PlayerNumber _playerNumber) : base(_go, _script, _playerNumber)
		{
			// CharacterScriptの上書き
			this.Script = _script as InToOutSinEnemyBulletScript;

			// アニメーションの設定
			RiaSpriteAnimation[] animations = { this.Script.Animation };
			this.animator.SetAnimations(animations, this.Script.Animation.KeyName);
		}

		// 基本的なやつ

		protected override void OnInit()
		{
			this.myAppearPos = this.Trans.position;
			this.playerPos = GameManager.Instance.GetPlayer(this.PlayerNumber).Trans.position;
		}

		protected override void OnWait()
		{
		}

		protected override void OnPlay()
		{
			//// 生成系処理
			//this.Division();

			//// 移動処理
			//this.Move();

			//// アニメーション処理
			//this.Animation();

			//// 衝突処理
			//this.Collision();

			//// 生死処理
			//this.DeadCheck();
		}

		protected override void OnEnd()
		{
		}

		// 細かいやつ

		/// <summary>
		/// 生成系処理 by flanny7
		/// </summary>
		public override void Division()
		{
		}
		
		/// <summary>
		/// 移動処理 by flanny7
		/// </summary>
		public override void Move()
		{
			Vector3 pos = this.Trans.position;
			Vector3 vectorMyselfPlayer = new Vector3(this.playerPos.x - this.myAppearPos.x,
				this.playerPos.y - this.myAppearPos.y, this.myAppearPos.z);

			
			pos.x += (vectorMyselfPlayer.x * this.MoveSpeed * Time.deltaTime);
			pos.y += (vectorMyselfPlayer.y * this.MoveSpeed * Time.deltaTime);
        
			this.Trans.position = pos;		}

		/// <summary>
		/// アニメーション処理 by flanny7
		/// </summary>
		public override void Animation()
		{
			this.animator.Run();
		}
	}
}
