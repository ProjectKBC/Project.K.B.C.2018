/* Author: close96
*/

using UnityEngine;
using RiaActorSystem;

namespace Game.Player
{
	using Game.Bullet.Player;

	public sealed class KaitoPlayer : RiaPlayer
	{
		// 通常ショット
		public class NormalShotParam
		{
			public float shotTime = 0;
		}

		// 特殊ショット
		public class SpecialShotParam
		{
			
		}

		// スキル
		public class SkilParam
		{

		}

		// CharacterScriptの上書き
		private new KaitoPlayerScript Script;

		// パラメータ
		public NormalShotParam nsParam;
		public SpecialShotParam ssParam;
		public SkilParam skilParam;

		public KaitoPlayer(GameObject _go, RiaCharacterScript _script, PlayerNumber _playerNumber) : base(_go, _script, _playerNumber)
		{
			// CharacterScriptの上書き
			this.Script = _script as KaitoPlayerScript;

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
					this.BulletManger.CreateKaitoBullet(
						PlayerBulletActorManager.BulletType.Normal,
						this.Trans.position, this.Trans.rotation);

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
			
		}

		/// <summary>
		/// スキル
		/// </summary>
		/// <param name="_status"></param>
		private void Skill()
		{

		}
	}
}