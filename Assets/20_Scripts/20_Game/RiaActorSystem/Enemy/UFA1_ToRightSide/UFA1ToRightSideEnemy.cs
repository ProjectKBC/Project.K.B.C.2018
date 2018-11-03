using UnityEngine;
using RiaActorSystem;

namespace Game.Enemy
{
	using Game.Bullet.Enemy;

	public class UFA1ToRightSideEnemy : RiaEnemy
	{
		private new UFA1ToRightSideEnemyScript Script;
		protected float topLeftPosX;
		protected float topRightPosX;

		// Bullet関係
		public class Shooter
		{
			private float stayTime = 2.0f;
			private float shotInterval;
			private float elapsedTime;
			private float shotTime;
			private EnemyBulletActorManager manager;
			private Transform trans;

			public Shooter(UFA1ToRightSideEnemyScript.ShooterParam _param, EnemyBulletActorManager _manager,
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

				/*
				if (this.shotInterval <= this.shotTime)
				{
					this.manager.CreateToRightSideEnemyBullet(this.trans.position);
					this.shotTime -= this.shotInterval;
				}
				*/
			}
			
		}

		private Shooter shooter;

		#region Constructor

		public UFA1ToRightSideEnemy(GameObject _go, RiaCharacterScript _script, PlayerNumber _playerNumber) : base(_go,
			_script, _playerNumber)
		{
			this.Script = _script as UFA1ToRightSideEnemyScript;

			// bullet関係
			this.shooter = new Shooter(this.Script.ShotParam, this.bulletManager, this.Trans);
			
			if (this.PlayerNumber.Equals(PlayerNumber.player1))
			{
				this.topLeftPosX = -78.0f;
				this.topRightPosX = -10.0f;
			}
			else if (this.PlayerNumber.Equals(PlayerNumber.player2))
			{
				this.topRightPosX = 78.0f;
				this.topLeftPosX = 10.0f;
			}
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
			if (this.Trans.position.x > this.Script.XForwardBorder)
			{
				this.XForwardEnemy(this.Script.XForwardBorder);
			}
			else
			{
				this.ToRightSideMove();
			}

		}

		protected void ToRightSideMove()
		{
			var pos = this.Trans.position;
			pos.x += this.MoveSpeed * Time.deltaTime;
			this.Trans.position = pos;
		}

		public override void Animation()
		{

		}

		#endregion
	}
}