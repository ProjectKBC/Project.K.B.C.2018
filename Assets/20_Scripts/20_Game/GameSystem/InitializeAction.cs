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

			// Todo: CommonDataのスコアを初期化
			this.gm.CommonData.player1Score = 0;
			this.gm.CommonData.player2Score = 0;

			// Todo: Stageの生成
			this.gm.PL1Managers.stageManager.Init();
			this.gm.PL2Managers.stageManager.Init();

			// Todo: Playerの生成
			this.gm.PL1Managers.playerManager.Init();
			this.gm.PL2Managers.playerManager.Init();

			// Todo: Enemyの生成
			//this.gm.PL1Managers.enemyManager.Init();
			//this.gm.PL2Managers.enemyManager.Init();

			// Todo: Bulletの生成
		}

		public override void Update()
		{
			//Debug.Log("InitializeAction_Update");

			// Todo: ロード画面の解除

			this.gm.ChageState(GameManager.State.Ready);
		}

		public override void End()
		{
			//Debug.Log("InitializeAction_End");
		}
	}
}