/* Author: flanny7
 * Update: 2018/10/31
*/

using UnityEngine;
using RiaActorSystem;

namespace Game.Bullet.Player
{
	public sealed class PlayerBulletActorManager : BulletActorManager
	{
		public enum BulletType
		{
			Normal,
			Special,
			Skill,
		}

		[SerializeField]
		private PlayerBulletActorFactory factory = null;

		#region Override Function

		/// <summary>
		/// 初期化 by flanny
		/// </summary>
		protected override void OnInitialize()
		{
			for (var i = 0; i < this.actors.Length; ++i)
			{
				this.actors[i].tag = (this.playerNumber == PlayerNumber.player1) ?
					TagEnum.PlayerBullet1.ToDescription() :
					TagEnum.PlayerBullet2.ToDescription();
			}
		}

		/// <summary>
		/// まとめて更新 by flanny7
		/// </summary>
		protected override void OnUpdate()
		{
		}

		#endregion

		#region Public Function

		public void CreateAirosBullet(BulletType _type, Vector3 _pos)
		{
			switch (_type)
			{
				case BulletType.Normal:
					this.factory.CreateAirosNormalBullet(this.playerNumber, GetFreeActor(), "right", _pos);
					this.factory.CreateAirosNormalBullet(this.playerNumber, GetFreeActor(), "left", _pos);
					break;

				case BulletType.Special:
					// this.factory.CreateAirosSpecialBullet(this.playerNumber, GetFreeActor(), _pos);
					break;

				case BulletType.Skill:

					break;
			}
		}

		public void CreateAnomaBullet(BulletType _type, Vector3 _pos)
		{
			switch (_type)
			{
				case BulletType.Normal:
					this.factory.CreateAnomaNormalBullet(this.playerNumber, GetFreeActor(), "right", _pos);
					this.factory.CreateAnomaNormalBullet(this.playerNumber, GetFreeActor(), "left", _pos);
					break;

				case BulletType.Special:

					break;

				case BulletType.Skill:

					break;
			}
		}
		
		public void CreateHeldBullet(BulletType _type, Vector3 _pos)
		{
			switch (_type)
			{
				case BulletType.Normal:
					this.factory.CreateHeldNormalBullet(this.playerNumber, GetFreeActor(), "right", _pos);
					this.factory.CreateHeldNormalBullet(this.playerNumber, GetFreeActor(), "left", _pos);
					break;

				case BulletType.Special:
					break;

				case BulletType.Skill:

					break;
			}
		}
		
		public void CreateKaitoBullet(BulletType _type, Vector3 _pos, Quaternion _rot)
		{
			switch (_type)
			{
				case BulletType.Normal:

					break;

				case BulletType.Special:
					this.factory.CreateKaitoNormalBullet(this.playerNumber, GetFreeActor(), "center", _pos, _rot);
					this.factory.CreateKaitoNormalBullet(this.playerNumber, GetFreeActor(), "right", _pos, _rot);
					this.factory.CreateKaitoNormalBullet(this.playerNumber, GetFreeActor(), "left", _pos, _rot);
					break;

				case BulletType.Skill:

					break;
			}
		}

//		public void CreateKaitoBullet(BulletType _type, Vector3 _pos, Quaternion _rot)
//		{
//			switch (_type)
//			{
//				case BulletType.Normal:
//					this.factory.CreateKaitoNormalBullet(this.playerNumber, GetFreeActor(), "center", _pos, _rot);
//					this.factory.CreateKaitoNormalBullet(this.playerNumber, GetFreeActor(), "right", _pos, _rot);
//					this.factory.CreateKaitoNormalBullet(this.playerNumber, GetFreeActor(), "left", _pos, _rot);
//					break;
//
//				case BulletType.Special:
//
//					break;
//
//				case BulletType.Skill:
//
//					break;
//			}
//		}

		public void CreateKaoruBullet(BulletType _type, Vector3 _pos)
		{
			switch (_type)
			{
				case BulletType.Normal:
					this.factory.CreateKaoruNormalBullet(this.playerNumber, GetFreeActor(), "right", _pos);
					this.factory.CreateKaoruNormalBullet(this.playerNumber, GetFreeActor(), "left", _pos);
					break;

				case BulletType.Special:

					break;

				case BulletType.Skill:

					break;
			}
		}
		
		public void CreateTwistBullet(BulletType _type, Vector3 _pos)
		{
			switch (_type)
			{
				case BulletType.Normal:
					this.factory.CreateTwistNormalBullet(this.playerNumber, GetFreeActor(), "right", _pos);
					this.factory.CreateTwistNormalBullet(this.playerNumber, GetFreeActor(), "left", _pos);
					break;

				case BulletType.Special:

					break;

				case BulletType.Skill:

					break;
			}
		}
		
		public void CreateVeronicaBullet(BulletType _type, Vector3 _pos)
		{
			switch (_type)
			{
				case BulletType.Normal:
					this.factory.CreateVeronicaNormalBullet(this.playerNumber, GetFreeActor(), "right", _pos);
					this.factory.CreateVeronicaNormalBullet(this.playerNumber, GetFreeActor(), "left", _pos);
					break;

				case BulletType.Special:

					break;

				case BulletType.Skill:

					break;
			}
		}

		public void CreateGeneralBullet(BulletType _type, Vector3 _pos, Quaternion _rot)
		{
			
			switch (_type)
			{
				case BulletType.Normal:
					this.factory.CreateAirosNormalBullet(this.playerNumber, GetFreeActor(), "right", _pos);
					this.factory.CreateAirosNormalBullet(this.playerNumber, GetFreeActor(), "left", _pos);
					break;

				case BulletType.Special:
					this.factory.CreateGeneralSpecialBullet(this.playerNumber, GetFreeActor(), "center", _pos, _rot);
					this.factory.CreateGeneralSpecialBullet(this.playerNumber, GetFreeActor(), "right", _pos, _rot);
					this.factory.CreateGeneralSpecialBullet(this.playerNumber, GetFreeActor(), "left", _pos, _rot);
					break;

				case BulletType.Skill:

					break;
			}
		}
		
		
		#endregion
	}
}