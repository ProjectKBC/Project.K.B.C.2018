/* Author: flanny7
 * Update: 2018/10/30
*/

using UnityEngine;
using RiaActorSystem;

namespace Game.Bullet.Enemy
{
	using Game.Player;
	using Game.Bullet.Player;

	public abstract class RiaEnemyBullet : RiaBullet
	{
		// RiaCharacterの上書き
		protected new RiaEnemyBulletScript Script;

		// パラメータ

		public RiaEnemyBullet(GameObject _go, RiaCharacterScript _script, PlayerNumber _playerNumber) : base(_go, _script, _playerNumber)
		{
			// RiaCharacterの上書き
			this.Script = _script as RiaEnemyBulletScript;

		}

		/// <summary>
		/// 衝突処理 by flanny7
		/// </summary>
		protected override void OnCollision()
		{
			if (this.colliderSupporter.IsTriggerEnter2D)
			{
				var go = this.colliderSupporter.TriggerEnter2DGameObjects.GetItems();

				for (var i = 0; i < go.Length; ++i)
				{
					var tag = go[i].tag;
					// 自機と衝突
					if (tag == this.playerTag)
					{
						this.isDead = true;
					}
					// 自機のショットと衝突
					else if (tag == this.playerBulletTag)
					{
						var bullet = go[i].GetComponent<RiaActor>().Character as RiaPlayerBullet;
						var bulletScript = bullet.Script as RiaPlayerBulletScript;
						this.Damaged(bulletScript.ATK);
					}
				}
			}
		}

	}
}