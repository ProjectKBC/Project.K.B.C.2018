/* Author: flanny7
 * Update: 2018/10/30
*/

using UnityEngine;
using RiaActorSystem;

namespace Game.Bullet.Enemy
{
	public sealed class EnemyBulletActorManager : BulletActorManager
	{
		[SerializeField]
		private PlayerNumber playerNumber = PlayerNumber.player1;

		[SerializeField]
		private EnemyBulletActorFactory factory = null;

		public void CreateStraightEnemyBullet(Vector3 _pos)
		{
			this.factory.CreateStraightEnemyBullet(this.playerNumber, this.GetFreeActor(), _pos);
		}

		protected override void OnInitialize()
		{
			for (var i = 0; i < this.actors.Length; ++i)
			{
				this.actors[i].tag = (this.playerNumber == PlayerNumber.player1) ?
					TagEnum.EnemyBullet1.ToDescription() : TagEnum.EnemyBullet2.ToDescription();
			}
		}

		protected override void OnUpdate()
		{

		}
	}
}