using UnityEngine;
using RiaActorSystem;
using System.Collections.Generic;
using System.Linq;

namespace Game.Player
{
	public sealed class AirosPlayer : RiaPlayer
	{
		private new AirosPlayerScript Script;

		public AirosPlayer(GameObject _go, RiaCharacterScript _script, PlayerNumber _playerNumber) : base(_go, _script, _playerNumber)
		{
			Debug.Log("airos");
			this.Script = _script as AirosPlayerScript;

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
			public float shotTime = 0;

			public RiaCharacter[] targetEnemys = new RiaCharacter[0];
			public float searchTime = 0;
			public int searchEnemyCount = 0;
		}

		// スキル
		public class SkilParam
		{
			public bool isUsing = false;

			public float timeCount = 0;

			public float rivalPlayerMoveSpeed;

			public float speedChangeRate = 0;
			public float alphaValue = 0;
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

					CreateBullet();
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
			
			// ショットの時間間隔
			var shotElapsedTime = this.playElapsedTime - param.shotTime;
			if (shotElapsedTime < script.shotCoolTime) { return; }

			// サーチ開始
			if (RiaInput.Instance.GetPushDown(RiaInput.KeyType.SpecialShot, this.PlayerNumber))
			{
				Debug.Log("サーチ開始");
				param.searchTime = this.playElapsedTime;
				param.searchEnemyCount = 0;
				param.targetEnemys = new RiaCharacter[script.searchNumOfTimes];
			}

			// サーチ中
			if (RiaInput.Instance.GetPush(RiaInput.KeyType.SpecialShot, this.PlayerNumber))
			{
				if (!NearSearchEnemy(script.shotRange)) { return; }
				if (script.searchNumOfTimes <= param.searchEnemyCount) { return; }

				// サーチ時間の更新
				var searchElapsedTime = this.playElapsedTime - param.searchTime;

				// サーチの時間間隔
				if (script.searchTime <= searchElapsedTime)
				{
					param.searchTime = this.playElapsedTime;

					++param.searchEnemyCount;
					param.targetEnemys[param.searchEnemyCount - 1] = NearSearchEnemy(script.shotRange);
					param.searchTime = 0;
				}
			}

			// ショット！
			if (RiaInput.Instance.GetPush(RiaInput.KeyType.SpecialShot, this.PlayerNumber))
			{
				Debug.Log(this.PlayerNumber + " : specialShot");

				for (int i = 0; i < param.searchEnemyCount; ++i)
				{
					// todo: enemyにする
					// todo: ダメージ処理

					param.shotTime = this.playElapsedTime;
				}
			}
		}

		/// <summary>
		/// スキル
		/// </summary>
		/// <param name="_status"></param>
		private void Skill()
		{
			var param = this.skilParam;
			var script = this.Script.skilParam;

			if (RiaInput.Instance.GetKeyDown(RiaInput.KeyType.Skil, this.PlayerNumber) && !param.isUsing)
			{
				Debug.Log(this.PlayerNumber + " : skil");
				param.isUsing = true;
				//_status.rivalPlayerStatus.SetMoveSpeedRate(1.0f);
			}

			if (param.isUsing)
			{
				// 時間の更新
				param.timeCount += Time.deltaTime;

				var beforeAlphaValue = param.alphaValue;

				// このアルファ値の変動で画面が上手い具合にフェードするか正直わかんない
				param.alphaValue = Mathf.Lerp(0, this.Script.AlphaMaxValue, Time.deltaTime);

				// アルファ値の変動に伴う相手プレイヤーのスピードの調整もよくわかんない
				param.speedChangeRate = (beforeAlphaValue <= param.alphaValue) ?
					1 - param.alphaValue :
					1 + (param.alphaValue * 2);

				//_status.rivalPlayerStatus.SetMoveSpeedRate(param.speedChangeRate);

				if (script.coolTime <= param.timeCount)
				{
					param.timeCount = 0;
					param.alphaValue = 0;
					//_status.rivalPlayerStatus.SetMoveSpeedRate(1.0f);
					param.isUsing = false;
				}
			}
		}

		private void CreateBullet()
		{
			var normalBullet = GameObject.Instantiate(Script.nsParam.bulletPrefab);


			var pos = this.Trans.position;
			//pos.z = 100;
			normalBullet.transform.position = pos;
			Debug.Log(normalBullet.transform.position);
		}

		private RiaCharacter NearSearchEnemy(float _searchRadius)
		{
			var param = this.ssParam;
			var script = this.Script.ssParam;

			SortedDictionary<float, RiaCharacter> searchEnemys = new SortedDictionary<float, RiaCharacter>();
			RiaCharacter targetEnemy = null;
			bool isSearchFlag = false;

			//var enemys = GameManager.ParentManager.EnemyActorManager.GetActiveActors();
			var rivalPlayerNumber = (this.PlayerNumber == PlayerNumber.player1) ?
					PlayerNumber.player2 : PlayerNumber.player1;

			var enemys = GameManager.Instance.GetEnemyActorManager(rivalPlayerNumber).GetActiveCharacter();

			foreach (var enemy in enemys)
			{
				// サーチ済みの敵は省く
				if (0 <= System.Array.IndexOf(param.targetEnemys, enemy)) { continue; }
				float tmpDistance = Vector3.Distance(enemy.Trans.position, this.Trans.position);
				if (tmpDistance <= _searchRadius)
				{
					searchEnemys.Add(Vector3.Distance(enemy.Trans.position, this.Trans.position), enemy);
					isSearchFlag = true;
				}
			}

			if (isSearchFlag) { targetEnemy = searchEnemys.First().Value; }
			return targetEnemy;
		}
	}
}