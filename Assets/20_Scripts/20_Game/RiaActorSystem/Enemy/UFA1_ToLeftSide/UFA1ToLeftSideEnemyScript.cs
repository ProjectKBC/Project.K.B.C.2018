using UnityEngine;
using RiaActorSystem;

namespace Game.Enemy
{
	[CreateAssetMenu(menuName = "RiaActorSystem/Enemy/UFA1ToLeftSideEnemyScript", fileName = "UFA1ToLeftSideEnemyScript")]
	public class UFA1ToLeftSideEnemyScript : RiaEnemyScript
	{
		[SerializeField]
		protected float xForwardBorder;
		// 
		
		[System.Serializable]
		public struct ShooterParam
		{
			public float shotInterval;
		}

		[SerializeField]
		private ShooterParam shotParam;

		public ShooterParam ShotParam { get { return this.shotParam; } }
		
		public float XForwardBorder
		{
			get { return this.xForwardBorder; }
		}
	}
}