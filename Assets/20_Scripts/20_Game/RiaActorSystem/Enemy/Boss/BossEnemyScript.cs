using System;
using RiaActorSystem;
using UnityEngine;

namespace Game.Enemy
{
	[CreateAssetMenu(menuName = "RiaActorSystem/Enemy/BossEnemyScript", fileName = "BossEnemyScript")]
	public class BossEnemyScript : RiaEnemyScript
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