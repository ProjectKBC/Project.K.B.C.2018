using UnityEngine;
using RiaActorSystem;

namespace Game.Bullet.Player
{
	[CreateAssetMenu(menuName = "RiaActorSystem/PlayerBullet/GeneralNormalBulletScript", fileName = "GeneralNormalBulletScript")]
	public class GeneralNormalBulletScript : RiaPlayerBulletScript
	{
		[SerializeField]
		private Sprite sprite;

		public Sprite Sprite { get { return this.sprite; } }
	}
}