using System;
using RiaActorSystem;
using UnityEngine;

namespace Game.Enemy
{
	[CreateAssetMenu(menuName = "RiaActorSystem/Enemy/UFA1OutToInEnemyScript", fileName = "UFA1OutToInEnemyScript")]
	public class UFA1OutToInScript : RiaEnemyScript
	{
		[System.Serializable]
		public struct ShotParam
		{
		}
		
		[System.Serializable]
		public struct MoveParam
		{
			[Tooltip("登場から一時停止までのX軸の移動距離")]
			public float oneWayDistance;
			[Tooltip("一時停止の待機時間")]
			public float stayTime;
		}
		
		// シリアライズ
		[SerializeField] private ShotParam shot;
		[SerializeField] private MoveParam move;
		
		// アクセサー
		public ShotParam Shot { get { return this.shot; } }
		public MoveParam Move { get { return this.move; } }
	}
}