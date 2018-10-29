/* Author : flanny7
 * Update : 2018/10/29
*/

using UnityEngine;
using RiaActorSystem;

namespace Game.Enemy
{
	public class RiaEnemyScript : RiaCharacterScript
	{
		[SerializeField]
		protected float maxHitPoint;
		[SerializeField]
		protected float moveSpeedBase;
		[SerializeField]
		protected float hitATK;
		[SerializeField]
		protected int score;
		[SerializeField]
		protected Sprite sprite;

		public float MaxHitPoint { get { return this.maxHitPoint; } }
		public float MoveSpeedBase { get { return this.moveSpeedBase; } }
		public float HitATK { get { return this.hitATK; } }
		public int Score { get { return this.score; } }
		public Sprite Sprite { get { return this.sprite; } }
	}
}