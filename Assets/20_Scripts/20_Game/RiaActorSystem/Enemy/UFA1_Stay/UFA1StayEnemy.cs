using UnityEngine;
using RiaActorSystem;

namespace Game.Enemy
{
	using Game.Bullet.Enemy;

	public class UFA1StayEnemy : RiaEnemy
	{
		private new UFA1StayEnemyScript Script;

		// Bullet関係
		public class Shooter
		{
			private float stayTime = 2.0f;
			private float shotInterval;
			private float elapsedTime;
			private float shotTime;
			private EnemyBulletActorManager manager;
			private Transform trans;

			public Shooter(UFA1StayEnemyScript.ShooterParam _param, EnemyBulletActorManager _manager,
				Transform _trans)
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
					this.manager.CreateStayEnemyBullet(this.trans.position);
					this.shotTime -= this.shotInterval;
				}
			}
		}

		private Shooter shooter;

		#region Constructor

		public UFA1StayEnemy(GameObject _go, RiaCharacterScript _script, PlayerNumber _playerNumber) : base(_go,
			_script, _playerNumber)
		{
			this.Script = _script as UFA1StayEnemyScript;

			// bullet関係
			this.shooter = new Shooter(this.Script.ShotParam, this.bulletManager, this.Trans);
		}

		#endregion

		#region Override Function

		protected override void OnInit()
		{
		}

		protected override void OnWait()
		{
		}

		protected override void OnPlay()
		{
			//// 攻撃処理
			//this.Shot();

			//// 移動処理
			//this.Move();

			//// 衝突処理
			//this.Collision();

			//// 生死処理
			//this.DeadCheck();
		}

		protected override void OnEnd()
		{
		}

		public override void Shot()
		{
			this.shooter.Update();
		}

		public override void Move()
		{
			this.elapsedTime += Time.deltaTime;
			if (this.elapsedTime >= this.Script.StayTime)
			{
				base.BackMove();
			}
			else if (this.Trans.position.y > this.Script.OrdinaryYForwardBorder)
			{
				this.YForwardEnemy(this.Script.OrdinaryYForwardBorder);
			}

			//Debug.Log(this.elapsedTime + "あああ" + this.Script.StayTime);
		}

		public override void Animation()
		{
		}

		#endregion
	}
}