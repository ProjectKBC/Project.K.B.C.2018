using UnityEngine;

namespace Game
{
	using Game.Stage;
	using Game.Player;
	using Game.Enemy;
	using Game.Bullet.Player;
	using Game.Bullet.Enemy;

	public class PlayAction : StateAction
    {
        private GameManager gm;
		private RiaStageManager pl1SM;
		private RiaStageManager pl2SM;
		private PlayerActorManager pl1PM;
		private PlayerActorManager pl2PM;
		private EnemyActorManager pl1EM;
		private EnemyActorManager pl2EM;
		private PlayerBulletActorManager pl1PBM;
		private PlayerBulletActorManager pl2PBM;
		private EnemyBulletActorManager pl1EBM;
		private EnemyBulletActorManager pl2EBM;

		public PlayAction()
        {
        }

        public override void Start()
        {
            Debug.Log("PlayAction_Start");
            this.gm = GameManager.Instance;

			this.pl1SM = this.gm.PL1Managers.stageManager;
			this.pl2SM = this.gm.PL2Managers.stageManager;
			this.pl1PM = this.gm.PL1Managers.playerManager;
			this.pl2PM = this.gm.PL2Managers.playerManager;
			this.pl1EM = this.gm.PL1Managers.enemyManager;
			this.pl2EM = this.gm.PL2Managers.enemyManager;
			this.pl1PBM = this.gm.PL1Managers.playerBulletManager;
			this.pl2PBM = this.gm.PL2Managers.playerBulletManager;
			this.pl1EBM = this.gm.PL1Managers.enemyBulletManager;
			this.pl2EBM = this.gm.PL2Managers.enemyBulletManager;
		}

		public override void Update()
        {
			// Debug.Log("PlayAction_Update");

			// Todo: Stageの更新
			if (true /* isBoss */)
			{
				if (this.pl1SM) { this.pl1SM.MainLoop(); }
				if (this.pl2SM) { this.pl2SM.MainLoop(); }
			}
			//else
			//{
			//	this.pl1SM.BossLoop();
			//	this.pl1SM.BossLoop();
			//}

			// Pauseへの移動
			if (RiaInput.Instance.GetPushDown(RiaInput.KeyType.Pause, PlayerNumber.player1) ||
			    RiaInput.Instance.GetPushDown(RiaInput.KeyType.Pause, PlayerNumber.player2))
			{
				this.gm.ChageState(GameManager.State.Pause);
			}

			// elapsedTimeのための虚無の更新
			if (this.pl1PM) { this.pl1PM.Play(); }
			if (this.pl2PM) { this.pl2PM.Play(); }
			if (this.pl1EM) { this.pl1EM.Play(); }
			if (this.pl2EM) { this.pl2EM.Play(); }
			if (this.pl1PBM) { this.pl1PBM.Play(); }
			if (this.pl2PBM) { this.pl2PBM.Play(); }
			if (this.pl1EBM) { this.pl1EBM.Play(); }
			if (this.pl2EBM) { this.pl2EBM.Play(); }

			// 敵機生成処理
			if (this.pl1EM) { this.pl1EM.Spown(); }
			if (this.pl2EM) { this.pl2EM.Spown(); }

			// 攻撃処理
			if (this.pl1PM) { this.pl1PM.Shot(); }
			if (this.pl2PM) { this.pl2PM.Shot(); }
			if (this.pl1EM) { this.pl1EM.Shot(); }
			if (this.pl2EM) { this.pl2EM.Shot(); }
			if (this.pl1PBM) { this.pl1PBM.Division(); }
			if (this.pl2PBM) { this.pl2PBM.Division(); }
			if (this.pl1EBM) { this.pl1EBM.Division(); }
			if (this.pl2EBM) { this.pl2EBM.Division(); }

			// 移動処理
			if (this.pl1PM) { this.pl1PM.Move(); }
			if (this.pl2PM) { this.pl2PM.Move(); }
			if (this.pl1EM) { this.pl1EM.Move(); }
			if (this.pl2EM) { this.pl2EM.Move(); }
			if (this.pl1PBM) { this.pl1PBM.Move(); }
			if (this.pl2PBM) { this.pl2PBM.Move(); }
			if (this.pl1EBM) { this.pl1EBM.Move(); }
			if (this.pl2EBM) { this.pl2EBM.Move(); }

			// アニメーション処理
			if (this.pl1PM) { this.pl1PM.Animation(); }
			if (this.pl2PM) { this.pl2PM.Animation(); }
			if (this.pl1EM) { this.pl1EM.Animation(); }
			if (this.pl2EM) { this.pl2EM.Animation(); }
			if (this.pl1PBM) { this.pl1PBM.Animation(); }
			if (this.pl2PBM) { this.pl2PBM.Animation(); }
			if (this.pl1EBM) { this.pl1EBM.Animation(); }
			if (this.pl2EBM) { this.pl2EBM.Animation(); }

			// 衝突処理
			if (this.pl1PM) { this.pl1PM.Collision(); }
			if (this.pl2PM) { this.pl2PM.Collision(); }
			if (this.pl1EM) { this.pl1EM.Collision(); }
			if (this.pl2EM) { this.pl2EM.Collision(); }
			if (this.pl1PBM) { this.pl1PBM.Collision(); }
			if (this.pl2PBM) { this.pl2PBM.Collision(); }
			if (this.pl1EBM) { this.pl1EBM.Collision(); }
			if (this.pl2EBM) { this.pl2EBM.Collision(); }

			// UI更新
			this.gm.UIManager.HPGageUpdate();
			this.gm.UIManager.ScoreUpdate();

			// 死亡処理
			if (this.pl1PM) { this.pl1PM.DeadCheck(); }
			if (this.pl2PM) { this.pl2PM.DeadCheck(); }
			if (this.pl1EM) { this.pl1EM.DeadCheck(); }
			if (this.pl2EM) { this.pl2EM.DeadCheck(); }
			if (this.pl1PBM) { this.pl1PBM.DeadCheck(); }
			if (this.pl2PBM) { this.pl2PBM.DeadCheck(); }
			if (this.pl1EBM) { this.pl1EBM.DeadCheck(); }
			if (this.pl2EBM) { this.pl2EBM.DeadCheck(); }
		}

		public override void End()
        {
            //Debug.Log("PlayAction_End");
        }
    }
}