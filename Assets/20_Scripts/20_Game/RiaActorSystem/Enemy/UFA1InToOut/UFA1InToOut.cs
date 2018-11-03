/* Author: sukapenpen
 * Update 2018/11/2
*/

using RiaActorSystem;
using UnityEngine;

namespace Game.Enemy
{
	using Game.Bullet.Enemy;
	
	public sealed class UFA1InToOut : RiaEnemy
	{
		private new UFA1InToOutScript Script;
		
		public class Shooter
		{
			private float shotInterval;
			private float elapsedTime;
			private float shotTime;
			private EnemyBulletActorManager manager;
			private Transform trans;

			public Shooter(UFA1InToOutScript.ShooterParam _param, EnemyBulletActorManager _manager, Transform _trans)
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
					this.manager.CreateInToOutEnemyBullet(this.trans.position);
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
		
		public UFA1InToOut(GameObject _go, RiaCharacterScript _script, PlayerNumber _playerNumber) :
			base(_go, _script, _playerNumber)
		{
			this.Script = _script as UFA1InToOutScript;
			
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
			this.UpdateGo();
			this.UpdateStay();
			this.UpdateBack();
		}

		public override void Animation()
		{
		}

		/// <summary>
		/// 行きの移動処理 by sukapenpen
		/// </summary>
		private void UpdateGo()
		{
			if (this.move.state != MoveParam.State.Go) { return; }
			
			// 移動処理
			var direction = (this.PlayerNumber == PlayerNumber.player1) ? -1 : 1;
			
			var pos = this.Trans.position;
			pos.x += this.Script.MoveSpeedBase * Time.deltaTime * direction;
			this.Trans.position = pos;
			
			// 移動しきったかを判定
			var result = (this.PlayerNumber == PlayerNumber.player1) ? 
				/* player1 */ this.Script.Move.oneWayDistance <= this.move.startPos_.x - this.Trans.position.x:
				/* player2 */ this.Script.Move.oneWayDistance <= -1 * this.move.startPos_.x + this.Trans.position.x;

			if (result)
			{
				this.move.state = MoveParam.State.Stay;
			}
		}

		/// <summary>
		/// 待機の移動処理 by sukapenpen
		/// </summary>
		private void UpdateStay()
		{
			if (this.move.state != MoveParam.State.Stay) { return; }
			
			// 待機
			this.move.stayElapsedTime += Time.deltaTime;
			if (this.Script.Move.stayTime <= this.move.stayElapsedTime)
			{
				this.move.state = MoveParam.State.Back;
			}
		}

		/// <summary>
		/// 帰りの移動処理 by sukapenpne
		/// </summary>
		private void UpdateBack()
		{
			if (this.move.state != MoveParam.State.Back) { return; }

			// 移動処理
			var direction = (this.PlayerNumber == PlayerNumber.player1) ? 1 : -1;
			
			var pos = this.Trans.position;
			pos.x += this.Script.MoveSpeedBase * Time.deltaTime * direction;
			this.Trans.position = pos;

		}
	}
}