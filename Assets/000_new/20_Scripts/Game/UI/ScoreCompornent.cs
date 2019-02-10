using UnityEngine;
using UnityEngine.UI;

namespace Game
{
	namespace UI
	{
		public class ScoreCompornent : BaseCompornent
		{
			public int Score { private get { return this.score; } set { this.score = value; } }
			
			[SerializeField] private int score = 0;
			[SerializeField] private Text text = null;
			
			private void Start()
			{
			}

			private void Update()
			{
				this.text.text = string.Format("{0:000 000 000}", this.Score);
			}
		}
	}
}
