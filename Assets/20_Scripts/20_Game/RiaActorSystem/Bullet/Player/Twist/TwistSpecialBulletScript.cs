using UnityEngine;
using RiaActorSystem;

namespace Game.Bullet.Player
{
	[CreateAssetMenu(menuName = "RiaActorSystem/PlayerBullet/TwistSpecialBulletScript", fileName = "TwistSpecialBulletScript")]
	public class TwistSpecialBulletScript : RiaPlayerBulletScript
	{
		[SerializeField]
		private Sprite sprite;

		public Sprite Sprite { get { return this.sprite; } }
	}
}