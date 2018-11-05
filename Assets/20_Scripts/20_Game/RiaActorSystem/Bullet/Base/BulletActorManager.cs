/* Author: flanny7
 * Update: 2018/10/30
*/ 

using UnityEngine;
using RiaActorSystem;

namespace Game.Bullet
{
	public abstract class BulletActorManager : RiaActorManager
	{
		[SerializeField]
		protected PlayerNumber playerNumber = PlayerNumber.player1;

		/// <summary>
		/// 生成系処理 by flanny7
		/// </summary>
		public void Division()
		{
			if (!this.IsInit) { Debug.LogError("Initializeされていません。", this.gameObject); return; }

			for (int i = 0; i < actors.Length; ++i)
			{
				if (!actors[i].IsActive) { continue; }

				var chara = actors[i].Character as RiaBullet;
				chara.Division();
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

				var chara = actors[i].Character as RiaBullet;
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

				var chara = actors[i].Character as RiaBullet;
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

				var chara = actors[i].Character as RiaBullet;
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

				var chara = actors[i].Character as RiaBullet;
				chara.DeadCheck();
			}
		}
	}
}