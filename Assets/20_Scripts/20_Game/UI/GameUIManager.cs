/* Author: flanny7
 * Update: 2018/11/1
*/

using UnityEngine;

namespace Game.UI
{
	public class GameUIManager : SingletonMonoBehaviour<GameUIManager>
	{
		
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

		#endregion
	}
}