/* Author: sukapenpen
 * Update 2018/11/2
*/

using RiaActorSystem;
using UnityEngine;

namespace Game.Enemy
{
	using Game.Bullet.Enemy;
	
	public sealed class UFA1OutToIn : RiaEnemy
	{
		private new UFA1OutToInScript Script;
		
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

		private ShotParam shot;
		private MoveParam move;
		
		public UFA1OutToIn(GameObject _go, RiaCharacterScript _script, PlayerNumber _playerNumber) :
			base(_go, _script, _playerNumber)
		{
			this.Script = _script as UFA1OutToInScript;

			// shot
			this.shot.elapsedTime = 0;
			
			// move
			this.move.state = MoveParam.State.Go;
			this.move.startPos_ = this.Trans.position;
			this.move.stayElapsedTime = 0;
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
			var direction = (this.PlayerNumber == PlayerNumber.player1) ? 1 : -1;
			
			var pos = this.Trans.position;
			pos.x += this.Script.MoveSpeedBase * Time.deltaTime * direction;
			this.Trans.position = pos;

			/*
			if (this.PlayerNumber.Equals(PlayerNumber.player1))
			{
				Debug.Log();
			}
			*/
			
			
			// 移動しきったかを判定
			var result = (this.PlayerNumber == PlayerNumber.player1) ? 
				/* player1 */ this.Script.Move.oneWayDistance <= -1 * (this.move.startPos_.x - this.Trans.position.x):
				/* player2 */ this.Script.Move.oneWayDistance <= this.move.startPos_.x - this.Trans.position.x;
			
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
			var direction = (this.PlayerNumber == PlayerNumber.player1) ? -1 : 1;
			
			var pos = this.Trans.position;
			pos.x += this.Script.MoveSpeedBase * Time.deltaTime * direction;
			this.Trans.position = pos;

		}
	}
}