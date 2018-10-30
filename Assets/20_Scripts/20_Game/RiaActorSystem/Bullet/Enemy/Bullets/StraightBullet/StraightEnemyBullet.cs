/* Author: flanny7
 * Update: 2018/10/30
*/

using UnityEngine;
using RiaActorSystem;
using RiaSpriteAnimationSystem;

namespace Game.Bullet.Enemy
{
	public sealed class StraightEnemyBullet : RiaEnemyBullet
	{
		// CharacterScriptの上書き
		private new StraightEnemyBulletScript Script;

		public StraightEnemyBullet(GameObject _go, RiaCharacterScript _script, PlayerNumber _playerNumber) : base(_go, _script, _playerNumber)
		{
			// CharacterScriptの上書き
			this.Script = _script as StraightEnemyBulletScript;

            // アニメーションの設定
            RiaSpriteAnimation[] animations = { this.Script.Animation };
			this.animator.SetAnimations(animations, this.Script.Animation.KeyName);
		}

		// 基本的なやつ

		protected override void OnInit()
		{
		}

		protected override void OnWait()
		{
		}

		protected override void OnPlay()
		{
			// 生成系処理
			this.Division();

			// 移動処理
			this.Move();

			// アニメーション処理
			this.Animation();

			// 衝突処理
			this.Collision();

			// 生死処理
			this.DeadCheck();
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
            this.Trans.position += Vector3.down * this.MoveSpeed * Time.deltaTime * 60.0f;
        }

        /// <summary>
        /// アニメーション処理 by flanny7
        /// </summary>
        public override void Animation()
		{
			this.animator.Run();
		}
	}
}
