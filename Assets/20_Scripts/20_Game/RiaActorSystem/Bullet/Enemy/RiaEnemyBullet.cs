/* Author: flanny7
 * Update: 2018/10/28
*/

using UnityEngine;
using RiaActorSystem;

namespace Game.Bullet.Enemy
{
	using Game.Player;
	using Game.Bullet.Player;

	public class RiaEnemyBullet : RiaBullet
	{
		public RiaEnemyBullet(GameObject _go, RiaCharacterScript _script, PlayerNumber _playerNumber) : base(_go, _script, _playerNumber)
		{
		}

		protected override void OnInit()
		{
		}

		protected override void OnWait()
		{
		}

		protected override void OnEnd()
		{
		}

		protected override void OnCollision()
		{
			if (this.colliderSupporter.IsTriggerEnter2D)
			{
				var go = this.colliderSupporter.TriggerEnter2DGameObjects.GetItems();

				for (var i = 0; i < go.Length; ++i)
				{
					var tag = go[i].tag;

					var playerTag = (this.PlayerNumber == PlayerNumber.player1) ?
						TagEnum.Player1.ToDescription() :
						TagEnum.Player2.ToDescription();

					var bulletTag = (this.PlayerNumber == PlayerNumber.player1) ?
						TagEnum.PlayerBulet1.ToDescription() :
						TagEnum.PlayerBulet2.ToDescription();

					// 自機と衝突
					if (tag == playerTag)
					{
						this.isDead = true;
					}
					// 自機のショットと衝突
					else if (tag == bulletTag)
					{
						var bullet = go[i].GetComponent<RiaActor>().Character as RiaPlayerBullet;
						var bulletScript = bullet.Script as RiaPlayerBulletScript;
						this.Damaged(bulletScript.ATK);
					}
				}
			}
		}

		protected override void OnDead()
		{

		}

		protected override void OnDivision()
		{

		}

		protected override void OnMove()
		{

		}
	}
}