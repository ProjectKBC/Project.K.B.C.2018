using UnityEngine;
using RiaActorSystem;

namespace Game.Player
{
	[CreateAssetMenu(menuName = "RiaActorSystem/Player/AirosPlayerScript", fileName = "AirosPlayerScript")]
	public sealed class AirosPlayerScript : RiaPlayerScript
	{
		public readonly PlayerCharacterEnum playerCharacter = PlayerCharacterEnum.airos;
		public readonly float AlphaMaxValue = 0.75f;

		[System.Serializable]
		public class NormalShotParam
		{
			public GameObject bulletPrefab = null;
			public float shotInterval = 0.15f;
		}

		[System.Serializable]
		public class SpecialShotParam
		{
			public float shotRange = 30.0f;
			public float searchTime = 0.5f;
			public int searchNumOfTimes = 3;
			public float shotCoolTime = 1.5f;
		}

		[System.Serializable]
		public class SkilParam
		{
			public float coolTime = 5.0f;
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