using UnityEngine;
using RiaActorSystem;

namespace Game.Enemy
{
	[CreateAssetMenu(menuName = "RiaActorSystem/Enemy/UFA1SinEnemyScript", fileName = "UFA1SinEnemyScript")]
	public class UFA1SinEnemyScript : RiaEnemyScript
	{
		[SerializeField]
		private float ordinaryYForwardBorder;
		
		[System.Serializable]
		public struct ShooterParam
		{
			public float shotInterval;
		}

		[SerializeField]
		private ShooterParam shotParam;

		public ShooterParam ShotParam { get { return this.shotParam; } }
		
		public float OrdinaryYForwardBorder
		{
			get { return this.ordinaryYForwardBorder; }
		}
	}
}