/* Author: flanny7
 * Update: 2018/10/30
*/

using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
	public class GameUIController : SingletonMonoBehaviour<GameUIController>
	{
		#region PauseWindow

		[System.Serializable]
		public class PauseWindow
		{
			[System.Serializable]
			public class Screen
			{
				public Image image;
				public bool IsDisp  { get { return this.image.enabled; } set {this.image.enabled = value; } }
			}

			[System.Serializable]
			public class ButtonSet
			{
				public enum State
				{
					Normal,
					Active,
				}

				public Image image;
				public Sprite spNormal;
				public Sprite spActive;
			}

			public enum State
			{
				Continue = 0,
				Restart = 1,
				PrevToSelect = 2,
				Lenght = 3,
			}

			public float pushIntervalTime = 1/30;
			public GameObject parentMainWindowObject = null;
			public ButtonSet continueButton = null;
			[Space(8)]
			public ButtonSet restartButton = null;
			public Screen restartScreen = null;
			[Space(8)]
			public ButtonSet prevToSelectButton = null;
			public Screen prevToSelectScreen = null;

			private float elapsedTime;
			private State currentState = State.Continue;
			private State prevState = State.Continue;

			public void Init()
			{
				this.elapsedTime = 0;

				this.currentState = PauseWindow.State.Continue;
				this.prevState = PauseWindow.State.Continue;

				this.parentMainWindowObject.SetActive(false);
			}

			public void WakeUp()
			{
				this.elapsedTime = 0;

				this.currentState = PauseWindow.State.Continue;
				this.prevState = PauseWindow.State.Lenght;

				this.restartScreen.IsDisp = false;
				this.prevToSelectScreen.IsDisp = false;

				this.UpdateDisp();

				this.parentMainWindowObject.SetActive(true);
			}

			public void Sleep()
			{
				this.restartScreen.IsDisp = false;
				this.prevToSelectScreen.IsDisp = false;
				this.parentMainWindowObject.SetActive(false);
			}

			public void Run()
			{	
				this.elapsedTime += Time.deltaTime;
				
				this.UpdateScreen();
				this.UpdateDisp();
			}

			private void UpdateScreen()
			{
				// 決定時
				if (this.pushIntervalTime <= this.elapsedTime &&
					(RiaInput.Instance.GetKeyDown(RiaInput.KeyType.Return, PlayerNumber.player1) ||
					 RiaInput.Instance.GetKeyDown(RiaInput.KeyType.Return, PlayerNumber.player2)))
				{
					// todo: 決定音

					// MainWindow
					if (!this.restartScreen.IsDisp && !this.prevToSelectScreen.IsDisp)
					{
						if (this.currentState == State.Continue)
						{
							this.Sleep();
							GameManager.Instance.ChageState(GameManager.State.Play);
						}
						else if (this.currentState == State.Restart)
						{
							this.elapsedTime = 0;
							this.restartScreen.IsDisp = true;
						}
						else if (this.currentState == State.PrevToSelect)
						{
							this.elapsedTime = 0;
							this.prevToSelectScreen.IsDisp = true;
						}
					}
					// RestartWindow
					else if (this.restartScreen.IsDisp)
					{
						this.Sleep();
						GameManager.Instance.ChageState(GameManager.State.Initialize);
					}
					// PrevToSelect
					else if (this.prevToSelectScreen.IsDisp)
					{
						this.Sleep();
						//GameManager.Instance.ChageState(GameManager.State.Finalize);
						FadeManager.Instance.LoadScene(2.0f, SceneEnum.Select.ToDescription());
					}
				}
				// キャンセル
				else if (this.pushIntervalTime <= this.elapsedTime &&
						 (RiaInput.Instance.GetKeyDown(RiaInput.KeyType.Cancel, PlayerNumber.player1) ||
						  RiaInput.Instance.GetKeyDown(RiaInput.KeyType.Cancel, PlayerNumber.player2)))
				{
					// キャンセル音

					if (!this.restartScreen.IsDisp && !this.prevToSelectScreen.IsDisp)
					{
						this.currentState = State.Continue;
					}
					else if (this.restartScreen.IsDisp)
					{
						this.elapsedTime = 0;
						this.restartScreen.IsDisp = false;
					}
					else if (this.prevToSelectScreen.IsDisp)
					{
						this.elapsedTime = 0;
						this.restartScreen.IsDisp = false;
					}
				}
				else
				{
					this.UpdateState();
				}
			}

			private void UpdateState()
			{
				this.prevState = this.currentState;

				if (this.restartScreen.IsDisp || this.prevToSelectScreen.IsDisp) { return; }

				if (RiaInput.Instance.GetKeyDown(RiaInput.KeyType.Down, PlayerNumber.player1) ||
					RiaInput.Instance.GetKeyDown(RiaInput.KeyType.Down, PlayerNumber.player2))
				{
					var currentStateIndex = (int)this.currentState;
					var nextStateIndex = (currentStateIndex + 1) % (int)State.Lenght;
					this.currentState = (State)Enum.ToObject(typeof(State), nextStateIndex);
				}
				else if (RiaInput.Instance.GetKeyDown(RiaInput.KeyType.Up, PlayerNumber.player1) ||
						 RiaInput.Instance.GetKeyDown(RiaInput.KeyType.Up, PlayerNumber.player2))
				{
					var currentStateIndex = (int)this.currentState;
					var prevStateIndex = ((int)State.Lenght + currentStateIndex - 1) % (int)State.Lenght;
					this.currentState = (State)Enum.ToObject(typeof(State), prevStateIndex);
				}
			}

			private void UpdateDisp()
			{

				if (this.prevState == this.currentState) { return; }

				switch (this.currentState)
				{
					case PauseWindow.State.Continue:
						this.ChageButtonState(
							PauseWindow.State.Continue,
							PauseWindow.ButtonSet.State.Active);
						this.ChageButtonState(
							PauseWindow.State.Restart,
							PauseWindow.ButtonSet.State.Normal);
						this.ChageButtonState(
							PauseWindow.State.PrevToSelect,
							PauseWindow.ButtonSet.State.Normal);
						return;

					case PauseWindow.State.Restart:
						this.ChageButtonState(
							PauseWindow.State.Continue,
							PauseWindow.ButtonSet.State.Normal);
						this.ChageButtonState(
							PauseWindow.State.Restart,
							PauseWindow.ButtonSet.State.Active);
						this.ChageButtonState(
							PauseWindow.State.PrevToSelect,
							PauseWindow.ButtonSet.State.Normal);
						return;

					case PauseWindow.State.PrevToSelect:
						this.ChageButtonState(
							PauseWindow.State.Continue,
							PauseWindow.ButtonSet.State.Normal);
						this.ChageButtonState(
							PauseWindow.State.Restart,
							PauseWindow.ButtonSet.State.Normal);
						this.ChageButtonState(
							PauseWindow.State.PrevToSelect,
							PauseWindow.ButtonSet.State.Active);
						return;
				}
			}

			private void ChageButtonState(State _button, ButtonSet.State _buttonState)
			{
				var button =
					(_button == State.Continue) ? this.continueButton :
					(_button == State.Restart) ? this.restartButton :
					(_button == State.PrevToSelect) ? this.prevToSelectButton :
							 null;

				if (button == null) { return; }

				button.image.sprite =
					(_buttonState == ButtonSet.State.Normal) ? button.spNormal :
					(_buttonState == ButtonSet.State.Active) ? button.spActive :
					null;
			}
		}

		#endregion

		#region HPGage
		[System.Serializable]
		public class HPGage
		{
			[SerializeField]
			private Image imgLiveColor;

			public PlayerNumber PlayerNumber { get; set; }
			public float HitPoint { get { return GameManager.Instance.GetPlayerHitPoint(this.PlayerNumber); } }
			public float HitPointMax { get; private set; }
			public float PrevHitPoint { get; private set; }

			public void Init(PlayerNumber _playerNumber)
			{
				this.PlayerNumber = _playerNumber;
				this.imgLiveColor.fillAmount = 1.0f;
				this.HitPointMax = GameManager.Instance.GetPlayerHitPointMax(this.PlayerNumber);
			}

			public void Run()
			{
				// キャッシュ
				var hp = this.HitPoint;

				var rate = Mathf.Ceil(hp) / this.HitPointMax;
				this.imgLiveColor.fillAmount = rate;
			}
		}
		#endregion

		[SerializeField, Header("Pause Window")]
		private PauseWindow pauseWindow = null;
		
		[SerializeField, Header("HitPointGage")]
		private HPGage hpgPlayer1 = null;
		[SerializeField]
		private HPGage hpgPlayer2 = null;

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