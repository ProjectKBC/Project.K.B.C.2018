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
		private EnemyBulletActorFactory factory = null;

		#region Override Function

		/// <summary>
		/// 初期化 by flanny
		/// </summary>
		protected override void OnInitialize()
		{
			for (var i = 0; i < this.actors.Length; ++i)
			{
				this.actors[i].tag = (this.playerNumber == PlayerNumber.player1) ?
					TagEnum.EnemyBullet1.ToDescription() :
					TagEnum.EnemyBullet2.ToDescription();
			}
		}

		/// <summary>
		/// まとめて更新 by flanny7
		/// </summary>
		protected override void OnUpdate()
		{
		}

		#endregion

		#region Public Function

		public void CreateStraightEnemyBullet(Vector3 _pos)
		{
			this.factory.CreateStraightEnemyBullet(this.playerNumber, this.GetFreeActor(), _pos);
		}
		
		public void CreateStayEnemyBullet(Vector3 _pos)
		{
			this.factory.CreateStayEnemyBullet(this.playerNumber, this.GetFreeActor(), _pos);
		}
		
		/*
		public void CreateSinEnemyBullet(Vector3 _pos)
		{
			this.factory.CreateSinEnemyBullet(this.playerNumber, this.GetFreeActor(), _pos);
		}
		*/
		
		
		public void CreateInToOutEnemyBullet(Vector3 _pos)
		{
			this.factory.CreateInToOutEnemyBullet(this.playerNumber, this.GetFreeActor(), _pos);
		}
		
		
		public void CreateOutToInEnemyBullet(Vector3 _pos)
		{
			this.factory.CreateOutToInEnemyBullet(this.playerNumber, this.GetFreeActor(), _pos);
		}

		#endregion
	}
}