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
	}
}