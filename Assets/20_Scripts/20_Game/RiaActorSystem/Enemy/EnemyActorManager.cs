/* Author: flanny7
 * Update: 2018/10/27
*/

using UnityEngine;
using RiaActorSystem;

namespace Game.Enemy
{
	public sealed class EnemyActorManager : RiaActorManager
	{
		[SerializeField]
		private PlayerNumber playerNumber = PlayerNumber.player1;

		[SerializeField]
		private EnemyActorFactory factory = null;

		[SerializeField]
		private float spownInterval = 1.0f;

		private float elapsedTime;
		private float spownTime;
		private Vector3 spownPosition;

		protected override void OnInitialize()
		{
			Debug.Log("create enemy : " + this.playerNumber);

			this.elapsedTime = 0;
			this.spownTime = 0;

			this.spownPosition = (this.playerNumber == PlayerNumber.player1) ?
				new Vector3(-44, 80, 0) :
				new Vector3(44, 80, 0);
		}

		protected override void OnUpdate()
		{
			this.Spown();
		}

		/// <summary>
		/// 敵機生成処理 by flanny7
		/// </summary>
		public void Spown()
		{
			var deltaTime = Time.deltaTime;
			this.elapsedTime += deltaTime;
			this.spownTime += deltaTime;

			if (this.spownInterval <= this.spownTime)
			{
				this.factory.Create(EnemyCharacterEnum.UAF1StraightEnemy, this.playerNumber, this.GetFreeActor(), this.spownPosition);
				this.spownTime -= this.spownInterval;
			}
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

				var chara = actors[i].Character as RiaEnemy;
				chara.Shot();
			}
		}

		/// <summary>
		/// 移動処理 by flanny7
		/// </summary>
		public void Move()
		{
			if (!this.IsInit) { Debug.LogError("Initializeされていません。", this.gameObject); return; }

			for (int i = 0; i < actors.Length; ++i)
			{
				if (!actors[i].IsActive) { continue; }

				var chara = actors[i].Character as RiaEnemy;
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

				var chara = actors[i].Character as RiaEnemy;
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

				var chara = actors[i].Character as RiaEnemy;
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

				var chara = actors[i].Character as RiaEnemy;
				chara.DeadCheck();
			}
		}
	}
}