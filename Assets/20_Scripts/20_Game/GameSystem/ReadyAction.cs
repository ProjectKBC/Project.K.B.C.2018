using UnityEngine;

namespace Game
{
	public class ReadyAction : StateAction
	{
		private GameManager gm;
		private float elapsedTime;

		private bool[] countDownFlg = new bool[5];

		public ReadyAction()
		{
		}

		public override void Start()
		{
			this.gm = GameManager.Instance;

			this.gm.UIManager.CountDownStart();
			
			// カウントダウン用のフラグを初期化
			for (var i = 0; i < countDownFlg.Length; ++i) { this.countDownFlg[i] = false; }

			this.elapsedTime = 0;
		}

		public override void Update()
		{
			this.elapsedTime += Time.deltaTime;

			// Stageのループ(だぶん待機状態)
			this.gm.PL1Managers.stageManager.ReadyLoop();
			this.gm.PL2Managers.stageManager.ReadyLoop();

			// カウントダウン
			if (5 < this.elapsedTime)
			{
				this.gm.ChageState(GameManager.State.Play);
			}
			else if (4 < this.elapsedTime && !this.countDownFlg[3])
			{
				// start!!

				this.gm.UIManager.CountDownUpdate("Start!!");
				this.countDownFlg[3] = true;

				// SE:
				AudioManager.Instance.PlaySe(SoundEffectEnum.countDown2);
			}
			else if (3 < this.elapsedTime && !this.countDownFlg[2])
			{
				// 1

				this.gm.UIManager.CountDownUpdate("1");
				this.countDownFlg[2] = true;

				// SE:
				AudioManager.Instance.PlaySe(SoundEffectEnum.countDown1);
			}
			else if (2 < this.elapsedTime && !this.countDownFlg[1])
			{
				// 2

				this.gm.UIManager.CountDownUpdate("2");
				this.countDownFlg[1] = true;

				// SE:
				AudioManager.Instance.PlaySe(SoundEffectEnum.countDown1);
			}
			else if (1 < this.elapsedTime && !this.countDownFlg[0])
			{
				// 3

				this.gm.UIManager.CountDownUpdate("3");
				this.countDownFlg[0] = true;

				// SE:
				AudioManager.Instance.PlaySe(SoundEffectEnum.countDown1);
			}
		}
	
		public override void End()
		{
			this.gm.UIManager.CountDownEnd();
		}
	}
}