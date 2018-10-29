using UnityEngine;

namespace Game
{
	public class InitializeAction : StateAction
	{
		private GameManager gm;

		public InitializeAction()
		{
		}

		public override void Start()
		{
			Debug.Log("InitializeAction_Start");

			this.gm = GameManager.Instance;

			// Todo: ロード画面の生成
			FadeManager.Instance.FadeOut(0);

			// Todo: CommonDataのスコアを初期化
			this.gm.ResetScore();

			// Todo: Stageの生成
			var pl1SM = this.gm.PL1Managers.stageManager;
			if (pl1SM) { pl1SM.Init(); }

			var pl2SM = this.gm.PL1Managers.stageManager;
			if (pl2SM) { pl2SM.Init(); }

			// Todo: Playerの生成
			var pl1PM = this.gm.PL1Managers.playerManager;
			if (pl1PM) { pl1PM.Init(); }

			var pl2PM = this.gm.PL2Managers.playerManager;
			if (pl2PM) { pl2PM.Init(); }

			// Todo: Enemyの生成
			var pl1EM = this.gm.PL1Managers.enemyManager;
			if (pl1EM) { pl1EM.Init(); }

			var pl2EM = this.gm.PL2Managers.enemyManager;
			if (pl2EM) { pl2EM.Init(); }

			// Todo: Bulletの生成
		}

		public override void Update()
		{
			//Debug.Log("InitializeAction_Update");

			// Todo: ロード画面の解除
			FadeManager.Instance.FadeIn(0.25f);

			this.gm.ChageState(GameManager.State.Ready);
		}

		public override void End()
		{
			//Debug.Log("InitializeAction_End");
		}
	}
}