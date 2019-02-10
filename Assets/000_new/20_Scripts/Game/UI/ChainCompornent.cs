using UnityEngine;
using UnityEngine.UI;

namespace Game
{
	namespace UI
	{
		public class ChainCompornent : BaseCompornent
		{
			public int Chain { private get { return this.chain; }  set { this.chain = value;}}
			
			[SerializeField] private int chain = 0;
			[SerializeField] private Text text = null;
			
			private void Start()
			{
			}

			private void Update()
			{
				this.text.text = string.Format("{000}", this.Chain);
			}
		}
	}
}
