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

			// Todo: Stageの生成

			// Todo: Playerの生成

			// Todo: Enemyの生成
			this.gm.UFAManagerPL1.Init();
			this.gm.UFAManagerPL2.Init();

			// Todo: Bulletの生成

			// Todo: ロード画面の解除
		}

		public override void Update()
		{
			//Debug.Log("InitializeAction_Update");
		}

		public override void End()
		{
			//Debug.Log("InitializeAction_End");
		}
	}
}