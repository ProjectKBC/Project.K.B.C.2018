/* Author : flanny7
 * Update : 2018/10/30
*/

using UnityEngine;
using RiaActorSystem;

namespace Game.Enemy
{
	using Game.Bullet.Enemy;

	public sealed class UFA1StraightEnemy : RiaEnemy
	{
		private new UFA1StraightEnemyScript Script;

		// Bullet関係
		public class Shooter
		{
			private float shotInterval;
			private float elapsedTime;
			private float shotTime;
			private EnemyBulletActorManager manager;
			private Transform trans;

			public Shooter(UFA1StraightEnemyScript.ShooterParam _param, EnemyBulletActorManager _manager, Transform _trans)
			{
				this.shotInterval = _param.shotInterval;

				this.elapsedTime = 0;
				this.shotTime = 0;

				this.manager = _manager;
				this.trans = _trans;
			}

			public void Update()
			{
				var deltaTime = Time.deltaTime;
				this.elapsedTime += deltaTime;
				this.shotTime += deltaTime;

				if (this.shotInterval <= this.shotTime)
				{
					this.manager.CreateStraightEnemyBullet(this.trans.position);
					this.shotTime -= this.shotInterval;
				}
			}
		}
		
		private Shooter shooter;

		public UFA1StraightEnemy(GameObject _go, RiaCharacterScript _script, PlayerNumber _playerNumber) : base(_go, _script, _playerNumber)
		{
			this.Script = _script as UFA1StraightEnemyScript;

			// bullet関係
			this.shooter = new Shooter(this.Script.ShotParam, this.bulletManager, this.Trans);
		}
		
		// メイン関数

		protected override void OnInit()
		{

		}

		protected override void OnWait()
		{

		}

		protected override void OnPlay()
		{
			// 攻撃処理
			this.Shot();

			// 移動処理
			this.Move();

			// 衝突処理
			this.Collision();

			// 生死処理
			this.DeadCheck();
		}

		protected override void OnEnd()
		{

		}

		// 
		
		protected override void Shot()
		{
			this.shooter.Update();
		}

		protected override void Move()
		{
			this.Trans.position += Vector3.down * this.MoveSpeed * Time.deltaTime * 60.0f;
		}

		protected override void Dead()
		{
			// todo: 爆発FXの生成
			// todo: 撃破SE
		}
	}
}