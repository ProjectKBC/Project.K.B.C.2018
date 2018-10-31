/* Author: flanny7
 * Update: 2018/11/1
*/

using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
	[System.Serializable]
	public sealed class PauseWindow
	{
		[System.Serializable]
		public class Screen
		{
			public Image image;
			public bool IsDisp { get { return this.image.enabled; } set { this.image.enabled = value; } }
		}

		[System.Serializable]
		public struct ButtonSet
		{
			public enum State
			{
				Normal,
				Active,
				Length
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

		[SerializeField, Header("ボタンの反応間隔")]
		private float pushIntervalTime = 1.0f / 30.0f;
		[Space(8)]
		[SerializeField, Header("ボタン")]
		private ButtonSet continueButton;
		[SerializeField]
		private ButtonSet restartButton;
		[SerializeField]
		private ButtonSet prevToSelectButton;
		[Space(8)]
		[SerializeField, Header("スクリーン")]
		private GameObject parentMainWindowObject = null;
		[SerializeField]
		private Screen restartScreen = null;
		[SerializeField]
		private Screen prevToSelectScreen = null;

		private float elapsedTime;
		private State currentState = State.Continue;
		private State prevState = State.Continue;

		public void Init()
		{
			this.elapsedTime = 0;

			this.currentState = State.Continue;
			this.prevState = State.Continue;

			this.parentMainWindowObject.SetActive(false);
		}

		public void WakeUp()
		{
			this.elapsedTime = 0;

			this.currentState = State.Continue;
			this.prevState = State.Lenght;

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
				case State.Continue:
					this.ChageButtonState(
						State.Continue,
						PauseWindow.ButtonSet.State.Active);
					this.ChageButtonState(
						State.Restart,
						PauseWindow.ButtonSet.State.Normal);
					this.ChageButtonState(
						State.PrevToSelect,
						PauseWindow.ButtonSet.State.Normal);
					return;

				case State.Restart:
					this.ChageButtonState(
						State.Continue,
						PauseWindow.ButtonSet.State.Normal);
					this.ChageButtonState(
						State.Restart,
						PauseWindow.ButtonSet.State.Active);
					this.ChageButtonState(
						State.PrevToSelect,
						PauseWindow.ButtonSet.State.Normal);
					return;

				case State.PrevToSelect:
					this.ChageButtonState(
						State.Continue,
						PauseWindow.ButtonSet.State.Normal);
					this.ChageButtonState(
						State.Restart,
						PauseWindow.ButtonSet.State.Normal);
					this.ChageButtonState(
						State.PrevToSelect,
						PauseWindow.ButtonSet.State.Active);
					return;
			}
		}

		private void ChageButtonState(State _button, ButtonSet.State _buttonState)
		{
			ButtonSet button;
			if (_button == State.Continue) { button = this.continueButton; }
			else if (_button == State.Restart) { button = this.restartButton; }
			else if (_button == State.PrevToSelect) { button = this.prevToSelectButton; }
			else { return; }

			button.image.sprite =
				(_buttonState == ButtonSet.State.Normal) ? button.spNormal :
				(_buttonState == ButtonSet.State.Active) ? button.spActive :
				null;
		}
	}
}