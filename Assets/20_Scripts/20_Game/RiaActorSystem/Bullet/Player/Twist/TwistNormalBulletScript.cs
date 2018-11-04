using UnityEngine;
using RiaActorSystem;

namespace Game.Bullet.Player
{
	[CreateAssetMenu(menuName = "RiaActorSystem/PlayerBullet/TwistNormalBulletScript", fileName = "TwistNormalBulletScript")]
	public class TwistNormalBulletScript : RiaPlayerBulletScript
	{
		[SerializeField]
		private Sprite sprite;

		public Sprite Sprite { get { return this.sprite; } }
	}
}