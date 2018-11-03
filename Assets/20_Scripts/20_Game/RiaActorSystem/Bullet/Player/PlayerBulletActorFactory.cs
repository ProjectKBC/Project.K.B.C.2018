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
			        this.catalog.AirosNormalBullet.airosNormalBulletLeft as RiaPlayerBulletScript;
			var character = new AirosNormalBullet(_actor.gameObject, script, _playerNumber);
			var pos = _position;
			pos.x += (_type == "right") ? 1.5f : -1.5f;
			_actor.WakeUp(character, script, pos, _rotation, _scale);
		}

		public void CreateAirosSpecialBullet(
			PlayerNumber _playerNumber,
			RiaActor _actor,
			Vector3 _position,
			Quaternion? _rotation = null,
			Vector3? _scale = null)
		{
			var script = this.catalog.AirosSpecialBullet.airosSpecialBullet as RiaPlayerBulletScript;
			var character = new AirosSpecialBullet(_actor.gameObject, script, _playerNumber);
			var pos = _position;
			_actor.WakeUp(character, script, pos, _rotation, _scale);
		}

		public void CreateAnomaNormalBullet(
			PlayerNumber _playerNumber,
			RiaActor _actor,
			string _type,
			Vector3 _position,
			Quaternion? _rotation = null,
			Vector3? _scale = null)
		{
			var script = (_type == "right") ?
				this.catalog.AnomaNormalBullet.anomaNormalBulletRight as RiaPlayerBulletScript :
					this.catalog.AnomaNormalBullet.anomaNormalBulletLeft as RiaPlayerBulletScript;
			var character = new AnomaNormalBullet(_actor.gameObject, script, _playerNumber);
			var pos = _position;
			pos.x += (_type == "right") ? 2 : -2;
			_actor.WakeUp(character, script, pos, _rotation, _scale);
		}

		public void CreateKaitoNormalBullet(
			PlayerNumber _playerNumber,
			RiaActor _actor,
			string _type,
			Vector3 _position,
			Quaternion _rotation,
			Vector3? _scale = null)
		{
			var script = (_type == "center") ?
				this.catalog.KaitoNormalBullet.kaitoNormalBulletCenter as RiaPlayerBulletScript :
					(_type == "right") ?
			        this.catalog.KaitoNormalBullet.kaitoNormalBulletRight as RiaPlayerBulletScript :
			        this.catalog.KaitoNormalBullet.kaitoNormalBulletLeft as RiaPlayerBulletScript;
			var character = new KaitoNormalBullet(_actor.gameObject, script, _playerNumber);
			var pos = _position;
			var rot = _rotation;
			var rotAngle = _rotation.eulerAngles;
			rotAngle.z = (_type == "center") ? rotAngle.z : (_type == "right") ? rotAngle.z - 10 : rotAngle.z + 10;
			rot = Quaternion.Euler(rotAngle);
			_actor.WakeUp(character, script, pos, rot, _scale);
		}

		public void CreateKaoruNormalBullet(
			PlayerNumber _playerNumber,
			RiaActor _actor,
			string _type,
			Vector3 _position,
			Quaternion? _rotation = null,
			Vector3? _scale = null)
		{
			var script = (_type == "right") ?
				this.catalog.KaoruNormalBullet.kaoruNormalBulletRight as RiaPlayerBulletScript :
					this.catalog.KaoruNormalBullet.kaoruNormalBulletLeft as RiaPlayerBulletScript;
			var character = new KaoruNormalBullet(_actor.gameObject, script, _playerNumber);
			var pos = _position;
			pos.x += (_type == "right") ? 1.5f : -1.5f;
			_actor.WakeUp(character, script, pos, _rotation, _scale);
		}
	}
}