/* Author: flanny7
 * Update: 2018/11/4
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

		/// <summary>
		/// WindowやStateの切り替え判定と処理 by flanny7
		/// </summary>
		private void UpdateScreen()
		{
			// 決定ボタンが押された
			if (this.pushIntervalTime <= this.elapsedTime && /* ボタン用のインターバル */
				(RiaInput.Instance.GetPushDown(RiaInput.KeyType.Return, PlayerNumber.player1) ||
				 RiaInput.Instance.GetPushDown(RiaInput.KeyType.Return, PlayerNumber.player2)))
			{
				// MainWindow
				if (!this.restartScreen.IsDisp && !this.prevToSelectScreen.IsDisp)
				{
					if (this.currentState == State.Continue)
					{
						this.Sleep();

						// SE: 決定音
						AudioManager.Instance.PlaySe(SoundEffectEnum.decision);

						// GamaManagerのStateを切り替える
						GameManager.Instance.ChageState(GameManager.State.Play);
					}
					else if (this.currentState == State.Restart)
					{
						this.elapsedTime = 0;

						// SE: 決定音
						AudioManager.Instance.PlaySe(SoundEffectEnum.decision);

						// リスタートウィンドウを表示するフラグを立てる
						this.restartScreen.IsDisp = true;
					}
					else if (this.currentState == State.PrevToSelect)
					{
						this.elapsedTime = 0;

						// SE: 決定音
						AudioManager.Instance.PlaySe(SoundEffectEnum.decision);

						// 選択画面へ戻るウィンドウを表示するフラグを立てる
						this.prevToSelectScreen.IsDisp = true;
					}
				}
				// RestartWindow
				else if (this.restartScreen.IsDisp)
				{
					this.Sleep();

					// SE: 決定音
					AudioManager.Instance.PlaySe(SoundEffectEnum.decision);

					// GamaManagerのStateを切り替える
					GameManager.Instance.ChageState(GameManager.State.Initialize);
				}
				// PrevToSelect
				else if (this.prevToSelectScreen.IsDisp)
				{
					this.Sleep();

					// SE: 決定音
					AudioManager.Instance.PlaySe(SoundEffectEnum.decision);

					// GamaManagerのStateを切り替える
					GameManager.Instance.ChageState(GameManager.State.Finalize);
				}
			}
			// キャンセルボタンが押されたとき
			else if (this.pushIntervalTime <= this.elapsedTime && /* ボタン用のインターバル */
					 (RiaInput.Instance.GetPushDown(RiaInput.KeyType.Cancel, PlayerNumber.player1) ||
					  RiaInput.Instance.GetPushDown(RiaInput.KeyType.Cancel, PlayerNumber.player2)))
			{
				// ウィンドウ非表示時
				if (!this.restartScreen.IsDisp && !this.prevToSelectScreen.IsDisp)
				{
					this.currentState = State.Continue;
				}
				// リスタートウィンドウ表示時
				else if (this.restartScreen.IsDisp)
				{
					this.elapsedTime = 0;
					this.restartScreen.IsDisp = false;
				}
				// 選択画面へ戻るウィンドウ表示時
				else if (this.prevToSelectScreen.IsDisp)
				{
					this.elapsedTime = 0;
					this.restartScreen.IsDisp = false;
				}

				// SE: キャンセル音
				AudioManager.Instance.PlaySe(SoundEffectEnum.cansel);
			}
			// 何も押されていない
			else
			{
				this.UpdateState();
			}
		}
		
		private void UpdateState()
		{
			// 更新前に格納
			this.prevState = this.currentState;
			
			// いずれかのウィンドウが表示されている場合のバリア
			if (this.restartScreen.IsDisp || this.prevToSelectScreen.IsDisp) { return; }

			if (RiaInput.Instance.GetPushDown(RiaInput.KeyType.Down, PlayerNumber.player1) ||
				RiaInput.Instance.GetPushDown(RiaInput.KeyType.Down, PlayerNumber.player2))
			{
				NextWindowState();

				// SE: 移動音
				AudioManager.Instance.PlaySe(SoundEffectEnum.cursor);
			}
			else if (RiaInput.Instance.GetPushDown(RiaInput.KeyType.Up, PlayerNumber.player1) ||
					 RiaInput.Instance.GetPushDown(RiaInput.KeyType.Up, PlayerNumber.player2))
			{
				PrevWindowState();

				// SE: 移動音
				AudioManager.Instance.PlaySe(SoundEffectEnum.cursor);
			}
		}

		private void NextWindowState()
		{
			var currentStateIndex = (int)this.currentState;
			var nextStateIndex = (currentStateIndex + 1) % (int)State.Lenght;
			this.currentState = (State)Enum.ToObject(typeof(State), nextStateIndex);
		}

		private void PrevWindowState()
		{
			var currentStateIndex = (int)this.currentState;
			var prevStateIndex = ((int)State.Lenght + currentStateIndex - 1) % (int)State.Lenght;
			this.currentState = (State)Enum.ToObject(typeof(State), prevStateIndex);
		}

		private void UpdateDisp()
		{

			if (this.prevState == this.currentState) { return; }

			switch (this.currentState)
			{
				case State.Continue:
					this.ChageButtonState(State.Continue, ButtonSet.State.Active);
					this.ChageButtonState(State.Restart, ButtonSet.State.Normal);
					this.ChageButtonState(State.PrevToSelect, ButtonSet.State.Normal);
					return;

				case State.Restart:
					this.ChageButtonState(State.Continue, ButtonSet.State.Normal);
					this.ChageButtonState(State.Restart, ButtonSet.State.Active);
					this.ChageButtonState(State.PrevToSelect, ButtonSet.State.Normal);
					return;

				case State.PrevToSelect:
					this.ChageButtonState(State.Continue, ButtonSet.State.Normal);
					this.ChageButtonState(State.Restart, ButtonSet.State.Normal);
					this.ChageButtonState(State.PrevToSelect, ButtonSet.State.Active);
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