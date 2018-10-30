/* Author : flanny7
 * Update : 2018/10/30
*/

using UnityEngine;
using RiaActorSystem;
using RiaSpriteAnimationSystem;

namespace Game.Player
{
	public abstract class RiaPlayerScript : RiaCharacterScript
	{
		[System.Serializable]
		public class Animations
		{
			public RiaSpriteAnimation waitAnimation;
			public RiaSpriteAnimation rightGoAnimation;
			public RiaSpriteAnimation leftGoAnimation;
		}

		[SerializeField]
		protected float maxHitPoint;
		[SerializeField]
		protected float moveSpeedBase;
		[Space(8)]
		[SerializeField, Header("無敵関係")]
		protected float invincibleTimeMax = 2.0f;
		[SerializeField, Tooltip("無敵点滅の更新フレーム")]
		protected int invincibleBlinkingUpdateFrame = 4;
		[Space(8)]
		[SerializeField]
		protected Sprite sprite;
		[SerializeField]
		protected float circleColliderRadius = 0.8f;
		[Space(8)]
		[SerializeField]
		protected Animations animations = null;

		// パラメータ
		/// 体力
		public float MaxHitPoint { get { return this.maxHitPoint; } }
		/// 移動速度
		public float MoveSpeedBase { get { return this.moveSpeedBase; } }
		/// 無敵
		public float InvincibleTime { get { return this.invincibleTimeMax; } }
		public int InvincibleBlinkingUpdateFrame { get { return this.invincibleBlinkingUpdateFrame; } }
		// Sprite
		public Sprite Sprite { get { return this.sprite; } }
		// Collider
		public float CircleColliderRadius { get { return this.circleColliderRadius; } }
		
		// Animation
		public RiaSpriteAnimation WaitAnimation { get { return this.animations.waitAnimation; } }

		public RiaSpriteAnimation RightGoAnimation { get { return this.animations.rightGoAnimation; } }

		public RiaSpriteAnimation LeftGoAnimation { get { return this.animations.leftGoAnimation; } }
	}
}