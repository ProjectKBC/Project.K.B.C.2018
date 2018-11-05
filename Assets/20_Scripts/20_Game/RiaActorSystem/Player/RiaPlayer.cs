/* Author : flanny7
 * Update : 2018/10/30
*/

using UnityEngine;
using RiaActorSystem;
using RiaSpriteAnimationSystem;

namespace Game.Player
{
	using Game.Enemy;
	using Game.Bullet.Enemy;
	using Game.Bullet.Player;

	public abstract class RiaPlayer : RiaCharacter
	{
		// todo: RiaPlayer全体のScritableObjectがあるとちょっとだけメモリ効率があがる
		private static readonly float SLOW_MOVE_RATE = 0.5f;

		// CharacterScriptの上書き
		protected new RiaPlayerScript Script;

		// パラメータ
		/// 体力系
		public float HitPoint { get; private set; }

		/// 移動速度系
		protected float moveSpeedRate = 1;
		protected float MoveSpeed { get { return this.Script.MoveSpeedBase * this.moveSpeedRate; } }
		public float MoveSpeedDebuffRate { get; set; }

		/// 画面外処理系
		protected float areaLeftLine;
		protected float areaRightLine;

		/// アニメーション系

		/// 無敵系
		protected bool isInvincible = false;
		private float invincibleElapsedTime = 0;
		private float invincibleBlinkingWaitTimeMax;
		private float invincibleBlinkingWaitTime = 0f;
		private SpriteRenderer sprCore;
		public float SpriteAlpha
		{
			get { return this.spRender.color.a; }
			set	{ var c = this.spRender.color; c.a = value; this.spRender.color = c; }
		}
		public float CoreSpriteAlpha
		{
			get { return this.sprCore.color.a; }
			set { var c = this.sprCore.color; c.a = value; this.sprCore.color = c; }
		}

		/// 生死判定
		public bool IsDead { get { return (this.HitPoint <= 0); } }

		// タグ
		protected string enemyBulletTag;
		protected string enemyTag;

		// キャッシュ
		protected SpriteRenderer spRender;
		protected RiaSpriteAnimator animator;
		protected Collider2DSupporter colliderSupporter;
		protected CircleCollider2D circleCollider;
		private PlayerBulletActorManager bulletManger;
		protected PlayerBulletActorManager BulletManger
		{
			get
			{
				if (this.bulletManger == null)
				{
					this.bulletManger = GameManager.Instance.GetPlayerBulletActorManager(this.PlayerNumber);
				}

				return this.bulletManger;
			}

			private set { this.bulletManger = value; }
		}
		private RiaPlayer rivalPlayer;
		protected RiaPlayer RivalPlayer
		{
			get
			{
				if (this.rivalPlayer == null)
				{
					this.rivalPlayer = GameManager.Instance.GetPlayer(this.RivalPlayerNumber);
				}

				return this.rivalPlayer;
			}

			private set { this.rivalPlayer = value; }
		}

		#region Constructor

		public RiaPlayer(GameObject _go, RiaCharacterScript _script, PlayerNumber _playerNumber) : base(_go, _script, _playerNumber)
		{
			// CharacterScriptの上書き
			this.Script = _script as RiaPlayerScript;

			// キャッシュ
			this.spRender = this.Go.GetComponent<SpriteRenderer>();
			this.spRender.sprite = this.Script.Sprite;

			this.colliderSupporter = this.Actor.ColliderSupporter;

			this.circleCollider = this.Go.GetComponent<CircleCollider2D>();
			this.circleCollider.radius = this.Script.CircleColliderRadius;
			this.animator = this.Actor.Animator;
			RiaSpriteAnimation[] anims = {
					GameObject.Instantiate<RiaSpriteAnimation>(this.Script.WaitAnimation),
					GameObject.Instantiate<RiaSpriteAnimation>(this.Script.RightGoAnimation),
					GameObject.Instantiate<RiaSpriteAnimation>(this.Script.LeftGoAnimation),
					};
			this.animator.SetAnimations(anims, this.Script.WaitAnimation.KeyName);

			this.bulletManger = null;

			this.rivalPlayer = null;

			// パラメータ
			/// 体力系
			this.HitPoint = this.Script.HitPointMax;

			/// 移動速度系
			this.MoveSpeedDebuffRate = 1.0f;

			/// タグ
			this.enemyTag = (this.PlayerNumber == PlayerNumber.player1) ?
				TagEnum.Enemy1.ToDescription() :
				TagEnum.Enemy2.ToDescription();
			this.enemyBulletTag = (this.PlayerNumber == PlayerNumber.player1) ?
				TagEnum.EnemyBullet1.ToDescription() :
				TagEnum.EnemyBullet2.ToDescription();

			/// 画面外処理
			this.areaLeftLine = (this.PlayerNumber == PlayerNumber.player1) ?
				PlayableArea.pl1AreaLeftLine :
				PlayableArea.pl2AreaLeftLine;
			this.areaRightLine = (this.PlayerNumber == PlayerNumber.player1) ?
				PlayableArea.pl1AreaRightLine :
				PlayableArea.pl2AreaRightLine;

			/// 無敵
			this.invincibleElapsedTime = 0;
			this.invincibleBlinkingWaitTime = 0;
			this.sprCore = this.Trans.GetChild(0).GetComponent<SpriteRenderer>();
			this.SpriteAlpha = 1;
			this.CoreSpriteAlpha = 1;

			/// フレームレートとフレーム時間の算出 by flanny
			var frameRate = (float)Application.targetFrameRate;
			if (frameRate < 0)
			{
				frameRate = (QualitySettings.vSyncCount == 2) ? 30f : 60f;
			}
			var frameTime = 1.0f / frameRate;

			/// 次のコマへ進めるまでの待ち時間の設定 by flanny
			this.invincibleBlinkingWaitTimeMax =
				frameTime * this.Script.InvincibleBlinkingUpdateFrame;
		}

