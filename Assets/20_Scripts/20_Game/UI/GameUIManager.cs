/* Author: flanny7
 * Update: 2018/11/1
*/

using UnityEngine;

namespace Game.UI
{
	public class GameUIManager : SingletonMonoBehaviour<GameUIManager>
	{
		[SerializeField, Header("Count Down")]
		private CountDownController countDown = null;

		[SerializeField, Header("Score")]
		private ScoreController scorePlayer1 = null;
		[SerializeField]
		private ScoreController scorePlayer2 = null;

		[SerializeField, Header("Pause Window")]
		private PauseWindow pauseWindow = null;
		
		[SerializeField, Header("HitPointGage")]
		private HPGageController hpgPlayer1 = null;
		[SerializeField]
		private HPGageController hpgPlayer2 = null;

		[SerializeField, Header("Result Window")]
		private ResultWindowController resultWindow = null;

		#region Override Function

		protected override void OnInit()
		{
		}

		#endregion

		#region Public Function

		public void Init()
		{
			this.pauseWindow.Init();

			this.hpgPlayer1.Init(PlayerNumber.player1);
			this.hpgPlayer2.Init(PlayerNumber.player2);

			this.scorePlayer1.Init(PlayerNumber.player1);
			this.scorePlayer2.Init(PlayerNumber.player2);
		}

		// CountDown系

		public void CountDownStart()
		{
			this.countDown.Start();
		}

		public void CountDownUpdate(string _text)
		{
			this.countDown.Update(_text);
		}

		public void CountDownEnd()
		{
			this.countDown.End();
		}

		// Score系

		public void ScoreUpdate()
		{
			this.scorePlayer1.Run();
			this.scorePlayer2.Run();
		}

		// Pause系

		public void PauseStart()
		{
			this.pauseWindow.WakeUp();
		}

		public void PauseUpdate()
		{
			this.pauseWindow.Run();
		}

		public void PauseEnd()
		{
			this.pauseWindow.Sleep();
		}

		// HPGage系
		
		public void HPGageUpdate()
		{
			this.hpgPlayer1.Run();
			this.hpgPlayer2.Run();
		}
		
		// Result系

		public void ResultStart(PlayerNumber _winner)
		{
			this.resultWindow.Start(_winner);
		}

		public void ResultUpdate(PlayerNumber _winner)
		{
			this.resultWindow.Update(_winner);
		}

		public void ResultEnd()
		{
			this.resultWindow.End();
		}
		
		#endregion
	}
}