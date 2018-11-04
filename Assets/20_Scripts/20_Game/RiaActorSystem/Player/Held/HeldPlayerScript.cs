using UnityEngine;
using RiaActorSystem;

namespace Game.Player
{
	[CreateAssetMenu(menuName = "RiaActorSystem/Player/HeldPlayerScript", fileName = "HeldPlayerScript")]
	public sealed class HeldPlayerScript : RiaPlayerScript
	{
		public readonly PlayerCharacterEnum playerCharacter = PlayerCharacterEnum.held;

		[System.Serializable]
		public class NormalShotParam
		{
			public float shotInterval = 0.15f;
		}

		[System.Serializable]
		public class SpecialShotParam
		{
			public float shotInterval = 0.5f;
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