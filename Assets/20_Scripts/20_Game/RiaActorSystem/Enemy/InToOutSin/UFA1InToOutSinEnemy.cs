using RiaActorSystem;
using UnityEngine;

namespace Game.Enemy
{
	using Game.Bullet.Enemy;
	
	public sealed class UFA1InToOutSinEnemy : RiaEnemy
	{
		private new UFA1InToOutSinEnemyScript Script;
		
		public class Shooter
		{
			private float shotInterval;
			private float elapsedTime;
			private float shotTime;
			private EnemyBulletActorManager manager;
			private Transform trans;

			public Shooter(UFA1InToOutSinEnemyScript.ShooterParam _param, EnemyBulletActorManager _manager, Transform _trans)
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
					this.manager.CreateInToOutSinEnemyBullet(this.trans.position);
					this.shotTime -= this.shotInterval;
				}
				
			}
		}
		
		public struct ShotParam
		{
			public float elapsedTime;	
		}
		
		public struct MoveParam
		{
			public enum State
			{
				Go,
				Stay,
				Back,
			}

			public State state;
			public Vector3 startPos_;
			public float stayElapsedTime;
		}

		private Shooter shooter;
		private MoveParam move;
		
		public UFA1InToOutSinEnemy(GameObject _go, RiaCharacterScript _script, PlayerNumber _playerNumber) :
			base(_go, _script, _playerNumber)
		{
			this.Script = _script as UFA1InToOutSinEnemyScript;
			
			// move
			this.move.state = MoveParam.State.Go;
			this.move.startPos_ = this.Trans.position;
			this.move.stayElapsedTime = 0;
			this.shooter = new Shooter(this.Script.ShotParam, this.bulletManager, this.Trans);
		}

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

		public override void Shot()
		{
			this.shooter.Update();
		}

		public override void Move()
		{
			Vector3 pos = this.Trans.position;
			var direction = (this.PlayerNumber == PlayerNumber.player1) ? -1 : 1;
			pos.x += this.Script.MoveSpeedBase * Time.deltaTime * direction;
			pos.y = Mathf.Sin(pos.x / this.Script.MoveSpeedBase) * this.Script.MoveSpeedBase;
			this.Trans.position = pos;
		}

		public override void Animation()
		{
		}

	}
}