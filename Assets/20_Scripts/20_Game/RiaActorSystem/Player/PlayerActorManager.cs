/* Author: flanny7
 * Update: 2018/10/23
*/

using UnityEngine;
using RiaActorSystem;

namespace Game.Player
{
	public sealed class PlayerActorManager : RiaActorManager
	{
		[SerializeField]
		private PlayerNumber playerNumber = PlayerNumber.player1;

		[SerializeField]
		private PlayerActorFactory factory = null;

		[SerializeField]
		private Vector3 spownPosition = Vector3.zero;

		protected override void OnInitialize()
		{
			Debug.Log("create player : " + this.playerNumber);
			this.factory.Create(this.playerNumber, this.GetFreeActor(), this.spownPosition);
		}

		protected override void OnUpdate()
		{

		}
		
		/// <summary>
		/// 攻撃処理 by flanny7
		/// </summary>
		public void Shot()
		{
			if (!this.IsInit) { Debug.LogError("Initializeされていません。", this.gameObject); return; }

			for (int i = 0; i < actors.Length; ++i)
			{
				if (!actors[i].IsActive) { continue; }

				var chara = actors[i].Character as RiaPlayer;
				chara.Shot();
			}
		}

		/// <summary>
		/// 移動処理 by flanny
		/// </summary>
		public void Move()
		{
			if (!this.IsInit) { Debug.LogError("Initializeされていません。", this.gameObject); return; }

			for (int i = 0; i < actors.Length; ++i)
			{
				if (!actors[i].IsActive) { continue; }

				var chara = actors[i].Character as RiaPlayer;
				chara.Move();
			}
		}

		/// <summary>
		/// アニメーション処理 by flanny7
		/// </summary>
		public void Animation()
		{
			if (!this.IsInit) { Debug.LogError("Initializeされていません。", this.gameObject); return; }

			for (int i = 0; i < actors.Length; ++i)
			{
				if (!actors[i].IsActive) { continue; }

				var chara = actors[i].Character as RiaPlayer;
				chara.Animation();
			}
		}

		/// <summary>
		/// 衝突処理 by flanny7
		/// </summary>
		public void Collision()
		{
			if (!this.IsInit) { Debug.LogError("Initializeされていません。", this.gameObject); return; }

			for (int i = 0; i < actors.Length; ++i)
			{
				if (!actors[i].IsActive) { continue; }

				var chara = actors[i].Character as RiaPlayer;
				chara.Collision();
			}
		}

		/// <summary>
		/// 死亡処理 by flanny7
		/// </summary>
		public void DeadCheck()
		{
			if (!this.IsInit) { Debug.LogError("Initializeされていません。", this.gameObject); return; }

			for (int i = 0; i < actors.Length; ++i)
			{
				if (!actors[i].IsActive) { continue; }

				var chara = actors[i].Character as RiaPlayer;
				chara.DeadCheck();
			}
		}
	}
}