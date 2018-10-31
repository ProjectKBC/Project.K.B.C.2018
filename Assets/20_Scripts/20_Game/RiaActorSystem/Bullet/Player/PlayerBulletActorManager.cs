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
		#endregion
	}
}