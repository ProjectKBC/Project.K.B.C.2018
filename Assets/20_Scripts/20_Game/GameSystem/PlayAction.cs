using UnityEngine;

namespace Game
{
	using Game.Stage;
	using Game.Player;
	using Game.Enemy;

    public class PlayAction : StateAction
    {
        private GameManager gm;
		private RiaStageManager pl1SM;
		private RiaStageManager pl2SM;

		public PlayAction()
        {
        }

        public override void Start()
        {
            Debug.Log("PlayAction_Start");
            this.gm = GameManager.Instance;

			this.pl1SM = this.gm.PL1Managers.stageManager;
			this.pl2SM = this.gm.PL2Managers.stageManager;
		}

		public override void Update()
        {
			// Debug.Log("PlayAction_Update");

			// Todo: Stageの更新
			if (true /* isBoss */)
			{
				this.pl1SM.MainLoop();
				this.pl1SM.MainLoop();
			}
			//else
			//{
			//	this.pl1SM.BossLoop();
			//	this.pl1SM.BossLoop();
			//}

			// Pauseへの移動
			if (RiaInput.Instance.GetKeyDown(RiaInput.KeyType.Pause, PlayerNumber.player1) ||
			    RiaInput.Instance.GetKeyDown(RiaInput.KeyType.Pause, PlayerNumber.player2))
			{
				this.gm.ChageState(GameManager.State.Pause);
			}

			this.gm.PL1Managers.playerManager.Play();
			this.gm.PL2Managers.playerManager.Play();

			this.gm.PL1Managers.enemyManager.Play();
			this.gm.PL2Managers.enemyManager.Play();
		}

		public override void End()
        {
            //Debug.Log("PlayAction_End");
        }
    }
}