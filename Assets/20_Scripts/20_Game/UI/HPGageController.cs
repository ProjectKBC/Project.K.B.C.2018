/* Author: flanny7
 * Update: 2018/10/30
*/

using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
	public sealed class HPGageController : MonoBehaviour
	{
		[System.Serializable]
		public class HPGageParam
		{
			[SerializeField]
			private Image imgLiveColor;

			public PlayerNumber PlayerNumber { get; set; }
			public float HitPoint { get { return GameManager.Instance.GetPlayerHitPoint(this.PlayerNumber); } }
			public float HitPointMax { get; private set; }
			public float PrevHitPoint { get; private set; }

			public void Init(PlayerNumber _playerNumber)
			{
				this.PlayerNumber = _playerNumber;
				this.imgLiveColor.fillAmount = 1.0f;
				this.HitPointMax = GameManager.Instance.GetPlayerHitPointMax(this.PlayerNumber);
			}

			public void Run()
			{
				// キャッシュ
				var hp = this.HitPoint;

				var rate = Mathf.Ceil(hp / this.HitPointMax);
				this.imgLiveColor.fillAmount = rate;
			}
		}

		[SerializeField]
		private HPGageParam player1;
		[SerializeField]
		private HPGageParam player2;

		public void Init()
		{
			this.player1.Init(PlayerNumber.player1);
			this.player2.Init(PlayerNumber.player2);
		}

		public void Run()
		{
			this.player1.Run();
			this.player2.Run();
		}
	}
}