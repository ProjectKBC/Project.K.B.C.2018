using UnityEngine;
using RiaActorSystem;
using RiaSpriteAnimationSystem;

namespace Game.Bullet.Player
{
	[CreateAssetMenu(menuName = "RiaActorSystem/PlayerBullet/AirosSpecialBulletScript", fileName = "AirosSpecialBulletScript")]
	public class AirosSpecialBulletScript : RiaPlayerBulletScript
	{
		[SerializeField]
		private Sprite sprite;

		public Sprite Sprite { get { return this.sprite; } }
		
//		[SerializeField]
//		private RiaSpriteAnimation animation;
//
//		public RiaSpriteAnimation Animation { get { return this.animation; } }
	}
}