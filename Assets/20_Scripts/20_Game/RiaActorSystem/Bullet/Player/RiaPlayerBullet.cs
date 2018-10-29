using UnityEngine;
using RiaActorSystem;

namespace Game.Bullet.Player
{
	public abstract class RiaPlayerBullet : RiaBullet
	{
		public RiaPlayerBullet(GameObject _go, RiaCharacterScript _script, PlayerNumber _playerNumber) : base(_go, _script, _playerNumber)
		{
		}
	}
}