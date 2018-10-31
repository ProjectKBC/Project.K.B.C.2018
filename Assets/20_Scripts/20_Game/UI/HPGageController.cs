/* Author: flanny7
 * Update: 2018/11/1
*/

using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
	[System.Serializable]
	public sealed class HPGageController
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

			var rate = Mathf.Ceil(hp) / this.HitPointMax;
			this.imgLiveColor.fillAmount = rate;
		}
	}
}