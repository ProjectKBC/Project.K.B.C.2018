using UnityEngine;
using RiaActorSystem;

namespace Game.Bullet.Player
{
	[CreateAssetMenu(menuName = "RiaActorSystem/PlayerBullet/KaoruSpecialBulletScript", fileName = "KaoruSpecialBulletScript")]
	public class KaoruSpecialBulletScript : RiaPlayerBulletScript
	{
		[SerializeField]
		private Sprite sprite;

		public Sprite Sprite { get { return this.sprite; } }
	}
}