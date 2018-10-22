/* Author close96
 * Transplant flanny7
 * Update 2018/10/21
*/

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Player
{
	[CreateAssetMenu(menuName = "RiaCharacterScript/Player/Airos", fileName = "AirosScript")]
	public sealed class AirosScript : PlayerScript
	{
		private readonly PlayerCharacterEnum playerCharacter = PlayerCharacterEnum.airos;
		private readonly float AlphaMaxValue = 0.75f;

		[System.Serializable]
		public class NormalShotParam
		{
			public GameObject bulletPrefab = null;
			public int shotInterval = 3;
		}

		[System.Serializable]
		public class SpecialShotParam
		{
			public float shotRange = 30.0f;
			public float searchTime = 0.5f;
			public int searchNumOfTimes = 3;
			public float shotCoolTime = 1.5f;
		}

		[System.Serializable]
		public class SkilParam
		{
			public float coolTime = 5.0f;
		}

		// 通常ショット
		[SerializeField, Header("NormalBullet"), Tooltip("通常ショット")]
		private NormalShotParam nsParam = null;

		// 特殊ショット
		[SerializeField, Header("SpecialBullet"), Tooltip("特殊ショット")]
		private SpecialShotParam ssParam = null;

		// スキル
		[SerializeField, Header("SpecialBullet"), Tooltip("スキル")]
		private SkilParam skilParam = null;

		protected override void OnInit(RiaCharacterStatus _status)
		{
			base.OnInit(_status);
			//var status = _status as AirosStatus;
			
		}

		protected override void OnPlay(RiaCharacterStatus _status)
		{
			base.OnPlay(_status);
			var status = _status as AirosStatus;
			
			NormalShot(status);
			SpecialShot(status);
			Skill(status);
		}

		protected override void OnEnd(RiaCharacterStatus _status)
		{
			base.OnEnd(_status);
			//var status = _status as AirosStatus;
		}
		
		/// <summary>
		/// 通常ショット
		/// </summary>
		/// <param name="_status"></param>
		private void NormalShot(AirosStatus _status)
		{
			var param = _status.nsParam;

			// 時間更新
			param.shotTime += Time.deltaTime;

			// キー入力
			if (RiaInput.Instance.GetKey(RiaInput.KeyType.NormalShot, _status.PlayerNumber))
			{
				// ショットの時間間隔
				var shotElapsedTime = _status.playElapsedTime - param.shotTime;
				if (this.nsParam.shotInterval < shotElapsedTime)
				{
					CreateBullet(_status);

					param.shotTime = 0;
					++param.shotCount;
				}
			}
		}

		/// <summary>
		/// 特殊ショット
		/// </summary>
		/// <param name="_status"></param>
		private void SpecialShot(AirosStatus _status)
		{
			var param = _status.ssParam;

			// ショットの時間更新
			param.shotTime += Time.deltaTime;

			// ショットの時間間隔
			var shotElapsedTime = _status.playElapsedTime - param.shotTime;
			if (shotElapsedTime < this.ssParam.shotCoolTime) { return; }

			// サーチ開始
			if (RiaInput.Instance.GetKeyDown(RiaInput.KeyType.SpecialShot, _status.PlayerNumber))
			{
				param.searchTime = 0;
				param.searchEnemyCount = 0;
				param.targetEnemys = new RiaActor[this.ssParam.searchNumOfTimes];
			}

			// サーチ中
			if (RiaInput.Instance.GetKey(RiaInput.KeyType.SpecialShot, _status.PlayerNumber))
			{
				if (!NearSearchEnemy(_status, this.ssParam.shotRange)) { return; }
				if (this.ssParam.searchNumOfTimes <= param.searchEnemyCount) { return; }

				// サーチ時間の更新
				param.searchTime += Time.deltaTime;

				// サーチの時間間隔
				if (this.ssParam.searchTime <= param.searchTime)
				{
					++param.searchEnemyCount;
					param.targetEnemys[param.searchEnemyCount - 1] = NearSearchEnemy(_status, this.ssParam.shotRange);
					param.searchTime = 0;
				}
			}

			// ショット！
			if (RiaInput.Instance.GetKeyUp(RiaInput.KeyType.SpecialShot, _status.PlayerNumber))
			{
				for (int i = 0; i < param.searchEnemyCount; ++i)
				{
					// todo: ここをenemyにする
					var enemy = param.targetEnemys[i].Script as RiaCharacterScript;
					// todo: ダメージ処理

					param.shotTime = 0;
				}
			}
		}

		/// <summary>
		/// スキル
		/// </summary>
		/// <param name="_status"></param>
		private void Skill(AirosStatus _status)
		{
			var param = _status.skilParam;

			if (RiaInput.Instance.GetKeyDown(RiaInput.KeyType.Skil, _status.PlayerNumber) && !param.isUsing)
			{
				param.isUsing = true;
				_status.rivalPlayerStatus.SetMoveSpeedRate(1.0f);
			}

			if (param.isUsing)
			{
				// 時間の更新
				param.timeCount += Time.deltaTime;

				var beforeAlphaValue = param.alphaValue;

				// このアルファ値の変動で画面が上手い具合にフェードするか正直わかんない
				param.alphaValue = Mathf.Lerp(0, AlphaMaxValue, Time.deltaTime);

				// アルファ値の変動に伴う相手プレイヤーのスピードの調整もよくわかんない
				param.speedChangeRate = (beforeAlphaValue <= param.alphaValue) ?
					1 - param.alphaValue :
					1 + (param.alphaValue * 2);

				_status.rivalPlayerStatus.SetMoveSpeedRate(param.speedChangeRate);

				if (this.skilParam.coolTime <= param.timeCount)
				{
					param.timeCount = 0;
					param.alphaValue = 0;
					_status.rivalPlayerStatus.SetMoveSpeedRate(1.0f);
					param.isUsing = false;
				}
			}
		}
		
		private void CreateBullet(AirosStatus _status)
		{
			GameObject normalBullets = Instantiate(this.nsParam.bulletPrefab);
			normalBullets.transform.position = _status.trans_.position;
		}

		private RiaActor NearSearchEnemy(AirosStatus _status, float _searchRadius)
		{
			var param = _status.ssParam;

			SortedDictionary<float, RiaActor> searchEnemys = new SortedDictionary<float, RiaActor>();
			RiaActor targetEnemy = null;
			bool isSearchFlag = false;

			var enemys = _status.ParentManager.EnemyActorManager.GetActiveActors();

			foreach (var enemy in enemys)
			{
				// サーチ済みの敵は省く
				if (0 <= System.Array.IndexOf(param.targetEnemys, enemy)) { continue; }
				float tmpDistance = Vector3.Distance(enemy.transform.position, _status.trans_.position);
				if (tmpDistance <= _searchRadius)
				{
					searchEnemys.Add(Vector3.Distance(enemy.transform.position, _status.trans_.position), enemy);
					isSearchFlag = true;
				}
			}

			if (isSearchFlag) { targetEnemy = searchEnemys.First().Value; }
			return targetEnemy;
		}

	}
}