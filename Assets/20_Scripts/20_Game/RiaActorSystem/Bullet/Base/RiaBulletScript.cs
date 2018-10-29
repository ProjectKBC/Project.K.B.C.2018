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
		public bool CanCollision { get { return this.canCollision; } }

		[SerializeField]
		protected float hitPointMax = 1;
		[SerializeField]
		protected float atk = 1;
		[SerializeField]
		protected float moveSpeedBase = 1;
		[SerializeField]
		protected bool canCollision = false;
	}
}