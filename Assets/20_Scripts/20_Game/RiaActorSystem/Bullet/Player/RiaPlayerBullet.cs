using UnityEngine;
using RiaActorSystem;

namespace Game.Bullet.Player
{
	public class RiaPlayerBullet : RiaBullet
	{
		public RiaPlayerBullet(GameObject _go, RiaCharacterScript _script, PlayerNumber _playerNumber) : base(_go, _script, _playerNumber)
		{
		}

		protected override void OnCollision()
		{
			throw new System.NotImplementedException();
		}

		protected override void OnDead()
		{
			throw new System.NotImplementedException();
		}

		protected override void OnDivision()
		{
			throw new System.NotImplementedException();
		}

		protected override void OnEnd()
		{
			throw new System.NotImplementedException();
		}

		protected override void OnInit()
		{
			throw new System.NotImplementedException();
		}

		protected override void OnMove()
		{
			throw new System.NotImplementedException();
		}

		protected override void OnWait()
		{
			throw new System.NotImplementedException();
		}
	}
}