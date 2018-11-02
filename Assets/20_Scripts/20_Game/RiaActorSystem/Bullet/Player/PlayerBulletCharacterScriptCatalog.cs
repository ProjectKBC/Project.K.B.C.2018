/* Author: flanny7
 * Update: 2018/10/31
*/

using UnityEngine;
using RiaActorSystem;

namespace Game.Bullet.Player
{
	[CreateAssetMenu(menuName = "RiaCharacterScript/CharacterCatalog/PlayerBullet", fileName = "PlayerBulletCharacterCatalog")]
	public sealed class PlayerBulletCharacterScriptCatalog : ScriptableObject
	{
		[System.Serializable]
		public class AirosNormalBulletSet
		{
			public RiaPlayerBulletScript airosNormalBulletRight;
			public RiaPlayerBulletScript airosNormalBulletLeft;
		}

		[System.Serializable]
		public class AirosSpecialBulletSet
		{
			public RiaPlayerBulletScript airosSpecialBullet;
		}

		[System.Serializable]
		public class AnomaNormalBulletSet
		{
			public RiaPlayerBulletScript anomaNormalBulletRight;
			public RiaPlayerBulletScript anomaNormalBulletLeft;
		}

		[System.Serializable]
		public class KaoruNormalBulletSet
		{
			public RiaPlayerBulletScript kaoruNormalBulletRight;
			public RiaPlayerBulletScript kaoruNormalBulletLeft;
		}

		[SerializeField]
		private AirosNormalBulletSet airosNormalBullet;

		public AirosNormalBulletSet AirosNormalBullet { get { return this.airosNormalBullet; } }

		[SerializeField]
		private AirosSpecialBulletSet airosSpecialBullet;

		public AirosSpecialBulletSet AirosSpecialBullet { get { return this.airosSpecialBullet; } }

		[SerializeField]
		private AnomaNormalBulletSet anomaNormalBullet;

		public AnomaNormalBulletSet AnomaNormalBullet { get { return this.anomaNormalBullet; } }

		[SerializeField]
		private KaoruNormalBulletSet kaoruNormalBullet;

		public KaoruNormalBulletSet KaoruNormalBullet { get { return this.kaoruNormalBullet; } }
	}
}