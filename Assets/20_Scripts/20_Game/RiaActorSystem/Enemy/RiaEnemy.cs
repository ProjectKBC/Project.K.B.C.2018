/* Author : flanny7
 * Update : 2018/10/30
*/

using UnityEngine;
using RiaActorSystem;

namespace Game.Enemy
{
	using Game.Player;
	using Game.Bullet.Enemy;
	using Game.Bullet.Player;

	public abstract class RiaEnemy : RiaCharacter
	{
		private static readonly float deleteTime = 5.0f;

		// CharacterScriptの上書き
		public new RiaEnemyScript Script { get; protected set; }

		// パラメータ
		/// 体力系
		public float HitPoint { get; private set; }

		/// 移動速度系
		protected float moveSpeedRate = 1;
		protected float MoveSpeed { get { return this.Script.MoveSpeedBase * this.moveSpeedRate; } }
		
		/// 生死判定
		protected bool IsDead { get { return (this.HitPoint <= 0 || isDead); } }
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
		protected SpriteRenderer spRender;
		protected Collider2D collider;
		protected Collider2DSupporter colliderSupporter;
		protected EnemyBulletActorManager bulletManager;

		#region Constructor

		public RiaEnemy(GameObject _go, RiaCharacterScript _script, PlayerNumber _playerNumber) : base(_go, _script, _playerNumber)
		{
			// CharacterScriptの上書き
			this.Script = _script as RiaEnemyScript;

			// キャッシュと初期化
			this.spRender = this.Go.GetComponent<SpriteRenderer>();
			this.spRender.sprite = this.Script.Sprite;

			this.collider = this.Go.GetComponent<Collider2D>();
			this.collider.isTrigger = true;

			this.colliderSupporter = this.Actor.ColliderSupporter;

			this.bulletManager = GameManager.Instance.GetEnemyBulletActorManager(this.PlayerNumber);

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
		}

		#endregion

		#region public Function

		/// <summary>
		/// 衝突処理 by flanny7
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
		/// 衝突処理(詳細) by flanny
		/// </summary>
		protected virtual void OnCollision()
		{
			if (this.colliderSupporter.IsTriggerEnter2D)
			{
				var go = this.colliderSupporter.TriggerEnter2DGameObjects.GetItems();

				for (var i = 0; i < go.Length; ++i)
				{
					var tag = go[i].tag;

					//Debug.Log(tag);

					// 自機のショットと衝突
					if (tag == this.playerBulletTag)
					{
						var bullet = go[i].GetComponent<RiaActor>().Character as RiaPlayerBullet;
						var bulletScript = bullet.Script as RiaPlayerBulletScript;

						this.Damaged(bulletScript.ATK);
					}
					// 自機と衝突
					else if (tag == this.playerTag)
					{
						this.isDead = true;
					}
				}
			}
		}

		/// <summary>
		/// 死亡処理 by flanny7
		/// </summary>
		protected void Dead()
		{
			// todo: 撃破FXの生成
			// todo: 撃破SE
			this.SendScore();
			this.Actor.Sleep();
		}

		/// <summary>
		/// 撃破ではない死亡処理 by flanny7
		/// </summary>
		protected void Delete()
		{
			this.Actor.Sleep();
		}

		/// <summary>
		/// スコア加算 by flanny7
		/// </summary>
		protected void SendScore()
		{
			GameManager.Instance.AddScore(this.Script.Score, this.PlayerNumber);
		}

		#endregion

		public abstract void Shot();
		public abstract void Move();
		public abstract void Animation();
	}
}