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
		public class AnomaNormalBulletSet
		{
			public RiaPlayerBulletScript anomaNormalBulletRight;
			public RiaPlayerBulletScript anomaNormalBulletLeft;
		}

		[SerializeField]
		private AirosNormalBulletSet airosNormalBullet;

		public AirosNormalBulletSet AirosNormalBullet { get { return this.airosNormalBullet; } }

		[SerializeField]
		private AnomaNormalBulletSet anomaNormalBullet;

		public AnomaNormalBulletSet AnomaNormalBullet { get { return this.anomaNormalBullet; } }
	}
}