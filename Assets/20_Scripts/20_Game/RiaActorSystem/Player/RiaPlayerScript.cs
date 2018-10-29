/* Author : flanny7
 * Update : 2018/10/29
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
		protected float moveSpeedBase;
		[Space(16)]
		[SerializeField, Header("無敵関係")]
		protected float invincibleTimeMax = 2.0f;
		[SerializeField, Tooltip("無敵点滅の更新フレーム")]
		protected int invincibleBlinkingUpdateFrame = 4;
		[Space(16)]
		[SerializeField]
		protected Sprite sprite;
		[SerializeField]
		protected float circleColliderRadius = 0.8f;
		
		public float MaxHitPoint { get { return this.maxHitPoint; } }
		public float MoveSpeedBase { get { return this.moveSpeedBase; } }
		public float InvincibleTime { get { return this.invincibleTimeMax; } }
		public Sprite Sprite { get { return this.sprite; } }
		public float CircleColliderRadius { get { return this.circleColliderRadius; } }
		public int InvincibleBlinkingUpdateFrame { get { return this.invincibleBlinkingUpdateFrame; } }
	}
}