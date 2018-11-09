/* Author: close96
*/

using UnityEngine;
using RiaActorSystem;

namespace Game.Player
{
	using Game.Bullet.Player;

	public sealed class KaoruPlayer : RiaPlayer
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
			// public GameObject specialBullet;
			// public bool existsGranade = false;
		}

		// スキル
		public class SkilParam
		{

		}

		// CharacterScriptの上書き
		private new KaoruPlayerScript Script;

		// パラメータ
		public NormalShotParam nsParam;
		public SpecialShotParam ssParam;
		public SkilParam skilParam;

		public KaoruPlayer(GameObject _go, RiaCharacterScript _script, PlayerNumber _playerNumber) : base(_go, _script, _playerNumber)
		{
			// CharacterScriptの上書き
			this.Script = _script as KaoruPlayerScript;

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

			//this.NormalShot();
			//this.SpecialShot();
			//this.Skill();
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
			if (RiaInput.Instance.GetPush(RiaInput.KeyType.NormalShot, this.PlayerNumber))
			{
				// 経過時間の更新
				var shotElapsedTime = this.playElapsedTime - param.shotTime;

				if (script.shotInterval <= shotElapsedTime)
				{
					param.shotTime = this.playElapsedTime;
					this.BulletManger.CreateKaoruBullet(
						PlayerBulletActorManager.BulletType.Normal,
						this.Trans.position);

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
			if (RiaInput.Instance.GetPush(RiaInput.KeyType.SpecialShot, this.PlayerNumber))
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
			
//			if (RiaInput.Instance.GetPushDown(RiaInput.KeyType.SpecialShot, this.PlayerNumber) &&
//				param.existsGranade)
//			{
//				Debug.Log(this.PlayerNumber + " : specialShot");
//
//				param.specialBullet.GetComponent<Animator>().enabled = true;
//			}
//
//			if (RiaInput.Instance.GetPushDown(RiaInput.KeyType.SpecialShot, this.PlayerNumber) &&
//			    !param.existsGranade)
//			{
//				CreateSpecialBullet();
//				param.existsGranade = true;
//			}
//
//			if(!param.specialBullet)
//			{
//				param.existsGranade = false;
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
//			ssParam.specialBullet = GameObject.Instantiate(Script.ssParam.bulletPrefab);
//
//			var pos = this.Trans.position;
//			//pos.z = 100;
//			ssParam.specialBullet.transform.position = pos;
//			Debug.Log(ssParam.specialBullet.transform.position);
//		}

	}
}