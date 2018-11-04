/* Author: flanny7
 * Update: 2018/10/28
*/

using System;
using System.Linq;
using UnityEngine;

namespace Game
{
	using Game.Stage;
	using Game.UI;
	using Game.Player;
	using Game.Enemy;
	using Game.Bullet.Player;
	using Game.Bullet.Enemy;

	public sealed class GameManager : SingletonMonoBehaviour<GameManager>
    {
        public enum State
        {
            Initialize,
			Ready,
            Play,
            Pause,
	        Result,
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
	    private ResultAction resultAct = new ResultAction();
        private FinalizeAction finalAct = new FinalizeAction();

		public float PlayElapsedTime { get; set; }

	    public PlayerNumber Winner { get; private set; }
	    private bool isFinishedBattle = false;

		[System.Serializable]
		public class Managers
		{
			public RiaStageManager stageManager;
			public PlayerActorManager playerManager;
			public PlayerBulletActorManager playerBulletManager;
			public EnemyActorManager enemyManager;
			public EnemyBulletActorManager enemyBulletManager;
		}

		[SerializeField, Header("CommonData")]
		private CommonData commonData = null;
		public CommonData CommonData { get { return this.commonData; } }

		[SerializeField, Header("UIController")]
		private GameUIManager uiManager = null;
		public GameUIManager UIManager { get { return this.uiManager; } }

		[SerializeField, Header("Managers")]
		private Managers pl1Managers = null;
		public Managers PL1Managers { get { return this.pl1Managers; } }

		[SerializeField]
		private Managers pl2Managers = null;
		public Managers PL2Managers { get { return this.pl2Managers; } }

		// 検索系
		/// Player
		public PlayerActorManager GetPlayerActorManager(PlayerNumber _playerNumber)
		{
			var manager =
				(_playerNumber == PlayerNumber.player1) ? this.pl1Managers.playerManager :
				(_playerNumber == PlayerNumber.player2) ? this.pl2Managers.playerManager :
				null;

			if (manager != null && !manager.IsInit)
			{
				Debug.LogError("初期化していません。", manager.gameObject);
				return null;
			}

			return manager;
		}

		public RiaPlayer GetPlayer(PlayerNumber _playerNumber)
		{
			var manager = (_playerNumber == PlayerNumber.player1) ? this.pl1Managers.playerManager :
						  (_playerNumber == PlayerNumber.player2) ? this.pl2Managers.playerManager :
						  null;

			if (manager == null || !manager.IsInit)
			{
				Debug.LogError("存在しないか、初期化していません。", manager.gameObject);
				return null;
			}

			return manager.GetActiveActors()[0].Character as RiaPlayer;
		}

		public float GetPlayerHitPoint(PlayerNumber _playerNumber)
		{
			return this.GetPlayer(_playerNumber).HitPoint;
		}

		public float GetPlayerHitPointMax(PlayerNumber _playerNumber)
		{
			return ((RiaPlayerScript) this.GetPlayer(_playerNumber).Actor.CharacterScript).HitPointMax;
		}

		public int GetPlayerScore(PlayerNumber _playerNumber)
		{
			int score = (_playerNumber == PlayerNumber.player1) ? this.commonData.player1Score :
			            (_playerNumber == PlayerNumber.player2) ? this.commonData.player2Score :
						-1;

			if (score == -1) { Debug.LogError("存在していません"); }

			return score;
		}

		/// Enemy
		public EnemyActorManager GetEnemyActorManager(PlayerNumber _playerNumber)
		{
			var manager =
				(_playerNumber == PlayerNumber.player1) ? this.pl1Managers.enemyManager :
				(_playerNumber == PlayerNumber.player2) ? this.pl2Managers.enemyManager :
				null;

			if (manager != null && !manager.IsInit)
			{
				Debug.LogError("初期化していません。", manager.gameObject);
				return null;
			}

			return manager;
		}

		public RiaEnemy[] GetEnemies(PlayerNumber _playerNumber)
		{
			var manager = (_playerNumber == PlayerNumber.player1) ? this.pl1Managers.enemyManager :
						  (_playerNumber == PlayerNumber.player2) ? this.pl2Managers.enemyManager :
						  null;

			if (manager == null || !manager.IsInit)
			{
				Debug.LogError("存在しないか、初期化していません。", manager.gameObject);
				return null;
			}

			return manager.GetActiveActors().Select(x => (x.Character as RiaEnemy)).ToArray();
		}

