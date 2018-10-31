/* Author: flanny7
 * Update: 2018/10/30
*/

using UnityEngine;
using RiaActorSystem;

namespace Game.Bullet.Enemy
{
	[System.Serializable]
	public sealed class EnemyBulletActorFactory : BulletActorFactory
	{
		[SerializeField]
		private EnemyBulletCharacterScriptCatalog catalog = null;

		public void CreateStraightEnemyBullet(
			PlayerNumber _playerNumber,
			RiaActor _actor,
			Vector3 _position,
			Quaternion? _rotation = null,
			Vector3? _scale = null)
		{
			var script = this.catalog.StraightEnemyBulletScript as RiaEnemyBulletScript;
			var character = new StraightEnemyBullet(_actor.gameObject, script, _playerNumber);
			_actor.WakeUp(character, script, _position, _rotation, _scale);
		}
	}
}