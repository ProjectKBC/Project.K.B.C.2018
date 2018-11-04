/* Author: close96
*/

using UnityEngine;
using RiaActorSystem;

namespace Game.Player
{
	using Game.Bullet.Player;

	public sealed class AnomaPlayer : RiaPlayer
	{
		// 通常ショット
		public class NormalShotParam
		{
			public float shotTime = 0;
		}

		// 特殊ショット
		public class SpecialShotParam
		{
			public float shotTime = 0;
			// public float chargeTimeCount = 0;
		}

		// スキル
		public class SkilParam
		{

		}

		// CharacterScriptの上書き
		private new AnomaPlayerScript Script;

		// パラメータ
		public NormalShotParam nsParam;
		public SpecialShotParam ssParam;
		public SkilParam skilParam;

		public AnomaPlayer(GameObject _go, RiaCharacterScript _script, PlayerNumber _playerNumber) : base(_go, _script, _playerNumber)
		{
			// CharacterScriptの上書き
			this.Script = _script as AnomaPlayerScript;

			// パラメータ
			nsParam = new NormalShotParam();
			ssParam = new SpecialShotParam();
			skilParam = new SkilParam();
		}

		protected override void OnInit()
		{

		}

		protected override void OnWait()
		{

		}

		protected override void OnPlay()
		{
			//this.Move();

		}

		protected override void OnEnd()
		{

		}
		
		public override void Shot()
		{
			this.NormalShot();
			this.SpecialShot();
			this.Skill();
		}

		/// <summary>
		/// 通常ショット
		/// </summary>
		private void NormalShot()
		{
			var param = this.nsParam;
			var script = this.Script.nsParam;

			// キー入力
			if (RiaInput.Instance.GetKey(RiaInput.KeyType.NormalShot, this.PlayerNumber))
			{
				// 経過時間の更新
				var shotElapsedTime = this.playElapsedTime - param.shotTime;

				if (script.shotInterval <= shotElapsedTime)
				{
					param.shotTime = this.playElapsedTime;
					this.BulletManger.CreateAnomaBullet(
						PlayerBulletActorManager.BulletType.Normal,
						this.Trans.position);

					// SE: NormalShot
					AudioManager.Instance.PlaySe(SoundEffectEnum.shotVeryShot);
				}
			}
		}

		/// <summary>
		/// 特殊ショット
		/// </summary>
		/// <param name="_status"></param>
		private void SpecialShot()
		{
			var param = this.ssParam;
			var script = this.Script.ssParam;

			// キー入力
			if (RiaInput.Instance.GetKey(RiaInput.KeyType.SpecialShot, this.PlayerNumber))
			{
				// 経過時間の更新
				var shotElapsedTime = this.playElapsedTime - param.shotTime;

				if (script.shotInterval <= shotElapsedTime)
				{
					param.shotTime = this.playElapsedTime;
					this.BulletManger.CreateGeneralBullet(
						PlayerBulletActorManager.BulletType.Special,
						this.Trans.position, this.Trans.rotation);
				}
			}

//			// キー入力
//			if (RiaInput.Instance.GetPush(RiaInput.KeyType.SpecialShot, this.PlayerNumber))
//			{
//				param.chargeTimeCount += Time.deltaTime;
//			}
//
//			// キー入力
//			if (RiaInput.Instance.GetPushUp(RiaInput.KeyType.SpecialShot, this.PlayerNumber))
//			{
//				if (script.shotChargeTime <= param.chargeTimeCount)
//				{
//					Debug.Log(this.PlayerNumber + " : specialShot");
//
//					CreateSpecialBullet();
//				}
//				param.chargeTimeCount = 0;
//			}
		}

		/// <summary>
		/// スキル
		/// </summary>
		/// <param name="_status"></param>
		private void Skill()
		{
			
		}

//		private void CreateSpecialBullet()
//		{
//			var specialBullet = GameObject.Instantiate(Script.ssParam.bulletPrefab);
//
//			var playerPos = this.Trans.position;
//			var shotPos = new Vector3(playerPos.x, playerPos.y += 20, playerPos.z);
//			//pos.z = 100;
//			specialBullet.transform.position = shotPos;
//			Debug.Log(specialBullet.transform.position);
//		}
	}
}