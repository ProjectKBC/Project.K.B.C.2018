using UnityEngine;
using RiaActorSystem;
using RiaSpriteAnimationSystem;

namespace Game.Bullet.Enemy
{
	[CreateAssetMenu(menuName = "RiaActorSystem/EnemyBullet/OutToInSinEnemyBulletScript", fileName = "OutToInSinEnemyBulletScript")]
	public sealed class OutToInSinEnemyBulletScript : RiaEnemyBulletScript
	{
		[SerializeField]
		private RiaSpriteAnimation animation = null;

		public RiaSpriteAnimation Animation { get { return GameObject.Instantiate<RiaSpriteAnimation>(this.animation); } }
	}
}