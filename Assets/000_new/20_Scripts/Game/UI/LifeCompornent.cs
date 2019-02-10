using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Game
{
	namespace UI
	{
		public class LifeCompornent : BaseCompornent
		{
			public int Life { private get { return this.life; } set { this.life = value; } }
			
			[SerializeField] private int life = 0;
			[SerializeField] private int lifeMax = 0;
			[SerializeField] private Image imgLiveColorBar = null;
			
			private void Update()
			{
				var rate = Mathf.Ceil(this.Life) / this.lifeMax;
				this.imgLiveColorBar.fillAmount = rate;
			}
		}
	}
}
