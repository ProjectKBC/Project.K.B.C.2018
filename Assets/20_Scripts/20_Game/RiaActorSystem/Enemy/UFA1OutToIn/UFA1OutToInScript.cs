using System;
using RiaActorSystem;
using UnityEngine;

namespace Game.Enemy
{
	[CreateAssetMenu(menuName = "RiaActorSystem/Enemy/UFA1OutToInEnemyScript", fileName = "UFA1OutToInEnemyScript")]
	public class UFA1OutToInScript : RiaEnemyScript
	{
		[System.Serializable]
		public struct ShooterParam
		{
			public float shotInterval;
		}
		
		
		[System.Serializable]
		public struct MoveParam
		{
			[Tooltip("登場から一時停止までのX軸の移動距離")]
			public float oneWayDistance;
			[Tooltip("一時停止の待機時間")]
			public float stayTime;
		}
		
		[SerializeField]
		private ShooterParam shotParam;
		
		// シリアライズ
		//[SerializeField] private ShootParam shot;
		[SerializeField] private MoveParam move;
		
		public ShooterParam ShotParam { get { return this.shotParam; } }
		
		// アクセサー
		//public ShootParam Shot { get { return this.shot; } }
		public MoveParam Move { get { return this.move; } }

	}
}