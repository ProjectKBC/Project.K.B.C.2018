using UnityEngine;
using RiaActorSystem;
using System.Collections.Generic;
using System.Linq;

namespace Game.Player
{
	public sealed class AnomaPlayer : RiaPlayer
	{
		private new AnomaPlayerScript Script;

		public AnomaPlayer(GameObject _go, RiaCharacterScript _script, PlayerNumber _playerNumber) : base(_go, _script, _playerNumber)
		{
			Debug.Log("anoma");
			this.Script = _script as AnomaPlayerScript;

			nsParam = new NormalShotParam();
			ssParam = new SpecialShotParam();
			skilParam = new SkilParam();
		}

		// 通常ショット
		public class NormalShotParam
		{
			public float shotTime = 0;
		}

		// 特殊ショット
		public class SpecialShotParam
		{
			public float chargeTimeCount = 0;
		}

		// スキル
		public class SkilParam
		{
			
		}

		public NormalShotParam nsParam;
		public SpecialShotParam ssParam;
		public SkilParam skilParam;

		protected override void OnInit()
		{

		}

		protected override void OnWait()
		{

		}

		protected override void OnPlay()
		{
			this.Move();

			this.NormalShot();
			this.SpecialShot();
			this.Skill();
		}

		protected override void OnEnd()
		{

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
				Debug.Log(this.PlayerNumber + " : normalShot");

				// ショットの時間間隔
				var shotElapsedTime = this.playElapsedTime - param.shotTime;
				if (script.shotInterval <= shotElapsedTime)
				{
					param.shotTime = this.playElapsedTime;

					CreateNormalBullet();
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
			if (RiaInput.Instance.GetPushDown(RiaInput.KeyType.SpecialShot, this.PlayerNumber))
			{
				param.chargeTimeCount += Time.deltaTime;
			}

			// キー入力
			if (RiaInput.Instance.GetPushUp(RiaInput.KeyType.SpecialShot, this.PlayerNumber))
			{
				if (script.shotChargeTime <= param.chargeTimeCount)
				{
					Debug.Log(this.PlayerNumber + " : specialShot");

					CreateSpecialBullet();
				}
				param.chargeTimeCount = this.playElapsedTime;
			}
		}

		/// <summary>
		/// スキル
		/// </summary>
		/// <param name="_status"></param>
		private void Skill()
		{
			
		}

		private void CreateNormalBullet()
		{
			var normalBullet = GameObject.Instantiate(Script.nsParam.bulletPrefab);

			var pos = this.Trans.position;
			//pos.z = 100;
			normalBullet.transform.position = pos;
			Debug.Log(normalBullet.transform.position);
		}

		private void CreateSpecialBullet()
		{
			var specialBullet = GameObject.Instantiate(Script.ssParam.bulletPrefab);

			var playerPos = this.Trans.position;
			var shotPos = new Vector3(playerPos.x, playerPos.y += 20, playerPos.z);
			//pos.z = 100;
			specialBullet.transform.position = shotPos;
			Debug.Log(specialBullet.transform.position);
		}
	}
}