using UnityEngine;

namespace Game
{
	public class ReadyAction : StateAction
	{
		private GameManager gm;
		private float elapsedTime;

		private bool[] trg = new bool[5];

		public ReadyAction()
		{
		}

		public override void Start()
		{
			Debug.Log("ReadyAction_Start");
			this.gm = GameManager.Instance;

			this.gm.UIManager.CountDownStart();

			this.gm.ResetBattle();
			
			for (var i = 0; i < trg.Length; ++i) { this.trg[i] = false; }

			this.elapsedTime = 0;
		}

		public override void Update()
		{
			//Debug.Log("ReadyAction_Update");
			this.elapsedTime += Time.deltaTime;

			// todo: Playerのループ(待機アニメーションかな)

			// Stageのループ(だぶん待機状態)
			this.gm.PL1Managers.stageManager.ReadyLoop();
			this.gm.PL2Managers.stageManager.ReadyLoop();

			// カウントダウン
			if (5 < this.elapsedTime)
			{
				this.gm.ChageState(GameManager.State.Play);
			}
			else if (4 < this.elapsedTime && !this.trg[3])
			{
				// start!!
				AudioManager.Instance.PlaySe(SoundEffectEnum.countDown2);
				this.gm.UIManager.CountDownUpdate("Start!!");
				this.trg[3] = true;
			}
			else if (3 < this.elapsedTime && !this.trg[2])
			{
				// 1
				AudioManager.Instance.PlaySe(SoundEffectEnum.countDown1);
				this.gm.UIManager.CountDownUpdate("1");
				this.trg[2] = true;
			}
			else if (2 < this.elapsedTime && !this.trg[1])
			{
				// 2
				AudioManager.Instance.PlaySe(SoundEffectEnum.countDown1);
				this.gm.UIManager.CountDownUpdate("2");
				this.trg[1] = true;
			}
			else if (1 < this.elapsedTime && !this.trg[0])
			{
				// 3
				AudioManager.Instance.PlaySe(SoundEffectEnum.countDown1);
				this.gm.UIManager.CountDownUpdate("3");
				this.trg[0] = true;
			}
		}
	
		public override void End()
		{
			//Debug.Log("ReadyAction_End");
			this.gm.UIManager.CountDownEnd();
		}
	}
}