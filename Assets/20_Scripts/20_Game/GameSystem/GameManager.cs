using System;
using UnityEngine;

namespace Game
{
	using Game.Stage;
	using Game.Player;
	using Game.Enemy;

    public sealed class GameManager : SingletonMonoBehaviour<GameManager>
    {
        public enum State
        {
            Initialize,
			Ready,
            Play,
            Pause,
            Finalize,

            Length,
        }

        // ステートマシン系
        private StateManager<State> stateManager = new StateManager<State>();
        private State currentState;
        private InitializeAction initAct = new InitializeAction();
		private ReadyAction readyAct = new ReadyAction();
        private PlayAction playAct = new PlayAction();
        private PauseAction pauseAct = new PauseAction();
        private FinalizeAction finalAct = new FinalizeAction();

		[System.Serializable]
		public class Managers
		{
			public RiaStageManager stageManager;
			public UFAActorManager ufaManager;
			public PlayerActorManager playerManager;
			public EnemyActorManager enemyManager;

			public void PlayActorManagers()
			{
				this.ufaManager.Play();
				this.playerManager.Play();
				this.enemyManager.Play();
			}
		}

		[SerializeField, Header("CommonData")]
		private CommonData commonData = null;
		public CommonData CommonData { get { return this.commonData; } }

		[SerializeField, Header("Managers")]
		private Managers pl1Managers = null;
		public Managers PL1Managers { get { return this.pl1Managers; } }

		[SerializeField]
		private Managers pl2Managers = null;
		public Managers PL2Managers { get { return this.pl2Managers; } }

		// Loading系

		// Play系

		// Pause系

		// Result系

		public void ChageState(State _state)
        {
            this.stateManager.SetState(_state);
        }

        protected override void OnInit()
        {
		}

        private void Start()
        {
            this.stateManager.Add(State.Initialize, this.initAct);
			this.stateManager.Add(State.Ready, this.readyAct);
			this.stateManager.Add(State.Play, this.playAct);
			this.stateManager.Add(State.Pause, this.pauseAct);
            this.stateManager.Add(State.Finalize, this.finalAct);

            this.SetState(State.Initialize);
        }

        private void Update()
		{
			this.stateManager.Update();

			DebugTest();
		}

		private void DebugTest()
		{
			if (Input.GetKeyDown(KeyCode.Return))
			{
				int tmpI = ((int)this.currentState + 1) % (int)State.Length;
				State tmp = (State)Enum.ToObject(typeof(State), tmpI);
				this.SetState(tmp);
			}

			if (Input.GetKeyDown(KeyCode.Space))
			{
				if (this.currentState == State.Play)
				{
					this.SetState(State.Pause);
				}
				else if (this.currentState == State.Pause)
				{
					this.SetState(State.Play);
				}
			}
		}

		private void SetState(State _state)
        {
            this.currentState = _state;
            this.stateManager.SetState(_state);
        }
    }
}