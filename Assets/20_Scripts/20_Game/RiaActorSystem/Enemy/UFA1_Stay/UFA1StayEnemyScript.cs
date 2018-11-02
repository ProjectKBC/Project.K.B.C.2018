using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Enemy
{
	[CreateAssetMenu(menuName = "RiaActorSystem/Enemy/UFA1StayEnemyScript", fileName = "UFA1StayEnemyScript")]
	public class UFA1StayEnemyScript : RiaEnemyScript
	{
		[SerializeField]
		private float ordinaryYForwardBorder;

		[SerializeField]
		private float stayTime;
		
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

		public float StayTime
		{
			get { return this.stayTime; }
		}
	}
}