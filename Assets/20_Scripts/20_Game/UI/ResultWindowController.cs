/* Author: flanny7
 * Update: 2018/11/1
*/

using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Game.UI
{
	[System.Serializable]
	public sealed class ResultWindowController
	{
		[System.Serializable]
		public struct Screen
		{
			[Header("CharaStandSprites")]
			public CharacterStandSpriteCatalog standCatalog;
			[Header("Parent GameObject (PlayerNumber)")]
			public GameObject goParent;
			[Space(4)]
			[Header("Winner")]
			public Image imgStandChara;
			public Image imgStandWinnerNameKana;
			public Image imgStandWinnerNameAlp;
			[Space(2)]
			public Image imgPanelWinnerName;
			public Text textWinnerScore;
			[Space(4)]
			[Header("Loser")]
			public Image imgPanelLoserName;
			public Text textLoserScore;
		}

		[SerializeField] private float canDeleteDelay = 3.0f;
		[SerializeField, Header("CharaNameSprites")]
		private CharacterNameSpriteCatalog nameCatalog;
		[SerializeField, Header("Result's Parent GameObject")]
		private GameObject goParent = null;
		[Space(6)]
		[SerializeField, Header("Player1")]
		private Screen player1Screen;
		[Space(2)]
		[SerializeField, Header("Player2")]
		private Screen player2Screen;
		
		//
		private CharacterStandSpriteDict pl1StandDict;
		private CharacterStandSpriteDict pl2StandDict;
		private CharacterNameSpriteDict nameDict;
		private float elapsedTime = 0;
		
		// キャッシュ
		private PlayerNumber winner;

		public void Start(PlayerNumber _winner)
		{
			this.winner = _winner;

			this.pl1StandDict = new CharacterStandSpriteDict(this.player1Screen.standCatalog);
			this.pl2StandDict = new CharacterStandSpriteDict(this.player2Screen.standCatalog);
			this.nameDict = new CharacterNameSpriteDict(this.nameCatalog);
			
			this.goParent.SetActive(true);
			this.StartScreen(this.winner);

			this.elapsedTime = 0;
		}

		public void Update(PlayerNumber _winner)
		{
			this.elapsedTime += Time.deltaTime;
			
			// 使用するScreenなどの選定（winnerのscreenを選定）
			Screen screen;
			int winnerScore, loserScore;
			PlayerCharacterEnum winnerChara, loserChara;
			CharacterStandSpriteDict standDict = null;

			var data = GameManager.Instance.CommonData;

			if (_winner == PlayerNumber.player1)
			{
				// スクリーン
				screen = this.player1Screen;
				// スコア
				winnerScore = data.player1Score;
				loserScore = data.player2Score;
				// キャラ
				winnerChara = data.playerCharacter1;
				loserChara = data.playerCharacter2;
				// スプライト
				standDict = this.pl1StandDict;
			}
			else// if (_winner == PlayerNumber.player2)
			{
				// スクリーン
				screen = this.player2Screen;
				// スコア
				winnerScore = data.player2Score;
				loserScore = data.player1Score;
				// キャラ
				winnerChara = data.playerCharacter2;
				loserChara = data.playerCharacter1;
				// スプライト
				standDict = this.pl2StandDict;
			}

			// Stand上の更新
			screen.imgStandChara.sprite = standDict.Get(winnerChara);
			screen.imgStandWinnerNameKana.sprite = this.nameDict.GetKana(winnerChara);
			screen.imgStandWinnerNameAlp.sprite = this.nameDict.GetAlp(winnerChara);

			// Panel上の更新
			// Winner
			screen.imgPanelWinnerName.sprite = this.nameDict.GetKana(winnerChara);
			screen.textWinnerScore.text = winnerScore.ToString("D9");
			// Loser
			screen.imgPanelLoserName.sprite = this.nameDict.GetKana(loserChara);
			screen.textLoserScore.text = loserScore.ToString("D9");

			if (this.canDeleteDelay <= this.elapsedTime && 
			    (RiaInput.Instance.GetPush(RiaInput.KeyType.Return, PlayerNumber.player1) || 
			     RiaInput.Instance.GetPush(RiaInput.KeyType.Return, PlayerNumber.player2)))
			{
				GameManager.Instance.ChageState(GameManager.State.Finalize);
			}
		}
		
		public void End()
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
	}
}