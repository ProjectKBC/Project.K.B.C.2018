using System;
using RiaActorSystem;
using UnityEngine;

namespace Game.Enemy
{
	[CreateAssetMenu(menuName = "RiaActorSystem/Enemy/UFA1OutToInSinEnemyScript", fileName = "UFA1OutToInSinEnemyScript")]
	public class UFA1OutToInSinEnemyScript : RiaEnemyScript
	{
		[System.Serializable]
		public struct ShooterParam
		{
			public float shotInterval;
		}
		
		[SerializeField]
		private ShooterParam shotParam;
		
		public ShooterParam ShotParam { get { return this.shotParam; } }
		
	}
}