		#endregion

		#region Public Function

		/// <summary>
		/// 移動処理 by flanny7
		/// </summary>
		public virtual void Move()
		{
			// SlowMoveRate by flanny7
			var slowRate = (RiaInput.Instance.GetPush(RiaInput.KeyType.LowSpeed, this.PlayerNumber)) ? SLOW_MOVE_RATE : 1.0f;
			
			// Up
			if (RiaInput.Instance.GetPush(RiaInput.KeyType.Up, this.PlayerNumber))
			{
				this.Trans.position += Vector3.up * this.MoveSpeed * Time.deltaTime * 60 * slowRate * this.MoveSpeedDebuffRate;
			}
			// Down
			if (RiaInput.Instance.GetPush(RiaInput.KeyType.Down, this.PlayerNumber))
			{
				this.Trans.position += Vector3.down * this.MoveSpeed * Time.deltaTime * 60 * slowRate * this.MoveSpeedDebuffRate;
			}
			// Right
			if (RiaInput.Instance.GetPush(RiaInput.KeyType.Right, this.PlayerNumber))
			{
				this.Trans.position += Vector3.right * this.MoveSpeed * Time.deltaTime * 60 * slowRate * this.MoveSpeedDebuffRate;
			}
			// Left
			if (RiaInput.Instance.GetPush(RiaInput.KeyType.Left, this.PlayerNumber))
			{
				this.Trans.position += Vector3.left * this.MoveSpeed * Time.deltaTime * 60 * slowRate * this.MoveSpeedDebuffRate;
			}

			// 画面外処理 by close96
			// todo: 画面外に移動したときだけ処理させたい
			// newしちゃってるし。 by flanny7
			var pos = this.Trans.position;
			this.Trans.position =
				new Vector3(
					Mathf.Clamp(pos.x, this.areaLeftLine, this.areaRightLine),
					Mathf.Clamp(pos.y, PlayableArea.playAreaBottomLine, PlayableArea.playAreaTopLine),
					pos.z);
		}
		
		/// <summary>
		/// アニメーション処理 by flanny7
		/// </summary>
		public virtual void Animation()
		{
			var rightPushDown = RiaInput.Instance.GetPushDown(RiaInput.KeyType.Right, this.PlayerNumber);
			var leftPushDown = RiaInput.Instance.GetPushDown(RiaInput.KeyType.Left, this.PlayerNumber);
			var rightPushUp = RiaInput.Instance.GetPushUp(RiaInput.KeyType.Right, this.PlayerNumber);
			var leftPushUp = RiaInput.Instance.GetPushUp(RiaInput.KeyType.Left, this.PlayerNumber);
			var rightPush = RiaInput.Instance.GetPush(RiaInput.KeyType.Right, this.PlayerNumber);
			var leftPush = RiaInput.Instance.GetPush(RiaInput.KeyType.Left, this.PlayerNumber);

			// 右に移動していたら
			if (rightPush && !leftPush)
			{
				if (rightPushDown || leftPushUp)
				{
					this.animator.ChangeAnim(this.Script.RightGoAnimation.KeyName);
				}
			}

			// 左に移動していたら
			if (leftPush && !rightPush)
			{
				if (leftPushDown || rightPushUp)
				{
					this.animator.ChangeAnim(this.Script.LeftGoAnimation.KeyName);
				}
			}
			
			// 移動していなければ
			if ((rightPush && leftPush) || (!rightPush && !leftPush))
			{	
				if ((rightPushDown && leftPush) ||
					(leftPushDown && rightPush) ||
					(rightPushUp || leftPushUp))
				{
					this.animator.ChangeAnim(this.Script.WaitAnimation.KeyName);
				}
			}

			this.animator.Run();
		}

