/* Author: flanny7
 * Update: 2018/10/30
*/

using UnityEngine;
using RiaActorSystem;

namespace Game.Bullet.Enemy
{
	[CreateAssetMenu(menuName = "RiaCharacterScript/CharacterCatalog/EnemyBullet", fileName = "EnemyBulletCharacterCatalog")]
	public sealed class EnemyBulletCharacterScriptCatalog : ScriptableObject
	{
		[SerializeField]
		private RiaEnemyBulletScript straightEnemyBulletScript = null;

		public RiaEnemyBulletScript StraightEnemyBulletScript { get { return this.straightEnemyBulletScript; } }
		
		[SerializeField]
		private RiaEnemyBulletScript stayEnemyBulletScript = null;

		public RiaEnemyBulletScript StayEnemyBulletScript { get { return this.stayEnemyBulletScript; } }
		
		
		[SerializeField]
		private RiaEnemyBulletScript outToInSinEnemyBulletScript = null;

		public RiaEnemyBulletScript OutToInSinEnemyBulletScript { get { return this.outToInSinEnemyBulletScript; } }
				
		[SerializeField]
		private RiaEnemyBulletScript inToOutSinEnemyBulletScript = null;

		public RiaEnemyBulletScript InToOutSinEnemyBulletScript { get { return this.inToOutSinEnemyBulletScript; } }

		[SerializeField]
		private RiaEnemyBulletScript inToOutEnemyBulletScript = null;

		public RiaEnemyBulletScript InToOutEnemyBulletScript { get { return this.inToOutEnemyBulletScript; } }
		
		[SerializeField]
		private RiaEnemyBulletScript outToInEnemyBulletScript = null;

		public RiaEnemyBulletScript OutToInEnemyBulletScript { get { return this.outToInEnemyBulletScript; } }
	}
}