using UnityEngine;
using RiaActorSystem;

namespace Game.Bullet.Player
{
	public class RiaPlayerBulletScript : RiaBulletScript
	{
		[SerializeField]
		private bool isPenetration = false;

		public bool IsPenetration { get { return this.isPenetration; } }
	}
}