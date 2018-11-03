using UnityEngine;
using RiaActorSystem;
using RiaSpriteAnimationSystem;

namespace Game.Bullet.Enemy
{
	[CreateAssetMenu(menuName = "RiaActorSystem/EnemyBullet/InToOutEnemyBulletScript", fileName = "InToOutEnemyBulletScript")]
	public sealed class InToOutEnemyBulletScript : RiaEnemyBulletScript
	{
		[SerializeField]
		private RiaSpriteAnimation animation = null;

		public RiaSpriteAnimation Animation { get { return GameObject.Instantiate<RiaSpriteAnimation>(this.animation); } }
	}
}
