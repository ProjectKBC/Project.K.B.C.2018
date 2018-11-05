using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
	[System.Serializable]
	public sealed class PlayTimeController
	{
		[SerializeField]
		private Text player1text;
		[SerializeField]
		private Text player2text;

		private int Time { get { return (int)Mathf.Floor(GameManager.Instance.PlayElapsedTime); } }

		public void Update()
		{
			this.player1text.text = string.Format("{0:000}", this.Time);
			this.player2text.text = string.Format("{0:000}", this.Time);
		}
	}
}