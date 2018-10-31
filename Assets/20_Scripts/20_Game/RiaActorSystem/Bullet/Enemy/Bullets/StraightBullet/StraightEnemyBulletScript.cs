/* Author: flanny7
 * Update: 2018/10/30
*/

using UnityEngine;
using RiaActorSystem;
using RiaSpriteAnimationSystem;

namespace Game.Bullet.Enemy
{
	[CreateAssetMenu(menuName = "RiaActorSystem/EnemyBullet/StraightEnemyBulletScript", fileName = "StraightEnemyBulletScript")]
	public sealed class StraightEnemyBulletScript : RiaEnemyBulletScript
	{
		[SerializeField]
		private RiaSpriteAnimation animation = null;

		public RiaSpriteAnimation Animation { get { return GameObject.Instantiate<RiaSpriteAnimation>(this.animation); } }
	}
}
