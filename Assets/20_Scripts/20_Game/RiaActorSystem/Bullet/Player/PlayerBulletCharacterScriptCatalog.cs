/* Author: flanny7
 * Update: 2018/10/31
*/

using System;
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
		public class AnomaSpecialBulletSet
		{
			public RiaPlayerBulletScript anomaSpecialBulletRight;
			public RiaPlayerBulletScript anomaSpecialBulletCenter;
			public RiaPlayerBulletScript anomaSpecialBulletLeft;
		}

		[System.Serializable]
		public class HeldNormalBulletSet
		{
			public RiaPlayerBulletScript heldNormalBulletRight;
			public RiaPlayerBulletScript heldNormalBulletLeft;
		}
		
		[System.Serializable]
		public class HeldSpecialBulletSet
		{
			public RiaPlayerBulletScript heldSpecialBulletRight;
			public RiaPlayerBulletScript heldSpecialBulletCenter;
			public RiaPlayerBulletScript heldSpecialBulletLeft;
		}

		[System.Serializable]
		public class KaitoNormalBulletSet
		{
			public RiaPlayerBulletScript kaitoNormalBulletRight;
			public RiaPlayerBulletScript kaitoNormalBulletCenter;
			public RiaPlayerBulletScript kaitoNormalBulletLeft;
		}
		
		[System.Serializable]
		public class KaitoSpecialBulletSet
		{
			public RiaPlayerBulletScript kaitoSpecialBulletRight;
			public RiaPlayerBulletScript kaitoSpecialBulletCenter;
			public RiaPlayerBulletScript kaitoSpecialBulletLeft;
		}

		[System.Serializable]
		public class KaoruNormalBulletSet
		{
			public RiaPlayerBulletScript kaoruNormalBulletRight;
			public RiaPlayerBulletScript kaoruNormalBulletLeft;
		}
		
		[System.Serializable]
		public class KaoruSpecialBulletSet
		{
			public RiaPlayerBulletScript kaoruSpecialBulletRight;
			public RiaPlayerBulletScript kaoruSpecialBulletCenter;
			public RiaPlayerBulletScript kaoruSpecialBulletLeft;
		}
		
		[System.Serializable]
		public class TwistNormalBulletSet
		{
			public RiaPlayerBulletScript twistNormalBulletRight;
			public RiaPlayerBulletScript twistNormalBulletLeft;
		}
		
		[System.Serializable]
		public class TwistSpecialBulletSet
		{
			public RiaPlayerBulletScript twistSpecialBulletRight;
			public RiaPlayerBulletScript twistSpecialBulletCenter;
			public RiaPlayerBulletScript twistSpecialBulletLeft;
		}
		
		[System.Serializable]
		public class VeronicaNormalBulletSet
		{
			public RiaPlayerBulletScript veronicaNormalBulletRight;
			public RiaPlayerBulletScript veronicaNormalBulletLeft;
		}
		[System.Serializable]
		public class VeronicaSpecialBulletSet
		{
			public RiaPlayerBulletScript veronicaSpecialBulletRight;
			public RiaPlayerBulletScript veronicaSpecialBulletCenter;
			public RiaPlayerBulletScript veronicaSpecialBulletLeft;
		}
		
		[System.Serializable]
		public class GeneralNormalBulletSet
		{
			public RiaPlayerBulletScript generalNormalBulletRight;
			public RiaPlayerBulletScript generalNormalBulletLeft;
		}

		[System.Serializable]
		public class GeneralSpecialBulletSet
		{
			public RiaPlayerBulletScript generalSpecialBulletRight;
			public RiaPlayerBulletScript generalSpecialBulletCenter;
			public RiaPlayerBulletScript generalSpecialBulletLeft;
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
		private AnomaSpecialBulletSet anomaSpecialBullet;

		public AnomaSpecialBulletSet AnomaSpecialBullet { get { return this.anomaSpecialBullet; } }
		
		[SerializeField]
		private HeldNormalBulletSet heldNormalBullet;
		
		public HeldNormalBulletSet HeldNormalBullet { get { return this.heldNormalBullet; } }
		
		[SerializeField]
		private HeldSpecialBulletSet heldSpecialBullet;
		
		public HeldSpecialBulletSet HeldSpecialBullet { get { return this.heldSpecialBullet; } }
		
		[SerializeField]
		private KaitoNormalBulletSet kaitoNormalBullet;

		public KaitoNormalBulletSet KaitoNormalBullet { get { return this.kaitoNormalBullet; } }
		
		[SerializeField]
		private KaitoSpecialBulletSet kaitoSpecialBullet;
		
		public KaitoSpecialBulletSet FakeKaitoSpecialBullet { get { return this.kaitoSpecialBullet; } }

		[SerializeField]
		private KaoruNormalBulletSet kaoruNormalBullet;

		public KaoruNormalBulletSet KaoruNormalBullet { get { return this.kaoruNormalBullet; } }
		
		[SerializeField]
		private KaoruSpecialBulletSet kaoruSpecialBullet;
		
		public KaoruSpecialBulletSet KaoruSpecialBullet { get { return this.kaoruSpecialBullet; } }
		
		[SerializeField]
		private TwistNormalBulletSet twistNormalBullet;

		public TwistNormalBulletSet TwistNormalBullet { get { return this.twistNormalBullet; } }
		
		[SerializeField]
		private TwistSpecialBulletSet twistSpecialBullet;

		public TwistSpecialBulletSet TwistSpecialBullet { get { return this.twistSpecialBullet; } }
		
		[SerializeField]
		private VeronicaNormalBulletSet veronicaNormalBullet;

		public VeronicaNormalBulletSet VeronicaNormalBullet { get { return this.veronicaNormalBullet; } }
		
		[SerializeField]
		private VeronicaSpecialBulletSet veronicaSpecialBullet;

		public VeronicaSpecialBulletSet VeronicaSpecialBullet { get { return this.veronicaSpecialBullet; } }
		
		[SerializeField]
		private GeneralNormalBulletSet generalNormalBullet;

		public GeneralNormalBulletSet GeneralNormalBullet { get { return this.generalNormalBullet; } }
		
		[SerializeField]
		private GeneralSpecialBulletSet generalSpecialBullet;

		public GeneralSpecialBulletSet GeneralSpecialBullet { get { return this.generalSpecialBullet; } }
	}
}