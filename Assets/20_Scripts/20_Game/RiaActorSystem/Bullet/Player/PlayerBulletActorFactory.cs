using UnityEngine;
using RiaActorSystem;

namespace Game.Bullet.Player
{
	[System.Serializable]
	public class PlayerBulletActorFactory : BulletActorFactory
	{
		[SerializeField]
		private PlayerBulletCharacterScriptCatalog catalog = null;

		public void CreateAirosNormalBullet(
			PlayerNumber _playerNumber,
			RiaActor _actor,
			string _type,
			Vector3 _position,
			Quaternion? _rotation = null,
			Vector3? _scale = null)
		{
			var script = (_type == "right") ?
				this.catalog.AirosNormalBullet.airosNormalBulletRight as RiaPlayerBulletScript:
				this.catalog.AirosNormalBullet.airosNormalBulletRight as RiaPlayerBulletScript;
			var character = new AirosNormalBullet(_actor.gameObject, script, _playerNumber);
			var pos = _position;
			pos.x += (_type == "right") ? 1.5f : -1.5f;
			_actor.WakeUp(character, script, pos, _rotation, _scale);
		}
	}
}