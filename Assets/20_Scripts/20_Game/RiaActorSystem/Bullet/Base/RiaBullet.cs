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
		private static readonly float deleteTime = 0.01f;

		// CharacterScriptの上書き
		public new RiaBulletScript Script { get; protected set; }

		// パラメータ
		/// 体力系
		public float HitPoint { get; private set; }

		/// 移動速度系
		protected float moveSpeedRate = 1;
		protected float MoveSpeed { get { return this.Script.MoveSpeedBase * this.moveSpeedRate; } }

		/// 生死判定
		public bool IsDead
		{
			get
			{
				return (this.HitPoint <= 0) ||
					   (this.Script.LifeTime <= this.playElapsedTime) ||
					    this.isDead;
			}
		}
		protected bool isDead = false;
		private float outOfAreaElapsedTime = 0;
		protected bool IsDelete
		{
			get
			{
				var pos = this.Trans.position;
				if (this.areaRightLine < pos.x - (this.Script.ColliderSize.x / 2) /*右画面外*/ ||
					pos.x + (this.Script.ColliderSize.x / 2) < this.areaLeftLine  /*左画面外*/ ||
					PlayableArea.playAreaTopLine < pos.y - (this.Script.ColliderSize.y / 2) /*上画面外*/ ||
					pos.y + (this.Script.ColliderSize.x / 2) < PlayableArea.playAreaBottomLine  /*下画面外*/)
				{
					this.outOfAreaElapsedTime += Time.deltaTime;
				}
				else
				{
					this.outOfAreaElapsedTime = -1;
				}

				return (deleteTime <= this.outOfAreaElapsedTime);
			}
		}

		// タグ
		protected string playerTag;
		protected string playerBulletTag;
		protected string enemyTag;
		protected string enemyBulletTag;

		// 画面外処理
		protected float areaLeftLine;
		protected float areaRightLine;
		
		// キャッシュ
		protected Collider2DSupporter colliderSupporter;
		protected RiaSpriteAnimator animator;

		#region Constructor

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
				TagEnum.PlayerBullet1.ToDescription() :
				TagEnum.PlayerBullet2.ToDescription();
			this.enemyTag = (this.PlayerNumber == PlayerNumber.player1) ?
				TagEnum.Enemy1.ToDescription() :
				TagEnum.Enemy2.ToDescription();
			this.enemyBulletTag = (this.PlayerNumber == PlayerNumber.player1) ?
				TagEnum.EnemyBullet1.ToDescription() :
				TagEnum.EnemyBullet2.ToDescription();

			// collider
			this.Actor.GetComponent<CapsuleCollider2D>().size = this.Script.ColliderSize;
		}

		#endregion

		#region Public Function

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
			}

			if (IsDelete)
			{
				this.Delete();
			}
		}

		#endregion

		#region Protected Function

		/// <summary>
		/// 死亡処理 by flanny7
		/// </summary>
		protected void Dead()
		{
			// 消滅SE
			this.Actor.Sleep();
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

		#endregion
		
		public abstract void Division();
		public abstract void Move();
		public abstract void Animation();
		protected abstract void OnCollision();
	}
}