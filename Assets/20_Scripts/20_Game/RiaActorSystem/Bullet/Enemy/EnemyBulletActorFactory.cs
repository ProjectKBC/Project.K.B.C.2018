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
		
		public void CreateStayEnemyBullet(
			PlayerNumber _playerNumber,
			RiaActor _actor,
			Vector3 _position,
			Quaternion? _rotation = null,
			Vector3? _scale = null)
		{
			var script = this.catalog.StayEnemyBulletScript as RiaEnemyBulletScript;
			var character = new StraightEnemyBullet(_actor.gameObject, script, _playerNumber);
			_actor.WakeUp(character, script, _position, _rotation, _scale);
		}
		
		
		public void CreateOutToInSinEnemyBullet(
			PlayerNumber _playerNumber,
			RiaActor _actor,
			Vector3 _position,
			Quaternion? _rotation = null,
			Vector3? _scale = null)
		{
			var script = this.catalog.OutToInSinEnemyBulletScript as RiaEnemyBulletScript;
			var character = new OutToInSinEnemyBullet(_actor.gameObject, script, _playerNumber);
			_actor.WakeUp(character, script, _position, _rotation, _scale);
		}
		
		/*
		public void CreateToLeftSideEnemyBullet(
			PlayerNumber _playerNumber,
			RiaActor _actor,
			Vector3 _position,
			Quaternion? _rotation = null,
			Vector3? _scale = null)
		{
			var script = this.catalog.ToLeftSideEnemyBulletScript as RiaEnemyBulletScript;
			var character = new StraightEnemyBullet(_actor.gameObject, script, _playerNumber);
			_actor.WakeUp(character, script, _position, _rotation, _scale);
		}
		*/
		
		public void CreateInToOutEnemyBullet(
			PlayerNumber _playerNumber,
			RiaActor _actor,
			Vector3 _position,
			Quaternion? _rotation = null,
			Vector3? _scale = null)
		{
			var script = this.catalog.InToOutEnemyBulletScript as RiaEnemyBulletScript;
			var character = new InToOutEnemyBullet(_actor.gameObject, script, _playerNumber);
			_actor.WakeUp(character, script, _position, _rotation, _scale);
		}
		
		
		public void CreateOutToInEnemyBullet(
			PlayerNumber _playerNumber,
			RiaActor _actor,
			Vector3 _position,
			Quaternion? _rotation = null,
			Vector3? _scale = null)
		{
			var script = this.catalog.OutToInEnemyBulletScript as RiaEnemyBulletScript;
			var character = new OutToInEnemyBullet(_actor.gameObject, script, _playerNumber);
			_actor.WakeUp(character, script, _position, _rotation, _scale);
		}
		
		
		/*
		public void CreateToRightSideEnemyBullet(
			PlayerNumber _playerNumber,
			RiaActor _actor,
			Vector3 _position,
			Quaternion? _rotation = null,
			Vector3? _scale = null)
		{
			var script = this.catalog.ToRightSideEnemyBulletScript as RiaEnemyBulletScript;
			var character = new StraightEnemyBullet(_actor.gameObject, script, _playerNumber);
			_actor.WakeUp(character, script, _position, _rotation, _scale);
		}
		*/
	}
}