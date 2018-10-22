using Game.Enemy;
using UnityEngine;

namespace Game.Player
{
	public sealed class PlayerActorManager : RiaActorManager
	{
		public PlayerActorManager RivalPlayerActorManager { get { return this.rivalPlayerActorManager; } }
		public EnemyActorManager EnemyActorManager { get { return this.enemyActorManager; } }
		public EnemyActorManager RivalEnemyActorManager { get { return this.rivalEnemyActorManager; } }

		[SerializeField]
		private PlayerNumber playerNumber = PlayerNumber.player1;

		[SerializeField]
		private PlayerActorFactory factory = null;

		[SerializeField]
		private Vector3 spownPosition = Vector3.zero;

		[Space(16)]

		[SerializeField]
		private PlayerActorManager rivalPlayerActorManager = null;

		[Space(16)]

		[SerializeField]
		private EnemyActorManager enemyActorManager = null;

		[SerializeField]
		private EnemyActorManager rivalEnemyActorManager = null;

		public void SecondInit()
		{
			RiaActor[] array = this.GetActiveActors();
			for (int i = 0; i < array.Length; i++)
			{
				RiaActor actor = array[i];
				var status = actor.Status as PlayerStatus;
				status.SecondInit();
			}
		}

		protected override void OnInitialize()
		{
			this.factory.Create(
				this.playerNumber,
				this.GetFreeActor(),
				this,
				this.spownPosition
				);
		}

		protected override void OnUpdate()
		{

		}
	}
}