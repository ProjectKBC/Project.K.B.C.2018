/* Author: flanny7
 * Update: 2018/10/28
*/

using UnityEngine;
using RiaActorSystem;

namespace Game.Bullet
{
	public class RiaBulletScript : RiaCharacterScript
	{
		public float ATK { get { return this.atk; } }
		public float HitPointMax { get { return this.hitPointMax; } }
		public float MoveSpeedBase { get { return this.moveSpeedBase; } }
		public float LifeTime { get { return this.lifeTime; } }
		public bool CanCollision { get { return this.canCollision; } }
		public Vector2 ColliderSize { get { return colliderSize; } }

		[SerializeField]
		protected float hitPointMax = 1;
		[SerializeField]
		protected float atk = 1;
		[SerializeField]
		protected float moveSpeedBase = 1;
		[SerializeField]
		private float lifeTime = 60.0f;
		[SerializeField]
		protected bool canCollision = false;
		[SerializeField]
		protected Vector2 colliderSize = Vector2.one;
	}
}