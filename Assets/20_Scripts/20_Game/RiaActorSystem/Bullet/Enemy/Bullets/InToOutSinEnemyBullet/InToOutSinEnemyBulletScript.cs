using UnityEngine;
using RiaActorSystem;
using RiaSpriteAnimationSystem;

namespace Game.Bullet.Enemy
{
	[CreateAssetMenu(menuName = "RiaActorSystem/EnemyBullet/InToOutSinEnemyBulletScript", fileName = "InToOutSinEnemyBulletScript")]
	public sealed class InToOutSinEnemyBulletScript : RiaEnemyBulletScript
	{
		[SerializeField]
		private RiaSpriteAnimation animation = null;

		public RiaSpriteAnimation Animation { get { return GameObject.Instantiate<RiaSpriteAnimation>(this.animation); } }
	}
}