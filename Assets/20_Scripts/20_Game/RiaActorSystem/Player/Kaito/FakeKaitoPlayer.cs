using UnityEngine;
using RiaActorSystem;

namespace Game.Player
{
	using Game.Bullet.Player;
	using Game.Enemy;

	public sealed class FakeKaitoPlayer : RiaPlayer
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
		}

		// スキル
		public class SkilParam
		{
		}

		// CharacterScriptの上書き
		private new FakeKaitoPlayerScript Script;

		// パラメータ
		public NormalShotParam nsParam;
		public SpecialShotParam ssParam;
		public SkilParam skilParam;

		public FakeKaitoPlayer(GameObject _go, RiaCharacterScript _script, PlayerNumber _playerNumber) : base(_go, _script, _playerNumber)
		{
			// CharacterScriptの上書き
			this.Script = _script as FakeKaitoPlayerScript;

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
					this.BulletManger.CreateGeneralBullet(
						PlayerBulletActorManager.BulletType.Normal,
						this.Trans.position, this.Trans.rotation);

				}
			}
		}

		/// <summary>
		/// 特殊ショット by close96 (+ flanny7)
		/// </summary>
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
		}

		/// <summary>
		/// スキル by close96 (+ flanny7)
		/// </summary>
		private void Skill()
		{
		}
		#endregion
	}
}