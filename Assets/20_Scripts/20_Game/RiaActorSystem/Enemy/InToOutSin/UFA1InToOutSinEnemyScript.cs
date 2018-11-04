using System;
using RiaActorSystem;
using UnityEngine;

namespace Game.Enemy
{
	[CreateAssetMenu(menuName = "RiaActorSystem/Enemy/UFA1InToOutSinEnemyScript", fileName = "UFA1InToOutSinEnemyScript")]
	public class UFA1InToOutSinEnemyScript : RiaEnemyScript
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