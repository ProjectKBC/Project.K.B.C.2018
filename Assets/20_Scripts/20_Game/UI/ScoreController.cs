using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{

	[System.Serializable]
	public sealed class ScoreController
	{
		[SerializeField]
		private Text scoreText;

		public PlayerNumber PlayerNumber { get; set; }
		public int Score { get { return GameManager.Instance.GetPlayerScore(this.PlayerNumber); } }

		public void Init(PlayerNumber _playerNumber)
		{
			this.PlayerNumber = _playerNumber;
		}

		public void Run()
		{
			this.scoreText.text = string.Format("{0:000 000 000}", this.Score);
		}
	}
}