		/// <summary>
		/// 衝突処理 by flanny7
		/// </summary>
		public virtual void Collision()
		{
			if (this.colliderSupporter.IsTriggerEnter2D)
			{

				var targets = this.colliderSupporter.TriggerEnter2DGameObjects.GetItems(); ;
				
				for (var i = 0; i < targets.Length && i < 1; ++i)
				{
					var tag = targets[i].tag;

					// 弾幕と衝突
					if (tag == this.enemyBulletTag)
					{
						var bullet = targets[i].GetComponent<RiaActor>().Character as RiaEnemyBullet;
						var bulletScript = bullet.Script as RiaEnemyBulletScript;

						this.Damaged(bulletScript.ATK);
						this.isInvincible = true;
					}
					// 敵機と衝突
					else if (tag == this.enemyTag)
					{
						var enemy = targets[i].GetComponent<RiaActor>().Character as RiaEnemy;
						var enemyScript = enemy.Script as RiaEnemyScript;

						this.Damaged(enemyScript.HitATK);
						this.isInvincible = true;
					}
				}
			}

			// 衝突ストックの削除
			this.Actor.ColliderSupporter.AfterUpdate();

			// 無敵処理の更新
			this.Invincible();
		}

		/// <summary>
		/// 死亡処理 by flanny7
		/// </summary>
		public virtual void DeadCheck()
		{
			if (IsDead)
			{
				this.Dead();
			}
		}

		#endregion

		#region Protected Function

		/// <summary>
		/// ダメージ処理 HitPointに-=する by flanny7
		/// </summary>
		/// <param name="_damagePoint"></param>
		protected void Damaged(float _damagePoint)
		{
			this.HitPoint -= _damagePoint;
		}

		/// <summary>
		/// 回復処理 HitPointに+=する by flanny7
		/// </summary>
		/// <param name="_healPoint">回復量</param>
		protected void Healing(float _healPoint)
		{
			this.HitPoint += _healPoint;
		}

		/// <summary>
		/// 死亡処理 by flanny7
		/// </summary>
		protected virtual void Dead()
		{
			// todo: 撃破FXの生成

			// SE:撃破
			AudioManager.Instance.PlaySe(SoundEffectEnum.explosion);
			
			// todo: 相手の勝利宣言
			// this.Actor.Sleep();
			GameManager.Instance.FinishBattle(this.RivalPlayerNumber);
		}

		/// <summary>
		/// 無敵処理 Update毎に呼び出す 衝突判定後に呼び出すと吉 by flanny7
		/// </summary>
		protected void Invincible()
		{
			if (!isInvincible) { return; }

			// 無敵になった瞬間
			if (this.invincibleElapsedTime == 0)
			{
				// 当たり判定の無効化
				this.circleCollider.enabled = false;
			}

			// 点滅待機時間の更新
			this.invincibleBlinkingWaitTime -= Time.deltaTime;

			// waitTimeが0以下であるときに点滅更新
			if (this.invincibleBlinkingWaitTime <= Vector3.kEpsilon)
			{
				// 点滅の透過
				var alp =
					(this.SpriteAlpha == 1) ? 0.25f :
					(this.SpriteAlpha == 0.25f) ? 1 :
					0.25f;

				this.SpriteAlpha = alp;
				this.CoreSpriteAlpha = alp;

				// 点滅待機時間のリセット
				this.invincibleBlinkingWaitTime += this.invincibleBlinkingWaitTimeMax;
			}

			// 無敵の解除
			if (this.Script.InvincibleTime <= this.invincibleElapsedTime)
			{
				// フラグ削除と値型の初期化
				this.isInvincible = false;
				this.invincibleElapsedTime = 0;
				this.invincibleBlinkingWaitTime = 0;

				// 点滅の修正
				this.SpriteAlpha = 1;
				this.CoreSpriteAlpha = 1;

				// 当たり判定の復帰
				this.circleCollider.enabled = true;

				// 無敵時間の更新を通らない
				return;
			}

			// 無敵時間の更新
			this.invincibleElapsedTime += Time.deltaTime;
		}

		#endregion

		/// <summary>
		/// 攻撃処理 by fkanny7
		/// </summary>
		public abstract void Shot();
	}
}
 