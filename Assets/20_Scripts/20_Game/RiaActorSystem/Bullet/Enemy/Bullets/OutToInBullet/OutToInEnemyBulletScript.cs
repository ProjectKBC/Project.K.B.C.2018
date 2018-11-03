using UnityEngine;
using RiaActorSystem;
using RiaSpriteAnimationSystem;

namespace Game.Bullet.Enemy
{
	[CreateAssetMenu(menuName = "RiaActorSystem/EnemyBullet/OutToInEnemyBulletScript", fileName = "OutToInEnemyBulletScript")]
	public sealed class OutToInEnemyBulletScript : RiaEnemyBulletScript
	{
		[SerializeField]
		private RiaSpriteAnimation animation = null;

		public RiaSpriteAnimation Animation { get { return GameObject.Instantiate<RiaSpriteAnimation>(this.animation); } }
	}
}