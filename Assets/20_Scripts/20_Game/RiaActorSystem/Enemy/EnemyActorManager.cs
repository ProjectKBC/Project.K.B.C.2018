using Game.Player;
using UnityEngine;

namespace Game.Enemy
{
	public class EnemyActorManager : RiaActorManager
	{
		[SerializeField]
		private PlayerNumber playerNumber = PlayerNumber.player1;

		[SerializeField]
		private EnemyActorFactory factory = null;

		[Space(16)]

		[SerializeField]
		private PlayerActorManager playerActorManager = null;

		[SerializeField]
		private PlayerActorManager rivalPlayerActorManager = null;

		protected override void OnInitialize()
		{
		}

		protected override void OnUpdate()
		{
		}
	}
}