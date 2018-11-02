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
		private RiaEnemyBulletScript sinEnemyBulletScript = null;

		public RiaEnemyBulletScript SinEnemyBulletScript { get { return this.sinEnemyBulletScript; } }
		
		[SerializeField]
		private RiaEnemyBulletScript toLeftSideEnemyBulletScript = null;

		public RiaEnemyBulletScript ToLeftSideEnemyBulletScript { get { return this.toLeftSideEnemyBulletScript; } }
	}
}