		/// PlayerBullet
		public PlayerBulletActorManager GetPlayerBulletActorManager(PlayerNumber _playerNumber)
		{
			var manager =
				(_playerNumber == PlayerNumber.player1) ? this.pl1Managers.playerBulletManager :
				(_playerNumber == PlayerNumber.player2) ? this.pl2Managers.playerBulletManager :
				null;

			if (manager != null && !manager.IsInit)
			{
				Debug.LogError("初期化していません。", manager.gameObject);
				return null;
			}

			return manager;
		}

		public RiaPlayerBullet[] GetPlayerBullets(PlayerNumber _playerNumber)
		{
			var manager = (_playerNumber == PlayerNumber.player1) ? this.pl1Managers.playerBulletManager :
						  (_playerNumber == PlayerNumber.player2) ? this.pl2Managers.playerBulletManager :
						  null;

			if (manager == null || !manager.IsInit)
			{
				Debug.LogError("存在しないか、初期化していません。", manager.gameObject);
				return null;
			}

			return manager.GetActiveActors().Select(x => (x.Character as RiaPlayerBullet)).ToArray();
		}

		/// EnemyBullet
		public EnemyBulletActorManager GetEnemyBulletActorManager(PlayerNumber _playerNumber)
		{
			var manager =
				(_playerNumber == PlayerNumber.player1) ? this.pl1Managers.enemyBulletManager :
				(_playerNumber == PlayerNumber.player2) ? this.pl2Managers.enemyBulletManager :
				null;

			if (manager != null && !manager.IsInit)
			{
				Debug.LogError("初期化していません。", manager.gameObject);
				return null;
			}

			return manager;
		}

		public RiaEnemyBullet[] GetEnemyBullets(PlayerNumber _playerNumber)
		{
			var manager = (_playerNumber == PlayerNumber.player1) ? this.pl1Managers.enemyBulletManager :
						  (_playerNumber == PlayerNumber.player2) ? this.pl2Managers.enemyBulletManager :
						  null;

			if (manager == null || !manager.IsInit)
			{
				Debug.LogError("存在しないか、初期化していません。", manager.gameObject);
				return null;
			}

			return manager.GetActiveActors().Select(x => (x.Character as RiaEnemyBullet)).ToArray();
		}

		// Loading系

		// Play系

		// BGM
		public BackGroundMusicEnum GetStageBGM()
		{
			switch (commonData.stage)
			{
				case StageEnum.stage1:
					return BackGroundMusicEnum.Dance_With_Powder;

				case StageEnum.stage2:
					return BackGroundMusicEnum.Sagittarius_2;

				default:
					return BackGroundMusicEnum.charaselectBGM;
			}
		}


		// Pause系

		// Result系

		/// <summary>
		/// ゲーム内フェーズを切り替える
		/// </summary>
		/// <param name="_state"></param>
		public void ChageState(State _state)
        {
            this.stateManager.SetState(_state);
        }

		// スコア系

		/// <summary>
		/// scoreを加算する by flanny7
		/// </summary>
		/// <param name="_baseScore">基本スコア</param>
		/// <param name="_playerNumber"></param>
		public void AddScore(int _baseScore, PlayerNumber _playerNumber)
		{
			if (_playerNumber == PlayerNumber.player1)
			{
				this.commonData.player1Score += this.ScoreCalc(_baseScore);
			}
			else if (_playerNumber == PlayerNumber.player2)
			{
				this.commonData.player2Score += this.ScoreCalc(_baseScore);
			}
		}

		public void ResetScore()
		{
			this.commonData.player1Score = 0;
			this.commonData.player2Score = 0;
		}

	    // 勝利条件系

	    public void FinishBattle(PlayerNumber _winner)
	    {
		    this.Winner = _winner;
		    this.isFinishedBattle = true;
		    this.ChageState(State.Result);
	    }

		/// <summary>
		/// 対戦状態をリセットする
		/// </summary>
	    public void ResetBattle()
	    {
		    this.isFinishedBattle = false;
	    }
	    
		// メイン

        protected override void OnInit()
        {
		}

        private void Start()
        {
            this.stateManager.Add(State.Initialize, this.initAct);
			this.stateManager.Add(State.Ready, this.readyAct);
			this.stateManager.Add(State.Play, this.playAct);
			this.stateManager.Add(State.Pause, this.pauseAct);
	        this.stateManager.Add(State.Result, this.resultAct);
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
			if (Input.GetKeyDown(KeyCode.Alpha1))
			{
				int tmpI = ((int)this.currentState + 1) % (int)State.Length;
				State tmp = (State)Enum.ToObject(typeof(State), tmpI);
				this.SetState(tmp);
			}
		}

		private void SetState(State _state)
        {
            this.currentState = _state;
            this.stateManager.SetState(_state);
        }

		private int ScoreCalc(int _baseScore)
		{
			// todo: 倍率とかの計算式
			return (int)Mathf.Floor(_baseScore);
		}
    }
}