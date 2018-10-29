/* Author: flanny7
 * Update: 2018/10/23
*/

using UnityEngine;
using RiaActorSystem;

namespace Game.Player
{
	public sealed class PlayerActorManager : RiaActorManager
	{
		[SerializeField]
		private PlayerNumber playerNumber = PlayerNumber.player1;

		[SerializeField]
		private PlayerActorFactory factory = null;

		[SerializeField]
		private Vector3 spownPosition = Vector3.zero;

		protected override void OnInitialize()
		{
			Debug.Log("create player : " + this.playerNumber);
			this.factory.Create(this.playerNumber, this.GetFreeActor(), this.spownPosition);
		}

		protected override void OnUpdate()
		{

		}
	}
}