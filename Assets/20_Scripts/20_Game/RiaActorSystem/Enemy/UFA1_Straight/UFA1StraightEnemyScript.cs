/* Author : flanny7
 * Update : 2018/10/28
*/

using UnityEngine;
using RiaActorSystem;

namespace Game.Enemy
{
	[CreateAssetMenu(menuName = "RiaActorSystem/Enemy/UFA1StraightEnemyScript", fileName = "UFA1StraightEnemyScript")]
	public class UFA1StraightEnemyScript : RiaEnemyScript
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