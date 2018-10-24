using UnityEngine;
using RiaActorSystem;

namespace Game.Bullet.Enemy
{
	public class RiaEnemyBullet : RiaBullet
	{
		public RiaEnemyBullet(GameObject _go, RiaCharacterScript _script, PlayerNumber _playerNumber) : base(_go, _script, _playerNumber)
		{
		}
	}
}