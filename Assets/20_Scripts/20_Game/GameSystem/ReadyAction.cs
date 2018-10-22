using UnityEngine;

namespace Game
{
	public class ReadyAction : StateAction
	{
		private GameManager gm;
		private float elapsedTime;

		public ReadyAction()
		{
		}

		public override void Start()
		{
			Debug.Log("ReadyAction_Start");
			this.gm = GameManager.Instance;

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
			else if (4 < this.elapsedTime)
			{
				// start!!
			}
			else if (3 < this.elapsedTime)
			{
				// 1
			}
			else if (2 < this.elapsedTime)
			{
				// 2
			}
			else if (1 < this.elapsedTime)
			{
				// 3
			}
		}

		public override void End()
		{
			//Debug.Log("ReadyAction_End");
		}
	}
}