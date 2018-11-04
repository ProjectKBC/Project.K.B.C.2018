using UnityEngine;
using RiaActorSystem;

namespace Game.Player
{
	[CreateAssetMenu(menuName = "RiaActorSystem/Player/AirosPlayerScript", fileName = "AirosPlayerScript")]
	public sealed class AirosPlayerScript : RiaPlayerScript
	{
		public readonly PlayerCharacterEnum playerCharacter = PlayerCharacterEnum.airos;

		[System.Serializable]
		public class NormalShotParam
		{
			public float shotInterval = 0.15f;
		}

		[System.Serializable]
		public class SpecialShotParam
		{
			public float shotInterval = 0.5f;
//			public float shotCoolTime = 1.5f;
//			[Space(8), Header("サーチ")]
//			public float searchInterval = 0.5f;
//			public int searchCountMax = 3;
//			public float searchAreaRange = 30.0f;
		}

		[System.Serializable]
		public class SkilParam
		{
//			[Header("")]
//			public float durationTime = 5.0f;
//			[Space(8), Header("効果")]
//			public float alphaMaxValue = 0.75f;
//			public float debuffSpeedMin = 0.5f;
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