/* Author : flanny7
 * Update : 2018/10/22
*/

using UnityEngine;
using RiaActorSystem;

namespace Game.Player
{
	public abstract class RiaPlayerScript : RiaCharacterScript
	{
		[SerializeField]
		protected float maxHitPoint;
		[SerializeField]
		protected float moveSpeed;
		[SerializeField]
		protected Sprite sprite;

		public float MaxHitPoint { get { return this.maxHitPoint; } }
		public float MoveSpeed { get { return this.moveSpeed; } }
		public Sprite Sprite { get { return this.sprite; } }
	}
}