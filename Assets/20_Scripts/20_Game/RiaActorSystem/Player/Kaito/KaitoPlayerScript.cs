using UnityEngine;
using RiaActorSystem;

namespace Game.Player
{
	[CreateAssetMenu(menuName = "RiaActorSystem/Player/KaitoPlayerScript", fileName = "KaitoPlayerScript")]
	public sealed class KaitoPlayerScript : RiaPlayerScript
	{
		public readonly PlayerCharacterEnum playerCharacter = PlayerCharacterEnum.kaito;

		[System.Serializable]
		public class NormalShotParam
		{
			public float shotInterval = 0.5f;
		}

		[System.Serializable]
		public class SpecialShotParam
		{
			
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