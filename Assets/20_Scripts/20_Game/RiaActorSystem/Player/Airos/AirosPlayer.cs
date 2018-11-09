/* Author: close96
*/

using UnityEngine;
using RiaActorSystem;
using System.Collections.Generic;
using System.Linq;

namespace Game.Player
{
	using Game.Bullet.Player;
	using Game.Enemy;

	public sealed class AirosPlayer : RiaPlayer
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

			public Transform[] targetEnemyTranses = new Transform[0];
			public float searchTime = 0;
			public int searchCount = 0;
		}

		// スキル
		public class SkilParam
		{
			public bool isUsing = false;
			public float elapsedTime = 0;
			public float alphaValue = 0;
		}

		// CharacterScriptの上書き
		private new AirosPlayerScript Script;

		// パラメータ
		public NormalShotParam nsParam;
		public SpecialShotParam ssParam;
		public SkilParam skilParam;

		public AirosPlayer(GameObject _go, RiaCharacterScript _script, PlayerNumber _playerNumber) : base(_go, _script, _playerNumber)
		{
			// CharacterScriptの上書き
			this.Script = _script as AirosPlayerScript;

			// パラメータ
			nsParam = new NormalShotParam();
			ssParam = new SpecialShotParam();
			skilParam = new SkilParam();
		}

		#region Override Function

		/// <summary>
		/// 初期化 by flanny7
		/// </summary>
		protected override void OnInit()
		{

		}

		/// <summary>
		/// 待機時の更新処理 by flanny7
		/// </summary>
		protected override void OnWait()
		{

		}

		/// <summary>
		/// 更新処理 by flanny7
		/// </summary>
		protected override void OnPlay()
		{
			//// 攻撃処理
			//this.NormalShot();
			//this.SpecialShot();
			//this.Skill();

			//// 移動処理
			//this.Move();

			//// 衝突処理
			//this.Collision();

			//// 生死判定
			//if (this.IsDead)
			//{
			//	this.Dead();
			//}
		}

		/// <summary>
		/// 破棄処理 by flanny7
		/// </summary>
		protected override void OnEnd()
		{

		}

		/// <summary>
		/// 攻撃処理 by flanny7
		/// </summary>
		public override void Shot()
		{
			this.NormalShot();
			this.SpecialShot();
			this.Skill();
		}
		
		#endregion

		#region Private Function

		/// <summary>
		/// 通常ショット by close96 (+ flanny7)
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
					this.BulletManger.CreateAirosBullet(
						PlayerBulletActorManager.BulletType.Normal,
						this.Trans.position);

					// SE: NormalShot
					AudioManager.Instance.PlaySe(SoundEffectEnum.shotVeryShot);
				}
			}
		}

		/// <summary>
		/// 特殊ショット by close96 (+ flanny7)
		/// </summary>
		private void SpecialShot()
		{
			// キャッシュ的な
			var param = this.ssParam;
			var script = this.Script.ssParam;
			
			// ショットの時間間隔
			var shotElapsedTime = this.playElapsedTime - param.shotTime;
			if (shotElapsedTime < script.shotCoolTime) { return; }

			// サーチ開始
			if (RiaInput.Instance.GetPushDown(RiaInput.KeyType.SpecialShot, this.PlayerNumber))
			{
				param.searchTime = this.playElapsedTime;
				param.searchCount = 0;
				param.targetEnemyTranses = new Transform[script.searchCountMax];
			}

			// サーチ中
			if (RiaInput.Instance.GetPush(RiaInput.KeyType.SpecialShot, this.PlayerNumber))
			{
				var searchedEnemyTransform = NearSearchEnemyTransform(script.searchAreaRange);

				if (!searchedEnemyTransform) { return; }
				if (script.searchCountMax <= param.searchCount) { return; }

				// サーチ時間の更新
				var searchElapsedTime = this.playElapsedTime - param.searchTime;

				// サーチの時間間隔
				if (script.searchInterval <= searchElapsedTime)
				{
					param.searchTime = this.playElapsedTime;

					++param.searchCount;
					param.targetEnemyTranses[param.searchCount - 1] = searchedEnemyTransform;
					param.searchTime = 0;
				}
			}

			// サーチ終了→ショット
			if (RiaInput.Instance.GetPushUp(RiaInput.KeyType.SpecialShot, this.PlayerNumber))
			{
				for (int i = 0; i < param.targetEnemyTranses.Length; ++i)
				{
					this.BulletManger.CreateAirosBullet(
						PlayerBulletActorManager.BulletType.Special,
						param.targetEnemyTranses[i].position);
				}
				param.shotTime = this.playElapsedTime;
			}
		}

		/// <summary>
		/// スキル by close96 (+ flanny7)
		/// </summary>
		private void Skill()
		{
			var param = this.skilParam;
			var script = this.Script.skilParam;

			// スキル発動 by flanny7
			if (RiaInput.Instance.GetPushDown(RiaInput.KeyType.Skil, this.PlayerNumber) && !param.isUsing)
			{
				//Debug.Log("skil");
				// 使用フラグを立てる by flanny7
				param.isUsing = true;
				// デバフのリセット by flanny7
				this.RivalPlayer.MoveSpeedDebuffRate = 1.0f;
			}

			// スキルの実部処理 by flanny7
			if (param.isUsing)
			{
				// 経過時間の更新 by flanny7
				param.elapsedTime += Time.deltaTime;

				var beforeAlphaValue = param.alphaValue;

				// このアルファ値の変動で画面が上手い具合にフェードするか正直わかんない by close96
				//param.alphaValue = Mathf.Lerp(0, this.Script.AlphaMaxValue, Time.deltaTime);

				// 点滅だから経過時間を変数にsin使えばよくね by flanny7
				param.alphaValue = Mathf.Lerp(0, script.alphaMaxValue, Mathf.Sin(param.elapsedTime / script.durationTime * 5));

				// アルファ値の変動に伴う相手プレイヤーのスピードの調整もよくわかんない by close96
				//param.speedChangeRate = (beforeAlphaValue <= param.alphaValue) ?
				//	1 - param.alphaValue :
				//	1 + (param.alphaValue * 2);
				
				// 対戦相手のプレイヤーに移動速度のデバフ by flanny7
				this.RivalPlayer.MoveSpeedDebuffRate = Mathf.Lerp(script.debuffSpeedMin, 1, Mathf.Sin(param.elapsedTime / script.durationTime * 5));

				//Debug.Log(this.RivalPlayer.MoveSpeedDebuffRate);

				// スキルの時間切れ by flanny7
				if (script.durationTime <= param.elapsedTime)
				{
					// パラメータの初期化 by flanny7
					param.elapsedTime = 0;
					param.alphaValue = 0;

					// デバフの解除 by flanny7
					this.RivalPlayer.MoveSpeedDebuffRate = 1.0f;

					// 使用フラグのリセット by flanny7
					param.isUsing = false;
				}
			}
		}

		/// <summary>
		/// 自分から近い距離にいる敵機を返す by close96
		/// </summary>
		/// <param name="_searchRadius"></param>
		/// <returns></returns>
		private Transform NearSearchEnemyTransform(float _searchRadius)
		{
			var param = this.ssParam;
			var script = this.Script.ssParam;

			// todo: SortedDictionaryを使わずに by flanny7
			SortedDictionary<float, RiaEnemy> searchEnemys = 
				new SortedDictionary<float, RiaEnemy>();

			Transform targetEnemyTrans = null;
			var isSearchFlag = false;

			// スマートに敵機を持ってこれるようにした by flanny7
			var enemys = GameManager.Instance.GetEnemies(this.PlayerNumber);

			for (var i = 0; i < enemys.Length; ++i)
			{
				var enemy = enemys[i];

				// サーチ済みの敵は省く
				if (0 <= System.Array.IndexOf(param.targetEnemyTranses, enemy.Trans)) { continue; }

				var distance = Vector3.Distance(enemy.Trans.position, this.Trans.position);
				if (distance <= _searchRadius)
				{
					searchEnemys.Add(distance, enemy);
					isSearchFlag = true;
				}
			}
			
			// foreachよりforが軽いのぜ by flanny7
			//foreach (var enemy in enemys)
			//{
			//	// サーチ済みの敵は省く
			//	if (0 <= System.Array.IndexOf(param.targetEnemys, enemy)) { continue; }
			//	float tmpDistance = Vector3.Distance(enemy.Trans.position, this.Trans.position);
			//	if (tmpDistance <= _searchRadius)
			//	{
			//      // Vector3.Distanceの計算が1つムダなのぜ by flanny7
			//		searchEnemys.Add(Vector3.Distance(enemy.Trans.position, this.Trans.position), enemy);
			//		isSearchFlag = true;
			//	}
			//}

			if (isSearchFlag) { targetEnemyTrans = searchEnemys.First().Value.Trans; }

			return targetEnemyTrans;
		}

		#endregion
	}
}