/* Author: flanny7
 * Update: 2018/10/28
*/

using UnityEngine;
using RiaActorSystem;

namespace Game.Bullet
{
	public abstract class RiaBullet : RiaCharacter
	{
		public new RiaBulletScript Script { get; protected set; }

		public bool IsDead { get { return (this.HitPoint < 0) || this.isDead; } }
		public float HitPoint { get; private set; }

		protected bool isDead = false;

		// キャッシュ
		protected Collider2DSupporter colliderSupporter;

		public RiaBullet(GameObject _go, RiaCharacterScript _script, PlayerNumber _playerNumber) : base(_go, _script, _playerNumber)
		{
			this.Script = _script as RiaBulletScript;

			this.HitPoint = this.Script.HitPointMax;

			this.colliderSupporter = this.Actor.ColliderSupporter;
		}

		protected override void OnPlay() { }
		
		public void Division()
		{
			this.OnDivision();
		}

		public void Move()
		{
			this.OnMove();
		}

		public void Collision()
		{
			this.OnCollision();
			this.Actor.ColliderSupporter.AfterUpdate();
		}

		public void Dead()
		{
			this.OnDead();
		}

		protected void Damaged(float _damagePoint)
		{
			if (!this.Script.CanCollision) { return; }

			this.HitPoint -= _damagePoint;
		}

		protected abstract void OnDivision();
		protected abstract void OnMove();
		protected abstract void OnCollision();
		protected abstract void OnDead();
	}
}