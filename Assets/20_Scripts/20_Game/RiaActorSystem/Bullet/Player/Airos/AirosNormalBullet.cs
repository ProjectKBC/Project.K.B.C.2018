using UnityEngine;
using RiaActorSystem;

namespace Game.Bullet.Player
{
	public sealed class AirosNormalBullet : RiaPlayerBullet
	{
		public AirosNormalBullet(GameObject _go, RiaCharacterScript _script, PlayerNumber _playerNumber) : base(_go, _script, _playerNumber)
		{
		}
	}
}