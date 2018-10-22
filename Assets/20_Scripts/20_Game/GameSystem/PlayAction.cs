using UnityEngine;

namespace Game
{
    public class PlayAction : StateAction
    {
        private GameManager gm;

        public PlayAction()
        {
        }

        public override void Start()
        {
            Debug.Log("PlayAction_Start");
            this.gm = GameManager.Instance;
        }

        public override void Update()
        {
			//Debug.Log("PlayAction_Update")

			// Todo: Stageの更新
			if (true /* isBoss */)
			{
				this.gm.PL1Managers.stageManager.MainLoop();
				this.gm.PL2Managers.stageManager.MainLoop();
			}
			else
			{
				this.gm.PL1Managers.stageManager.BossLoop();
				this.gm.PL2Managers.stageManager.BossLoop();
			}

			this.gm.PL1Managers.PlayActorManagers();
			this.gm.PL2Managers.PlayActorManagers();

			// Todo: Bulletの更新
		}

		public override void End()
        {
            //Debug.Log("PlayAction_End");
        }
    }
}