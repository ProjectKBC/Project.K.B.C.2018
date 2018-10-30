/* Author: flanny7
 * Update: 2018/10/31
*/

using UnityEngine;
using RiaActorSystem;

namespace Game.Bullet.Player
{
	using Game.Enemy;
	using Game.Bullet.Enemy;

	public abstract class RiaPlayerBullet : RiaBullet
	{
		// RiaCharacterの上書き
		protected new RiaPlayerBulletScript Script;

		// パラメータ

		public RiaPlayerBullet(GameObject _go, RiaCharacterScript _script, PlayerNumber _playerNumber) : base(_go, _script, _playerNumber)
		{
			// RiaCharacterの上書き
			this.Script = _script as RiaPlayerBulletScript;

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
					// 敵機と衝突
					if (tag == this.enemyTag)
					{
						// 貫通弾でなければ
						if (!this.Script.IsPenetration)
						{
							this.isDead = true;
						}
					}
					// 敵機のショットと衝突
					else if (tag == this.enemyBulletTag)
					{
						//var bullet = go[i].GetComponent<RiaActor>().Character as RiaEnemyBullet;
						//var bulletScript = bullet.Script as RiaEnemyBulletScript;
						//this.Damaged(bulletScript.ATK);
					}
				}
			}
		}
	}
}