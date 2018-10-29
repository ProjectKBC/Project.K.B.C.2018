/* Author: flanny7
 * Update: 2018/10/28
*/

using UnityEngine;
using RiaActorSystem;
using RiaSpriteAnimationSystem;

namespace Game.Bullet
{
	public abstract class RiaBullet : RiaCharacter
	{
		private static readonly float deleteTime = 5.0f;

		// CharacterScriptの上書き
		public new RiaBulletScript Script { get; protected set; }

		// パラメータ
		/// 体力系
		public float HitPoint { get; private set; }

		/// 移動速度系
		protected float moveSpeedRate = 1;
		protected float MoveSpeed { get { return this.Script.MoveSpeedBase * this.moveSpeedRate; } }

		/// 生死判定
		public bool IsDead { get { return (this.HitPoint < 0) || this.isDead; } }
		protected bool isDead = false;
		private float outOfAreaElapsedTime = 0;
		protected bool IsDelete
		{
			get
			{
				var pos = this.Trans.position;
				if (this.areaRightLine < pos.x /*右画面外*/ ||
					pos.x < this.areaLeftLine  /*左画面外*/ ||
					PlayableArea.playAreaTopLine < pos.y /*上画面外*/ ||
					pos.y < PlayableArea.playAreaBottomLine  /*下画面外*/)
				{
					this.outOfAreaElapsedTime += Time.deltaTime;
				}
				else
				{
					this.outOfAreaElapsedTime = 0;
				}

				return (deleteTime <= this.outOfAreaElapsedTime);
			}
		}

		// タグ
		protected string playerTag;
		protected string playerBulletTag;

		// 画面外処理
		protected float areaLeftLine;
		protected float areaRightLine;
		
		// キャッシュ
		protected Collider2DSupporter colliderSupporter;
		protected RiaSpriteAnimator animator;

		public RiaBullet(GameObject _go, RiaCharacterScript _script, PlayerNumber _playerNumber) : base(_go, _script, _playerNumber)
		{
			// CharacterScriptの上書き
			this.Script = _script as RiaBulletScript;

			// キャッシュと初期化
			this.colliderSupporter = this.Actor.ColliderSupporter;
			this.animator = this.Actor.Animator;

			// パラメータ
			/// 体力
			this.HitPoint = this.Script.HitPointMax;

			/// 画面外処理
			this.areaLeftLine = (this.PlayerNumber == PlayerNumber.player1) ?
				PlayableArea.pl1AreaLeftLine :
				PlayableArea.pl2AreaLeftLine;
			this.areaRightLine = (this.PlayerNumber == PlayerNumber.player1) ?
				PlayableArea.pl1AreaRightLine :
				PlayableArea.pl2AreaRightLine;

			// タグ
			this.playerTag = (this.PlayerNumber == PlayerNumber.player1) ?
				TagEnum.Player1.ToDescription() :
				TagEnum.Player2.ToDescription();
			this.playerBulletTag = (this.PlayerNumber == PlayerNumber.player1) ?
				TagEnum.PlayerBulet1.ToDescription() :
				TagEnum.PlayerBulet2.ToDescription();
		}
		
		/// <summary>
		/// 生成系処理 by flanny7
		/// </summary>
		public void Division()
		{
			this.OnDivision();
		}

		/// <summary>
		/// 移動処理 by flanny7
		/// </summary>
		public void Move()
		{
			this.OnMove();
		}

		/// <summary>
		/// アニメーション処理 by flanny7
		/// </summary>
		public void Animation()
		{
			this.OnMove();
		}

		/// <summary>
		/// 衝突判定 by flanny7
		/// </summary>
		public void Collision()
		{
			this.OnCollision();
			this.Actor.ColliderSupporter.AfterUpdate();
		}

		/// <summary>
		/// 死亡処理 by flanny7
		/// </summary>
		public void DeadCheck()
		{
			if (IsDead)
			{
				this.Dead();
				this.Actor.Sleep();
			}

			if (IsDelete)
			{
				this.Delete();
			}
		}

		/// <summary>
		/// 撃破ではない死亡処理 by flanny7
		/// </summary>
		protected void Delete()
		{
			this.Actor.Sleep();
		}

		protected void Damaged(float _damagePoint)
		{
			if (!this.Script.CanCollision) { return; }

			this.HitPoint -= _damagePoint;
		}

		protected abstract void OnDivision();
		protected abstract void OnMove();
		protected abstract void OnAnimation();
		protected abstract void OnCollision();
		protected abstract void Dead();
	}
}