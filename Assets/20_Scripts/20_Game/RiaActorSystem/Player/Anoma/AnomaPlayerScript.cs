using UnityEngine;
using RiaActorSystem;

namespace Game.Player
{
	[CreateAssetMenu(menuName = "RiaActorSystem/Player/AnomaPlayerScript", fileName = "AnomaPlayerScript")]
	public sealed class AnomaPlayerScript : RiaPlayerScript
	{
		public readonly PlayerCharacterEnum playerCharacter = PlayerCharacterEnum.anoma;

		[System.Serializable]
		public class NormalShotParam
		{
			public GameObject bulletPrefab = null;
			public float shotInterval = 0.1f;
		}

		[System.Serializable]
		public class SpecialShotParam
		{
			public GameObject bulletPrefab = null;
			public float shotChargeTime = 3;
		}

		[System.Serializable]
		public class SkilParam
		{
			
		}

		// 通常ショット
		[Header("NormalBullet"), Tooltip("通常ショット")]
		public NormalShotParam nsParam = null;

		// 特殊ショット
		[Header("SpecialBullet"), Tooltip("特殊ショット")]
		public SpecialShotParam ssParam = null;

		// スキル
		[Header("SpecialBullet"), Tooltip("スキル")]
		public SkilParam skilParam = null;
	}
}