using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
	public class GameUIController : SingletonMonoBehaviour<GameUIController>
	{
		[System.Serializable]
		public class PoseWindow
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

			public float pushIntervalTime = 1.5f;
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

				this.currentState = PoseWindow.State.Continue;
				this.prevState = PoseWindow.State.Continue;

				this.parentMainWindowObject.SetActive(false);
			}

			public void WakeUp()
			{
				this.elapsedTime = 0;

				this.currentState = PoseWindow.State.Continue;
				this.prevState = PoseWindow.State.Lenght;

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

			public void Update()
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
					case PoseWindow.State.Continue:
						this.ChageButtonState(
							PoseWindow.State.Continue,
							PoseWindow.ButtonSet.State.Active);
						this.ChageButtonState(
							PoseWindow.State.Restart,
							PoseWindow.ButtonSet.State.Normal);
						this.ChageButtonState(
							PoseWindow.State.PrevToSelect,
							PoseWindow.ButtonSet.State.Normal);
						return;

					case PoseWindow.State.Restart:
						this.ChageButtonState(
							PoseWindow.State.Continue,
							PoseWindow.ButtonSet.State.Normal);
						this.ChageButtonState(
							PoseWindow.State.Restart,
							PoseWindow.ButtonSet.State.Active);
						this.ChageButtonState(
							PoseWindow.State.PrevToSelect,
							PoseWindow.ButtonSet.State.Normal);
						return;

					case PoseWindow.State.PrevToSelect:
						this.ChageButtonState(
							PoseWindow.State.Continue,
							PoseWindow.ButtonSet.State.Normal);
						this.ChageButtonState(
							PoseWindow.State.Restart,
							PoseWindow.ButtonSet.State.Normal);
						this.ChageButtonState(
							PoseWindow.State.PrevToSelect,
							PoseWindow.ButtonSet.State.Active);
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

		[SerializeField, Header("Pause Window")]
		private PoseWindow poseWindow = null;

		protected override void OnInit()
		{
			this.poseWindow.Init();
		}

		public void PauseStart()
		{
			this.poseWindow.WakeUp();
		}

		public void PauseUpdate()
		{
			this.poseWindow.Update();
		}

		public void PauseEnd()
		{
			this.poseWindow.Sleep();
		}
	}
}
