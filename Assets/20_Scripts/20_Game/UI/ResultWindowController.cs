/* Author: flanny7
 * Update: 2018/11/1
*/

using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
	[System.Serializable]
	public sealed class ResultWindowController
	{
		[System.Serializable]
		public struct Screen
		{
			[Header("Parent GameObject (PlayerNumber)")]
			public GameObject goParent;
			[Space(4)]
			[Header("Winner")]
			public Image imgStandChara;
			public Image imgStandWinnerName;
			[Space(2)]
			public Image imgPanelWinnerName;
			public Text textWinnerScore;
			[Space(4)]
			[Header("Loser")]
			public Image imgPanelLoserName;
			public Text textLoserScore;
		}

		[SerializeField, Header("Result's Parent GameObject")]
		private GameObject goParent = null;
		[Space(6)]
		[SerializeField, Header("Player1")]
		private Screen player1Screen;
		[Space(2)]
		[SerializeField, Header("Player2")]
		private Screen player2Screen;

		// キャッシュ
		private PlayerNumber winner;

		private void StartResult(PlayerNumber _winner)
		{
			this.winner = _winner;

			this.goParent.SetActive(true);
			this.StartScreen(this.winner);
		}

		private void EndResult()
		{
			this.player1Screen.goParent.SetActive(false);
			this.player2Screen.goParent.SetActive(false);
			this.goParent.SetActive(false);
		}

		private void StartScreen(PlayerNumber _winner)
		{
			if (_winner == PlayerNumber.player1)
			{
				this.player1Screen.goParent.SetActive(true);
				this.player2Screen.goParent.SetActive(false);
			}
			else if (_winner == PlayerNumber.player2)
			{
				this.player1Screen.goParent.SetActive(false);
				this.player2Screen.goParent.SetActive(true);
			}
		}

		private void UpdateScreen(PlayerNumber _winner)
		{
			// 使用するScreenなどの選定（winnerのscreenを選定）
			Screen screen;
			int winnerScore, loserScore;
			PlayerCharacterEnum winnerChara, loserChara;

			var data = GameManager.Instance.CommonData;

			Screen? tmp = null;
			if (_winner == PlayerNumber.player1)
			{
				/// スクリーン
				tmp = this.player1Screen;
				/// スコア
				winnerScore = data.player1Score;
				loserScore = data.player2Score;
				/// キャラ
				winnerChara = data.playerCharacter1;
				loserChara = data.playerCharacter2;
			}
			else if (_winner == PlayerNumber.player2)
			{
				/// スクリーン
				tmp = this.player2Screen;
				/// スコア
				winnerScore = data.player2Score;
				loserScore = data.player1Score;
				/// キャラ
				winnerChara = data.playerCharacter2;
				loserChara = data.playerCharacter1;
			}

			if (tmp == null)
			{
				Debug.LogError("_winnerの値が不正です");
				Debug.Break();
			}
			else
			{
				screen = (Screen)tmp;
			}

			// Stand上の更新
			screen.imgStandChara = null;
			screen.imgStandWinnerName = null;

			// Panel上の更新
			/// Winner
			screen.imgPanelWinnerName = null;
			screen.textWinnerScore = null;
			/// Loser
			screen.imgPanelLoserName = null;
			screen.textLoserScore = null;
		}
	}
}