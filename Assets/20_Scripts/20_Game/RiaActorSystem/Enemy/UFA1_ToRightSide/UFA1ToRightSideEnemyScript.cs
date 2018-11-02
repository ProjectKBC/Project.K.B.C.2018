using UnityEngine;
using RiaActorSystem;

namespace Game.Enemy
{
	[CreateAssetMenu(menuName = "RiaActorSystem/Enemy/UFA1ToRightSideEnemyScript", fileName = "UFA1ToRightSideEnemyScript")]
	public class UFA1ToRightSideEnemyScript : RiaEnemyScript
	{
		//[SerializeField]
		protected float xForwardBorder;
		
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
			//set {if ()}
			get { return this.xForwardBorder; }
		}
	}
}