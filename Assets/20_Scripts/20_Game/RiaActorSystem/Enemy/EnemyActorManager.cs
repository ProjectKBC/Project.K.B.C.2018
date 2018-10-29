/* Author: flanny7
 * Update: 2018/10/27
*/

using UnityEngine;
using RiaActorSystem;

namespace Game.Enemy
{
	public sealed class EnemyActorManager : RiaActorManager
	{
		[SerializeField]
		private PlayerNumber playerNumber = PlayerNumber.player1;
		
		[SerializeField]
		private EnemyActorFactory factory = null;

		[SerializeField]
		private float spownInterval = 1.0f;

		private float elapsedTime;
		private float spownTime;
		private Vector3 spownPosition;

		protected override void OnInitialize()
		{
			Debug.Log("create enemy : " + this.playerNumber);

			this.elapsedTime = 0;
			this.spownTime = 0;

			this.spownPosition = (this.playerNumber == PlayerNumber.player1) ?
				new Vector3(-44, 80, 0) :
				new Vector3(44, 80, 0);
		}

		protected override void OnUpdate()
		{
			var deltaTime = Time.deltaTime;
			this.elapsedTime += deltaTime;
			this.spownTime += deltaTime;

			if (this.spownInterval <= this.spownTime)
			{
				this.factory.Create(EnemyCharacterEnum.UAF1StraightEnemy, this.playerNumber, this.GetFreeActor(), this.spownPosition);
				this.spownTime -= this.spownInterval;
			}
		}
	}
}