/* Author: flanny7
 * Update: 2018/11/4
*/

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
			this.gm = GameManager.Instance;

			// Todo: BGM
			AudioManager.Instance.StopBgm();
			this.gm.IsFromPause = false;

			// ロード画面の生成
			// FadeManager.Instance.FadeOut(0);

			// CommonDataのスコアを初期化
			this.gm.ResetScore();

			// Stageの生成
			var pl1SM = this.gm.PL1Managers.stageManager;
			if (pl1SM) { pl1SM.Init(); }

			var pl2SM = this.gm.PL2Managers.stageManager;
			if (pl2SM) { pl2SM.Init(); }

			// Bulletの生成
			var pl1PBM = this.gm.PL1Managers.playerBulletManager;
			if (pl1PBM) { pl1PBM.Init(); }

			var pl2PBM = this.gm.PL2Managers.playerBulletManager;
			if (pl2PBM) { pl2PBM.Init(); }

			var pl1EBM = this.gm.PL1Managers.enemyBulletManager;
			if (pl1EBM) { pl1EBM.Init(); }

			var pl2EBM = this.gm.PL2Managers.enemyBulletManager;
			if (pl2EBM) { pl2EBM.Init(); }

			// Playerの生成
			var pl1PM = this.gm.PL1Managers.playerManager;
			if (pl1PM) { pl1PM.Init(); }

			var pl2PM = this.gm.PL2Managers.playerManager;
			if (pl2PM) { pl2PM.Init(); }

			// Enemyの生成
			var pl1EM = this.gm.PL1Managers.enemyManager;
			if (pl1EM) { pl1EM.Init(); }

			var pl2EM = this.gm.PL2Managers.enemyManager;
			if (pl2EM) { pl2EM.Init(); }

			// UIのセッティング
			var ui = this.gm.UIManager;
			if (ui) { ui.Init(); }
		}

		public override void Update()
		{
			// ロード画面の解除
			// FadeManager.Instance.FadeIn(0.25f);

			this.gm.ChageState(GameManager.State.Ready);
		}

		public override void End()
		{
		}
	}
}